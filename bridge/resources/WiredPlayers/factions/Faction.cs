using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.globals;
using WiredPlayers.chat;
using WiredPlayers.database;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System;

namespace WiredPlayers.factions
{
    public class Faction : Script
    {
        public static List<ChannelModel> channelList;
        public static List<FactionWarningModel> factionWarningList;

        public static string GetPlayerFactionRank(Client player)
        {
            string rankString = string.Empty;
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            int rank = player.GetData(EntityData.PLAYER_RANK);
            foreach (FactionModel factionModel in Constants.FACTION_RANK_LIST)
            {
                if (factionModel.faction == faction && factionModel.rank == rank)
                {
                    rankString = player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? factionModel.descriptionMale : factionModel.descriptionFemale;
                    break;
                }
            }
            return rankString;
        }

        public static FactionWarningModel GetFactionWarnByTarget(int playerId, int faction)
        {
            FactionWarningModel warn = null;
            foreach (FactionWarningModel factionWarn in factionWarningList)
            {
                if (factionWarn.playerId == playerId && factionWarn.faction == faction)
                {
                    warn = factionWarn;
                    break;
                }
            }
            return warn;
        }

        private ChannelModel GetPlayerOwnedChannel(int playerId)
        {
            ChannelModel channel = null;
            foreach (ChannelModel channelModel in channelList)
            {
                if (channelModel.owner == playerId)
                {
                    channel = channelModel;
                    break;
                }
            }
            return channel;
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private bool CheckInternalAffairs(int faction, Client target)
        {
            bool isInternalAffairs = false;

            if (faction == Constants.FACTION_TOWNHALL && (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE && target.GetData(EntityData.PLAYER_RANK) == 7))
            {
                isInternalAffairs = true;
            }

            return isInternalAffairs;
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            factionWarningList = new List<FactionWarningModel>();
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Client player)
        {
            if (player.HasData(EntityData.PLAYER_FACTION_WARNING) == true)
            {
                Checkpoint locationCheckpoint = player.GetData(EntityData.PLAYER_FACTION_WARNING);
                locationCheckpoint.Delete();

                // Delete map blip
                player.TriggerEvent("deleteFactionWarning");
                
                player.ResetData(EntityData.PLAYER_FACTION_WARNING);

                // Remove the report
                factionWarningList.RemoveAll(x => x.takenBy == player.Value);
            }
        }

        [Command(Messages.COM_F, Messages.GEN_F_COMMAND, GreedyArg = true)]
        public void FCommand(Client player, string message)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            if (faction > 0 && faction < Constants.LAST_STATE_FACTION)
            {
                string rank = GetPlayerFactionRank(player);
                
                string secondMessage = string.Empty;

                if (message.Length > Constants.CHAT_LENGTH)
                {
                    // We need two lines to write the message
                    secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                    message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                }

                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == faction)
                    {
                       target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_CHAT_FACTION + "(([ID: " + player.Value + "] " + rank + " " + player.Name + ": " + message + "..." : Constants.COLOR_CHAT_FACTION + "(([ID: " + player.Value + "] " + rank + " " + player.Name + ": " + message + "))");
                        if (secondMessage.Length > 0)
                        {
                           target.SendChatMessage(Constants.COLOR_CHAT_FACTION + secondMessage + "))");
                        }
                    }
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_STATE_FACTION);
            }
        }

        [Command(Messages.COM_R, Messages.GEN_R_COMMAND, GreedyArg = true)]
        public void RCommand(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                int faction = player.GetData(EntityData.PLAYER_FACTION);
                if (faction > 0 && faction < Constants.LAST_STATE_FACTION)
                {
                    // Get player's rank in faction
                    string rank = GetPlayerFactionRank(player);
                    
                    string secondMessage = string.Empty;

                    if (message.Length > Constants.CHAT_LENGTH)
                    {
                        // We need two lines to write the message
                        secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                        message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                    }

                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.HasData(EntityData.PLAYER_PLAYING) && (target.GetData(EntityData.PLAYER_FACTION) == faction || CheckInternalAffairs(faction, target) == true))
                        {
                           target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_RADIO + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message + "..." : Constants.COLOR_RADIO + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message);
                            if (secondMessage.Length > 0)
                            {
                               target.SendChatMessage(Constants.COLOR_RADIO + secondMessage);
                            }
                        }
                    }

                    // Send the chat message to near players
                    Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_RADIO, player.Dimension > 0 ? 7.5f : 10.0f);

                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_STATE_FACTION);
                }
            }
        }

        [Command(Messages.COM_DP, Messages.GEN_DP_COMMAND, GreedyArg = true)]
        public void DpCommand(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY)
                {
                    string rank = GetPlayerFactionRank(player);
                    
                    string secondMessage = string.Empty;

                    if (message.Length > Constants.CHAT_LENGTH)
                    {
                        // We need two lines to write the message
                        secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                        message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                    }

                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE)
                        {
                           target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_RADIO + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message + "..." : Constants.COLOR_RADIO + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message);
                            if (secondMessage.Length > 0)
                            {
                               target.SendChatMessage(Constants.COLOR_RADIO + secondMessage);
                            }
                        }
                        else if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY)
                        {
                           target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_RADIO_POLICE + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message + "..." : Constants.COLOR_RADIO_POLICE + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message);
                            if (secondMessage.Length > 0)
                            {
                               target.SendChatMessage(Constants.COLOR_RADIO_POLICE + secondMessage);
                            }
                        }
                    }

                    // Send the chat message to near players
                    Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_RADIO, player.Dimension > 0 ? 7.5f : 10.0f);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_EMERGENCY_FACTION);
                }
            }
        }

        [Command(Messages.COM_DE, Messages.GEN_DE_COMMAND, GreedyArg = true)]
        public void DeCommand(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE)
                {
                    string rank = GetPlayerFactionRank(player);
                    
                    string secondMessage = string.Empty;

                    if (message.Length > Constants.CHAT_LENGTH)
                    {
                        // We need two lines to write the message
                        secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                        message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                    }

                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE)
                        {
                           target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_RADIO_POLICE + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message + "..." : Constants.COLOR_RADIO_POLICE + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message);
                            if (secondMessage.Length > 0)
                            {
                               target.SendChatMessage(Constants.COLOR_RADIO_POLICE + secondMessage);
                            }
                        }
                        else if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY)
                        {
                           target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_RADIO + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message + "..." : Constants.COLOR_RADIO + Messages.GEN_RADIO + rank + " " + player.Name + Messages.GEN_CHAT_SAY + message);
                            if (secondMessage.Length > 0)
                            {
                               target.SendChatMessage(Constants.COLOR_RADIO + secondMessage);
                            }
                        }
                    }

                    // Send the chat message to near players
                    Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_RADIO, player.Dimension > 0 ? 7.5f : 10.0f);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_EMERGENCY_FACTION);
                }
            }
        }

        [Command(Messages.COM_FR, Messages.GEN_FR_COMMAND, GreedyArg = true)]
        public void FrCommand(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                int radio = player.GetData(EntityData.PLAYER_RADIO);
                if (radio > 0)
                {
                    string name = player.GetData(EntityData.PLAYER_NAME);
                    
                    string secondMessage = string.Empty;

                    if (message.Length > Constants.CHAT_LENGTH)
                    {
                        // We need two lines to write the message
                        secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                        message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                    }

                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_RADIO) == radio)
                        {
                           target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_RADIO + Messages.GEN_RADIO + name + Messages.GEN_CHAT_SAY + message + "..." : Constants.COLOR_RADIO + Messages.GEN_RADIO + name + Messages.GEN_CHAT_SAY + message);
                            if (secondMessage.Length > 0)
                            {
                               target.SendChatMessage(Constants.COLOR_RADIO + secondMessage);
                            }
                        }
                    }

                    // Send the chat message to near players
                    Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_RADIO, player.Dimension > 0 ? 7.5f : 10.0f);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RADIO_FREQUENCY_NONE);
                }
            }
        }

        [Command(Messages.COM_FREQUENCY, Messages.GEN_FREQUENCY_COMMAND, GreedyArg = true)]
        public void FrequencyCommand(Client player, string args)
        {
            if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
            {
                int itemId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                ItemModel item = Globals.GetItemModelFromId(itemId);
                if (item != null && item.hash == Constants.ITEM_HASH_WALKIE)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ChannelModel ownedChannel = GetPlayerOwnedChannel(playerId);
                    string[] arguments = args.Trim().Split(' ');
                    switch (arguments[0].ToLower())
                    {
                        case Messages.ARG_CREATE:
                            if (arguments.Length == 2)
                            {
                                if (ownedChannel == null)
                                {
                                    // We create the new frequency
                                    MD5 md5Hash = MD5.Create();
                                    ChannelModel channel = new ChannelModel();
                                    channel.owner = playerId;
                                    channel.password = GetMd5Hash(md5Hash, arguments[1]);


                                    Task.Factory.StartNew(() =>
                                    {
                                        // Create the new channel
                                        channel.id = Database.AddChannel(channel);
                                        channelList.Add(channel);

                                        // Sending the message with created channel
                                        string message = string.Format(Messages.INF_CHANNEL_CREATED, channel.id);
                                        player.SendChatMessage(Constants.COLOR_INFO + message);
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ALREADY_OWNED_CHANNEL);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_FREQUENCY_CREATE_COMMAND);
                            }
                            break;
                        case Messages.ARG_MODIFY:
                            if (arguments.Length == 2)
                            {
                                if (ownedChannel != null)
                                {
                                    MD5 md5Hash = MD5.Create();
                                    ownedChannel.password = GetMd5Hash(md5Hash, arguments[1]);

                                    // We kick all the players from the channel
                                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                                    {
                                        int targetId = player.GetData(EntityData.PLAYER_SQL_ID);
                                        if (target.GetData(EntityData.PLAYER_RADIO) == ownedChannel.id && targetId != ownedChannel.owner)
                                        {
                                            target.SetData(EntityData.PLAYER_RADIO, 0);
                                           target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CHANNEL_DISCONNECTED);
                                        }
                                    }


                                    Task.Factory.StartNew(() =>
                                    {
                                        // Update the channel and disconnect the leader
                                        Database.UpdateChannel(ownedChannel);
                                        Database.DisconnectFromChannel(ownedChannel.id);

                                        // Message sent with the confirmation
                                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CHANNEL_UPDATED);
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_OWNED_CHANNEL);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_FREQUENCY_MODIFY_COMMAND);
                            }
                            break;
                        case Messages.ARG_REMOVE:
                            if (ownedChannel != null)
                            {
                                // We kick all the players from the channel
                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    int targetId = player.GetData(EntityData.PLAYER_SQL_ID);
                                    if (target.GetData(EntityData.PLAYER_RADIO) == ownedChannel.id)
                                    {
                                        target.SetData(EntityData.PLAYER_RADIO, 0);
                                        if (ownedChannel.owner != targetId)
                                        {
                                           target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CHANNEL_DISCONNECTED);
                                        }
                                    }
                                }

                                Task.Factory.StartNew(() =>
                                {
                                    // Disconnect the leader from the channel
                                    Database.DisconnectFromChannel(ownedChannel.id);

                                    // We destroy the channel
                                    Database.RemoveChannel(ownedChannel.id);
                                    channelList.Remove(ownedChannel);

                                    // Message sent with the confirmation
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CHANNEL_DELETED);
                                });
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_OWNED_CHANNEL);
                            }
                            break;
                        case Messages.ARG_CONNECT:
                            if (arguments.Length == 3)
                            {
                                if (int.TryParse(arguments[1], out int frequency) == true)
                                {
                                    // We encrypt the password
                                    MD5 md5Hash = MD5.Create();
                                    string password = GetMd5Hash(md5Hash, arguments[2]);

                                    foreach (ChannelModel channel in channelList)
                                    {
                                        if (channel.id == frequency && channel.password == password)
                                        {
                                            string message = string.Format(Messages.INF_CHANNEL_CONNECTED, channel.id);
                                            player.SetData(EntityData.PLAYER_RADIO, channel.id);
                                            player.SendChatMessage(Constants.COLOR_INFO + message);
                                            return;
                                        }
                                    }

                                    // Couldn't find any channel with that id
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CHANNEL_NOT_FOUND);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_FREQUENCY_CONNECT_COMMAND);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_FREQUENCY_CONNECT_COMMAND);
                            }
                            break;
                        case Messages.ARG_DISCONNECT:
                            player.SetData(EntityData.PLAYER_RADIO, 0);
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CHANNEL_DISCONNECTED);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_FREQUENCY_COMMAND);
                            break;
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_WALKIE_IN_HAND);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RIGHT_HAND_EMPTY);
            }
        }

        [Command(Messages.COM_RECRUIT, Messages.GEN_RECRUIT_COMMAND)]
        public void RecruitCommand(Client player, string targetString)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction > Constants.FACTION_NONE)
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) > 0)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_FACTION);
                }
                else
                {
                    int rank = player.GetData(EntityData.PLAYER_RANK);

                    switch (faction)
                    {
                        case Constants.FACTION_POLICE:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_JOB);
                            }
                            else if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RECRUIT);
                            }
                            else
                            {
                                string targetMessage = string.Format(Messages.INF_FACTION_RECRUITED, Messages.GEN_FACTION_LSPD);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_POLICE);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_EMERGENCY:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_JOB);
                            }
                            else if (rank < 10)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RECRUIT);
                            }
                            else
                            {
                                string targetMessage = string.Format(Messages.INF_FACTION_RECRUITED, Messages.GEN_FACTION_EMS);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_EMERGENCY);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_NEWS:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_JOB);
                            }
                            else if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RECRUIT);
                            }
                            else
                            {
                                string targetMessage = string.Format(Messages.INF_FACTION_RECRUITED, Messages.GEN_FACTION_NEWS);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_NEWS);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_TOWNHALL:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_JOB);
                            }
                            else if (rank < 3)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RECRUIT);
                            }
                            else
                            {
                                string targetMessage = string.Format(Messages.INF_FACTION_RECRUITED, Messages.GEN_FACTION_TOWNHALL);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_TOWNHALL);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_TAXI_DRIVER:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_JOB);
                            }
                            else if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RECRUIT);
                            }
                            else
                            {
                                string targetMessage = string.Format(Messages.INF_FACTION_RECRUITED, Messages.GEN_FACTION_TRANSPORT);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_TAXI_DRIVER);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        default:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RECRUIT);
                            }
                            else
                            {
                                string targetMessage = string.Format(Messages.INF_FACTION_RECRUITED, faction);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, faction);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                    }

                    // We send the message to the recruiter
                    string playerMessage = string.Format(Messages.INF_PLAYER_RECRUITED, target.Name);
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NO_FACTION);
            }
        }

        [Command(Messages.COM_DISMISS, Messages.GEN_DISMISS_COMMAND)]
        public void DismissCommand(Client player, string targetString)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction != Constants.FACTION_NONE)
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) != faction)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_IN_SAME_FACTION);
                }
                else
                {
                    int rank = player.GetData(EntityData.PLAYER_RANK);

                    switch (faction)
                    {
                        case Constants.FACTION_POLICE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_DISMISS);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_EMERGENCY:
                            if (rank < 10)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_DISMISS);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_NEWS:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_DISMISS);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_TOWNHALL:
                            if (rank < 3)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_DISMISS);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_TAXI_DRIVER:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_DISMISS);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        default:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_DISMISS);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                    }

                    string playerMessage = string.Format(Messages.INF_PLAYER_DISMISSED, target.Name);
                    string targetMessage = string.Format(Messages.INF_FACTION_DISMISSED, player.Name);

                    // Send the messages to both players
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                   target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NO_FACTION);
            }
        }

        [Command(Messages.COM_RANK, Messages.GEN_RANK_COMMAND)]
        public void RankCommand(Client player, string arguments)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction != Constants.FACTION_NONE)
            {
                string[] args = arguments.Split(' ');

                // Get the target player
                Client target = int.TryParse(args[0], out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(args[0] + " " + args[1]);

                if (target == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) != faction)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_IN_SAME_FACTION);
                }
                else
                {
                    int rank = player.GetData(EntityData.PLAYER_RANK);
                    int givenRank = args.Length > 2 ? int.Parse(args[2]) : int.Parse(args[1]);

                    switch (faction)
                    {
                        case Constants.FACTION_POLICE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RANK);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_EMERGENCY:
                            if (rank < 10)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RANK);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_NEWS:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RANK);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_TOWNHALL:
                            if (rank < 3)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RANK);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_TAXI_DRIVER:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RANK);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        default:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RANK_TOO_LOW_RANK);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                    }

                    string playerMessage = string.Format(Messages.INF_PLAYER_RANK_CHANGED, target.Name, givenRank);
                    string targetMessage = string.Format(Messages.INF_FACTION_RANK_CHANGED, player.Name, givenRank);

                    // Send the message to both players
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                   target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NO_FACTION);
            }
        }

        [Command(Messages.COM_REPORTS)]
        public void ReportsCommand(Client player)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction == Constants.FACTION_POLICE || faction == Constants.FACTION_EMERGENCY)
            {
                int currentElement = 0;
                int totalWarnings = 0;

                // Reports' header
                player.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_REPORTS_HEADER);

                foreach (FactionWarningModel factionWarning in factionWarningList)
                {
                    if (factionWarning.faction == faction)
                    {
                        string message = string.Empty;
                        if (factionWarning.place.Length > 0)
                        {
                            message = currentElement + ". " + Messages.GEN_TIME + factionWarning.hour + ", " + Messages.GEN_PLACE + factionWarning.place;
                        }
                        else
                        {
                            message = currentElement + ". " + Messages.GEN_TIME + factionWarning.hour;
                        }

                        // Check if attended
                        if (factionWarning.takenBy > -1)
                        {
                            Client target = Globals.GetPlayerById(factionWarning.takenBy);
                            message += ", " + Messages.GEN_ATTENDED_BY + target.Name;
                        }
                        else
                        {
                            message += ", " + Messages.GEN_UNATTENDED;
                        }

                        // We send the message to the player
                        player.SendChatMessage(Constants.COLOR_HELP + message);
                        
                        totalWarnings++;
                    }
                    
                    currentElement++;
                }
                
                if (totalWarnings == 0)
                {
                    // There are no reports in the list
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_NOT_FACTION_WARNING);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_EMERGENCY_FACTION);
            }
        }

        [Command(Messages.COM_ATTEND, Messages.GEN_ATTEND_COMMAND)]
        public void AttendCommand(Client player, int warning)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction == Constants.FACTION_POLICE || faction == Constants.FACTION_EMERGENCY)
            {
                try
                {
                    FactionWarningModel factionWarning = factionWarningList.ElementAt(warning);

                    // Check the faction and whether the report is attended
                    if (factionWarning.faction != faction)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_FACTION_WARNING_NOT_FOUND);
                    }
                    else if (factionWarning.takenBy > -1)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_FACTION_WARNING_TAKEN);
                    }
                    else if (player.HasData(EntityData.PLAYER_FACTION_WARNING) == true)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_HAVE_FACTION_WARNING);
                    }
                    else
                    {
                        Checkpoint factionWarningCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, factionWarning.position, new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                        player.SetData(EntityData.PLAYER_FACTION_WARNING, factionWarningCheckpoint);
                        factionWarning.takenBy = player.Value;

                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_FACTION_WARNING_TAKEN);

                        player.TriggerEvent("showFactionWarning", factionWarning.position);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_FACTION_WARNING_NOT_FOUND);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_EMERGENCY_FACTION);
            }
        }

        [Command(Messages.COM_CLEAR_REPORTS, Messages.GEN_CLEAR_REPORTS_COMMAND)]
        public void ClearReportsCommand(Client player, int warning)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction == Constants.FACTION_POLICE || faction == Constants.FACTION_EMERGENCY)
            {
                try
                {
                    FactionWarningModel factionWarning = factionWarningList.ElementAt(warning);

                    // Check the faction and whether the report is attended
                    if (factionWarning.faction != faction)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_FACTION_WARNING_NOT_FOUND);
                    }
                    else
                    {
                        // We remove the report
                        factionWarningList.Remove(factionWarning);

                        // Send the message to the user
                        string message = string.Format(Messages.INF_FACTION_WARNING_DELETED, warning);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_FACTION_WARNING_NOT_FOUND);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_EMERGENCY_FACTION);
            }
        }

        [Command(Messages.COM_MEMBERS)]
        public void MembersCommand(Client player)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            if (faction > 0)
            {
                player.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_MEMBERS_ONLINE);
                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == faction)
                    {
                        string rank = GetPlayerFactionRank(target);

                        if (rank == string.Empty)
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + "[Id: " + player.Value + "] " + target.Name);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + "[Id: " + player.Value + "] " + rank + " " + target.Name);
                        }
                    }
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NO_FACTION);
            }
        }
    }
}
