using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.vehicles;
using WiredPlayers.database;
using WiredPlayers.business;
using WiredPlayers.parking;
using WiredPlayers.house;
using WiredPlayers.weapons;
using WiredPlayers.factions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace WiredPlayers.admin
{
    public class Admin : Script
    {
        public static List<PermissionModel> permissionList;

        private bool HasUserCommandPermission(Client player, string command, string option = "")
        {
            bool hasPermission = false;
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

            foreach (PermissionModel permission in permissionList)
            {
                if (permission.playerId == playerId && command == permission.command)
                {
                    // We check whether it's a command option or just the command
                    if (option == string.Empty || option == permission.option)
                    {
                        hasPermission = true;
                        break;
                    }
                }
            }

            return hasPermission;
        }

        private void SendHouseInfo(Client player, HouseModel house)
        {
            string title = string.Format(Messages.GEN_HOUSE_CHECK_TITLE, house.id);
            player.SendChatMessage(title);
            player.SendChatMessage(Messages.GEN_NAME + house.name);
            player.SendChatMessage(Messages.GEN_IPL + house.ipl);
            player.SendChatMessage(Messages.GEN_OWNER + house.owner);
            player.SendChatMessage(Messages.GEN_PRICE + house.price);
            player.SendChatMessage(Messages.GEN_STATUS + house.status);
        }

        [Command(Messages.COM_SKIN, Messages.GEN_SKIN_COMMAND)]
        public void SkinCommand(Client player, string pedModel)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                PedHash pedHash = NAPI.Util.PedNameToModel(pedModel);
                player.SetSkin(pedHash);
            }
        }

        [Command(Messages.COM_ADMIN, Messages.GEN_ADMIN_COMMAND, GreedyArg = true)]
        public void AdminCommand(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // Check the message length
                string secondMessage = string.Empty;

                if (message.Length > Constants.CHAT_LENGTH)
                {
                    // Message needs to be printed in two lines
                    secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                    message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                }

                // We send the message to all the players in the server
                NAPI.Chat.SendChatMessageToAll(secondMessage.Length > 0 ? Constants.COLOR_ADMIN_INFO + Messages.GEN_ADMIN_NOTICE + message + "..." : Constants.COLOR_ADMIN_INFO + Messages.GEN_ADMIN_NOTICE + message);
                if (secondMessage.Length > 0)
                {
                    NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + secondMessage);
                }
            }
        }

        [Command(Messages.COM_COORD, Messages.GEN_COORD_COMMAND)]
        public void CoordCommand(Client player, float posX, float posY, float posZ)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                player.Dimension = 0;
                player.Position = new Vector3(posX, posY, posZ);
                player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
            }
        }

        [Command(Messages.COM_TP, Messages.GEN_TP_COMMAND, GreedyArg = true)]
        public void TpCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We get the player from the input string
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    string message = string.Format(Messages.ADM_GOTO_PLAYER, target.Name);

                    // We get interior variables from the target player
                    int targetHouse = target.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    int targetBusiness = target.GetData(EntityData.PLAYER_BUSINESS_ENTERED);

                    // Change player's position and dimension
                    player.Position = target.Position;
                    player.Dimension = target.Dimension;
                    player.SetData(EntityData.PLAYER_HOUSE_ENTERED, targetHouse);
                    player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, targetBusiness);

                    // Confirmation message sent to the command executor
                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_BRING, Messages.GEN_BRING_COMMAND, GreedyArg = true)]
        public void BringCommand(Client player, string targetString)
        {

            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We get the player from the input string
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    string message = string.Format(Messages.ADM_BRING_PLAYER, player.SocialClubName);

                    // We get interior variables from the player
                    int playerHouse = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    int playerBusiness = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);

                    // Change target's position and dimension
                    target.Position = player.Position;
                    target.Dimension = player.Dimension;
                    target.SetData(EntityData.PLAYER_HOUSE_ENTERED, playerHouse);
                    target.SetData(EntityData.PLAYER_BUSINESS_ENTERED, playerBusiness);

                    // Confirmation message sent to the command executor
                    target.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_GUN, Messages.GEN_GUN_COMMAND)]
        public void GunCommand(Client player, string targetString, string weaponName, int ammo)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
            {
                // We get the player from the input string
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    WeaponHash weapon = NAPI.Util.WeaponNameToModel(weaponName);
                    if (weapon == 0)
                    {
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_GUN_COMMAND);
                    }
                    else
                    {
                        // Give the weapon to the player
                        Weapons.GivePlayerNewWeapon(target, weapon, ammo, false);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_VEHICLE, Messages.GEN_VEHICLE_COMMAND, GreedyArg = true)]
        public void VehicleCommand(Client player, string args)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                int vehicleId = 0;
                Vehicle veh = null;
                VehicleModel vehicle = new VehicleModel();
                if (args.Trim().Length > 0)
                {
                    string[] arguments = args.Split(' ');
                    switch (arguments[0].ToLower())
                    {
                        case Messages.ARG_INFO:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                veh = Globals.GetClosestVehicle(player);
                                if (veh == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                                }
                                else
                                {
                                    vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                                    string title = string.Format(Messages.GEN_VEHICLE_CHECK_TITLE, vehicleId);
                                    string model = veh.GetData(EntityData.VEHICLE_MODEL);
                                    string owner = veh.GetData(EntityData.VEHICLE_OWNER);

                                    player.SendChatMessage(title);
                                    player.SendChatMessage(Messages.GEN_VEHICLE_MODEL + model);
                                    player.SendChatMessage(Messages.GEN_OWNER + owner);
                                }
                            }
                            break;
                        case Messages.ARG_CREATE:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                if (arguments.Length == 4)
                                {
                                    string[] firstColorArray = arguments[2].Split(',');
                                    string[] secondColorArray = arguments[3].Split(',');
                                    if (firstColorArray.Length == Constants.TOTAL_COLOR_ELEMENTS && secondColorArray.Length == Constants.TOTAL_COLOR_ELEMENTS)
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = arguments[1];
                                        vehicle.faction = Constants.FACTION_ADMIN;
                                        vehicle.position = player.Position;
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "0,0,0";
                                        vehicle.secondColor = "0,0,0";
                                        vehicle.pearlescent = 0;
                                        vehicle.owner = string.Empty;
                                        vehicle.plate = string.Empty;
                                        vehicle.price = 0;
                                        vehicle.parking = 0;
                                        vehicle.parked = 0;
                                        vehicle.gas = 50.0f;
                                        vehicle.kms = 0.0f;


                                        // Create the vehicle
                                        Vehicles.CreateVehicle(player, vehicle, true);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_CREATE_COMMAND);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_CREATE_COMMAND);
                                }
                            }
                            break;
                        case Messages.ARG_MODIFY:
                            if (arguments.Length > 1)
                            {
                                switch (arguments[1].ToLower())
                                {
                                    case Messages.ARG_COLOR:
                                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            if (arguments.Length == 4)
                                            {
                                                veh = Globals.GetClosestVehicle(player);
                                                if (veh == null)
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                                                }
                                                else
                                                {
                                                    string[] firstColorArray = arguments[2].Split(',');
                                                    string[] secondColorArray = arguments[3].Split(',');
                                                    if (firstColorArray.Length == Constants.TOTAL_COLOR_ELEMENTS && secondColorArray.Length == Constants.TOTAL_COLOR_ELEMENTS)
                                                    {
                                                        try
                                                        {
                                                            /*vehicle.firstColor = new ColorModel(int.Parse(firstColorArray[0]), int.Parse(firstColorArray[1]), int.Parse(firstColorArray[2]));
                                                            vehicle.secondColor = new ColorModel(int.Parse(secondColorArray[0]), int.Parse(secondColorArray[1]), int.Parse(secondColorArray[2]));
                                                            NAPI.SetVehicleCustomPrimaryColor(veh, vehicle.firstColor.red, vehicle.firstColor.green, vehicle.firstColor.blue);
                                                            NAPI.SetVehicleCustomSecondaryColor(veh, vehicle.secondColor.red, vehicle.secondColor.green, vehicle.secondColor.blue);
                                                            veh.SetData(EntityData.VEHICLE_FIRST_COLOR, vehicle.firstColor.ToString());
                                                            veh.SetData(EntityData.VEHICLE_SECOND_COLOR, vehicle.secondColor.ToString());
                                                            Database.UpdateVehicleColor(vehicle);*/
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            NAPI.Util.ConsoleOutput("[EXCEPTION Vehicle modify color] " + ex.Message);
                                                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_COLOR_COMMAND);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_COLOR_COMMAND);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_COLOR_COMMAND);
                                            }
                                        }
                                        break;
                                    case Messages.ARG_DIMENSION:
                                        if (arguments.Length == 4)
                                        {
                                            if (int.TryParse(arguments[2], out vehicleId) == true)
                                            {
                                                veh = Vehicles.GetVehicleById(vehicleId);
                                                if (veh == null)
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_EXISTS);
                                                }
                                                else
                                                {
                                                    // Obtenemos la dimension
                                                    if (uint.TryParse(arguments[3], out uint dimension) == true)
                                                    {
                                                        string message = string.Format(Messages.ADM_VEHICLE_DIMENSION_MODIFIED, dimension);

                                                        veh.Dimension = dimension;
                                                        vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                                                        veh.SetData(EntityData.VEHICLE_DIMENSION, dimension);

                                                        Task.Factory.StartNew(() => {
                                                            // Update the vehicle's dimension into the database
                                                            Database.UpdateVehicleSingleValue("dimension", Convert.ToInt32(dimension), vehicleId);
                                                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                        });
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_DIMENSION_COMMAND);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_DIMENSION_COMMAND);
                                            }
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_DIMENSION_COMMAND);
                                        }
                                        break;
                                    case Messages.ARG_FACTION:
                                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {

                                            if (arguments.Length == 3)
                                            {
                                                veh = Globals.GetClosestVehicle(player);
                                                if (veh == null)
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                                                }
                                                else
                                                {
                                                    // Obtenemos la facción
                                                    if (int.TryParse(arguments[2], out int faction) == true)
                                                    {
                                                        string message = string.Format(Messages.ADM_VEHICLE_FACTION_MODIFIED, faction);
                                                        vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                                                        veh.SetData(EntityData.VEHICLE_FACTION, faction);

                                                        Task.Factory.StartNew(() => {
                                                            // Update the vehicle's faction into the database
                                                            Database.UpdateVehicleSingleValue("faction", faction, vehicleId);
                                                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                        });
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_FACTION_COMMAND);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_FACTION_COMMAND);
                                            }
                                        }
                                        break;
                                    case Messages.ARG_POSITION:
                                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            if (player.IsInVehicle)
                                            {
                                                vehicle.position = player.Vehicle.Position;
                                                vehicle.rotation = player.Vehicle.Rotation;
                                                vehicle.id = player.Vehicle.GetData(EntityData.VEHICLE_ID);
                                                player.Vehicle.SetData(EntityData.VEHICLE_POSITION, vehicle.position);
                                                player.Vehicle.SetData(EntityData.VEHICLE_ROTATION, vehicle.rotation);

                                                Task.Factory.StartNew(() => {
                                                    // Update the vehicle's position into the database
                                                    Database.UpdateVehiclePosition(vehicle);
                                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_VEHICLE_POS_UPDATED);
                                                });
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE);
                                            }
                                        }
                                        break;
                                    case Messages.ARG_OWNER:
                                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            if (arguments.Length == 4)
                                            {
                                                veh = Globals.GetClosestVehicle(player);
                                                if (veh == null)
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                                                }
                                                else
                                                {
                                                    string owner = arguments[2] + " " + arguments[3];
                                                    string message = string.Format(Messages.ADM_VEHICLE_OWNER_MODIFIED, owner);
                                                    vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                                                    veh.SetData(EntityData.VEHICLE_OWNER, owner);

                                                    Task.Factory.StartNew(() => {
                                                        // Update the vehicle's owner into the database
                                                        Database.UpdateVehicleSingleString("owner", owner, vehicleId);
                                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_OWNER_COMMAND);
                                            }
                                        }
                                        break;
                                    default:
                                        player.SendChatMessage(Messages.GEN_VEHICLE_MODIFY_COMMAND);
                                        break;
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Messages.GEN_VEHICLE_MODIFY_COMMAND);
                            }
                            break;
                        case Messages.ARG_REMOVE:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                if (arguments.Length == 2 && int.TryParse(arguments[1], out vehicleId) == true)
                                {
                                    veh = Vehicles.GetVehicleById(vehicleId);
                                    if (veh != null)
                                    {
                                        Task.Factory.StartNew(() => {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Remove the vehicle
                                                veh.Delete();
                                                Database.RemoveVehicle(vehicleId);
                                            });
                                        });
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Messages.GEN_VEHICLE_DELETE_COMMAND);
                                }
                            }
                            break;
                        case Messages.ARG_REPAIR:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                player.Vehicle.Repair();
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_VEHICLE_REPAIRED);
                            }
                            break;
                        case Messages.ARG_LOCK:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                veh = Globals.GetClosestVehicle(player);
                                if (veh == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                                }
                                else if (veh.Locked)
                                {
                                    veh.Locked = false;
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.SUC_VEH_UNLOCKED);
                                }
                                else
                                {
                                    veh.Locked = true;
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.SUC_VEH_LOCKED);
                                }
                            }
                            break;
                        case Messages.ARG_START:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                if (player.VehicleSeat == (int)VehicleSeat.Driver)
                                {
                                    player.Vehicle.EngineStatus = true;
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_VEHICLE_DRIVING);
                                }
                            }
                            break;
                        case Messages.ARG_BRING:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                if (arguments.Length == 2 && int.TryParse(arguments[1], out vehicleId) == true)
                                {
                                    veh = Vehicles.GetVehicleById(vehicleId);
                                    if (veh != null)
                                    {
                                        // Get the vehicle to the player's position
                                        veh.Position = player.Position;
                                        veh.SetData(EntityData.VEHICLE_POSITION, veh.Position);

                                        // Send the message to the player
                                        string message = string.Format(Messages.ADM_VEHICLE_BRING, vehicleId);
                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_EXISTS);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Messages.GEN_VEHICLE_BRING_COMMAND);
                                }
                            }
                            break;
                        case Messages.ARG_TP:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                if (arguments.Length == 2 && int.TryParse(arguments[1], out vehicleId) == true)
                                {
                                    veh = Vehicles.GetVehicleById(vehicleId);
                                    if (veh == null)
                                    {
                                        VehicleModel vehModel = Vehicles.GetParkedVehicleById(vehicleId);

                                        if (vehModel == null)
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_EXISTS);
                                        }
                                        else
                                        {
                                            // Teleport player to the parking
                                            ParkingModel parking = Parking.GetParkingById(vehModel.parking);
                                            player.Position = parking.position;

                                            // Send the message to the player
                                            string message = string.Format(Messages.ADM_VEHICLE_GOTO, vehicleId);
                                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                        }
                                    }
                                    else
                                    {
                                        // Get the player to the vehicle's position
                                        player.Position = veh.Position;

                                        // Send the message to the player
                                        string message = string.Format(Messages.ADM_VEHICLE_GOTO, vehicleId);
                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Messages.GEN_VEHICLE_GOTO_COMMAND);
                                }
                            }
                            break;
                        default:
                            player.SendChatMessage(Messages.GEN_VEHICLE_COMMAND);
                            break;
                    }
                }
            }
        }

        [Command(Messages.COM_GO, Messages.GEN_GO_COMMAND)]
        public void GoCommand(Client player, string location)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                switch (location.ToLower())
                {
                    case Messages.ARG_WORKSHOP:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1204.13f, -1489.49f, 4.34967f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_ELECTRONICS:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1148.98f, -1608.94f, 4.41592f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_POLICE:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1111.952f, -824.9194f, 19.31578f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_TOWNHALL:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1285.544f, -567.0439f, 31.71239f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_LICENSE:
                        player.Dimension = 0;
                        player.Position = new Vector3(-70f, -1100f, 28f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_VANILLA:
                        player.Dimension = 0;
                        player.Position = new Vector3(120f, -1400f, 30f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_HOSPITAL:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1385.481f, -976.4036f, 9.273162f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_NEWS:
                        player.Dimension = 0;
                        player.Position = new Vector3(-600f, -950f, 25f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_BAHAMA:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1400f, -590f, 30f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_MECHANIC:
                        player.Dimension = 0;
                        player.Position = new Vector3(492f, -1300f, 30f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Messages.ARG_GARBAGE:
                        player.Dimension = 0;
                        player.Position = new Vector3(-320f, -1550f, 30f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_GO_COMMAND);
                        break;

                }
            }
        }

        [Command(Messages.COM_BUSINESS, Messages.GEN_BUSINESS_COMMAND, GreedyArg = true)]
        public void BusinessCommand(Client player, string args)
        {
            if (HasUserCommandPermission(player, Messages.COM_BUSINESS) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                if (args.Trim().Length > 0)
                {
                    BusinessModel business = new BusinessModel();
                    string[] arguments = args.Split(' ');
                    string message = string.Empty;
                    switch (arguments[0].ToLower())
                    {
                        case Messages.ARG_INFO:
                            break;
                        case Messages.ARG_CREATE:
                            if (HasUserCommandPermission(player, Messages.COM_BUSINESS, Messages.ARG_CREATE) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                if (arguments.Length == 2)
                                {
                                    // We get the business type
                                    if (int.TryParse(arguments[1], out int type) == true)
                                    {
                                        business.type = type;
                                        business.ipl = Business.GetBusinessTypeIpl(type);
                                        business.position = player.Position;
                                        business.dimension = player.Dimension;
                                        business.multiplier = 3.0f;
                                        business.owner = string.Empty;
                                        business.locked = false;
                                        business.name = Messages.GEN_BUSINESS;

                                        Task.Factory.StartNew(() => {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Get the id from the business
                                                business.id = Database.AddNewBusiness(business); 
                                                business.businessLabel = NAPI.TextLabel.CreateTextLabel(business.name, business.position, 20.0f, 0.75f, 4, new Color(255, 255, 255), false, business.dimension);
                                                Business.businessList.Add(business);
                                            });
                                        });
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_COMMAND);
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND);
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND2);
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND3);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_COMMAND);
                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND);
                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND2);
                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND3);
                                }
                            }
                            break;
                        case Messages.ARG_MODIFY:
                            if (HasUserCommandPermission(player, Messages.COM_BUSINESS, Messages.ARG_MODIFY) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                business = Business.GetClosestBusiness(player);
                                if (business != null)
                                {
                                    if (arguments.Length > 1)
                                    {
                                        switch (arguments[1].ToLower())
                                        {
                                            case Messages.ARG_NAME:
                                                if (arguments.Length > 2)
                                                {
                                                    // We change business name
                                                    string businessName = string.Join(" ", arguments.Skip(2));
                                                    business.name = businessName;
                                                    business.businessLabel.Text = businessName;
                                                    message = string.Format(Messages.ADM_BUSINESS_NAME_MODIFIED, businessName);

                                                    Task.Factory.StartNew(() => {
                                                        // Update the business information
                                                        Database.UpdateBusiness(business);
                                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                    });
                                                }
                                                else
                                                {
                                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_MODIFY_NAME_COMMAND);
                                                }
                                                break;
                                            case Messages.ARG_TYPE:
                                                if (arguments.Length == 3)
                                                {
                                                    // We get business type
                                                    if (int.TryParse(arguments[2], out int businessType) == true)
                                                    {
                                                        // Changing business type
                                                        business.type = businessType;
                                                        business.ipl = Business.GetBusinessTypeIpl(businessType);
                                                        message = string.Format(Messages.ADM_BUSINESS_TYPE_MODIFIED, businessType);

                                                        Task.Factory.StartNew(() => {
                                                            // Update the business information
                                                            Database.UpdateBusiness(business);
                                                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                        });
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_MODIFY_TYPE_COMMAND);
                                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND);
                                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND2);
                                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND3);
                                                    }
                                                }
                                                else
                                                {
                                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_MODIFY_TYPE_COMMAND);
                                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND);
                                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND2);
                                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_CREATE_TYPES_FIRST_COMMAND3);
                                                }
                                                break;
                                            default:
                                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_MODIFY_COMMAND);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_MODIFY_COMMAND);
                                    }
                                }
                            }
                            break;
                        case Messages.ARG_REMOVE:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                business = Business.GetClosestBusiness(player);
                                if (business != null)
                                {
                                    Task.Factory.StartNew(() => {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Delete the business
                                            business.businessLabel.Delete();
                                            Database.DeleteBusiness(business.id);
                                            Business.businessList.Remove(business);
                                        });
                                    });
                                }
                            }
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_COMMAND);
                            break;
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BUSINESS_COMMAND);
                }
            }
        }

        [Command(Messages.COM_CHARACTER, Messages.GEN_CHARACTER_COMMAND)]
        public void CharacterCommand(Client player, string action, string name = "", string surname = "", string amount = "")
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                Client target = null;

                // We check whether we have an id or a full name
                if (int.TryParse(name, out int targetId) == true)
                {
                    target = Globals.GetPlayerById(targetId);
                    amount = surname;
                }
                else
                {
                    target = NAPI.Player.GetPlayerFromName(name + " " + surname);
                }

                // We check whether the player is connected
                if (target != null && target.HasData(EntityData.PLAYER_PLAYING) == true)
                {
                    // Getting the amount
                    if (int.TryParse(amount, out int value) == true)
                    {
                        string message = string.Empty;
                        switch (action.ToLower())
                        {
                            case Messages.ARG_BANK:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                                {
                                    target.SetSharedData(EntityData.PLAYER_BANK, value);
                                    message = string.Format(Messages.ADM_PLAYER_BANK_MODIFIED, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Messages.ARG_MONEY:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                                {
                                    target.SetSharedData(EntityData.PLAYER_MONEY, value);
                                    message = string.Format(Messages.ADM_PLAYER_MONEY_MODIFIED, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Messages.ARG_FACTION:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                {
                                    target.SetData(EntityData.PLAYER_FACTION, value);
                                    message = string.Format(Messages.ADM_PLAYER_FACTION_MODIFIED, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Messages.ARG_JOB:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                {
                                    target.SetData(EntityData.PLAYER_JOB, value);
                                    message = string.Format(Messages.ADM_PLAYER_JOB_MODIFIED, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Messages.ARG_RANK:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                {
                                    target.SetData(EntityData.PLAYER_RANK, value);
                                    message = string.Format(Messages.ADM_PLAYER_RANK_MODIFIED, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Messages.ARG_DIMENSION:
                                target.Dimension = Convert.ToUInt32(value);
                                message = string.Format(Messages.ADM_PLAYER_DIMENSION_MODIFIED, value, target.Name);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                break;
                            default:
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_CHARACTER_COMMAND);
                                break;
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_CHARACTER_COMMAND);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_HOUSE, Messages.GEN_HOUSE_COMMAND, GreedyArg = true)]
        public void HouseCommand(Client player, string args)
        {
            if (HasUserCommandPermission(player, Messages.COM_HOUSE) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                HouseModel house = House.GetClosestHouse(player);
                string[] arguments = args.Split(' ');
                switch (arguments[0].ToLower())
                {
                    case Messages.ARG_INFO:
                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                        {
                            // We get house identifier
                            if (arguments.Length == 2 && int.TryParse(arguments[1], out int houseId) == true)
                            {
                                house = House.GetHouseById(houseId);
                                if (house != null)
                                {
                                    SendHouseInfo(player, house);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_NOT_EXISTS);
                                }
                            }
                            else if (arguments.Length == 1)
                            {
                                SendHouseInfo(player, house);
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.GEN_HOUSE_INFO_COMMAND);
                            }
                        }
                        break;
                    case Messages.ARG_CREATE:
                        if (HasUserCommandPermission(player, Messages.COM_HOUSE, Messages.ARG_CREATE) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                        {
                            string houseLabel = string.Empty;
                            house = new HouseModel();
                            house.ipl = Constants.HOUSE_IPL_LIST[0].ipl;
                            house.name = Messages.GEN_HOUSE;
                            house.position = player.Position;
                            house.dimension = player.Dimension;
                            house.price = 10000;
                            house.owner = string.Empty;
                            house.status = Constants.HOUSE_STATE_BUYABLE;
                            house.tenants = 2;
                            house.rental = 0;
                            house.locked = true;

                            Task.Factory.StartNew(() => {
                                NAPI.Task.Run(() =>
                                {
                                    // Add a new house
                                    house.id = Database.AddHouse(house);
                                    house.houseLabel = NAPI.TextLabel.CreateTextLabel(House.GetHouseLabelText(house), house.position, 20.0f, 0.75f, 4, new Color(255, 255, 255));
                                    House.houseList.Add(house);

                                    // Send the confirmation message
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_HOUSE_CREATED);
                                });
                            });
                        }
                        break;
                    case Messages.ARG_MODIFY:
                        if (arguments.Length > 2)
                        {
                            string message = string.Empty;

                            if (int.TryParse(arguments[2], out int value) == true)
                            {
                                // Numeric modifications
                                switch (arguments[1].ToLower())
                                {
                                    case Messages.ARG_INTERIOR:
                                        if (HasUserCommandPermission(player, Messages.COM_HOUSE, Messages.ARG_INTERIOR) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            if (value >= 0 && value < Constants.HOUSE_IPL_LIST.Count)
                                            {
                                                house.ipl = Constants.HOUSE_IPL_LIST[value].ipl;

                                                Task.Factory.StartNew(() => {
                                                    // Update the house's information
                                                    Database.UpdateHouse(house);

                                                    // Confirmation message sent to the player
                                                    message = string.Format(Messages.ADM_HOUSE_INTERIOR_MODIFIED, value);
                                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                });
                                            }
                                            else
                                            {
                                                message = string.Format(Messages.ERR_HOUSE_INTERIOR_MODIFY, Constants.HOUSE_IPL_LIST.Count - 1);
                                                player.SendChatMessage(Constants.COLOR_ERROR + message);
                                            }
                                        }
                                        break;
                                    case Messages.ARG_PRICE:
                                        if (HasUserCommandPermission(player, Messages.COM_HOUSE, Messages.ARG_PRICE) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            if (value > 0)
                                            {
                                                house.price = value;
                                                house.status = Constants.HOUSE_STATE_BUYABLE;
                                                house.houseLabel.Text = House.GetHouseLabelText(house);

                                                Task.Factory.StartNew(() => {
                                                    // Update the house's information
                                                    Database.UpdateHouse(house);

                                                    // Confirmation message sent to the player
                                                    message = string.Format(Messages.ADM_HOUSE_PRICE_MODIFIED, value);
                                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                });
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_PRICE_MODIFY);
                                            }
                                        }
                                        break;
                                    case Messages.ARG_STATE:
                                        if (value >= 0 && value < 3)
                                        {
                                            house.status = value;
                                            house.houseLabel.Text = House.GetHouseLabelText(house);

                                            Task.Factory.StartNew(() => {
                                                // Update the house's information
                                                Database.UpdateHouse(house);

                                                // Confirmation message sent to the player
                                                message = string.Format(Messages.ADM_HOUSE_STATUS_MODIFIED, value);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_STATUS_MODIFY);
                                        }
                                        break;
                                    case Messages.ARG_RENT:
                                        if (value > 0)
                                        {
                                            house.rental = value;
                                            house.status = Constants.HOUSE_STATE_RENTABLE;
                                            house.houseLabel.Text = House.GetHouseLabelText(house);

                                            Task.Factory.StartNew(() => {
                                                // Update the house's information
                                                Database.UpdateHouse(house);

                                                // Confirmation message sent to the player
                                                message = string.Format(Messages.ADM_HOUSE_RENTAL_MODIFIED, value);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_RENTAL_MODIFY);
                                        }
                                        break;
                                    default:
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_HOUSE_MODIFY_INT_COMMAND);
                                        break;
                                }
                            }
                            else
                            {
                                string name = string.Empty;
                                for (int i = 2; i < arguments.Length; i++)
                                {
                                    name += arguments[i] + " ";
                                }

                                // Text based modifications
                                switch (arguments[1].ToLower())
                                {
                                    case Messages.ARG_OWNER:
                                        if (HasUserCommandPermission(player, Messages.COM_HOUSE, Messages.ARG_OWNER) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            house.owner = name.Trim();

                                            Task.Factory.StartNew(() => {
                                                // Update the house's information
                                                Database.UpdateHouse(house);

                                                // Confirmation message sent to the player
                                                message = string.Format(Messages.ADM_HOUSE_OWNER_MODIFIED, value);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        }
                                        break;
                                    case Messages.ARG_NAME:
                                        if (HasUserCommandPermission(player, Messages.COM_HOUSE, Messages.ARG_NAME) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            house.name = name.Trim();
                                            house.houseLabel.Text = House.GetHouseLabelText(house);

                                            Task.Factory.StartNew(() => {
                                                // Update the house's information
                                                Database.UpdateHouse(house);

                                                // Confirmation message sent to the player
                                                message = string.Format(Messages.ADM_HOUSE_NAME_MODIFIED, value);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        }
                                        break;
                                    default:
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_HOUSE_MODIFY_String_COMMAND);
                                        break;

                                }
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_HOUSE_MODIFY_COMMAND);
                        }
                        break;
                    case Messages.ARG_REMOVE:
                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                        {
                            if (house != null)
                            {
                                Task.Factory.StartNew(() => {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Remove the house
                                        house.houseLabel.Delete();
                                        Database.DeleteHouse(house.id);
                                        House.houseList.Remove(house);

                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_HOUSE_DELETED);
                                    });
                                });
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_HOUSE_NEAR);
                            }
                        }
                        break;
                    case Messages.ARG_TP:
                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                        {
                            // We get the house
                            if (arguments.Length == 2 && int.TryParse(arguments[1], out int houseId) == true)
                            {
                                house = House.GetHouseById(houseId);
                                if (house != null)
                                {
                                    player.Position = house.position;
                                    player.Dimension = house.dimension;
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_NOT_EXISTS);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Messages.GEN_HOUSE_GOTO_COMMAND);
                            }
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_HOUSE_COMMAND);
                        break;
                }
            }
        }

        [Command(Messages.COM_PARKING, Messages.GEN_PARKING_COMMAND, GreedyArg = true)]
        public void ParkingCommand(Client player, string args)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                string[] arguments = args.Split(' ');
                ParkingModel parking = Parking.GetClosestParking(player);
                switch (arguments[0].ToLower())
                {
                    case Messages.ARG_INFO:
                        if (parking != null)
                        {
                            int vehicles = 0;
                            string vehicleList = string.Empty;
                            string info = string.Format(Messages.ADM_PARKING_INFO, parking.id);
                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + info);
                            foreach (ParkedCarModel parkedCar in Parking.parkedCars)
                            {
                                if (parkedCar.parkingId == parking.id)
                                {
                                    vehicleList += parkedCar.vehicle.model + " LS-" + parkedCar.vehicle.id + " ";
                                    vehicles++;
                                }
                            }
                            
                            if (vehicles > 0)
                            {
                                // We show all the vehicles in this parking
                                player.SendChatMessage(Constants.COLOR_HELP + vehicleList);
                            }
                            else
                            {
                                // There are no vehicles in this parking
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PARKING_EMPTY);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_PARKING_NEAR);
                        }
                        break;
                    case Messages.ARG_CREATE:
                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                        {
                            if (arguments.Length == 2)
                            {
                                // We get the parking type
                                if (int.TryParse(arguments[1], out int type) == true)
                                {
                                    if (type < Constants.PARKING_TYPE_PUBLIC || type > Constants.PARKING_TYPE_DEPOSIT)
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_CREATE_COMMAND);
                                    }
                                    else
                                    {
                                        parking = new ParkingModel();
                                        parking.type = type;
                                        parking.position = player.Position;

                                        Task.Factory.StartNew(() => {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Create the new parking
                                                parking.id = Database.AddParking(parking);
                                                parking.parkingLabel = NAPI.TextLabel.CreateTextLabel(Parking.GetParkingLabelText(parking.type), parking.position, 20.0f, 0.75f, 4, new Color(255, 255, 255));
                                                Parking.parkingList.Add(parking);

                                                // Send the confirmation message to the player
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_PARKING_CREATED);
                                            });
                                        });
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_CREATE_COMMAND);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_CREATE_COMMAND);
                            }
                        }
                        break;
                    case Messages.ARG_MODIFY:
                        if (arguments.Length == 3)
                        {
                            if (parking != null)
                            {
                                switch (arguments[1].ToLower())
                                {
                                    case Messages.ARG_HOUSE:
                                        if (parking.type == Constants.PARKING_TYPE_GARAGE)
                                        {
                                            // We link the house to this parking
                                            if (int.TryParse(arguments[2], out int houseId) == true)
                                            {
                                                parking.houseId = houseId;

                                                Task.Factory.StartNew(() => {
                                                    // Update the parking's information
                                                    Database.UpdateParking(parking);

                                                    // Confirmation message sent to the player
                                                    string message = string.Format(Messages.ADM_PARKING_HOUSE_MODIFIED, houseId);
                                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                });
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_MODIFY_COMMAND);
                                            }
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PARKING_NOT_GARAGE);
                                        }
                                        break;
                                    case Messages.ARG_PLACES:
                                        int slots = 0;
                                        if (int.TryParse(arguments[2], out slots) == true)
                                        {
                                            parking.capacity = slots;
                                            parking.parkingLabel = NAPI.TextLabel.CreateTextLabel(Parking.GetParkingLabelText(parking.type), parking.position, 20.0f, 0.75f, 4, new Color(255, 255, 255));

                                            Task.Factory.StartNew(() => {
                                                // Update the parking's information
                                                Database.UpdateParking(parking);

                                                // Confirmation message sent to the player
                                                string message = string.Format(Messages.ADM_PARKING_SLOTS_MODIFIED, slots);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_MODIFY_COMMAND);
                                        }
                                        break;
                                    case Messages.ARG_TYPE:
                                        int type = 0;
                                        if (int.TryParse(arguments[2], out type) == true)
                                        {
                                            parking.type = type;

                                            Task.Factory.StartNew(() => {
                                                // Update the parking's information
                                                Database.UpdateParking(parking);

                                                // Confirmation message sent to the player
                                                string message = string.Format(Messages.ADM_PARKING_TYPE_MODIFIED, type);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_MODIFY_COMMAND);
                                        }
                                        break;
                                    default:
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_MODIFY_COMMAND);
                                        break;
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_PARKING_NEAR);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_MODIFY_COMMAND);
                        }
                        break;
                    case Messages.ARG_REMOVE:
                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                        {
                            if (parking != null)
                            {
                                Task.Factory.StartNew(() => {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Update the parking's information
                                        parking.parkingLabel.Delete();
                                        Database.DeleteParking(parking.id);
                                        Parking.parkingList.Remove(parking);

                                        // Confirmation message sent to the player
                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_PARKING_DELETED);
                                    });
                                });
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_PARKING_NEAR);
                            }
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PARKING_COMMAND);
                        break;
                }
            }
        }

        [Command(Messages.COM_POS)]
        public void PosCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                Vector3 position = player.Position;
                NAPI.Util.ConsoleOutput("{0},{1},{2}", player.Position.X, player.Position.Y, player.Position.Z);
            }
        }

        [Command(Messages.COM_REVIVE, Messages.GEN_REVIVE_COMMAND)]
        public void ReviveCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We get the target player
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (target.GetData(EntityData.PLAYER_KILLED) != 0)
                    {
                        Emergency.CancelPlayerDeath(target);
                        string playerMessage = string.Format(Messages.ADM_PLAYER_REVIVED, target.Name);
                        string targetMessage = string.Format(Messages.SUC_ADMIN_REVIVED, player.SocialClubName);
                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                        target.SendChatMessage(Constants.COLOR_SUCCESS + targetMessage);
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

        [Command(Messages.COM_WEATHER, Messages.GEN_WEATHER_COMMAND)]
        public void WeatherCommand(Client player, int weather)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                if (weather < 0 || weather > 13)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_WEATHER_VALUE_INVALID);
                }
                else
                {
                    NAPI.World.SetWeather(weather.ToString());

                    string message = string.Format(Messages.ADM_WEATHER_CHANGED, player.Name, weather);
                    NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + message);
                }
            }
        }

        [Command(Messages.COM_JAIL, Messages.GEN_JAIL_COMMAND, GreedyArg = true)]
        public void JailCommand(Client player, string args)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                int jailTime = 0;
                string[] arguments = args.Trim().Split(' ');

                if (arguments.Length > 2)
                {
                    Client target = null;
                    string reason = string.Empty;

                    if (int.TryParse(arguments[0], out int targetId) == true)
                    {
                        target = Globals.GetPlayerById(targetId);
                        if (int.TryParse(arguments[1], out jailTime) == true)
                        {
                            reason = string.Join(" ", arguments.Skip(2));
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_JAIL_COMMAND);
                        }
                    }
                    else if (arguments.Length > 3)
                    {
                        target = NAPI.Player.GetPlayerFromName(arguments[0] + " " + arguments[1]);
                        if (int.TryParse(arguments[2], out jailTime) == true)
                        {
                            reason = string.Join(" ", arguments.Skip(3));
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_JAIL_COMMAND);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_JAIL_COMMAND);
                        return;
                    }

                    // We move the player to the jail
                    target.Dimension = 0;
                    target.Position = new Vector3(1651.441f, 2569.83f, 45.56486f);

                    // We set jail type
                    target.SetData(EntityData.PLAYER_JAILED, jailTime);
                    target.SetData(EntityData.PLAYER_JAIL_TYPE, Constants.JAIL_TYPE_OOC);

                    // Message sent to the whole server
                    string message = string.Format(Messages.ADM_PLAYER_JAILED, target.Name, jailTime, reason);
                    NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + message);

                    Task.Factory.StartNew(() => {
                        // We add the log in the database
                        Database.AddAdminLog(player.SocialClubName, target.Name, "jail", jailTime, reason);
                    });
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_JAIL_COMMAND);
                }
            }
        }

        [Command(Messages.COM_KICK, Messages.GEN_KICK_COMMAND, GreedyArg = true)]
        public void KickCommand(Client player, string targetString, string reason)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We get the target player
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                target.Kick(reason);

                //  Message sent to the whole server
                string message = string.Format(Messages.ADM_PLAYER_KICKED, player.Name, target.Name, reason);
                NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + message);

                Task.Factory.StartNew(() => {
                    // We add the log in the database
                    Database.AddAdminLog(player.SocialClubName, target.Name, "kick", 0, reason);
                });
            }
        }

        [Command(Messages.COM_KICKALL)]
        public void KickAllCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (target != player)
                    {
                        target.Kick();
                    }
                }

                // Confirmation message sent to the player
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_KICKED_ALL);
            }
        }

        [Command(Messages.COM_BAN, Messages.GEN_BAN_COMMAND, GreedyArg = true)]
        public void BanCommand(Client player, string targetString, string reason)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
            {
                // We get the target player
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                target.Ban(reason);

                string message = string.Format(Messages.ADM_PLAYER_BANNED, player.Name, target.Name, reason);
                NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + message);
                
                Task.Factory.StartNew(() => {
                    // We add the log in the database
                    Database.AddAdminLog(player.SocialClubName, target.Name, "ban", 0, reason);
                });
            }
        }

        [Command(Messages.COM_HEALTH, Messages.GEN_HEAL_COMMAND)]
        public void HealthCommand(Client player, string targetString, int health)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
            {
                // We get the target player
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                target.Health = health;

                // We send the confirmation message to both players
                string playerMessage = string.Format(Messages.ADM_PLAYER_HEALTH, target.Name, health);
                string targetMessage = string.Format(Messages.ADM_TARGET_HEALTH, player.Name, health);
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                target.SendChatMessage(Constants.COLOR_ADMIN_INFO + targetMessage);
            }
        }

        [Command(Messages.COM_SAVE)]
        public void SaveCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                string message = string.Empty;

                // We print a message saying when the command starts
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_SAVE_START);

                // Saving all business
                Database.UpdateAllBusiness(Business.businessList);

                message = string.Format(Messages.ADM_SAVE_BUSINESS, Business.businessList.Count);
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);

                // Saving all connected players
                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (target.HasData(EntityData.PLAYER_PLAYING) == true)
                    {
                        PlayerModel character = new PlayerModel();

                        // Non shared data
                        character.position = target.Position;
                        character.rotation = target.Rotation;
                        character.health = target.Health;
                        character.armor = target.Armor;
                        character.id = target.GetData(EntityData.PLAYER_SQL_ID);
                        character.phone = target.GetData(EntityData.PLAYER_PHONE);
                        character.radio = target.GetData(EntityData.PLAYER_RADIO);
                        character.killed = target.GetData(EntityData.PLAYER_KILLED);
                        character.faction = target.GetData(EntityData.PLAYER_FACTION);
                        character.job = target.GetData(EntityData.PLAYER_JOB);
                        character.rank = target.GetData(EntityData.PLAYER_RANK);
                        character.duty = target.GetData(EntityData.PLAYER_ON_DUTY);
                        character.carKeys = target.GetData(EntityData.PLAYER_VEHICLE_KEYS);
                        character.documentation = target.GetData(EntityData.PLAYER_DOCUMENTATION);
                        character.licenses = target.GetData(EntityData.PLAYER_LICENSES);
                        character.insurance = target.GetData(EntityData.PLAYER_MEDICAL_INSURANCE);
                        character.weaponLicense = target.GetData(EntityData.PLAYER_WEAPON_LICENSE);
                        character.houseRent = target.GetData(EntityData.PLAYER_RENT_HOUSE);
                        character.houseEntered = target.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                        character.businessEntered = target.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                        character.employeeCooldown = target.GetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN);
                        character.jobCooldown = target.GetData(EntityData.PLAYER_JOB_COOLDOWN);
                        character.jobDeliver = target.GetData(EntityData.PLAYER_JOB_DELIVER);
                        character.jobPoints = target.GetData(EntityData.PLAYER_JOB_POINTS);
                        character.rolePoints = target.GetData(EntityData.PLAYER_ROLE_POINTS);
                        character.played = target.GetData(EntityData.PLAYER_PLAYED);
                        character.jailed = target.GetData(EntityData.PLAYER_JAIL_TYPE) + "," + target.GetData(EntityData.PLAYER_JAILED);

                        // Shared data
                        character.money = target.GetSharedData(EntityData.PLAYER_MONEY);
                        character.bank = target.GetSharedData(EntityData.PLAYER_BANK);

                        // Saving the character information into the database
                        Database.SaveCharacterInformation(character);
                    }
                }

                // All the characters saved
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_CHARACTERS_SAVED);

                // Vehicles saving
                List<VehicleModel> vehicleList = new List<VehicleModel>();

                foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                {
                    if (vehicle.GetData(EntityData.VEHICLE_FACTION) == 0 && vehicle.GetData(EntityData.VEHICLE_PARKING) == 0)
                    {
                        VehicleModel vehicleModel = new VehicleModel();

                        // Getting the needed values to be stored
                        vehicleModel.id = vehicle.GetData(EntityData.VEHICLE_ID);
                        vehicleModel.model = vehicle.GetData(EntityData.VEHICLE_MODEL);
                        vehicleModel.position = vehicle.Position;
                        vehicleModel.rotation = vehicle.Rotation;
                        vehicleModel.dimension = vehicle.Dimension;
                        vehicleModel.colorType = vehicle.GetData(EntityData.VEHICLE_COLOR_TYPE);
                        vehicleModel.firstColor = vehicle.GetData(EntityData.VEHICLE_FIRST_COLOR);
                        vehicleModel.secondColor = vehicle.GetData(EntityData.VEHICLE_SECOND_COLOR);
                        vehicleModel.pearlescent = vehicle.GetData(EntityData.VEHICLE_PEARLESCENT_COLOR);
                        vehicleModel.faction = vehicle.GetData(EntityData.VEHICLE_FACTION);
                        vehicleModel.plate = vehicle.GetData(EntityData.VEHICLE_PLATE);
                        vehicleModel.owner = vehicle.GetData(EntityData.VEHICLE_OWNER);
                        vehicleModel.price = vehicle.GetData(EntityData.VEHICLE_PRICE);
                        vehicleModel.parking = vehicle.GetData(EntityData.VEHICLE_PARKING);
                        vehicleModel.parked = vehicle.GetData(EntityData.VEHICLE_PARKED);
                        vehicleModel.gas = vehicle.GetData(EntityData.VEHICLE_GAS);
                        vehicleModel.kms = vehicle.GetData(EntityData.VEHICLE_KMS);

                        // We add the vehicle to the list
                        vehicleList.Add(vehicleModel);
                    }
                }

                // Saving the list into database
                Database.SaveAllVehicles(vehicleList);

                // All vehicles saved
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_VEHICLES_SAVED);

                // End of the command
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_SAVE_FINISH);
            }
        }

        [Command(Messages.COM_ADUTY)]
        public void ADutyCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                if (player.HasData(EntityData.PLAYER_ADMIN_ON_DUTY))
                {
                    player.Invincible = false;
                    player.ResetNametagColor();
                    player.ResetData(EntityData.PLAYER_ADMIN_ON_DUTY);
                    player.SendNotification(Messages.INF_PLAYER_ADMIN_FREE_TIME);
                }
                else
                {
                    player.Invincible = true;
                    player.NametagColor = new Color(231, 133, 46);
                    player.SetData(EntityData.PLAYER_ADMIN_ON_DUTY, true);
                    player.SendNotification(Messages.INF_PLAYER_ADMIN_ON_DUTY);
                }
            }
        }

        [Command(Messages.COM_TICKETS)]
        public void TicketsCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_TICKET_LIST);
                foreach (AdminTicketModel adminTicket in Globals.adminTicketList)
                {
                    Client target = Globals.GetPlayerById(adminTicket.playerId);
                    string ticket = target.Name + " (" + adminTicket.playerId + "): " + adminTicket.question;
                    player.SendChatMessage(Constants.COLOR_HELP + ticket);
                }
            }
        }

        [Command(Messages.COM_ATICKET, Messages.GEN_ANSWER_HELP_REQUEST, GreedyArg = true)]
        public void ATicketCommand(Client player, int ticket, string message)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                foreach (AdminTicketModel adminTicket in Globals.adminTicketList)
                {
                    if (adminTicket.playerId == ticket)
                    {
                        Client target = Globals.GetPlayerById(adminTicket.playerId);

                        // We send the answer to the player
                        string targetMessage = string.Format(Messages.INF_TICKET_ANSWER, message);
                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                        // We send the confirmation to the staff
                        string playerMessage = string.Format(Messages.ADM_TICKET_ANSWERED, ticket);
                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);

                        // Ticket removed
                        Globals.adminTicketList.Remove(adminTicket);
                        return;
                    }
                }

                // There's no ticket with that identifier
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ADMIN_TICKET_NOT_FOUND);
            }
        }

        [Command(Messages.COM_A, Messages.GEN_ADMIN_TEXT_COMMAND, GreedyArg = true)]
        public void ACommand(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                string secondMessage = string.Empty;

                if (message.Length > Constants.CHAT_LENGTH)
                {
                    // We split the message in two lines
                    secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                    message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                }

                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
                    {
                       target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_ADMIN_INFO + "((Staff [ID: " + player.Value + "] " + player.Name + ": " + message + "..." : Constants.COLOR_ADMIN_INFO + "((Staff [ID: " + player.Value + "] " + player.Name + ": " + message + "))");
                        if (secondMessage.Length > 0)
                        {
                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + secondMessage + "))");
                        }
                    }
                }
            }
        }

        [Command(Messages.COM_RECON, Messages.GEN_RECON_COMMAND, GreedyArg = true)]
        public void ReconCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target.HasData(EntityData.PLAYER_PLAYING) == false)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
                else if (target.Spectating)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_SPECTATING);
                }
                else if (target == player)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CANT_SPECT_SELF);
                }
                else
                {
                    player.Spectate(target);
                    string message = string.Format(Messages.ADM_SPECTATING_PLAYER, target.Name);
                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                }
            }
        }

        [Command(Messages.COM_RECOFF)]
        public void RecoffCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                if (!player.Spectating)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_SPECTATING);
                }
                else
                {
                    player.StopSpectating();
                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_SPECT_STOPPED);
                }
            }
        }

        [Command(Messages.COM_INFO, Messages.GEN_INFO_COMMAND)]
        public void InfoCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    // Get player's basic data
                    Globals.GetPlayerBasicData(player, target);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_POINTS, Messages.GEN_POINTS_COMMAND, GreedyArg = true)]
        public void PuntosCommand(Client player, string arguments)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
            {
                string[] args = arguments.Trim().Split(' ');
                if (args.Length == 3 || args.Length == 4)
                {
                    int rolePoints = 0;
                    Client target = null;

                    if (int.TryParse(args[1], out int targetId) == true)
                    {
                        target = Globals.GetPlayerById(targetId);
                        rolePoints = int.Parse(args[2]);
                    }
                    else
                    {
                        target = NAPI.Player.GetPlayerFromName(args[1] + " " + args[2]);
                        rolePoints = int.Parse(args[3]);
                    }

                    if (target != null && target.HasData(EntityData.PLAYER_PLAYING) == true)
                    {
                        // We get player's role points
                        string playerMessage = string.Empty;
                        string targetMessage = string.Empty;
                        int targetRolePoints = target.GetData(EntityData.PLAYER_ROLE_POINTS);

                        switch (args[0].ToLower())
                        {
                            case Messages.ARG_GIVE:
                                // We give role points to the player
                                target.SetData(EntityData.PLAYER_ROLE_POINTS, targetRolePoints + rolePoints);

                                playerMessage = string.Format(Messages.ADM_ROLE_POINTS_GIVEN, target.Name, rolePoints);
                                targetMessage = string.Format(Messages.ADM_ROLE_POINTS_RECEIVED, player.SocialClubName, rolePoints);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                               target.SendChatMessage(Constants.COLOR_ADMIN_INFO + targetMessage);

                                break;
                            case Messages.ARG_REMOVE:
                                // We remove role points to the player
                                target.SetData(EntityData.PLAYER_ROLE_POINTS, targetRolePoints - rolePoints);

                                playerMessage = string.Format(Messages.ADM_ROLE_POINTS_REMOVED, target.Name, rolePoints);
                                targetMessage = string.Format(Messages.ADM_ROLE_POINTS_LOST, player.SocialClubName, rolePoints);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                               target.SendChatMessage(Constants.COLOR_ADMIN_INFO + targetMessage);
                                break;
                            case Messages.ARG_SET:
                                // We set player's role points
                                target.SetData(EntityData.PLAYER_ROLE_POINTS, rolePoints);

                                playerMessage = string.Format(Messages.ADM_ROLE_POINTS_SET, target.Name, rolePoints);
                                targetMessage = string.Format(Messages.ADM_ROLE_POINTS_ESTABLISHED, player.SocialClubName, rolePoints);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                               target.SendChatMessage(Constants.COLOR_ADMIN_INFO + targetMessage);
                                break;
                            default:
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_POINTS_COMMAND);
                                break;
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_POINTS_COMMAND);
                }
            }
        }
    }
}
