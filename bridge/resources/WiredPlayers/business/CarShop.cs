using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.vehicles;
using System.Collections.Generic;
using System;
using System.Linq;

namespace WiredPlayers.business
{
    public class CarShop : Script
    {
        private TextLabel carShopTextLabel;
        private TextLabel motorbikeShopTextLabel;
        private TextLabel shipShopTextLabel;

        private int GetClosestCarShop(Client player, float distance = 2.0f)
        {
            int carShop = -1;
            if (player.Position.DistanceTo(carShopTextLabel.Position) < distance)
            {
                carShop = 0;
            }
            else if (player.Position.DistanceTo(motorbikeShopTextLabel.Position) < distance)
            {
                carShop = 1;
            }
            else if (player.Position.DistanceTo(shipShopTextLabel.Position) < distance)
            {
                carShop = 2;
            }
            return carShop;
        }

        private List<CarShopVehicleModel> GetVehicleListInCarShop(int carShop)
        {
            // Get all the vehicles in the list
            return Constants.CARSHOP_VEHICLE_LIST.Where(vehicle => vehicle.carShop == carShop).ToList();
        }

        private int GetVehiclePrice(VehicleHash vehicleHash)
        {
            int price = 0;
            foreach (CarShopVehicleModel vehicle in Constants.CARSHOP_VEHICLE_LIST)
            {
                if (vehicle.hash == vehicleHash)
                {
                    price = vehicle.price;
                    break;
                }
            }
            return price;
        }

        private string GetVehicleModel(VehicleHash vehicleHash)
        {
            string model = string.Empty;
            foreach (CarShopVehicleModel vehicle in Constants.CARSHOP_VEHICLE_LIST)
            {
                if (vehicle.hash == vehicleHash)
                {
                    model = vehicle.model;
                    break;
                }
            }
            return model;
        }

