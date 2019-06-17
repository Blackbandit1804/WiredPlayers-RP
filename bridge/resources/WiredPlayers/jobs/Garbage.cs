using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.jobs
{
    public class Garbage : Script
    {
        private static Dictionary<int, Timer> garbageTimerList = new Dictionary<int, Timer>();

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == true)
            {
                garbageTimer.Dispose();
                garbageTimerList.Remove(player.Value);
            }
        }

        private void RespawnGarbageVehicle(Vehicle vehicle)
        {
            vehicle.Repair();
            vehicle.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            vehicle.Rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);
        }

        private void OnGarbageTimer(object playerObject)
        {
            Client player = (Client)playerObject;
            Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
            Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);
            
            RespawnGarbageVehicle(vehicle);

            // Cancel the garbage route
            player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
            player.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
            target.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
            
            if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == true)
            {
                // Remove the timer
                garbageTimer.Dispose();
                garbageTimerList.Remove(player.Value);
            }

            // Send the message to both players
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_JOB_VEHICLE_ABANDONED);
            target.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_JOB_VEHICLE_ABANDONED);
        }

        private void OnGarbageCollectedTimer(object playerObject)
        {
            Client player = (Client)playerObject;
            Client driver = player.GetData(EntityData.PLAYER_JOB_PARTNER);

            // Get garbage bag
            GTANetworkAPI.Object garbageBag = player.GetData(EntityData.PLAYER_GARBAGE_BAG);
            player.StopAnimation();
            garbageBag.Delete();

            // Get the remaining checkpoints
            int route = driver.GetData(EntityData.PLAYER_JOB_ROUTE);
            int checkPoint = driver.GetData(EntityData.PLAYER_JOB_CHECKPOINT) + 1;
            int totalCheckPoints = Constants.GARBAGE_LIST.Where(x => x.route == route).Count();

            // Get the current checkpoint
            Checkpoint garbageCheckpoint = player.GetData(EntityData.PLAYER_JOB_COLSHAPE);

            if (checkPoint < totalCheckPoints)
            {
                Vector3 currentGarbagePosition = GetGarbageCheckPointPosition(route, checkPoint);
                Vector3 nextGarbagePosition = GetGarbageCheckPointPosition(route, checkPoint + 1);

                // Show the next checkpoint
                garbageCheckpoint.Position = currentGarbagePosition;
                garbageCheckpoint.Direction = nextGarbagePosition;

                driver.SetData(EntityData.PLAYER_JOB_CHECKPOINT, checkPoint);
                player.SetData(EntityData.PLAYER_JOB_CHECKPOINT, checkPoint);

                driver.TriggerEvent("showGarbageCheckPoint", currentGarbagePosition);
                player.TriggerEvent("showGarbageCheckPoint", currentGarbagePosition);

                // Add the garbage bag
                garbageBag = NAPI.Object.CreateObject(628215202, currentGarbagePosition, new Vector3(0.0f, 0.0f, 0.0f));
                player.SetData(EntityData.PLAYER_GARBAGE_BAG, garbageBag);
            }
            else
            {
                NAPI.Entity.SetEntityModel(garbageCheckpoint, 4);
                garbageCheckpoint.Position = new Vector3(-339.0206f, -1560.117f, 25.23038f);

                driver.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ROUTE_FINISHED);

                driver.TriggerEvent("showGarbageCheckPoint", garbageCheckpoint.Position);
                player.TriggerEvent("deleteGarbageCheckPoint");
            }

            if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == true)
            {
                garbageTimer.Dispose();
                garbageTimerList.Remove(player.Value);
            }
            
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_GARBAGE_COLLECTED);
        }

        private Vector3 GetGarbageCheckPointPosition(int route, int checkPoint)
        {
            Vector3 position = new Vector3();
            foreach (GarbageModel garbage in Constants.GARBAGE_LIST)
            {
                if (garbage.route == route && garbage.checkPoint == checkPoint)
                {
                    position = garbage.position;
                    break;
                }
            }
            return position;
        }

        private void FinishGarbageRoute(Client driver, bool canceled = false)
        {
            Client partner = driver.GetData(EntityData.PLAYER_JOB_PARTNER);
            
            RespawnGarbageVehicle(driver.Vehicle);

            // Destroy the previous checkpoint
            Checkpoint garbageCheckpoint = driver.GetData(EntityData.PLAYER_JOB_COLSHAPE);
            driver.TriggerEvent("deleteGarbageCheckPoint");
            garbageCheckpoint.Delete();

            // Entity data reset
            driver.ResetData(EntityData.PLAYER_JOB_PARTNER);
            driver.ResetData(EntityData.PLAYER_JOB_COLSHAPE);
            driver.ResetData(EntityData.PLAYER_JOB_ROUTE);
            driver.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
            driver.ResetData(EntityData.PLAYER_JOB_VEHICLE);

            partner.ResetData(EntityData.PLAYER_JOB_PARTNER);
            partner.ResetData(EntityData.PLAYER_GARBAGE_BAG);
            partner.ResetData(EntityData.PLAYER_JOB_ROUTE);
            partner.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
            partner.ResetData(EntityData.PLAYER_JOB_VEHICLE);
            partner.ResetData(EntityData.PLAYER_ANIMATION);

            if (!canceled)
            {
                // Pay the earnings to both players
                int driverMoney = driver.GetSharedData(EntityData.PLAYER_MONEY);
                int partnerMoney = partner.GetSharedData(EntityData.PLAYER_MONEY);
                driver.SetSharedData(EntityData.PLAYER_MONEY, driverMoney + Constants.MONEY_GARBAGE_ROUTE);
                partner.SetSharedData(EntityData.PLAYER_MONEY, partnerMoney + Constants.MONEY_GARBAGE_ROUTE);

                // Send the message with the earnings
                string message = string.Format(Messages.INF_GARBAGE_EARNINGS, Constants.MONEY_GARBAGE_ROUTE);
                driver.SendChatMessage(Constants.COLOR_INFO + message);
                partner.SendChatMessage(Constants.COLOR_INFO + message);
            }

            // Remove players from the vehicle
            driver.WarpOutOfVehicle();
            partner.WarpOutOfVehicle();
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_GARBAGE + Constants.MAX_FACTION_VEHICLES)
            {
                if (player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    if (player.HasData(EntityData.PLAYER_JOB_ROUTE) == false && player.HasData(EntityData.PLAYER_JOB_VEHICLE) == false)
                    {
                        player.WarpOutOfVehicle();
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_ROUTE);
                    }
                    else if (player.HasData(EntityData.PLAYER_JOB_VEHICLE) && player.GetData(EntityData.PLAYER_JOB_VEHICLE) != vehicle)
                    {
                        player.WarpOutOfVehicle();
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_YOUR_JOB_VEHICLE);
                    }
                    else
                    {
                        if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == true)
                        {
                            garbageTimer.Dispose();
                            garbageTimerList.Remove(player.Value);
                        }

                        // Check whether route starts or he's returning to the truck
                        if (player.HasData(EntityData.PLAYER_JOB_VEHICLE) == false)
                        {
                            player.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                            player.SetData(EntityData.PLAYER_JOB_VEHICLE, vehicle);
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_WAITING_PARTNER);
                        }
                        else
                        {
                            // We continue with the previous route
                            Client partner = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                            int garbageRoute = player.GetData(EntityData.PLAYER_JOB_ROUTE);
                            int checkPoint = player.GetData(EntityData.PLAYER_JOB_CHECKPOINT);
                            Vector3 garbagePosition = GetGarbageCheckPointPosition(garbageRoute, checkPoint);

                            player.TriggerEvent("showGarbageCheckPoint", garbagePosition);
                            partner.TriggerEvent("showGarbageCheckPoint", garbagePosition);
                        }
                    }
                }
                else
                {
                    foreach (Client driver in vehicle.Occupants)
                    {
                        if (driver.HasData(EntityData.PLAYER_JOB_PARTNER) && driver.VehicleSeat == (int)VehicleSeat.Driver)
                        {
                            Client partner = driver.GetData(EntityData.PLAYER_JOB_PARTNER);

                            if (partner == driver)
                            {
                                if (player.GetData(EntityData.PLAYER_ON_DUTY) == 1)
                                {
                                    // Link both players as partners
                                    player.SetData(EntityData.PLAYER_JOB_PARTNER, driver);
                                    driver.SetData(EntityData.PLAYER_JOB_PARTNER, player);

                                    // Set the route to the passenger
                                    int garbageRoute = driver.GetData(EntityData.PLAYER_JOB_ROUTE);
                                    player.SetData(EntityData.PLAYER_JOB_ROUTE, garbageRoute);
                                    driver.SetData(EntityData.PLAYER_JOB_CHECKPOINT, 0);
                                    player.SetData(EntityData.PLAYER_JOB_CHECKPOINT, 0);

                                    // Create the first checkpoint
                                    Vector3 currentGarbagePosition = GetGarbageCheckPointPosition(garbageRoute, 0);
                                    Vector3 nextGarbagePosition = GetGarbageCheckPointPosition(garbageRoute, 1);
                                    Checkpoint garbageCheckpoint = NAPI.Checkpoint.CreateCheckpoint(0, currentGarbagePosition, nextGarbagePosition, 2.5f, new Color(198, 40, 40, 200));
                                    player.SetData(EntityData.PLAYER_JOB_COLSHAPE, garbageCheckpoint);

                                    // Add garbage bag
                                    GTANetworkAPI.Object trashBag = NAPI.Object.CreateObject(628215202, currentGarbagePosition, new Vector3(0.0f, 0.0f, 0.0f));
                                    player.SetData(EntityData.PLAYER_GARBAGE_BAG, trashBag);

                                    driver.TriggerEvent("showGarbageCheckPoint", currentGarbagePosition);
                                    player.TriggerEvent("showGarbageCheckPoint", currentGarbagePosition);
                                }
                                else
                                {
                                    player.WarpOutOfVehicle();
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
                                }
                            }
                            return;
                        }
                    }

                    // There's no player driving, kick the passenger
                    player.WarpOutOfVehicle();
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_WAIT_GARBAGE_DRIVER);
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (player.HasData(EntityData.PLAYER_JOB_VEHICLE) && vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_GARBAGE + Constants.MAX_FACTION_VEHICLES)
            {
                if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == vehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                    string warn = string.Format(Messages.INF_JOB_VEHICLE_LEFT, 45);
                    player.SendChatMessage(Constants.COLOR_INFO + warn);
                    player.TriggerEvent("deleteGarbageCheckPoint");
                    target.TriggerEvent("deleteGarbageCheckPoint");

                    // Create the timer for driver to get into the vehicle
                    Timer garbageTimer = new Timer(OnGarbageTimer, player, 45000, Timeout.Infinite);
                    garbageTimerList.Add(player.Value, garbageTimer);
                }
            }
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Client player)
        {
            if (player.HasData(EntityData.PLAYER_JOB_COLSHAPE) && player.GetData(EntityData.PLAYER_JOB) == Constants.JOB_GARBAGE)
            {
                // Get garbage checkpoint
                Checkpoint garbageCheckpoint = player.GetData(EntityData.PLAYER_JOB_COLSHAPE);

                if (player.VehicleSeat == (int)VehicleSeat.Driver && garbageCheckpoint == checkpoint)
                {
                    NetHandle vehicle = player.Vehicle;
                    if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == vehicle)
                    {
                        // Finish the route
                        FinishGarbageRoute(player);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE_JOB);
                    }
                }
            }
        }

        [Command(Messages.COM_GARBAGE, Messages.GEN_GARBAGE_JOB_COMMAND)]
        public void GarbageCommand(Client player, string action)
        {
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_GARBAGE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_GARBAGE);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else
            {
                switch (action.ToLower())
                {
                    case Messages.ARG_ROUTE:
                        if (player.HasData(EntityData.PLAYER_JOB_ROUTE) == true)
                        {
                            player.SendChatMessage(Messages.ERR_ALREADY_IN_ROUTE);
                        }
                        else
                        {
                            Random random = new Random();
                            int garbageRoute = random.Next(Constants.MAX_GARBAGE_ROUTES);
                            player.SetData(EntityData.PLAYER_JOB_ROUTE, garbageRoute);
                            switch (garbageRoute)
                            {
                                case 0:
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_ROUTE_NORTH);
                                    break;
                                case 1:
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_ROUTE_SOUTH);
                                    break;
                                case 2:
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_ROUTE_EAST);
                                    break;
                                case 3:
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_ROUTE_WEST);
                                    break;
                            }
                        }
                        break;
                    case Messages.ARG_PICKUP:
                        if (player.IsInVehicle)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_GARBAGE_IN_VEHICLE);
                        }
                        else if (player.HasData(EntityData.PLAYER_JOB_COLSHAPE) == false)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_GARBAGE_NEAR);
                        }
                        else
                        {
                            Checkpoint garbageCheckpoint = player.GetData(EntityData.PLAYER_JOB_COLSHAPE);
                            if (player.Position.DistanceTo(garbageCheckpoint.Position) < 3.5f)
                            {
                                if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == false)
                                {
                                    player.PlayAnimation("anim@move_m@trash", "pickup", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.AllowPlayerControl));
                                    player.SetData(EntityData.PLAYER_ANIMATION, true);

                                    // Make the timer for garbage collection
                                    garbageTimer = new Timer(OnGarbageCollectedTimer, player, 15000, Timeout.Infinite);
                                    garbageTimerList.Add(player.Value, garbageTimer);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ALREADY_GARBAGE);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_GARBAGE_NEAR);
                            }
                        }
                        break;
                    case Messages.ARG_CANCEL:
                        if (player.HasData(EntityData.PLAYER_JOB_PARTNER) == true)
                        {
                            Client partner = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                            if (partner != player)
                            {
                                GTANetworkAPI.Object trashBag = null;
                                Checkpoint garbageCheckpoint = null;

                                if (player.VehicleSeat == (int)VehicleSeat.Driver)
                                {
                                    // Driver canceled
                                    trashBag = player.GetData(EntityData.PLAYER_GARBAGE_BAG);
                                    garbageCheckpoint = player.GetData(EntityData.PLAYER_JOB_COLSHAPE);
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ROUTE_FINISHED);
                                    partner.TriggerEvent("deleteGarbageCheckPoint");
                                }
                                else
                                {
                                    // Passenger canceled
                                    trashBag = partner.GetData(EntityData.PLAYER_GARBAGE_BAG);
                                    garbageCheckpoint = partner.GetData(EntityData.PLAYER_JOB_COLSHAPE);
                                    trashBag = partner.GetData(EntityData.PLAYER_GARBAGE_BAG);
                                    player.TriggerEvent("deleteGarbageCheckPoint");
                                }

                                trashBag.Delete();

                                // Create finish checkpoint
                                NAPI.Entity.SetEntityModel(garbageCheckpoint, 4);
                                garbageCheckpoint.Position = new Vector3(-339.0206f, -1560.117f, 25.23038f);
                            }
                            else
                            {
                                // Player doesn't have any partner
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ROUTE_CANCELED);
                            }

                            // Remove player from partner search
                            player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                        }
                        else if (player.HasData(EntityData.PLAYER_JOB_ROUTE) == true)
                        {
                            // Cancel the route
                            player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_GARBAGE_ROUTE_CANCELED);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_ROUTE);
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_GARBAGE_JOB_COMMAND);
                        break;
                }
            }
        }
    }
}