using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.globals;
using WiredPlayers.database;
using WiredPlayers.house;
using WiredPlayers.business;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WiredPlayers.factions
{
    public class Emergency : Script
    {
        public static List<BloodModel> bloodList;

        private void CreateEmergencyReport(DeathModel death)
        {
            // Get the server time
            int totalSeconds = Globals.GetTotalSeconds();

            if (death.killer.Value == Constants.ENVIRONMENT_KILL)
            {
                // Check if the player was dead
                int databaseKiller = death.player.GetData(EntityData.PLAYER_KILLED);

                if (databaseKiller == 0)
                {
                    // There's no killer, we set the environment as killer
                    death.player.SetData(EntityData.PLAYER_KILLED, Constants.ENVIRONMENT_KILL);
                }
            }
            else
            {
                int killerId = death.killer.GetData(EntityData.PLAYER_SQL_ID);
                death.player.SetData(EntityData.PLAYER_KILLED, killerId);
            }

            death.player.Invincible = true;
            death.player.SetData(EntityData.TIME_HOSPITAL_RESPAWN, totalSeconds + 240);
            death.player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_EMERGENCY_WARN);
        }

        private int GetRemainingBlood()
        {
            int remaining = 0;
            foreach (BloodModel blood in bloodList)
            {
                if (blood.used)
                {
                    remaining--;
                }
                else
                {
                    remaining++;
                }
            }
            return remaining;
        }

        public static void CancelPlayerDeath(Client player)
        {
            player.Invincible = false;
            NAPI.Player.SpawnPlayer(player, player.Position);
            player.SetData(EntityData.PLAYER_KILLED, 0);
            player.ResetData(EntityData.TIME_HOSPITAL_RESPAWN);
        }

        private void TeleportPlayerToHospital(Client player)
        {
            player.Dimension = 0;
            player.Invincible = false;
            player.Position = new Vector3(-1385.481f, -976.4036f, 9.273162f);

            player.ResetData(EntityData.TIME_HOSPITAL_RESPAWN);
            player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
            player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
        }

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Client player, Client killer, uint weapon)
        {
            if(player.GetData(EntityData.PLAYER_KILLED) == 0)
            {
                DeathModel death = new DeathModel(player, killer, weapon);

                Vector3 deathPosition = null;
                string deathPlace = string.Empty;
                string deathHour = DateTime.Now.ToString("h:mm:ss tt");

                // Checking if player died into a house or business
                if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0)
                {
                    int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    HouseModel house = House.GetHouseById(houseId);
                    deathPosition = house.position;
                    deathPlace = house.name;
                }
                else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                {
                    int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                    BusinessModel business = Business.GetBusinessById(businessId);
                    deathPosition = business.position;
                    deathPlace = business.name;
                }
                else
                {
                    deathPosition = player.Position;
                }

                // We add the report to the list
                FactionWarningModel factionWarning = new FactionWarningModel(Constants.FACTION_EMERGENCY, player.Value, deathPlace, deathPosition, -1, deathHour);
                Faction.factionWarningList.Add(factionWarning);

                // Report message
                string warnMessage = string.Format(Messages.INF_EMERGENCY_WARNING, Faction.factionWarningList.Count - 1);

                // Sending the report to all the emergency department's members
                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY && target.GetData(EntityData.PLAYER_ON_DUTY) > 0)
                    {
                       target.SendChatMessage(Constants.COLOR_INFO + warnMessage);
                    }
                }

                // Create the emergency report
                CreateEmergencyReport(death);
            }
        }

        [Command(Messages.COM_HEAL, Messages.GEN_HEAL_COMMAND)]
        public void HealCommand(Client player, string targetString)
        {
            Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

            if (target != null && player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY)
            {
                if (target.Health < 100)
                {
                    string playerMessage = string.Format(Messages.INF_MEDIC_HEALED_PLAYER, target.Name);
                    string targetMessage = string.Format(Messages.INF_PLAYER_HEALED_MEDIC, player.Name);

                    // We heal the character
                    target.Health = 100;

                    foreach (Client targetPlayer in NAPI.Pools.GetAllPlayers())
                    {
                        if (targetPlayer.Position.DistanceTo(player.Position) < 20.0f)
                        {
                            string message = string.Format(Messages.INF_MEDIC_REANIMATED, player.Name, target.Name);
                            targetPlayer.SendChatMessage(Constants.COLOR_CHAT_ME + message);
                        }
                    }
                    
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                   target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_HURT);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
            }
        }

        [Command(Messages.COM_REANIMATE, Messages.GEN_REANIMATE_COMMAND)]
        public void ReanimateCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_EMERGENCY)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_EMERGENCY_FACTION);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (target.GetData(EntityData.PLAYER_KILLED) != 0)
                    {
                        if (GetRemainingBlood() > 0)
                        {
                            CancelPlayerDeath(target);

                            // We create blood model
                            BloodModel bloodModel = new BloodModel();
                            bloodModel.doctor = player.GetData(EntityData.PLAYER_SQL_ID);
                            bloodModel.patient = target.GetData(EntityData.PLAYER_SQL_ID);
                            bloodModel.type = string.Empty;
                            bloodModel.used = true;

                            Task.Factory.StartNew(() =>
                            {
                                // Add the blood consumption to the database
                                bloodModel.id = Database.AddBloodTransaction(bloodModel);
                                bloodList.Add(bloodModel);

                                // Send the confirmation message to both players
                                string playerMessage = string.Format(Messages.INF_PLAYER_REANIMATED, target.Name);
                                string targetMessage = string.Format(Messages.SUC_TARGET_REANIMATED, player.Name);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                               target.SendChatMessage(Constants.COLOR_SUCCESS + targetMessage);
                            });
                        }
                        else
                        {
                            // There's no blood left
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_BLOOD_LEFT);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_DEAD);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_EXTRACT, Messages.GEN_EXTRACT_COMMAND)]
        public void ExtractCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null && player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY)
                {
                    if (target.Health > 15)
                    {
                        // We create the blood model
                        BloodModel blood = new BloodModel();
                        blood.doctor = player.GetData(EntityData.PLAYER_SQL_ID);
                        blood.patient = target.GetData(EntityData.PLAYER_SQL_ID);
                        blood.type = string.Empty;
                        blood.used = false;

                        Task.Factory.StartNew(() =>
                        {
                            // We add the blood unit to the database
                            blood.id = Database.AddBloodTransaction(blood);
                            bloodList.Add(blood);

                            target.Health -= 15;

                            string playerMessage = string.Format(Messages.INF_BLOOD_EXTRACTED, target.Name);
                            string targetMessage = string.Format(Messages.INF_BLOOD_EXTRACTED, player.Name);
                            player.SendChatMessage(playerMessage);
                            target.SendChatMessage(targetMessage);
                        });
                    }
                    else
                    {
                        player.SendChatMessage(Messages.ERR_LOW_BLOOD);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_DIE)]
        public void DieCommand(Client player)
        {
            // Check if the player is dead
            if (player.HasData(EntityData.TIME_HOSPITAL_RESPAWN) == true)
            {
                int totalSeconds = Globals.GetTotalSeconds();

                if (player.GetData(EntityData.TIME_HOSPITAL_RESPAWN) <= totalSeconds)
                {
                    // Move player to the hospital
                    TeleportPlayerToHospital(player);

                    // Get the report generated with the death
                    FactionWarningModel factionWarn = Faction.GetFactionWarnByTarget(player.Value, Constants.FACTION_EMERGENCY);

                    if (factionWarn != null)
                    {
                        if (factionWarn.takenBy >= 0)
                        {
                            // Tell the player who attended the report it's been canceled
                            Client doctor = Globals.GetPlayerById(factionWarn.takenBy);
                            doctor.SendChatMessage(Constants.COLOR_INFO + Messages.INF_FACTION_WARN_CANCELED);
                        }

                        // Remove the report from the list
                        Faction.factionWarningList.Remove(factionWarn);
                    }

                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_DEATH_TIME_NOT_PASSED);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_DEAD);
            }
        }
    }
}