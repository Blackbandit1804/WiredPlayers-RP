using GTANetworkAPI;
using WiredPlayers.business;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.house;
using WiredPlayers.model;
using WiredPlayers.vehicles;
using WiredPlayers.factions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace WiredPlayers.jobs
{
    public class Thief : Script
    {
        private static Dictionary<int, Timer> robberyTimerList = new Dictionary<int, Timer>();

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
            {
                robberyTimer.Dispose();
                robberyTimerList.Remove(player.Value);
            }
        }

        private void OnLockpickTimer(object playerObject)
        {
            Client player = (Client)playerObject;

            Vehicle vehicle = player.GetData(EntityData.PLAYER_LOCKPICKING);
            vehicle.Locked = false;

            player.StopAnimation();
            player.ResetData(EntityData.PLAYER_LOCKPICKING);
            player.ResetData(EntityData.PLAYER_ANIMATION);
            
            if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
            {
                robberyTimer.Dispose();
                robberyTimerList.Remove(player.Value);
            }
            
            player.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_LOCKPICKED);
        }

        private void OnHotwireTimer(object playerObject)
        {
            Client player = (Client)playerObject;

            Vehicle vehicle = player.GetData(EntityData.PLAYER_HOTWIRING);
            vehicle.EngineStatus = true;

            player.StopAnimation();
            player.ResetData(EntityData.PLAYER_HOTWIRING);
            player.ResetData(EntityData.PLAYER_ANIMATION);
            
            if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
            {
                robberyTimer.Dispose();
                robberyTimerList.Remove(player.Value);
            }

            foreach (Client target in NAPI.Pools.GetAllPlayers())
            {
                if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE)
                {
                   target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_POLICE_WARNING);
                    target.SetData(EntityData.PLAYER_EMERGENCY_WITH_WARN, player.Position);
                }
            }
            
            player.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_VEH_HOTWIREED);
        }

        private void OnPlayerRob(object playerObject)
        {
            Client player = (Client)playerObject;
            int playerSqlId = player.GetData(EntityData.PLAYER_SQL_ID);
            int timeElapsed = Globals.GetTotalSeconds() - player.GetData(EntityData.PLAYER_ROBBERY_START);
            decimal stolenItemsDecimal = timeElapsed / Constants.ITEMS_ROBBED_PER_TIME;
            int totalStolenItems = (int)Math.Round(stolenItemsDecimal);

            // Check if the player has stolen items
            ItemModel stolenItemModel = Globals.GetPlayerItemModelFromHash(playerSqlId, Constants.ITEM_HASH_STOLEN_OBJECTS);

            if (stolenItemModel == null)
            {
                stolenItemModel = new ItemModel();

                stolenItemModel.amount = totalStolenItems;
                stolenItemModel.hash = Constants.ITEM_HASH_STOLEN_OBJECTS;
                stolenItemModel.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                stolenItemModel.ownerIdentifier = playerSqlId;
                stolenItemModel.dimension = 0;
                stolenItemModel.position = new Vector3(0.0f, 0.0f, 0.0f);

                Task.Factory.StartNew(() =>
                {
                    stolenItemModel.id = Database.AddNewItem(stolenItemModel);
                    Globals.itemList.Add(stolenItemModel);
                });
            }
            else
            {
                stolenItemModel.amount += totalStolenItems;

                Task.Factory.StartNew(() =>
                {
                    // Update the amount into the database
                    Database.UpdateItem(stolenItemModel);
                });
            }

            // Allow player movement
            player.Freeze(false);
            player.StopAnimation();
            player.ResetData(EntityData.PLAYER_ANIMATION);
            player.ResetData(EntityData.PLAYER_ROBBERY_START);
            
            if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
            {
                robberyTimer.Dispose();
                robberyTimerList.Remove(player.Value);
            }

            // Avisamos de los objetos robados
            string message = string.Format(Messages.INF_PLAYER_ROBBED, totalStolenItems);
            player.SendChatMessage(Constants.COLOR_INFO + message);

            // Check if the player commited the maximum thefts allowed
            int totalThefts = player.GetData(EntityData.PLAYER_JOB_DELIVER);
            if (Constants.MAX_THEFTS_IN_ROW == totalThefts)
            {
                // Apply a cooldown to the player
                player.SetData(EntityData.PLAYER_JOB_DELIVER, 0);
                player.SetData(EntityData.PLAYER_JOB_COOLDOWN, 60);
                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_ROB_PRESSURE);
            }
            else
            {
                player.SetData(EntityData.PLAYER_JOB_DELIVER, totalThefts + 1);
            }
        }

        private void GeneratePoliceRobberyWarning(Client player)
        {
            Vector3 robberyPosition = null;
            string robberyPlace = string.Empty;
            string robberyHour = DateTime.Now.ToString("h:mm:ss tt");

            // Check if he robbed into a house or business
            if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0)
            {
                int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                HouseModel house = House.GetHouseById(houseId);
                robberyPosition = house.position;
                robberyPlace = house.name;
            }
            else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
            {
                int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                BusinessModel business = Business.GetBusinessById(businessId);
                robberyPosition = business.position;
                robberyPlace = business.name;
            }
            else
            {
                robberyPosition = player.Position;
            }

            // Create the police report
            FactionWarningModel factionWarning = new FactionWarningModel(Constants.FACTION_POLICE, player.Value, robberyPlace, robberyPosition, -1, robberyHour);
            Faction.factionWarningList.Add(factionWarning);
            
            string warnMessage = string.Format(Messages.INF_EMERGENCY_WARNING, Faction.factionWarningList.Count - 1);
            
            foreach (Client target in NAPI.Pools.GetAllPlayers())
            {
                if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE && target.GetData(EntityData.PLAYER_ON_DUTY) == 1)
                {
                   target.SendChatMessage(Constants.COLOR_INFO + warnMessage);
                }
            }
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            foreach (Vector3 pawnShop in Constants.PAWN_SHOP)
            {
                // Create pawn shops
                NAPI.TextLabel.CreateTextLabel(Messages.GEN_PAWN_SHOP, pawnShop, 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (player.GetData(EntityData.PLAYER_JOB) == Constants.JOB_THIEF)
            {
                if (player.HasData(EntityData.PLAYER_HOTWIRING) == true)
                {
                    // Remove player's hotwire
                    player.ResetData(EntityData.PLAYER_HOTWIRING);
                    player.StopAnimation();
                    
                    if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
                    {
                        robberyTimer.Dispose();
                        robberyTimerList.Remove(player.Value);
                    }
                    
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_STOPPED_HOTWIRE);
                }
                else if (player.HasData(EntityData.PLAYER_ROBBERY_START) == true)
                {
                    OnPlayerRob(player);
                }
            }
        }

        [Command(Messages.COM_FORCE)]
        public void ForceCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_THIEF)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_THIEF);
                }
                else if (player.HasData(EntityData.PLAYER_LOCKPICKING) == true)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ALREADY_LOCKPICKING);
                }
                else
                {
                    Vehicle vehicle = Globals.GetClosestVehicle(player);
                    if (vehicle == null)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                    }
                    else if (Vehicles.HasPlayerVehicleKeys(player, vehicle) == true)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_CANT_LOCKPICK_OWN_VEHICLE);
                    }
                    else if (!vehicle.Locked)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEH_ALREADY_UNLOCKED);
                    }
                    else
                    {
                        // Generate police report
                        GeneratePoliceRobberyWarning(player);

                        player.SetData(EntityData.PLAYER_LOCKPICKING, vehicle);
                        player.PlayAnimation("missheistfbisetup1", "hassle_intro_loop_f", (int)Constants.AnimationFlags.Loop);
                        player.SetData(EntityData.PLAYER_ANIMATION, true);

                        // Timer to finish forcing the door
                        Timer robberyTimer = new Timer(OnLockpickTimer, player, 10000, Timeout.Infinite);
                        robberyTimerList.Add(player.Value, robberyTimer);

                    }
                }
            }
        }

        [Command(Messages.COM_STEAL)]
        public void StealCommand(Client player)
        {
            if (player.Position.DistanceTo(new Vector3(-286.7586f, -849.3693f, 31.74337f)) > 1150.0f)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_THIEF_AREA);
            }
            else if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_THIEF)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_THIEF);
            }
            else if (player.HasData(EntityData.PLAYER_ROBBERY_START) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_STEALING);
            }
            else if (player.GetData(EntityData.PLAYER_JOB_COOLDOWN) > 0)
            {
                int timeLeft = player.GetData(EntityData.PLAYER_JOB_COOLDOWN) - Globals.GetTotalSeconds();
                string message = string.Format(Messages.ERR_PLAYER_COOLDOWN_THIEF, timeLeft);
                player.SendChatMessage(Constants.COLOR_ERROR + message);
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0 || player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                {
                    int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    HouseModel house = House.GetHouseById(houseId);
                    if (house != null && House.HasPlayerHouseKeys(player, house) == true)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_CANT_ROB_OWN_HOUSE);
                    }
                    else
                    {
                        // Generate the police report
                        GeneratePoliceRobberyWarning(player);

                        // Start stealing items
                        player.PlayAnimation("misscarstealfinalecar_5_ig_3", "crouchloop", (int)Constants.AnimationFlags.Loop);
                        player.SetData(EntityData.PLAYER_ROBBERY_START, Globals.GetTotalSeconds());
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_SEARCHING_VALUE_ITEMS);
                        player.SetData(EntityData.PLAYER_ANIMATION, true);
                        player.Freeze(true);

                        // Timer to finish the robbery
                        Timer robberyTimer = new Timer(OnPlayerRob, player, 20000, Timeout.Infinite);
                        robberyTimerList.Add(player.Value, robberyTimer);
                    }
                }
                else if (player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    Vehicle vehicle = player.Vehicle;
                    if (Vehicles.HasPlayerVehicleKeys(player, vehicle) == true)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_CANT_ROB_OWN_VEHICLE);
                    }
                    else if (vehicle.EngineStatus)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ENGINE_ON);
                    }
                    else
                    {
                        // Generate the police report
                        GeneratePoliceRobberyWarning(player);

                        // Start stealing items
                        player.PlayAnimation("veh@plane@cuban@front@ds@base", "hotwire", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.AllowPlayerControl));
                        player.SetData(EntityData.PLAYER_ROBBERY_START, Globals.GetTotalSeconds());
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_SEARCHING_VALUE_ITEMS);
                        player.SetData(EntityData.PLAYER_ANIMATION, true);

                        // Timer to finish the robbery
                        Timer robberyTimer = new Timer(OnPlayerRob, player, 35000, Timeout.Infinite);
                        robberyTimerList.Add(player.Value, robberyTimer);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_CANT_ROB);
                }
            }
        }

        [Command(Messages.COM_HOTWIRE)]
        public void HotwireCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_THIEF)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_THIEF);
            }
            else if (player.HasData(EntityData.PLAYER_HOTWIRING) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_HOTWIRING);
            }
            else if (!player.IsInVehicle)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE);
            }
            else
            {
                Vehicle vehicle = player.Vehicle;
                if (player.VehicleSeat != (int)VehicleSeat.Driver)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_VEHICLE_DRIVING);
                }
                else if (Vehicles.HasPlayerVehicleKeys(player, vehicle) == true)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_CANT_HOTWIRE_OWN_VEHICLE);
                }
                else if (vehicle.EngineStatus)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ENGINE_ALREADY_STARTED);
                }
                else
                {
                    int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                    Vector3 position = vehicle.Position;

                    player.SetData(EntityData.PLAYER_HOTWIRING, vehicle);
                    player.SetData(EntityData.PLAYER_ANIMATION, true);
                    player.PlayAnimation("veh@plane@cuban@front@ds@base", "hotwire", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.AllowPlayerControl));

                    // Create timer to finish the hotwire
                    Timer robberyTimer = new Timer(OnHotwireTimer, player, 15000, Timeout.Infinite);
                    robberyTimerList.Add(player.Value, robberyTimer);
                    
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_HOTWIRE_STARTED);

                    Task.Factory.StartNew(() =>
                    {
                        // Add hotwire log to the database
                        Database.LogHotwire(player.Name, vehicleId, position);
                    });
                }
            }
        }

        [Command(Messages.COM_PAWN)]
        public void PawnCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_THIEF)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_THIEF);
            }
            else
            {
                foreach (Vector3 pawnShop in Constants.PAWN_SHOP)
                {
                    if (player.Position.DistanceTo(pawnShop) < 1.5f)
                    {
                        int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                        ItemModel stolenItems = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_STOLEN_OBJECTS);
                        if (stolenItems != null)
                        {
                            // Calculate the earnings
                            int wonAmount = stolenItems.amount * Constants.PRICE_STOLEN;
                            string message = string.Format(Messages.INF_PLAYER_PAWNED_ITEMS, wonAmount);
                            int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;

                            Task.Factory.StartNew(() =>
                            {
                                // Delete stolen items
                                Database.RemoveItem(stolenItems.id);
                                Globals.itemList.Remove(stolenItems);
                            });

                            player.SetSharedData(EntityData.PLAYER_MONEY, money);
                            player.SendChatMessage(Constants.COLOR_INFO + message);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_STOLEN_ITEMS);
                        }
                        return;
                    }
                }
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_PAWN_SHOW);
            }
        }
    }
}
