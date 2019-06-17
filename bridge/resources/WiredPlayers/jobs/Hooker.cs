using GTANetworkAPI;
using WiredPlayers.globals;
using System.Collections.Generic;
using System.Threading;

namespace WiredPlayers.jobs
{
    public class Hooker : Script
    {
        public static Dictionary<int, Timer> sexTimerList = new Dictionary<int, Timer>();

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (sexTimerList.TryGetValue(player.Value, out Timer sexTimer) == true)
            {
                sexTimer.Dispose();
                sexTimerList.Remove(player.Value);
            }
        }

        public static void OnSexServiceTimer(object playerObject)
        {
            Client player = (Client)playerObject;
            Client target = player.GetData(EntityData.PLAYER_ALREADY_FUCKING);

            // We stop both animations
            player.StopAnimation();
            target.StopAnimation();

            // Health the player
            player.Health = 100;
            
            player.ResetData(EntityData.PLAYER_ANIMATION);
            player.ResetData(EntityData.HOOKER_TYPE_SERVICE);
            player.ResetData(EntityData.PLAYER_ALREADY_FUCKING);
            target.ResetData(EntityData.PLAYER_ALREADY_FUCKING);
            
            if (sexTimerList.TryGetValue(player.Value, out Timer sexTimer) == true)
            {
                sexTimer.Dispose();
                sexTimerList.Remove(player.Value);
            }

            // Send finish message to both players
           target.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_HOOKER_CLIENT_SATISFIED);
            player.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_HOOKER_SERVICE_FINISHED);
        }

        [Command(Messages.COM_SERVICE, Messages.GEN_HOOKER_SERVICE_COMMAND)]
        public void ServiceCommand(Client player, string service, string targetString, int price)
        {
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_HOOKER)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_HOOKER);
            }
            else if (player.HasData(EntityData.PLAYER_ALREADY_FUCKING) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ALREADY_FUCKING);
            }
            else if (player.VehicleSeat != (int)VehicleSeat.RightFront)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_VEHICLE_PASSENGER);
            }
            else
            {
                NetHandle vehicle = player.Vehicle;
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target.VehicleSeat != (int)VehicleSeat.Driver)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CLIENT_NOT_VEHICLE_DRIVING);
                }
                else
                {
                    string playerMessage = string.Empty;
                    string targetMessage = string.Empty;

                    switch (service.ToLower())
                    {
                        case Messages.ARG_ORAL:
                            target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                            target.SetData(EntityData.JOB_OFFER_PRICE, price);
                            target.SetData(EntityData.HOOKER_TYPE_SERVICE, Constants.HOOKER_SERVICE_BASIC);

                            playerMessage = string.Format(Messages.INF_ORAL_SERVICE_OFFER, target.Name, price);
                            targetMessage = string.Format(Messages.INF_ORAL_SERVICE_RECEIVE, player.Name, price);
                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                           target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            break;
                        case Messages.ARG_SEX:
                            target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                            target.SetData(EntityData.JOB_OFFER_PRICE, price);
                            target.SetData(EntityData.HOOKER_TYPE_SERVICE, Constants.HOOKER_SERVICE_FULL);

                            playerMessage = string.Format(Messages.INF_SEX_SERVICE_OFFER, target.Name, price);
                            targetMessage = string.Format(Messages.INF_SEX_SERVICE_RECEIVE, player.Name, price);
                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                           target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.GEN_HOOKER_SERVICE_COMMAND);
                            break;
                    }
                }
            }
        }
    }
}