        private bool SpawnPurchasedVehicle(Client player, List<Vector3> spawns, VehicleHash vehicleHash, int vehiclePrice, string firstColor, string secondColor)
        {
            for (int i = 0; i < spawns.Count; i++)
            {
                // Check if the spawn point has a vehicle on it
                bool spawnOccupied = NAPI.Pools.GetAllVehicles().Where(veh => spawns[i].DistanceTo(veh.Position) < 2.5f).Any();

                if (!spawnOccupied)
                {
                    // Basic data for vehicle creation
                    VehicleModel vehicleModel = new VehicleModel();
                    vehicleModel.model = GetVehicleModel(vehicleHash);
                    vehicleModel.plate = string.Empty;
                    vehicleModel.position = spawns[i];
                    vehicleModel.rotation = new Vector3(0.0, 0.0, 0.0);
                    vehicleModel.owner = player.GetData(EntityData.PLAYER_NAME);
                    vehicleModel.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                    vehicleModel.firstColor = firstColor;
                    vehicleModel.secondColor = secondColor;
                    vehicleModel.pearlescent = 0;
                    vehicleModel.price = vehiclePrice;
                    vehicleModel.parking = 0;
                    vehicleModel.parked = 0;
                    vehicleModel.engine = 0;
                    vehicleModel.locked = 0;
                    vehicleModel.gas = 50.0f;
                    vehicleModel.kms = 0.0f;

                    // Creating the purchased vehicle
                    Vehicles.CreateVehicle(player, vehicleModel, false);

                    return true;
                }
            }

            return false;
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            // Car dealer creation
            carShopTextLabel = NAPI.TextLabel.CreateTextLabel("/" + Messages.COM_CATALOG, new Vector3(-56.88f, -1097.12f, 26.52f), 10.0f, 0.5f, 4, new Color(255, 255, 153));
            TextLabel carShopSubTextLabel = NAPI.TextLabel.CreateTextLabel(Messages.GEN_CATALOG_HELP, new Vector3(-56.88f, -1097.12f, 26.42f), 10.0f, 0.5f, 4, new Color(255, 255, 255));
            Blip carShopBlip = NAPI.Blip.CreateBlip(new Vector3(-56.88f, -1097.12f, 26.52f));
            carShopBlip.Name = Messages.GEN_CAR_DEALER;
            carShopBlip.Sprite = 225;

            // Motorcycle dealer creation
            motorbikeShopTextLabel = NAPI.TextLabel.CreateTextLabel("/" + Messages.COM_CATALOG, new Vector3(286.76f, -1148.36f, 29.29f), 10.0f, 0.5f, 4, new Color(255, 255, 153));
            TextLabel motorbikeShopSubTextLabel = NAPI.TextLabel.CreateTextLabel(Messages.GEN_CATALOG_HELP, new Vector3(286.76f, -1148.36f, 29.19f), 10.0f, 0.5f, 4, new Color(255, 255, 255));
            Blip motorbikeShopBlip = NAPI.Blip.CreateBlip(new Vector3(286.76f, -1148.36f, 29.29f));
            motorbikeShopBlip.Name = Messages.GEN_MOTORCYCLE_DEALER;
            motorbikeShopBlip.Sprite = 226;

            // Boat dealer creation
            shipShopTextLabel = NAPI.TextLabel.CreateTextLabel("/" + Messages.COM_CATALOG, new Vector3(-711.6249f, -1299.427f, 5.41f), 10.0f, 0.5f, 4, new Color(255, 255, 153));
            TextLabel shipShopSubTextLabel = NAPI.TextLabel.CreateTextLabel(Messages.GEN_CATALOG_HELP, new Vector3(-711.6249f, -1299.427f, 5.31f), 10.0f, 0.5f, 4, new Color(255, 255, 255));
            Blip shipShopBlip = NAPI.Blip.CreateBlip(new Vector3(-711.6249f, -1299.427f, 5.41f));
            shipShopBlip.Name = Messages.GEN_BOAT_DEALER;
            shipShopBlip.Sprite = 455;
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Client player)
        {
            if (player.HasData(EntityData.PLAYER_DRIVING_COLSHAPE) && player.HasData(EntityData.PLAYER_TESTING_VEHICLE) == true)
            {
                if (player.IsInVehicle && player.GetData(EntityData.PLAYER_DRIVING_COLSHAPE) == checkpoint)
                {
                    Vehicle vehicle = player.GetData(EntityData.PLAYER_TESTING_VEHICLE);
                    if (player.Vehicle == vehicle)
                    {
                        // We destroy the vehicle and the checkpoint
                        Checkpoint testCheckpoint = player.GetData(EntityData.PLAYER_DRIVING_COLSHAPE);
                        player.WarpOutOfVehicle();
                        testCheckpoint.Delete();
                        vehicle.Delete();

                        // Variable cleaning
                        player.ResetData(EntityData.PLAYER_TESTING_VEHICLE);
                        player.ResetData(EntityData.PLAYER_DRIVING_COLSHAPE);

                        // Deleting checkpoint
                        player.TriggerEvent("deleteCarshopCheckpoint");
                    }
                }
            }
        }

