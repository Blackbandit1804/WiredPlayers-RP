using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.vehicles;
using WiredPlayers.house;
using WiredPlayers.jobs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WiredPlayers.parking
{
    public class Parking : Script
    {
        public static List<ParkingModel> parkingList;
        public static List<ParkedCarModel> parkedCars;

        public void LoadDatabaseParkings()
        {
            parkingList = Database.LoadAllParkings();
            foreach (ParkingModel parking in parkingList)
            {
                string parkingLabelText = GetParkingLabelText(parking.type);
                parking.parkingLabel = NAPI.TextLabel.CreateTextLabel(parkingLabelText, parking.position, 30.0f, 0.75f, 4, new Color(255, 255, 255));
            }
        }

        public static ParkingModel GetClosestParking(Client player, float distance = 1.5f)
        {
            ParkingModel parking = null;
            foreach (ParkingModel parkingModel in parkingList)
            {
                if (parkingModel.position.DistanceTo(player.Position) < distance)
                {
                    distance = parkingModel.position.DistanceTo(player.Position);
                    parking = parkingModel;
                }
            }
            return parking;
        }

        public static int GetParkedCarAmount(ParkingModel parking)
        {
            int totalVehicles = 0;
            foreach (ParkedCarModel parkedCar in parkedCars)
            {
                if (parkedCar.parkingId == parking.id)
                {
                    totalVehicles++;
                }
            }
            return totalVehicles;
        }

        public static string GetParkingLabelText(int type)
        {
            string labelText = string.Empty;
            switch (type)
            {
                case Constants.PARKING_TYPE_PUBLIC:
                    labelText = Messages.GEN_PUBLIC_PARKING;
                    break;
                case Constants.PARKING_TYPE_GARAGE:
                    labelText = Messages.GEN_GARAGE;
                    break;
                case Constants.PARKING_TYPE_SCRAPYARD:
                    labelText = Messages.GEN_SCRAPYARD;
                    break;
                case Constants.PARKING_TYPE_DEPOSIT:
                    labelText = Messages.GEN_POLICE_DEPOT;
                    break;
            }
            return labelText;
        }

        public static ParkingModel GetParkingById(int parkingId)
        {
            ParkingModel parking = null;
            foreach (ParkingModel parkingModel in parkingList)
            {
                if (parkingModel.id == parkingId)
                {
                    parking = parkingModel;
                    break;
                }
            }
            return parking;
        }

        private static ParkedCarModel GetParkedVehicle(int vehicleId)
        {
            ParkedCarModel vehicle = null;
            foreach (ParkedCarModel parkedCar in parkedCars)
            {
                if (parkedCar.vehicle.id == vehicleId)
                {
                    vehicle = parkedCar;
                    break;
                }
            }
            return vehicle;
        }

        private void PlayerParkVehicle(Client player, ParkingModel parking)
        {
            // Get vehicle data
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.rotation = player.Vehicle.Rotation;
            vehicleModel.id = player.Vehicle.GetData(EntityData.VEHICLE_ID);
            vehicleModel.model = player.Vehicle.GetData(EntityData.VEHICLE_MODEL);
            vehicleModel.colorType = player.Vehicle.GetData(EntityData.VEHICLE_COLOR_TYPE);
            vehicleModel.firstColor = player.Vehicle.GetData(EntityData.VEHICLE_FIRST_COLOR);
            vehicleModel.secondColor = player.Vehicle.GetData(EntityData.VEHICLE_SECOND_COLOR);
            vehicleModel.pearlescent = player.Vehicle.GetData(EntityData.VEHICLE_PEARLESCENT_COLOR);
            vehicleModel.faction = player.Vehicle.GetData(EntityData.VEHICLE_FACTION);
            vehicleModel.plate = player.Vehicle.GetData(EntityData.VEHICLE_PLATE);
            vehicleModel.owner = player.Vehicle.GetData(EntityData.VEHICLE_OWNER);
            vehicleModel.price = player.Vehicle.GetData(EntityData.VEHICLE_PRICE);
            vehicleModel.gas = player.Vehicle.GetData(EntityData.VEHICLE_GAS);
            vehicleModel.kms = player.Vehicle.GetData(EntityData.VEHICLE_KMS);

            // Update parking values
            vehicleModel.position = parking.position;
            vehicleModel.dimension = Convert.ToUInt32(parking.id);
            vehicleModel.parking = parking.id;
            vehicleModel.parked = 0;

            // Link vehicle to the parking
            ParkedCarModel parkedCarModel = new ParkedCarModel();
            parkedCarModel.vehicle = vehicleModel;
            parkedCarModel.parkingId = parking.id;
            parkedCars.Add(parkedCarModel);

            // Save the vehicle and delete it from the game
            player.WarpOutOfVehicle();
            player.Vehicle.Delete();

            Task.Factory.StartNew(() =>
            {
                // Save the vehicle
                Database.SaveVehicle(vehicleModel);
            });
        }

        [Command(Messages.COM_PARK)]
        public void ParkCommand(Client player)
        {
            if (player.VehicleSeat != (int)VehicleSeat.Driver)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_VEHICLE_DRIVING);
            }
            else if (player.Vehicle.GetData(EntityData.VEHICLE_FACTION) != Constants.FACTION_NONE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_FACTION_PARK);
            }
            else
            {
                Vehicle vehicle = player.Vehicle;
                if (Vehicles.HasPlayerVehicleKeys(player, vehicle) && player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
                }
                else
                {
                    foreach (ParkingModel parking in parkingList)
                    {
                        if (player.Position.DistanceTo(parking.position) < 3.5f)
                        {
                            switch (parking.type)
                            {
                                case Constants.PARKING_TYPE_PUBLIC:
                                    string message = string.Format(Messages.INF_PARKING_COST, Constants.PRICE_PARKING_PUBLIC);
                                    player.SendChatMessage(Constants.COLOR_INFO + message);
                                    PlayerParkVehicle(player, parking);
                                    break;
                                case Constants.PARKING_TYPE_GARAGE:
                                    HouseModel house = House.GetHouseById(parking.houseId);
                                    if (house == null || House.HasPlayerHouseKeys(player, house) == false)
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_GARAGE_ACCESS);
                                    }
                                    else if (GetParkedCarAmount(parking) == parking.capacity)
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PARKING_FULL);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_VEHICLE_GARAGE_PARKED);
                                        PlayerParkVehicle(player, parking);
                                    }
                                    break;
                                case Constants.PARKING_TYPE_DEPOSIT:
                                    if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_VEHICLE_DEPOSIT_PARKED);
                                        PlayerParkVehicle(player, parking);
                                    }
                                    break;
                                default:
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_PARKING_ALLOWED);
                                    break;
                            }
                            return;
                        }
                    }

                    // There's no parking near
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_PARKING_NEAR);
                }
            }
        }

        [Command(Messages.COM_UNPARK, Messages.GEN_UNPARK_COMMAND)]
        public void UnparkCommand(Client player, int vehicleId)
        {
            VehicleModel vehicle = Vehicles.GetParkedVehicleById(vehicleId);

            if (vehicle == null)
            {
                // There's no vehicle with that identifier
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_EXISTS);
            }
            else if (Vehicles.HasPlayerVehicleKeys(player, vehicle) == false)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_CAR_KEYS);
            }
            else
            {
                foreach (ParkingModel parking in parkingList)
                {
                    if (player.Position.DistanceTo(parking.position) < 2.5f)
                    {
                        // Check whether the vehicle is in this parking
                        if (parking.id == vehicle.parking)
                        {
                            int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);
                            
                            switch (parking.type)
                            {
                                case Constants.PARKING_TYPE_PUBLIC:
                                    break;
                                case Constants.PARKING_TYPE_SCRAPYARD:
                                    break;
                                case Constants.PARKING_TYPE_DEPOSIT:
                                    // Remove player's money
                                    if (playerMoney >= Constants.PRICE_PARKING_DEPOSIT)
                                    {
                                        player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - Constants.PRICE_PARKING_DEPOSIT);
                                        
                                        string message = string.Format(Messages.INF_UNPARK_MONEY, Constants.PRICE_PARKING_DEPOSIT);
                                        player.SendChatMessage(Constants.COLOR_INFO + message);
                                    }
                                    else
                                    {
                                        string message = string.Format(Messages.ERR_PARKING_NOT_MONEY, Constants.PRICE_PARKING_DEPOSIT);
                                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                                        return;
                                    }
                                    break;
                            }

                            // Get parked vehicle model
                            ParkedCarModel parkedCar = GetParkedVehicle(vehicleId);

                            // Recreate the vehicle
                            Vehicle newVehicle = NAPI.Vehicle.CreateVehicle(NAPI.Util.VehicleNameToModel(vehicle.model), parking.position, vehicle.rotation.Z, new Color(0, 0, 0), new Color(0, 0, 0));
                            newVehicle.NumberPlate = vehicle.plate == string.Empty ? "LS " + (1000 + vehicle.id) : vehicle.plate;
                            newVehicle.EngineStatus = false;
                            newVehicle.Locked = false;
                            
                            if (vehicle.colorType == Constants.VEHICLE_COLOR_TYPE_PREDEFINED)
                            {
                                newVehicle.PrimaryColor = int.Parse(vehicle.firstColor);
                                newVehicle.SecondaryColor = int.Parse(vehicle.secondColor);
                                newVehicle.PearlescentColor = vehicle.pearlescent;
                            }
                            else
                            {
                                string[] firstColor = vehicle.firstColor.Split(',');
                                string[] secondColor = vehicle.secondColor.Split(',');
                                newVehicle.CustomPrimaryColor = new Color(int.Parse(firstColor[0]), int.Parse(firstColor[1]), int.Parse(firstColor[2]));
                                newVehicle.CustomSecondaryColor = new Color(int.Parse(secondColor[0]), int.Parse(secondColor[1]), int.Parse(secondColor[2]));
                            }

                            newVehicle.SetData(EntityData.VEHICLE_ID, vehicle.id);
                            newVehicle.SetData(EntityData.VEHICLE_MODEL, vehicle.model);
                            newVehicle.SetData(EntityData.VEHICLE_POSITION, parking.position);
                            newVehicle.SetData(EntityData.VEHICLE_ROTATION, vehicle.rotation);
                            newVehicle.SetData(EntityData.VEHICLE_COLOR_TYPE, vehicle.colorType);
                            newVehicle.SetData(EntityData.VEHICLE_FIRST_COLOR, vehicle.firstColor);
                            newVehicle.SetData(EntityData.VEHICLE_SECOND_COLOR, vehicle.secondColor);
                            newVehicle.SetData(EntityData.VEHICLE_PEARLESCENT_COLOR, vehicle.pearlescent);
                            newVehicle.SetData(EntityData.VEHICLE_FACTION, vehicle.faction);
                            newVehicle.SetData(EntityData.VEHICLE_PLATE, vehicle.plate);
                            newVehicle.SetData(EntityData.VEHICLE_OWNER, vehicle.owner);
                            newVehicle.SetData(EntityData.VEHICLE_PRICE, vehicle.price);
                            newVehicle.SetData(EntityData.VEHICLE_GAS, vehicle.gas);
                            newVehicle.SetData(EntityData.VEHICLE_KMS, vehicle.kms);

                            // Update parking values
                            newVehicle.SetData(EntityData.VEHICLE_DIMENSION, 0);
                            newVehicle.SetData(EntityData.VEHICLE_PARKING, 0);
                            newVehicle.SetData(EntityData.VEHICLE_PARKED, 0);

                            // Add tunning
                            Mechanic.AddTunningToVehicle(newVehicle);

                            // Unlink from the parking
                            parkedCars.Remove(parkedCar);

                            return;
                        }

                        // The vehicle is not in this parking
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_THIS_PARKING);
                        return;
                    }
                }

                // Player's not in any parking
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_PARKING_NEAR);
            }
        }
    }
}
