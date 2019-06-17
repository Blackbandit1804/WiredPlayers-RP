using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.parking;
using WiredPlayers.business;
using WiredPlayers.weapons;
using WiredPlayers.chat;
using WiredPlayers.jobs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.vehicles
{
    public class Vehicles : Script
    {
        private static Dictionary<int, Timer> gasTimerList = new Dictionary<int, Timer>();
        private static Dictionary<int, Timer> vehicleRespawnTimerList = new Dictionary<int, Timer>();

        public void LoadDatabaseVehicles()
        {
            List<VehicleModel> vehicleList = Database.LoadAllVehicles();
            Parking.parkedCars = new List<ParkedCarModel>();

            foreach (VehicleModel vehModel in vehicleList)
            {
                if (vehModel.parking == 0)
                {
                    // Create the vehicle ingame
                    CreateIngameVehicle(vehModel);
                }
                else
                {
                    // Link the car to the parking
                    ParkedCarModel parkedCarModel = new ParkedCarModel();
                    parkedCarModel.parkingId = vehModel.parking;
                    parkedCarModel.vehicle = vehModel;
                    
                    Parking.parkedCars.Add(parkedCarModel);
                }
            }
        }

        public static void CreateVehicle(Client player, VehicleModel vehModel, bool adminCreated)
        {
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Add the vehicle to the database
                    vehModel.id = Database.AddNewVehicle(vehModel);

                    // Create the vehicle ingame
                    CreateIngameVehicle(vehModel);

                    if (!adminCreated)
                    {
                        int moneyLeft = player.GetSharedData(EntityData.PLAYER_BANK) - vehModel.price;
                        string purchaseMssage = string.Format(Messages.SUC_VEHICLE_PURCHASED, vehModel.model, vehModel.price);
                        player.SendChatMessage(Constants.COLOR_SUCCESS + purchaseMssage);
                        player.SetSharedData(EntityData.PLAYER_BANK, moneyLeft);
                    }
                });
            });
        }

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (gasTimerList.TryGetValue(player.Value, out Timer gasTimer) == true)
            {
                gasTimer.Dispose();
                gasTimerList.Remove(player.Value);
            }
        }

        public static bool HasPlayerVehicleKeys(Client player, object vehicle)
        {
            bool hasKeys = false;
            int vehicleId = vehicle is Vehicle ? ((Vehicle)vehicle).GetData(EntityData.VEHICLE_ID) : ((VehicleModel)vehicle).id;
            string vehicleOwner = vehicle is Vehicle ? ((Vehicle)vehicle).GetData(EntityData.VEHICLE_OWNER) : ((VehicleModel)vehicle).owner;

            if (vehicleOwner == player.Name)
            {
                hasKeys = true;
            }
            else
            {
                string keyString = player.GetData(EntityData.PLAYER_VEHICLE_KEYS);
                hasKeys = keyString.Split(',').Any(key => int.Parse(key) == vehicleId);
            }

            return hasKeys;
        }

        public static Vehicle GetVehicleById(int vehicleId)
        {
            Vehicle vehicle = null;

            foreach (Vehicle veh in NAPI.Pools.GetAllVehicles())
            {
                if (veh.GetData(EntityData.VEHICLE_ID) == vehicleId)
                {
                    vehicle = veh;
                    break;
                }
            }

            return vehicle;
        }

        public static VehicleModel GetParkedVehicleById(int vehicleId)
        {
            VehicleModel vehicle = null;
            foreach (ParkedCarModel parkedVehicle in Parking.parkedCars)
            {
                if (parkedVehicle.vehicle.id == vehicleId)
                {
                    vehicle = parkedVehicle.vehicle;
                    break;
                }
            }
            return vehicle;
        }

        private bool IsVehicleTrunkInUse(Vehicle vehicle)
        {
            bool trunkUsed = false;

            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasData(EntityData.PLAYER_OPENED_TRUNK) == true)
                {
                    Vehicle openedVehicle = player.GetData(EntityData.PLAYER_OPENED_TRUNK);
                    if (openedVehicle == vehicle)
                    {
                        trunkUsed = true;
                        break;
                    }
                }
            }

            return trunkUsed;
        }

        private static void CreateIngameVehicle(VehicleModel vehModel)
        {
            Vehicle vehicle = NAPI.Vehicle.CreateVehicle(NAPI.Util.VehicleNameToModel(vehModel.model), vehModel.position, vehModel.rotation.Z, new Color(0, 0, 0), new Color(0, 0, 0));
            vehicle.NumberPlate = vehModel.plate == string.Empty ? "LS " + (1000 + vehModel.id) : vehModel.plate;
            vehicle.EngineStatus = vehModel.engine != 0;
            vehicle.Locked = vehModel.locked != 0;
            vehicle.Dimension = Convert.ToUInt32(vehModel.dimension);

            if (vehModel.colorType == Constants.VEHICLE_COLOR_TYPE_PREDEFINED)
            {
                vehicle.PrimaryColor = int.Parse(vehModel.firstColor);
                vehicle.SecondaryColor = int.Parse(vehModel.secondColor);
                vehicle.PearlescentColor = vehModel.pearlescent;
            }
            else
            {
                string[] firstColor = vehModel.firstColor.Split(',');
                string[] secondColor = vehModel.secondColor.Split(',');
                vehicle.CustomPrimaryColor = new Color(int.Parse(firstColor[0]), int.Parse(firstColor[1]), int.Parse(firstColor[2]));
                vehicle.CustomSecondaryColor = new Color(int.Parse(secondColor[0]), int.Parse(secondColor[1]), int.Parse(secondColor[2]));
            }

            vehicle.SetData(EntityData.VEHICLE_ID, vehModel.id);
            vehicle.SetData(EntityData.VEHICLE_MODEL, vehModel.model);
            vehicle.SetData(EntityData.VEHICLE_POSITION, vehModel.position);
            vehicle.SetData(EntityData.VEHICLE_ROTATION, vehModel.rotation);
            vehicle.SetData(EntityData.VEHICLE_DIMENSION, vehModel.dimension);
            vehicle.SetData(EntityData.VEHICLE_COLOR_TYPE, vehModel.colorType);
            vehicle.SetData(EntityData.VEHICLE_FIRST_COLOR, vehModel.firstColor);
            vehicle.SetData(EntityData.VEHICLE_SECOND_COLOR, vehModel.secondColor);
            vehicle.SetData(EntityData.VEHICLE_PEARLESCENT_COLOR, vehModel.pearlescent);
            vehicle.SetData(EntityData.VEHICLE_FACTION, vehModel.faction);
            vehicle.SetData(EntityData.VEHICLE_PLATE, vehModel.plate);
            vehicle.SetData(EntityData.VEHICLE_OWNER, vehModel.owner);
            vehicle.SetData(EntityData.VEHICLE_PRICE, vehModel.price);
            vehicle.SetData(EntityData.VEHICLE_PARKING, vehModel.parking);
            vehicle.SetData(EntityData.VEHICLE_PARKED, vehModel.parked);
            vehicle.SetData(EntityData.VEHICLE_GAS, vehModel.gas);
            vehicle.SetData(EntityData.VEHICLE_KMS, vehModel.kms);

            // Set vehicle's tunning
            Mechanic.AddTunningToVehicle(vehicle);
        }

        private void OnVehicleDeathTimer(object vehicleObject)
        {
            Vehicle vehicle = (Vehicle)vehicleObject;

            // Get the needed data for recreating the vehicle
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.id = vehicle.GetData(EntityData.VEHICLE_ID);
            vehicleModel.model = vehicle.GetData(EntityData.VEHICLE_MODEL);
            vehicleModel.position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            vehicleModel.rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);
            vehicleModel.dimension = vehicle.GetData(EntityData.VEHICLE_DIMENSION);
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

            NAPI.Task.Run(() =>
            {
                // Delete the vehicle
                vehicle.Delete();
            });

            if (vehicleModel.faction == Constants.FACTION_NONE && vehicle.Occupants.Count > 0)
            {
                ParkingModel scrapyard = Parking.parkingList.Where(p => p.type == Constants.PARKING_TYPE_SCRAPYARD).FirstOrDefault();

                if (scrapyard != null)
                {
                    // Link the vehicle to the scrapyard
                    ParkedCarModel parkedCar = new ParkedCarModel();
                    parkedCar.parkingId = scrapyard.id;
                    parkedCar.vehicle = vehicleModel;
                    Parking.parkedCars.Add(parkedCar);
                    vehicleModel.parking = scrapyard.id;
                }

                Task.Factory.StartNew(() =>
                {
                    // Save vehicle data
                    Database.SaveVehicle(vehicleModel);
                });
            }
            else
            {
                NAPI.Task.Run(() =>
                {
                    // Recreate the vehicle in the same position
                    CreateIngameVehicle(vehicleModel);
                });
            }
            
            Timer vehicleRespawnTimer = vehicleRespawnTimerList[vehicleModel.id];
            if (vehicleRespawnTimer != null)
            {
                vehicleRespawnTimer.Dispose();
                vehicleRespawnTimerList.Remove(vehicleModel.id);
            }
        }

        private void OnVehicleRefueled(object vehicleObject)
        {
            Vehicle vehicle = (Vehicle)vehicleObject;
            Client player = vehicle.GetData(EntityData.VEHICLE_REFUELING);

            vehicle.ResetData(EntityData.VEHICLE_REFUELING);
            player.ResetData(EntityData.PLAYER_REFUELING);
            
            if (gasTimerList.TryGetValue(player.Value, out Timer gasTimer) == true)
            {
                gasTimer.Dispose();
                gasTimerList.Remove(player.Value);
            }
            
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_VEHICLE_REFUELED);
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Client player)
        {
            if (player.HasData(EntityData.PLAYER_PARKED_VEHICLE) == true)
            {
                Checkpoint vehicleCheckpoint = player.GetData(EntityData.PLAYER_PARKED_VEHICLE);

                if (vehicleCheckpoint == checkpoint)
                {
                    // Delete the checkpoint
                    vehicleCheckpoint.Delete();
                    player.ResetData(EntityData.PLAYER_PARKED_VEHICLE);
                    player.TriggerEvent("deleteVehicleLocation");
                }
            }
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (Convert.ToInt32(seat) == (int)VehicleSeat.Driver)
            {
                if (vehicle.HasData(EntityData.VEHICLE_TESTING) == true)
                {
                    if (player.HasData(EntityData.PLAYER_TESTING_VEHICLE) == true)
                    {
                        Vehicle testingVehicle = player.GetData(EntityData.PLAYER_TESTING_VEHICLE);
                        if (vehicle != testingVehicle)
                        {
                            player.WarpOutOfVehicle();
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_TESTING_VEHICLE);
                            return;
                        }
                    }
                    else
                    {
                        player.WarpOutOfVehicle();
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_TESTING_VEHICLE);
                        return;
                    }
                }
                else
                {
                    int vehFaction = vehicle.GetData(EntityData.VEHICLE_FACTION);

                    if (vehFaction > 0)
                    {
                        // Get player's faction and job
                        int playerFaction = player.GetData(EntityData.PLAYER_FACTION);
                        int playerJob = player.GetData(EntityData.PLAYER_JOB) + Constants.MAX_FACTION_VEHICLES;

                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) == Constants.STAFF_NONE && vehFaction == Constants.FACTION_ADMIN)
                        {
                            player.WarpOutOfVehicle();
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ADMIN_VEHICLE);
                            return;
                        }
                        else if (vehFaction > 0 && vehFaction < Constants.MAX_FACTION_VEHICLES && playerFaction != vehFaction && vehFaction != Constants.FACTION_DRIVING_SCHOOL && vehFaction != Constants.FACTION_ADMIN)
                        {
                            player.WarpOutOfVehicle();
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE_FACTION);
                            return;
                        }
                        else if (vehFaction > Constants.MAX_FACTION_VEHICLES && playerJob != vehFaction)
                        {
                            player.WarpOutOfVehicle();
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE_JOB);
                            return;
                        }
                    }
                }

                // Engine toggle message
                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_HOW_TO_START_ENGINE);

                // Initialize speedometer and engine status
                float kms = vehicle.GetData(EntityData.VEHICLE_KMS);
                float gas = vehicle.GetData(EntityData.VEHICLE_GAS);
                player.TriggerEvent("initializeSpeedometer", kms, gas, vehicle.EngineStatus);
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (player.Seatbelt)
            {
                player.Seatbelt = false;
                Chat.SendMessageToNearbyPlayers(player, Messages.INF_SEATBELT_UNFASTEN, Constants.MESSAGE_ME, 20.0f);
                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_TRUNK_WITHDRAW_ITEMS);
            }

            // Save gas and kms values
            player.TriggerEvent("resetSpeedometer", vehicle);
        }

        [ServerEvent(Event.VehicleDeath)]
        public void OnVehicleDeath(Vehicle vehicle)
        {
            int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
            Timer vehicleRespawnTimer = new Timer(OnVehicleDeathTimer, vehicle, 7500, Timeout.Infinite);
            vehicleRespawnTimerList.Add(vehicleId, vehicleRespawnTimer);
        }

        [RemoteEvent("stopPlayerCar")]
        public void StopPlayerCarEvent(Client player)
        {
            // Turn the engine off
            player.Vehicle.EngineStatus = false;
        }

        [RemoteEvent("engineOnEventKey")]
        public void EngineOnEventKeyEvent(Client player)
        {
            Vehicle vehicle = player.Vehicle;

            if (vehicle.HasData(EntityData.VEHICLE_TESTING) == false)
            {
                // Get player's faction and job
                int playerFaction = player.GetData(EntityData.PLAYER_FACTION);
                int playerJob = player.GetData(EntityData.PLAYER_JOB) + Constants.MAX_FACTION_VEHICLES;

                int vehicleFaction = vehicle.GetData(EntityData.VEHICLE_FACTION);

                if (player.HasData(EntityData.PLAYER_ALREADY_FUCKING) == true)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CANT_TOOGLE_ENGINE_WHILE_FUCKING);
                }
                else if (vehicle.HasData(EntityData.VEHICLE_REFUELING) == true)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_START_REFUELING);
                }
                else if (vehicle.HasData(EntityData.VEHICLE_WEAPON_UNPACKING) == true)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_START_WEAPON_UNPACKING);
                }
                else if (!HasPlayerVehicleKeys(player, vehicle) && vehicleFaction == Constants.FACTION_NONE)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                }
                else if (player.GetData(EntityData.PLAYER_ADMIN_RANK) == Constants.STAFF_NONE && vehicleFaction == Constants.FACTION_ADMIN)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ADMIN_VEHICLE);
                }
                else if (vehicleFaction > 0 && vehicleFaction < Constants.MAX_FACTION_VEHICLES && playerFaction != vehicleFaction && vehicleFaction != Constants.FACTION_DRIVING_SCHOOL && vehicleFaction != Constants.FACTION_ADMIN)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE_FACTION);
                }
                else if (vehicleFaction > Constants.MAX_FACTION_VEHICLES && playerJob != vehicleFaction)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE_JOB);
                }
                else
                {
                    vehicle.EngineStatus = !vehicle.EngineStatus;
                    player.SendNotification(vehicle.EngineStatus ? Messages.INF_VEHICLE_TURNED_ON : Messages.INF_VEHICLE_TURNED_OFF);
                }
            }
        }

        [RemoteEvent("saveVehicleConsumes")]
        public void SaveVehicleConsumesEvent(Client player, Vehicle vehicle, float kms, float gas)
        {
            // Update kms and gas
            vehicle.SetData(EntityData.VEHICLE_KMS, kms);
            vehicle.SetData(EntityData.VEHICLE_GAS, gas);
        }

        [Command(Messages.COM_SEATBELT)]
        public void SeatbeltCommand(Client player)
        {
            if (!player.IsInVehicle)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE);
            }
            else
            {
                player.Seatbelt = !player.Seatbelt;
                Chat.SendMessageToNearbyPlayers(player, player.Seatbelt ? Messages.INF_SEATBELT_FASTEN : Messages.INF_SEATBELT_UNFASTEN, Constants.MESSAGE_ME, 20.0f);
            }
        }

        [Command(Messages.COM_LOCK)]
        public void LockCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                Vehicle vehicle = Globals.GetClosestVehicle(player);
                if (vehicle == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                }
                else if (HasPlayerVehicleKeys(player, vehicle) == false)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                }
                else
                {
                    string vehicleModel = vehicle.GetData(EntityData.VEHICLE_MODEL);
                    VehicleHash vehicleHash = NAPI.Util.VehicleNameToModel(vehicleModel);
                    if (NAPI.Vehicle.GetVehicleClass(vehicleHash) == Constants.VEHICLE_CLASS_CYCLES || NAPI.Vehicle.GetVehicleClass(vehicleHash) == Constants.VEHICLE_CLASS_MOTORCYCLES || NAPI.Vehicle.GetVehicleClass(vehicleHash) == Constants.VEHICLE_CLASS_BOATS)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_LOCKABLE);
                    }
                    else
                    {
                        vehicle.Locked = !vehicle.Locked;
                        Chat.SendMessageToNearbyPlayers(player, vehicle.Locked ? Messages.SUC_VEH_LOCKED : Messages.SUC_VEH_UNLOCKED, Constants.MESSAGE_ME, 20.0f);
                    }
                }
            }
        }

        [Command(Messages.COM_HOOD)]
        public void HoodCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                Vehicle vehicle = Globals.GetClosestVehicle(player, 3.75f);
                if (vehicle != null)
                {
                    if (HasPlayerVehicleKeys(player, vehicle) == false && player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_MECHANIC)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                    }
                    else if (vehicle.IsDoorOpen(Constants.VEHICLE_HOOD) == false)
                    {
                        vehicle.OpenDoor(Constants.VEHICLE_HOOD);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_HOOD_OPENED);
                    }
                    else
                    {
                        vehicle.CloseDoor(Constants.VEHICLE_HOOD);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_HOOD_CLOSED);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                }
            }
        }

        [Command(Messages.COM_TRUNK, Messages.GEN_TRUNK_COMMAND)]
        public void TrunkCommand(Client player, string action)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                // Get closest vehicle
                Vehicle vehicle = Globals.GetClosestVehicle(player, 3.75f);

                if (vehicle != null)
                {
                    List<InventoryModel> inventory = null;

                    switch (action.ToLower())
                    {
                        case Messages.ARG_OPEN:
                            if (!HasPlayerVehicleKeys(player, vehicle) && vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.FACTION_NONE)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                            }
                            else if (vehicle.IsDoorOpen(Constants.VEHICLE_TRUNK) == true)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_TRUNK_OPENED);
                            }
                            else
                            {
                                vehicle.OpenDoor(Constants.VEHICLE_TRUNK);
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_TRUNK_OPENED);
                            }
                            break;
                        case Messages.ARG_CLOSE:
                            if (!HasPlayerVehicleKeys(player, vehicle) && vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.FACTION_NONE)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                            }
                            else if (vehicle.IsDoorOpen(Constants.VEHICLE_TRUNK) == false)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_TRUNK_CLOSED);
                            }
                            else
                            {
                                vehicle.CloseDoor(Constants.VEHICLE_TRUNK);
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_TRUNK_CLOSED);
                            }
                            break;
                        case Messages.ARG_STORE:
                            if (vehicle.IsDoorOpen(Constants.VEHICLE_TRUNK) == false)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_TRUNK_CLOSED);
                            }
                            else if (IsVehicleTrunkInUse(vehicle) == true)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_TRUNK_IN_USE);
                            }
                            else
                            {
                                if (player.HasSharedData(EntityData.PLAYER_WEAPON_CRATE) == true)
                                {
                                    int vehicleCrates = 0;

                                    // Get player's hand item
                                    int weaponCrateIndex = player.GetSharedData(EntityData.PLAYER_WEAPON_CRATE);
                                    WeaponCrateModel weaponCrate = Weapons.weaponCrateList.ElementAt(weaponCrateIndex);

                                    // Store the item in the trunk
                                    weaponCrate.carriedEntity = Constants.ITEM_ENTITY_VEHICLE;
                                    weaponCrate.carriedIdentifier = vehicle.GetData(EntityData.VEHICLE_ID);

                                    // Remove player's weapon box
                                    player.StopAnimation();
                                    weaponCrate.crateObject.Detach();
                                    weaponCrate.crateObject.Delete();
                                    player.ResetSharedData(EntityData.PLAYER_WEAPON_CRATE);

                                    // Check for any crate into vehicle's trunk
                                    foreach (WeaponCrateModel crates in Weapons.weaponCrateList)
                                    {
                                        if (crates.carriedEntity == Constants.ITEM_ENTITY_VEHICLE && vehicle.GetData(EntityData.VEHICLE_ID) == crates.carriedIdentifier)
                                        {
                                            vehicleCrates++;
                                        }
                                    }

                                    if (vehicleCrates == 1)
                                    {
                                        // Check if there's somebody driving the vehicle
                                        foreach (Client target in vehicle.Occupants)
                                        {
                                            if (target.VehicleSeat == (int)VehicleSeat.Driver)
                                            {
                                                Vector3 weaponPosition = new Vector3(-2085.543f, 2600.857f, -0.4712417f);
                                                Checkpoint weaponCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, weaponPosition, new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                                                target.SetData(EntityData.PLAYER_JOB_CHECKPOINT, weaponCheckpoint);
                                                target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_WEAPON_POSITION_MARK);
                                                target.TriggerEvent("showWeaponCheckpoint", weaponPosition);
                                            }
                                        }
                                    }

                                    // Send the message to the player
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_TRUNK_STORED_ITEMS);
                                }
                                else if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
                                {
                                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                                    ItemModel rightHand = Globals.GetItemInEntity(playerId, Constants.ITEM_ENTITY_RIGHT_HAND);
                                    rightHand.ownerIdentifier = vehicle.GetData(EntityData.VEHICLE_ID);
                                    rightHand.ownerEntity = Constants.ITEM_ENTITY_VEHICLE;

                                    // If it's a weapon, we remove it from the player
                                    if (int.TryParse(rightHand.hash, out int itemHash) == false)
                                    {
                                        WeaponHash weapon = NAPI.Util.WeaponNameToModel(rightHand.hash);
                                        player.RemoveWeapon(weapon);
                                    }

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Update item into database
                                        Database.UpdateItem(rightHand);
                                    });

                                    // Send the message to the player
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_TRUNK_STORED_ITEMS);
                                }
                                else
                                {
                                    // Get player's inventory
                                    inventory = Globals.GetPlayerInventoryAndWeapons(player);

                                    if (inventory.Count > 0)
                                    {
                                        vehicle.OpenDoor(Constants.VEHICLE_TRUNK);
                                        player.SetData(EntityData.PLAYER_OPENED_TRUNK, vehicle);
                                        player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_VEHICLE_PLAYER);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_ITEMS_INVENTORY);
                                    }
                                }
                            }
                            break;
                        case Messages.ARG_WITHDRAW:
                            if (vehicle.IsDoorOpen(Constants.VEHICLE_TRUNK) == false)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_TRUNK_CLOSED);
                            }
                            else if (IsVehicleTrunkInUse(vehicle) == true)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_TRUNK_IN_USE);
                            }
                            else
                            {
                                // Load items into the trunk
                                inventory = Globals.GetVehicleTrunkInventory(vehicle);
                                if (inventory.Count > 0)
                                {
                                    vehicle.OpenDoor(Constants.VEHICLE_TRUNK);
                                    player.SetData(EntityData.PLAYER_OPENED_TRUNK, vehicle);
                                    player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_VEHICLE_TRUNK);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_ITEMS_TRUNK);
                                }
                            }
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_TRUNK_COMMAND);
                            break;
                    }
                }
                else
                {
                    // There's no vehicle near
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                }
            }
        }

        [Command(Messages.COM_KEYS, Messages.GEN_KEYS_COMMAND, GreedyArg = true)]
        public void KeysCommand(Client player, string action, int vehicleId, string targetString = "")
        {
            Vehicle vehicle = null;

            // Get lent keys
            string playerKeys = player.GetData(EntityData.PLAYER_VEHICLE_KEYS);
            string[] playerKeysArray = playerKeys.Split(',');

            switch (action.ToLower())
            {
                case Messages.ARG_SEE:
                    foreach (string key in playerKeysArray)
                    {
                        if (int.Parse(key) == vehicleId)
                        {
                            vehicle = GetVehicleById(vehicleId);

                            if (vehicle != null)
                            {
                                string model = vehicle.GetData(EntityData.VEHICLE_MODEL);
                                string owner = vehicle.GetData(EntityData.VEHICLE_OWNER);
                                player.SendChatMessage(Constants.COLOR_INFO + string.Format(Messages.INF_VEHICLE_KEYS_INFO, vehicleId, model, owner));
                            }
                            else
                            {
                                // Check if the vehicle is parked
                                VehicleModel vehicleModel = GetParkedVehicleById(vehicleId);

                                if (vehicleModel != null)
                                {
                                    string message = string.Format(Messages.INF_VEHICLE_KEYS_INFO, vehicleId, vehicleModel.model, vehicleModel.owner);
                                    player.SendChatMessage(Constants.COLOR_INFO + message);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_EXISTS);
                                }
                            }
                            return;
                        }
                    }

                    // The player doesn't have the keys
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                    break;
                case Messages.ARG_LEND:
                    vehicle = GetVehicleById(vehicleId);

                    if (vehicle == null)
                    {
                        // Check if the vehicle is parked
                        VehicleModel vehicleModel = GetParkedVehicleById(vehicleId);

                        if (vehicle == null)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_EXISTS);
                            return;
                        }
                    }

                    if (!HasPlayerVehicleKeys(player, vehicle))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                    }
                    else
                    {
                        if (targetString.Length > 0)
                        {
                            Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                            if (target != null && target.Position.DistanceTo(player.Position) < 5.0f)
                            {
                                string targetKeys = target.GetData(EntityData.PLAYER_VEHICLE_KEYS);
                                string[] targetKeysArray = targetKeys.Split(',');
                                for (int i = 0; i < targetKeysArray.Length; i++)
                                {
                                    if (int.Parse(targetKeysArray[i]) == 0)
                                    {
                                        targetKeysArray[i] = vehicleId.ToString();
                                        string playerMessage = string.Format(Messages.INF_VEHICLE_KEYS_GIVEN, target.Name);
                                        string targetMessage = string.Format(Messages.INF_VEHICLE_KEYS_RECEIVED, player.Name);
                                        target.SetData(EntityData.PLAYER_VEHICLE_KEYS, string.Join(",", targetKeysArray));
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        return;
                                    }
                                }
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_KEYS_FULL);
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_TOO_FAR);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_KEYS_COMMAND);
                        }
                    }
                    break;
                case Messages.ARG_DROP:
                    for (int i = 0; i < playerKeysArray.Length; i++)
                    {
                        if (playerKeysArray[i] == vehicleId.ToString())
                        {
                            playerKeysArray[i] = "0";
                            Array.Sort(playerKeysArray);
                            Array.Reverse(playerKeysArray);
                            player.SetData(EntityData.PLAYER_VEHICLE_KEYS, string.Join(",", playerKeysArray));
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_VEHICLE_KEYS_THROWN);
                            return;
                        }
                    }

                    // Send a message telling that keys are not found
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                    break;
                default:
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_KEYS_COMMAND);
                    break;
            }
        }

        [Command(Messages.COM_LOCATE, Messages.GEN_LOCATE_COMMAND)]
        public void LocateCommand(Client player, int vehicleId)
        {
            Vehicle vehicle = GetVehicleById(vehicleId);

            if (vehicle == null)
            {
                // Check if the vehicle is parked
                VehicleModel vehModel = GetParkedVehicleById(vehicleId);

                if (vehModel != null)
                {
                    if (HasPlayerVehicleKeys(player, vehModel) == false)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                    }
                    else
                    {
                        ParkingModel parking = Parking.GetParkingById(vehModel.parking);

                        // Set the checkpoint into the zone
                        Checkpoint locationCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, parking.position, new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                        player.SetData(EntityData.PLAYER_PARKED_VEHICLE, locationCheckpoint);
                        player.TriggerEvent("locateVehicle", parking.position);

                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_VEHICLE_PARKED);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_EXISTS);
                }
            }
            else
            {
                if (HasPlayerVehicleKeys(player, vehicle) == false)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                }
                else
                {
                    foreach (Vehicle veh in NAPI.Pools.GetAllVehicles())
                    {
                        if (veh.GetData(EntityData.VEHICLE_ID) == vehicleId)
                        {
                            // Set the checkpoint into the zone
                            Vector3 vehiclePosition = veh.Position;
                            Checkpoint locationCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, vehiclePosition, new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                            player.SetData(EntityData.PLAYER_PARKED_VEHICLE, locationCheckpoint);
                            player.TriggerEvent("locateVehicle", vehiclePosition);
                            
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_VEHICLE_PARKED);
                            break;
                        }
                    }
                }
            }
        }

        [Command(Messages.COM_REFUEL, Messages.GEN_FUEL_COMMAND)]
        public void RefuelCommand(Client player, int amount)
        {
            foreach (BusinessModel business in Business.businessList)
            {
                if (business.type == Constants.BUSINESS_TYPE_GAS_STATION && player.Position.DistanceTo(business.position) < 20.5f)
                {
                    Vehicle vehicle = Globals.GetClosestVehicle(player);
                    
                    int faction = player.GetData(EntityData.PLAYER_FACTION);
                    int job = player.GetData(EntityData.PLAYER_JOB);

                    if (vehicle == null)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                    }
                    else if (vehicle.HasData(EntityData.VEHICLE_REFUELING) == true)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_REFUELING);
                    }
                    else if (player.HasData(EntityData.PLAYER_REFUELING) == true)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_REFUELING);
                    }
                    else if (vehicle.EngineStatus)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ENGINE_ON);
                    }
                    else
                    {
                        int vehicleFaction = vehicle.GetData(EntityData.VEHICLE_FACTION);
                        if (HasPlayerVehicleKeys(player, vehicle) || vehicleFaction == faction || vehicleFaction + 100 == job)
                        {
                            float gasRefueled = 0.0f;
                            float currentGas = vehicle.GetData(EntityData.VEHICLE_GAS);
                            float gasLeft = 50.0f - currentGas;
                            int maxMoney = (int)Math.Round(gasLeft * Constants.PRICE_GAS * business.multiplier);
                            int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);

                            if (amount == 0 || amount > maxMoney)
                            {
                                amount = maxMoney;
                                gasRefueled = gasLeft;
                            }
                            else if (amount > 0)
                            {
                                gasRefueled = amount / (Constants.PRICE_GAS * business.multiplier);
                            }

                            if (amount > 0 && playerMoney >= amount)
                            {
                                vehicle.SetData(EntityData.VEHICLE_GAS, currentGas + gasRefueled);
                                player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - amount);
                                
                                player.SetData(EntityData.PLAYER_REFUELING, vehicle);
                                vehicle.SetData(EntityData.VEHICLE_REFUELING, player);

                                // Timer called when vehicle's refueled
                                Timer gasTimer = new Timer(OnVehicleRefueled, vehicle, (int)Math.Round(gasLeft * 1000), Timeout.Infinite);
                                gasTimerList.Add(player.Value, gasTimer);
                                
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_VEHICLE_REFUELING);
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ENOUGH_MONEY);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                        }
                    }

                    return;
                }
            }
            
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_FUEL_STATION_NEAR);
        }

        [Command(Messages.COM_FILL)]
        public void FillCommand(Client player)
        {
            if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
            {
                int itemId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                ItemModel item = Globals.GetItemModelFromId(itemId);

                if (item.hash == Constants.ITEM_HASH_JERRYCAN)
                {
                    Vehicle vehicle = Globals.GetClosestVehicle(player);
                    if (vehicle != null)
                    {
                        if (HasPlayerVehicleKeys(player, vehicle) == true || player.GetData(EntityData.PLAYER_JOB) == Constants.JOB_MECHANIC)
                        {
                            float gas = vehicle.GetData(EntityData.VEHICLE_GAS);
                            vehicle.SetData(EntityData.VEHICLE_GAS, gas + Constants.GAS_CAN_LITRES > 50.0f ? 50.0f : gas + Constants.GAS_CAN_LITRES);

                            item.objectHandle.Detach();
                            item.objectHandle.Delete();
                            player.ResetData(EntityData.PLAYER_RIGHT_HAND);

                            Task.Factory.StartNew(() =>
                            {
                                // Remove the item
                                Database.RemoveItem(item.id);
                                Globals.itemList.Remove(item);
                            });
                            
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_VEHICLE_REFILLED);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NO_JERRYCAN);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RIGHT_HAND_EMPTY);
            }
        }

        [Command(Messages.COM_SCRAP)]
        public void ScrapCommand(Client player)
        {
            if (player.VehicleSeat == (int)VehicleSeat.Driver)
            {
                ParkingModel parking = Parking.GetClosestParking(player, 3.5f);
                if (parking != null && parking.type == Constants.PARKING_TYPE_SCRAPYARD)
                {
                    Vehicle vehicle = player.Vehicle;
                    if (vehicle.GetData(EntityData.VEHICLE_OWNER) == player.Name)
                    {
                        // Get vehicle basic data
                        int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                        int vehiclePrice = vehicle.GetData(EntityData.VEHICLE_PRICE);
                        float vehicleKms = vehicle.GetData(EntityData.VEHICLE_KMS);

                        // Get player's money
                        int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);

                        // Calculate amount won
                        int vehicleMaxValue = (int)Math.Round(vehiclePrice * 0.5f);
                        int vehicleMinValue = (int)Math.Round(vehiclePrice * 0.1f);
                        float vehicleReduction = vehicleKms / Constants.REDUCTION_PER_KMS / 1000;
                        int amountGiven = vehicleMaxValue - (int)Math.Round(vehicleReduction / 100 * vehicleMaxValue);
                        
                        if (amountGiven < vehicleMinValue)
                        {
                            amountGiven = vehicleMinValue;
                        }
                        
                        // Payment to the player
                        player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney + amountGiven);

                        player.WarpOutOfVehicle();
                        vehicle.Delete();

                        Task.Factory.StartNew(() =>
                        {
                            // Delete the vehicle
                            Database.RemoveVehicle(vehicleId);
                        });
                        
                        string message = string.Format(Messages.SUC_VEHICLE_SCRAPYARD, amountGiven);
                        player.SendChatMessage(Constants.COLOR_SUCCESS + message);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_VEH_OWNER);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_SCRAPYARD_NEAR);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_VEHICLE_DRIVING);
            }
        }
    }
}