        [RemoteEvent("purchaseVehicle")]
        public void PurchaseVehicleEvent(Client player, string hash, string firstColor, string secondColor)
        {
            int carShop = GetClosestCarShop(player);
            VehicleHash vehicleHash = (VehicleHash)uint.Parse(hash);
            int vehiclePrice = GetVehiclePrice(vehicleHash);

            if (vehiclePrice > 0 && player.GetSharedData(EntityData.PLAYER_BANK) >= vehiclePrice)
            {
                bool vehicleSpawned = false;

                switch (carShop)
                {
                    case 0:
                        // Create a new car
                        vehicleSpawned = SpawnPurchasedVehicle(player, Constants.CARSHOP_SPAWNS, vehicleHash, vehiclePrice, firstColor, secondColor);                        
                        break;
                    case 1:
                        // Create a new motorcycle
                        vehicleSpawned = SpawnPurchasedVehicle(player, Constants.BIKESHOP_SPAWNS, vehicleHash, vehiclePrice, firstColor, secondColor);
                        break;
                    case 2:
                        // Create a new ship
                        vehicleSpawned = SpawnPurchasedVehicle(player, Constants.SHIP_SPAWNS, vehicleHash, vehiclePrice, firstColor, secondColor);
                        break;
                }

                if(!vehicleSpawned)
                {
                    // Parking places are occupied
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CARSHOP_SPAWN_OCCUPIED);
                }
            }
            else
            {
                string message = string.Format(Messages.ERR_CARSHOP_NO_MONEY, vehiclePrice);
                player.SendChatMessage(Constants.COLOR_ERROR + message);
            }
        }

        [RemoteEvent("testVehicle")]
        public void TestVehicleEvent(Client player, string hash)
        {
            Vehicle vehicle = null;
            Checkpoint testFinishCheckpoint = null;
            VehicleHash vehicleModel = (VehicleHash)uint.Parse(hash);

            switch (GetClosestCarShop(player))
            {
                case 0:
                    vehicle = NAPI.Vehicle.CreateVehicle(vehicleModel, new Vector3(-51.54087f, -1076.941f, 26.94754f), 75.0f, new Color(0, 0, 0), new Color(0, 0, 0));
                    testFinishCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, new Vector3(-28.933f, -1085.566f, 25.565f), new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                    break;
                case 1:
                    vehicle = NAPI.Vehicle.CreateVehicle(vehicleModel, new Vector3(307.0036f, -1162.707f, 29.29191f), 180.0f, new Color(0, 0, 0), new Color(0, 0, 0));
                    testFinishCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, new Vector3(267.412f, -1159.755f, 28.263f), new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                    break;
                case 2:
                    vehicle = NAPI.Vehicle.CreateVehicle(vehicleModel, new Vector3(-717.3467f, -1319.792f, -0.42f), 180.0f, new Color(0, 0, 0), new Color(0, 0, 0));
                    testFinishCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, new Vector3(-711.267f, -1351.501f, -1.359f), new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                    break;
            }

            // Vehicle variable initialization
            vehicle.SetData(EntityData.VEHICLE_KMS, 0.0f);
            vehicle.SetData(EntityData.VEHICLE_GAS, 50.0f);
            vehicle.SetData(EntityData.VEHICLE_TESTING, true);
            player.SetData(EntityData.PLAYER_TESTING_VEHICLE, vehicle);
            player.SetIntoVehicle(vehicle, (int)VehicleSeat.Driver);
            vehicle.EngineStatus = true;

            // Adding the checkpoint
            player.SetData(EntityData.PLAYER_DRIVING_COLSHAPE, testFinishCheckpoint);
            player.TriggerEvent("showCarshopCheckpoint", testFinishCheckpoint.Position);

            // Confirmation message sent to the player
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_TEST_VEHICLE);
        }

        [Command(Messages.COM_CATALOG)]
        public void CatalogoCommand(Client player)
        {
            int carShop = GetClosestCarShop(player);

            if (carShop > -1)
            {
                // We get the vehicle list
                List<CarShopVehicleModel> carList = GetVehicleListInCarShop(carShop);

                // Getting the speed for each vehicle in the list
                foreach (CarShopVehicleModel carShopVehicle in carList)
                {
                    VehicleHash vehicleHash = NAPI.Util.VehicleNameToModel(carShopVehicle.model);
                    carShopVehicle.speed = (int)Math.Round(NAPI.Vehicle.GetVehicleMaxSpeed(vehicleHash) * 3.6f);
                }

                // We show the catalog
                player.TriggerEvent("showVehicleCatalog", NAPI.Util.ToJson(carList), carShop);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_CARSHOP);
            }
        }
    }
}
