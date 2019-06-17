using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.database;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace WiredPlayers.jobs
{
    public class Fishing : Script
    {
        private static Dictionary<int, Timer> fishingTimerList = new Dictionary<int, Timer>();

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (fishingTimerList.TryGetValue(player.Value, out Timer fishingTimer) == true)
            {
                // Remove the timer
                fishingTimer.Dispose();
                fishingTimerList.Remove(player.Value);
            }
        }

        private void OnFishingPrewarnTimer(object playerObject)
        {
            Client player = (Client)playerObject;
            
            if (fishingTimerList.TryGetValue(player.Value, out Timer fishingTimer) == true)
            {
                // Remove the timer
                fishingTimer.Dispose();
                fishingTimerList.Remove(player.Value);
            }

            // Start the minigame
            player.TriggerEvent("fishingBaitTaken");

            // Send the message and play fishing animation
            player.PlayAnimation("amb@world_human_stand_fishing@idle_a", "idle_c", (int)Constants.AnimationFlags.Loop);
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_SOMETHING_BAITED);
        }

        private int GetPlayerFishingLevel(Client player)
        {
            // Get player points
            int fishingPoints = Job.GetJobPoints(player, Constants.JOB_FISHERMAN);

            // Calculamos el nivel
            if (fishingPoints > 600) return 5;
            if (fishingPoints > 300) return 4;
            if (fishingPoints > 150) return 3;
            if (fishingPoints > 50) return 2;

            return 1;
        }

        [RemoteEvent("startFishingTimer")]
        public void StartFishingTimerEvent(Client player)
        {
            Random random = new Random();

            // Timer for the game to start
            Timer fishingTimer = new Timer(OnFishingPrewarnTimer, player, random.Next(1250, 2500), Timeout.Infinite);
            fishingTimerList.Add(player.Value, fishingTimer);

            // Confirmation message
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_FISHING_ROD_THROWN);
        }

        [RemoteEvent("fishingCanceled")]
        public void FishingCanceledEvent(Client player)
        {
            if (fishingTimerList.TryGetValue(player.Value, out Timer fishingTimer) == true)
            {
                fishingTimer.Dispose();
                fishingTimerList.Remove(player.Value);
            }

            // Cancel the fishing
            player.StopAnimation();
            player.Freeze(false);
            player.ResetData(EntityData.PLAYER_FISHING);
            
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_FISHING_CANCELED);
        }

        [RemoteEvent("fishingSuccess")]
        public void FishingSuccessEvent(Client player)
        {
            // Calculate failure chance
            bool failed = false;
            Random random = new Random();
            int successChance = random.Next(100);

            // Getting player's level
            int fishingLevel = GetPlayerFishingLevel(player);

            switch (fishingLevel)
            {
                case 1:
                    failed = successChance >= 70;
                    break;
                case 2:
                    failed = successChance >= 80;
                    break;
                default:
                    failed = successChance >= 90;
                    fishingLevel = 3;
                    break;
            }

            if (!failed)
            {
                // Get player earnings
                int fishWeight = random.Next(fishingLevel * 100, fishingLevel * 750);
                int playerDatabaseId = player.GetData(EntityData.PLAYER_SQL_ID);
                ItemModel fishItem = Globals.GetPlayerItemModelFromHash(playerDatabaseId, Constants.ITEM_HASH_FISH);

                if (fishItem == null)
                {
                    fishItem = new ItemModel();
                    fishItem.amount = fishWeight;
                    fishItem.hash = Constants.ITEM_HASH_FISH;
                    fishItem.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                    fishItem.ownerIdentifier = playerDatabaseId;
                    fishItem.position = new Vector3(0.0f, 0.0f, 0.0f);
                    fishItem.dimension = 0;

                    Task.Factory.StartNew(() =>
                    {
                        // Add the fish item to database
                        fishItem.id = Database.AddNewItem(fishItem);
                        Globals.itemList.Add(fishItem);
                    });
                }
                else
                {
                    Task.Factory.StartNew(() =>
                    {  
                        // Update the inventory
                        fishItem.amount += fishWeight;
                        Database.UpdateItem(fishItem);
                    });
                }

                // Send the message to the player
                string message = string.Format(Messages.INF_FISHED_WEIGHT, fishWeight);
                player.SendChatMessage(Constants.COLOR_INFO + message);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_GARBAGE_FISHED);
            }

            // Add one skill point to the player
            int fishingPoints = Job.GetJobPoints(player, Constants.JOB_FISHERMAN);
            Job.SetJobPoints(player, Constants.JOB_FISHERMAN, fishingPoints + 1);

            // Cancel fishing
            player.StopAnimation();
            player.Freeze(false);
            player.ResetData(EntityData.PLAYER_FISHING);
        }

        [RemoteEvent("fishingFailed")]
        public void FishingFailedEvent(Client player)
        {
            // Cancel fishing
            player.StopAnimation();
            player.Freeze(false);
            player.ResetData(EntityData.PLAYER_FISHING);
            
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_FISHING_FAILED);
        }

        [Command(Messages.COM_FISH)]
        public void FishCommand(Client player)
        {
            if (player.HasData(EntityData.PLAYER_FISHING) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_FISHING);
            }
            else if (player.VehicleSeat == (int)VehicleSeat.Driver)
            {
                VehicleHash vehicleModel = (VehicleHash)player.Vehicle.Model;

                if (vehicleModel == VehicleHash.Marquis || vehicleModel == VehicleHash.Tug)
                {
                    if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_FISHERMAN)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FISHERMAN);
                    }
                    else if (player.HasData(EntityData.PLAYER_FISHABLE) == false)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_FISHING_ZONE);
                    }
                    else
                    {

                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_FISHING_BOAT);
                }
            }
            else if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
            {
                int fishingRodId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                ItemModel fishingRod = Globals.GetItemModelFromId(fishingRodId);

                if (fishingRod != null && fishingRod.hash == Constants.ITEM_HASH_FISHING_ROD)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel bait = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_BAIT);
                    if (bait != null && bait.amount > 0)
                    {
                        foreach (Vector3 fishingPosition in Constants.FISHING_POSITION_LIST)
                        {
                            // Set player looking to the sea
                            player.Freeze(true);
                            player.Rotation = new Vector3(0.0f, 0.0f, 142.532f);
                            
                            if (bait.amount == 1)
                            {
                                Task.Factory.StartNew(() =>
                                {
                                    // Remove the baits from the invetory
                                    Globals.itemList.Remove(bait);
                                    Database.RemoveItem(bait.id);
                                });
                            }
                            else
                            {
                                bait.amount--;

                                Task.Factory.StartNew(() =>
                                {
                                    // Update the amount
                                    Database.UpdateItem(bait);
                                });
                            }

                            // Start fishing minigame
                            player.SetData(EntityData.PLAYER_FISHING, true);
                            player.PlayAnimation("amb@world_human_stand_fishing@base", "base", (int)Constants.AnimationFlags.Loop);
                            player.TriggerEvent("startPlayerFishing");
                            return;
                        }

                        // Player's not in the fishing zone
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_FISHING_ZONE);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NO_BAITS);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_FISHING_ROD);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ROD_BOAT);
            }
        }
    }
}
