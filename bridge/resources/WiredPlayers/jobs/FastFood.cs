using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.jobs
{
    public class FastFood : Script
    {
        private static Dictionary<int, Timer> fastFoodTimerList = new Dictionary<int, Timer>();

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (fastFoodTimerList.TryGetValue(player.Value, out Timer fastFoodTimer) == true)
            {
                // Destroy the timer
                fastFoodTimer.Dispose();
                fastFoodTimerList.Remove(player.Value);
            }
        }

        private int GetFastFoodOrderAmount(Client player)
        {
            int amount = 0;
            int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
            foreach (FastFoodOrderModel order in Globals.fastFoodOrderList)
            {
                if (order.id == orderId)
                {
                    amount += order.pizzas * Constants.PRICE_PIZZA;
                    amount += order.hamburgers * Constants.PRICE_HAMBURGER;
                    amount += order.sandwitches * Constants.PRICE_SANDWICH;
                    break;
                }
            }
            return amount;
        }

        private FastFoodOrderModel GetFastfoodOrderFromId(int orderId)
        {
            FastFoodOrderModel order = null;

            foreach (FastFoodOrderModel orderModel in Globals.fastFoodOrderList)
            {
                if (orderModel.id == orderId)
                {
                    order = orderModel;
                    break;
                }
            }

            return order;
        }

        private void RespawnFastfoodVehicle(Vehicle vehicle)
        {
            vehicle.Repair();
            vehicle.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            vehicle.Rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);
        }

        private void OnFastFoodTimer(object playerObject)
        {
            Client player = (Client)playerObject;
            Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

            // Vehicle respawn
            RespawnFastfoodVehicle(vehicle);

            // Cancel the order
            player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
            player.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
            player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
            player.ResetData(EntityData.PLAYER_JOB_WON);

            // Delete map blip
            player.TriggerEvent("fastFoodDeliverFinished");

            // Remove timer from the list
            Timer fastFoodTimer = fastFoodTimerList[player.Value];
            if (fastFoodTimer != null)
            {
                fastFoodTimer.Dispose();
                fastFoodTimerList.Remove(player.Value);
            }

            // Send the message to the player
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_JOB_VEHICLE_ABANDONED);
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_FASTFOOD + Constants.MAX_FACTION_VEHICLES)
            {
                if (player.HasData(EntityData.PLAYER_DELIVER_ORDER) == false && player.HasData(EntityData.PLAYER_JOB_VEHICLE) == false)
                {
                    player.WarpOutOfVehicle();
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_DELIVERING_ORDER);
                }
                else if (player.HasData(EntityData.PLAYER_JOB_VEHICLE) && player.GetData(EntityData.PLAYER_JOB_VEHICLE) != vehicle)
                {
                    player.WarpOutOfVehicle();
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_YOUR_JOB_VEHICLE);
                }
                else
                {
                    if (fastFoodTimerList.TryGetValue(player.Value, out Timer fastFoodTimer) == true)
                    {
                        fastFoodTimer.Dispose();
                        fastFoodTimerList.Remove(player.Value);
                    }
                    if (player.HasData(EntityData.PLAYER_JOB_VEHICLE) == false)
                    {
                        int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
                        FastFoodOrderModel order = GetFastfoodOrderFromId(orderId);
                        Checkpoint playerFastFoodCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, order.position, new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));

                        player.SetData(EntityData.PLAYER_JOB_CHECKPOINT, playerFastFoodCheckpoint);
                        player.SetData(EntityData.PLAYER_JOB_VEHICLE, vehicle);

                        player.TriggerEvent("fastFoodDestinationCheckPoint", order.position);
                    }
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_FASTFOOD + Constants.MAX_FACTION_VEHICLES && player.HasData(EntityData.PLAYER_JOB_VEHICLE) == true)
            {
                if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == vehicle)
                {
                    string warn = string.Format(Messages.INF_JOB_VEHICLE_LEFT, 60);
                    player.SendChatMessage(Constants.COLOR_INFO + warn);

                    // Timer with the time left to get into the vehicle
                    Timer fastFoodTimer = new Timer(OnFastFoodTimer, player, 60000, Timeout.Infinite);
                    fastFoodTimerList.Add(player.Value, fastFoodTimer);
                }
            }
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Client player)
        {
            if (player.GetData(EntityData.PLAYER_JOB) == Constants.JOB_FASTFOOD)
            {
                // Get the player's deliver checkpoint
                Checkpoint playerDeliverColShape = player.GetData(EntityData.PLAYER_JOB_CHECKPOINT);

                if (playerDeliverColShape == checkpoint)
                {
                    if (player.HasData(EntityData.PLAYER_DELIVER_START) == true)
                    {
                        if (!player.IsInVehicle)
                        {
                            Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);
                            playerDeliverColShape.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);

                            int elapsed = Globals.GetTotalSeconds() - player.GetData(EntityData.PLAYER_DELIVER_START);
                            int extra = (int)Math.Round((player.GetData(EntityData.PLAYER_DELIVER_TIME) - elapsed) / 2.0f);
                            int amount = GetFastFoodOrderAmount(player) + extra;

                            player.ResetData(EntityData.PLAYER_DELIVER_START);
                            player.SetData(EntityData.PLAYER_JOB_WON, amount > 0 ? amount : 25);

                            player.TriggerEvent("fastFoodDeliverBack", playerDeliverColShape.Position);

                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_DELIVER_COMPLETED);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_DELIVER_IN_VEHICLE);
                        }
                    }
                    else
                    {
                        Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);
                        if (player.Vehicle == vehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                        {
                            int won = player.GetData(EntityData.PLAYER_JOB_WON);
                            int money = player.GetSharedData(EntityData.PLAYER_MONEY);
                            int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
                            string message = string.Format(Messages.INF_JOB_WON, won);
                            Globals.fastFoodOrderList.RemoveAll(order => order.id == orderId);

                            playerDeliverColShape.Delete();
                            player.WarpOutOfVehicle();

                            player.SetSharedData(EntityData.PLAYER_MONEY, money + won);
                            player.SendChatMessage(Constants.COLOR_INFO + message);

                            player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                            player.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
                            player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                            player.ResetData(EntityData.PLAYER_JOB_WON);

                            player.TriggerEvent("fastFoodDeliverFinished");

                            // We get the motorcycle to its spawn point
                            RespawnFastfoodVehicle(vehicle);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_YOUR_JOB_VEHICLE);
                        }
                    }
                }
            }
        }

        [RemoteEvent("takeFastFoodOrder")]
        public void TakeFastFoodOrderEvent(Client player, int orderId)
        {
            foreach (FastFoodOrderModel order in Globals.fastFoodOrderList)
            {
                if (order.id == orderId)
                {
                    if (order.taken)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ORDER_TAKEN);
                    }
                    else
                    {
                        // Get the time to reach the destination
                        int start = Globals.GetTotalSeconds();
                        int time = (int)Math.Round(player.Position.DistanceTo(order.position) / 9.5f);

                        // We take the order
                        order.taken = true;

                        player.SetData(EntityData.PLAYER_DELIVER_ORDER, orderId);
                        player.SetData(EntityData.PLAYER_DELIVER_START, start);
                        player.SetData(EntityData.PLAYER_DELIVER_TIME, time);

                        // Information message sent to the player
                        string orderMessage = string.Format(Messages.INF_DELIVER_ORDER, time);
                        player.SendChatMessage(Constants.COLOR_INFO + orderMessage);
                    }
                    return;
                }
            }

            // Order has been deleted
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ORDER_TIMEOUT);
        }

        [Command(Messages.COM_ORDERS)]
        public void OrdersCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_FASTFOOD)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_FASTFOOD);
            }
            else if (player.HasData(EntityData.PLAYER_DELIVER_ORDER) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ORDER_DELIVERING);
            }
            else
            {
                // Get the deliverable orders
                List<FastFoodOrderModel> fastFoodOrders = Globals.fastFoodOrderList.Where(o => !o.taken).ToList();

                if (fastFoodOrders.Count > 0)
                {

                    List<float> distancesList = new List<float>();

                    foreach (FastFoodOrderModel order in fastFoodOrders)
                    {
                        float distance = player.Position.DistanceTo(order.position);
                        distancesList.Add(distance);
                    }

                    player.TriggerEvent("showFastfoodOrders", NAPI.Util.ToJson(fastFoodOrders), NAPI.Util.ToJson(distancesList));
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ORDER_NONE);
                }
            }
        }
    }
}
