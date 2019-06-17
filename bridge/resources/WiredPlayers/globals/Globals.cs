using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.house;
using WiredPlayers.business;
using WiredPlayers.chat;
using WiredPlayers.weapons;
using WiredPlayers.parking;
using WiredPlayers.vehicles;
using WiredPlayers.drivingschool;
using WiredPlayers.factions;
using WiredPlayers.jobs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.globals
{
    public class Globals : Script
    {
        private int fastFoodId = 1;
        public static int orderGenerationTime;
        public static List<FastFoodOrderModel> fastFoodOrderList;
        public static List<ClothesModel> clothesList;
        public static List<TattooModel> tattooList;
        public static List<ItemModel> itemList;
        public static List<ScoreModel> scoreList;
        public static List<AdminTicketModel> adminTicketList;
        private Timer minuteTimer;
        private Timer playersCheckTimer;

        public static Client GetPlayerById(int id)
        {
            Client target = null;
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.Value == id)
                {
                    target = player;
                    break;
                }
            }
            return target;
        }

        public static Vector3 GetBusinessIplExit(string ipl)
        {
            Vector3 position = null;
            foreach (BusinessIplModel iplModel in Constants.BUSINESS_IPL_LIST)
            {
                if (iplModel.ipl == ipl)
                {
                    position = iplModel.position;
                    break;
                }
            }
            return position;
        }

        public static Vector3 GetHouseIplExit(string ipl)
        {
            Vector3 position = null;
            foreach (HouseIplModel iplModel in Constants.HOUSE_IPL_LIST)
            {
                if (iplModel.ipl == ipl)
                {
                    position = iplModel.position;
                    break;
                }
            }
            return position;
        }

        public static Vehicle GetClosestVehicle(Client player, float distance = 2.5f)
        {
            Vehicle vehicle = null;
            foreach (Vehicle veh in NAPI.Pools.GetAllVehicles())
            {
                Vector3 vehPos = veh.Position;
                float distanceVehicleToPlayer = player.Position.DistanceTo(vehPos);

                if (distanceVehicleToPlayer < distance && player.Dimension == veh.Dimension)
                {
                    distance = distanceVehicleToPlayer;
                    vehicle = veh;
                }
            }
            return vehicle;
        }

        public static int GetTotalSeconds()
        {
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private void UpdatePlayerList(object unused)
        {
            // Update player list
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasData(EntityData.PLAYER_PLAYING) == true)
                {
                    ScoreModel scoreModel = scoreList.First(score => score.playerId == player.Value);
                    scoreModel.playerPing = player.Ping;
                }
            }
        }

        private void OnMinuteSpent(object unused)
        {
            // Adjust server's time
            TimeSpan currentTime = TimeSpan.FromTicks(DateTime.Now.Ticks);
            NAPI.World.SetTime(currentTime.Hours, currentTime.Minutes, currentTime.Seconds);

            int totalSeconds = GetTotalSeconds();
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasData(EntityData.PLAYER_PLAYING) == true)
                {
                    int played = player.GetData(EntityData.PLAYER_PLAYED);
                    if (played > 0 && played % 60 == 0)
                    {
                        // Reduce job cooldown
                        int employeeCooldown = player.GetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN);
                        if (employeeCooldown > 0)
                        {
                            player.SetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN, employeeCooldown - 1);
                        }

                        // Generate the payday
                        GeneratePlayerPayday(player);
                    }
                    player.SetData(EntityData.PLAYER_PLAYED, played + 1);

                    // Check if the player is injured waiting for the hospital respawn
                    if (player.HasData(EntityData.TIME_HOSPITAL_RESPAWN) == true)
                    {
                        if (player.GetData(EntityData.TIME_HOSPITAL_RESPAWN) <= totalSeconds)
                        {
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_CAN_DIE);
                        }
                    }

                    // Check if the player has job cooldown
                    int jobCooldown = player.GetData(EntityData.PLAYER_JOB_COOLDOWN);
                    if (jobCooldown > 0)
                    {
                        player.SetData(EntityData.PLAYER_JOB_COOLDOWN, jobCooldown - 1);
                    }

                    // Check if the player's in jail
                    if (player.HasData(EntityData.PLAYER_JAILED) == true)
                    {
                        int jailTime = player.GetData(EntityData.PLAYER_JAILED);
                        if (jailTime == 1)
                        {
                            if (player.GetData(EntityData.PLAYER_JAIL_TYPE) == Constants.JAIL_TYPE_IC)
                            {
                                player.Position = Constants.JAIL_SPAWNS[3];
                            }
                            else
                            {
                                player.Position = Constants.JAIL_SPAWNS[4];
                            }

                            // Remove player from jail
                            player.SetData(EntityData.PLAYER_JAILED, 0);
                            player.SetData(EntityData.PLAYER_JAIL_TYPE, 0);

                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_UNJAILED);
                        }
                        else if (jailTime > 0)
                        {
                            jailTime--;
                            player.SetData(EntityData.PLAYER_JAILED, jailTime);
                        }
                    }

                    if (player.HasData(EntityData.PLAYER_DRUNK_LEVEL) == true)
                    {
                        // Lower alcohol level
                        float drunkLevel = player.GetData(EntityData.PLAYER_DRUNK_LEVEL) - 0.05f;

                        if (drunkLevel <= 0.0f)
                        {
                            player.ResetData(EntityData.PLAYER_DRUNK_LEVEL);
                        }
                        else
                        {
                            if (drunkLevel < Constants.WASTED_LEVEL)
                            {
                                player.ResetSharedData(EntityData.PLAYER_WALKING_STYLE);
                                NAPI.ClientEvent.TriggerClientEventForAll("resetPlayerWalkingStyle", player.Handle);
                            }

                            player.SetData(EntityData.PLAYER_DRUNK_LEVEL, drunkLevel);
                        }
                    }

                    PlayerModel character = new PlayerModel();

                    character.position = player.Position;
                    character.rotation = player.Rotation;
                    character.health = player.Health;
                    character.armor = player.Armor;
                    character.id = player.GetData(EntityData.PLAYER_SQL_ID);
                    character.phone = player.GetData(EntityData.PLAYER_PHONE);
                    character.radio = player.GetData(EntityData.PLAYER_RADIO);
                    character.killed = player.GetData(EntityData.PLAYER_KILLED);
                    character.faction = player.GetData(EntityData.PLAYER_FACTION);
                    character.job = player.GetData(EntityData.PLAYER_JOB);
                    character.rank = player.GetData(EntityData.PLAYER_RANK);
                    character.duty = player.GetData(EntityData.PLAYER_ON_DUTY);
                    character.carKeys = player.GetData(EntityData.PLAYER_VEHICLE_KEYS);
                    character.documentation = player.GetData(EntityData.PLAYER_DOCUMENTATION);
                    character.licenses = player.GetData(EntityData.PLAYER_LICENSES);
                    character.insurance = player.GetData(EntityData.PLAYER_MEDICAL_INSURANCE);
                    character.weaponLicense = player.GetData(EntityData.PLAYER_WEAPON_LICENSE);
                    character.houseRent = player.GetData(EntityData.PLAYER_RENT_HOUSE);
                    character.houseEntered = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    character.businessEntered = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                    character.employeeCooldown = player.GetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN);
                    character.jobCooldown = player.GetData(EntityData.PLAYER_JOB_COOLDOWN);
                    character.jobDeliver = player.GetData(EntityData.PLAYER_JOB_DELIVER);
                    character.jobPoints = player.GetData(EntityData.PLAYER_JOB_POINTS);
                    character.rolePoints = player.GetData(EntityData.PLAYER_ROLE_POINTS);
                    character.played = player.GetData(EntityData.PLAYER_PLAYED);
                    character.jailed = player.GetData(EntityData.PLAYER_JAIL_TYPE) + "," + player.GetData(EntityData.PLAYER_JAILED);

                    character.money = player.GetSharedData(EntityData.PLAYER_MONEY);
                    character.bank = player.GetSharedData(EntityData.PLAYER_BANK);

                    Task.Factory.StartNew(() =>
                    {
                        // Save the player into database
                        Database.SaveCharacterInformation(character);
                    });
                }
            }

            // Generate new fastfood orders
            if (orderGenerationTime <= totalSeconds && House.houseList.Count > 0)
            {
                Random rnd = new Random();
                int generatedOrders = rnd.Next(7, 20);
                for (int i = 0; i < generatedOrders; i++)
                {
                    FastFoodOrderModel order = new FastFoodOrderModel();
                    order.id = fastFoodId;
                    order.pizzas = rnd.Next(0, 4);
                    order.hamburgers = rnd.Next(0, 4);
                    order.sandwitches = rnd.Next(0, 4);
                    order.position = GetPlayerFastFoodDeliveryDestination();
                    order.limit = totalSeconds + 300;
                    order.taken = false;
                    fastFoodOrderList.Add(order);
                    fastFoodId++;
                }

                // Update the new timer time
                orderGenerationTime = totalSeconds + rnd.Next(2, 5) * 60;
            }

            // Remove old orders
            fastFoodOrderList.RemoveAll(order => !order.taken && order.limit <= totalSeconds);

            List<VehicleModel> vehicleList = new List<VehicleModel>();

            foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
            {
                if (!vehicle.HasData(EntityData.VEHICLE_TESTING) && vehicle.GetData(EntityData.VEHICLE_FACTION) == 0)
                {
                    VehicleModel vehicleModel = new VehicleModel();
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

                    // Add vehicle into the list
                    vehicleList.Add(vehicleModel);
                }
            }

            Task.Factory.StartNew(() =>
            {
                // Save all the vehicles
                Database.SaveAllVehicles(vehicleList);
            });
        }

        private void GeneratePlayerPayday(Client player)
        {
            int total = 0;
            int bank = player.GetSharedData(EntityData.PLAYER_BANK);
            int playerJob = player.GetData(EntityData.PLAYER_JOB);
            int playerRank = player.GetData(EntityData.PLAYER_RANK);
            int playerFaction = player.GetData(EntityData.PLAYER_FACTION);
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PAYDAY_TITLE);

            // Generate the salary
            if (playerFaction > 0 && playerFaction <= Constants.LAST_STATE_FACTION)
            {
                foreach (FactionModel faction in Constants.FACTION_RANK_LIST)
                {
                    if (faction.faction == playerFaction && faction.rank == playerRank)
                    {
                        total += faction.salary;
                        break;
                    }
                }
            }
            else
            {
                foreach (JobModel job in Constants.JOB_LIST)
                {
                    if (job.job == playerJob)
                    {
                        total += job.salary;
                        break;
                    }
                }
            }
            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_SALARY + total + "$");

            // Extra income from the level
            int levelEarnings = GetPlayerLevel(player) * Constants.PAID_PER_LEVEL;
            total += levelEarnings;
            if (levelEarnings > 0)
            {
                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_EXTRA_INCOME + levelEarnings + "$");
            }

            // Bank interest
            int bankInterest = (int)Math.Round(bank * 0.001);
            total += bankInterest;
            if (bankInterest > 0)
            {
                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_BANK_INTEREST + bankInterest + "$");
            }

            // Generación de impuestos por vehículos
            foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
            {
                VehicleHash vehicleHass = (VehicleHash)vehicle.Model;
                if (vehicle.GetData(EntityData.VEHICLE_OWNER) == player.Name && NAPI.Vehicle.GetVehicleClass(vehicleHass) != Constants.VEHICLE_CLASS_CYCLES)
                {
                    int vehicleTaxes = (int)Math.Round(vehicle.GetData(EntityData.VEHICLE_PRICE) * Constants.TAXES_VEHICLE);
                    int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                    string vehicleModel = vehicle.GetData(EntityData.VEHICLE_MODEL);
                    string vehiclePlate = vehicle.GetData(EntityData.VEHICLE_PLATE) == string.Empty ? "LS " + (1000 + vehicleId) : vehicle.GetData(EntityData.VEHICLE_PLATE);
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_TAXES_FROM + vehicleModel + " (" + vehiclePlate + "): -" + vehicleTaxes + "$");
                    total -= vehicleTaxes;
                }
            }

            // Vehicle taxes
            foreach (ParkedCarModel parkedCar in Parking.parkedCars)
            {
                VehicleHash vehicleHass = NAPI.Util.VehicleNameToModel(parkedCar.vehicle.model);
                if (parkedCar.vehicle.owner == player.Name && NAPI.Vehicle.GetVehicleClass(vehicleHass) != Constants.VEHICLE_CLASS_CYCLES)
                {
                    int vehicleTaxes = (int)Math.Round(parkedCar.vehicle.price * Constants.TAXES_VEHICLE);
                    string vehiclePlate = parkedCar.vehicle.plate == string.Empty ? "LS " + (1000 + parkedCar.vehicle.id) : parkedCar.vehicle.plate;
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_VEHICLE_TAXES_FROM + parkedCar.vehicle.model + " (" + vehiclePlate + "): -" + vehicleTaxes + "$");
                    total -= vehicleTaxes;
                }
            }

            // House taxes
            foreach (HouseModel house in House.houseList)
            {
                if (house.owner == player.Name)
                {
                    int houseTaxes = (int)Math.Round(house.price * Constants.TAXES_HOUSE);
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_HOUSE_TAXES_FROM + house.name + ": -" + houseTaxes + "$");
                    total -= houseTaxes;
                }
            }

            // Calculate the total balance
            player.SendChatMessage(Constants.COLOR_HELP + "=====================");
            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_TOTAL + total + "$");
            player.SetSharedData(EntityData.PLAYER_BANK, bank + total);

            Task.Factory.StartNew(() =>
            {
                // Add the payment log
                Database.LogPayment("Payday", player.Name, "Payday", total);
            });
        }

        private Vector3 GetPlayerFastFoodDeliveryDestination()
        {
            Random random = new Random();
            int element = random.Next(House.houseList.Count);
            return House.houseList[element].position;
        }

        public static ItemModel GetItemModelFromId(int itemId)
        {
            ItemModel item = null;
            foreach (ItemModel itemModel in itemList)
            {
                if (itemModel.id == itemId)
                {
                    item = itemModel;
                    break;
                }
            }
            return item;
        }

        public static ItemModel GetPlayerItemModelFromHash(int playerId, string hash)
        {
            ItemModel itemModel = null;
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_PLAYER && item.ownerIdentifier == playerId && item.hash == hash)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }

        public static ItemModel GetClosestItem(Client player)
        {
            ItemModel itemModel = null;
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_GROUND && player.Position.DistanceTo(item.position) < 2.0f)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }

        public static ItemModel GetClosestItemWithHash(Client player, string hash)
        {
            ItemModel itemModel = null;
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_GROUND && item.hash == hash && player.Position.DistanceTo(item.position) < 2.0f)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }

        public static ItemModel GetItemInEntity(int entityId, string entity)
        {
            ItemModel item = null;
            foreach (ItemModel itemModel in itemList)
            {
                if (itemModel.ownerEntity == entity && itemModel.ownerIdentifier == entityId)
                {
                    item = itemModel;
                    break;
                }
            }
            return item;
        }

        private void SubstractPlayerItems(ItemModel item, int amount = 1)
        {
            item.amount -= amount;
            if (item.amount == 0)
            {
                Task.Factory.StartNew(() =>
                {
                    // Remove the item from the database
                    Database.RemoveItem(item.id);
                    itemList.Remove(item);
                });
            }
        }

        private int GetPlayerInventoryTotal(Client player)
        {
            // Return the amount of items in the player's inventory
            return itemList.Count(item => item.ownerEntity == Constants.ITEM_ENTITY_PLAYER && player.GetData(EntityData.PLAYER_SQL_ID) == item.ownerIdentifier);
        }

        private List<InventoryModel> GetPlayerInventory(Client player)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_PLAYER && item.ownerIdentifier == playerId)
                {
                    BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);
                    if (businessItem != null && businessItem.type != Constants.ITEM_TYPE_WEAPON)
                    {
                        // Create the item into the inventory
                        InventoryModel inventoryItem = new InventoryModel();
                        inventoryItem.id = item.id;
                        inventoryItem.hash = item.hash;
                        inventoryItem.description = businessItem.description;
                        inventoryItem.type = businessItem.type;
                        inventoryItem.amount = item.amount;

                        // Add the item to the inventory
                        inventory.Add(inventoryItem);
                    }
                }
            }
            return inventory;
        }

        public static List<InventoryModel> GetPlayerInventoryAndWeapons(Client player)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_PLAYER && item.ownerIdentifier == playerId)
                {
                    BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);
                    if (businessItem != null)
                    {
                        // Create the item into the inventory
                        InventoryModel inventoryItem = new InventoryModel();
                        inventoryItem.id = item.id;
                        inventoryItem.hash = item.hash;
                        inventoryItem.description = businessItem.description;
                        inventoryItem.type = businessItem.type;
                        inventoryItem.amount = item.amount;

                        // Add the item to the inventory
                        inventory.Add(inventoryItem);
                    }
                }
            }
            return inventory;
        }

        public static List<InventoryModel> GetVehicleTrunkInventory(Vehicle vehicle)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_VEHICLE && item.ownerIdentifier == vehicleId)
                {
                    // Check whether is a common item or a weapon
                    InventoryModel inventoryItem = new InventoryModel();
                    BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

                    if (businessItem != null)
                    {
                        inventoryItem.description = businessItem.description;
                        inventoryItem.type = businessItem.type;
                    }
                    else
                    {
                        inventoryItem.description = item.hash;
                        inventoryItem.type = Constants.ITEM_TYPE_WEAPON;
                    }

                    // Update the values
                    inventoryItem.id = item.id;
                    inventoryItem.hash = item.hash;
                    inventoryItem.amount = item.amount;

                    // Add the item to the inventory
                    inventory.Add(inventoryItem);
                }
            }
            return inventory;
        }

        public static List<ClothesModel> GetPlayerClothes(int playerId)
        {
            // Get a list with the player's clothes
            return clothesList.Where(c => c.player == playerId).ToList();
        }

        public static ClothesModel GetDressedClothesInSlot(int playerId, int type, int slot)
        {
            // Get the clothes in the selected slot
            return clothesList.FirstOrDefault(c => c.player == playerId && c.type == type && c.slot == slot && c.dressed);
        }

        public static List<string> GetClothesNames(List<ClothesModel> clothesList)
        {
            List<string> clothesNames = new List<string>();
            foreach (ClothesModel clothes in clothesList)
            {
                foreach (BusinessClothesModel businessClothes in Constants.BUSINESS_CLOTHES_LIST)
                {
                    if (businessClothes.clothesId == clothes.drawable && businessClothes.bodyPart == clothes.slot && businessClothes.type == clothes.type)
                    {
                        clothesNames.Add(businessClothes.description);
                        break;
                    }
                }
            }
            return clothesNames;
        }

        public static void UndressClothes(int playerId, int type, int slot)
        {
            foreach (ClothesModel clothes in clothesList)
            {
                if (clothes.player == playerId && clothes.type == type && clothes.slot == slot && clothes.dressed)
                {
                    clothes.dressed = false;

                    Task.Factory.StartNew(() =>
                    {
                        // Update the clothes' state
                        Database.UpdateClothes(clothes);
                    });

                    break;
                }
            }
        }
        
        public static void GetPlayerBasicData(Client asker, Client player)
        {
            int rolePoints = player.GetData(EntityData.PLAYER_ROLE_POINTS);
            string sex = player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? Messages.GEN_SEX_MALE : Messages.GEN_SEX_FEMALE;
            string age = player.GetData(EntityData.PLAYER_AGE) + Messages.GEN_YEARS;
            string money = player.GetSharedData(EntityData.PLAYER_MONEY) + "$";
            string bank = player.GetSharedData(EntityData.PLAYER_BANK) + "$";
            string job = Messages.GEN_UNEMPLOYED;
            string faction = Messages.GEN_NO_FACTION;
            string rank = Messages.GEN_NO_RANK;
            string houses = string.Empty;
            string ownedVehicles = string.Empty;
            string lentVehicles = player.GetData(EntityData.PLAYER_VEHICLE_KEYS);
            TimeSpan played = TimeSpan.FromMinutes(player.GetData(EntityData.PLAYER_PLAYED));

            // Check if the player has a job
            foreach (JobModel jobModel in Constants.JOB_LIST)
            {
                if (player.GetData(EntityData.PLAYER_JOB) == jobModel.job)
                {
                    job = player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? jobModel.descriptionMale : jobModel.descriptionFemale;
                    break;
                }
            }

            // Check if the player is in any faction
            foreach (FactionModel factionModel in Constants.FACTION_RANK_LIST)
            {
                if (player.GetData(EntityData.PLAYER_FACTION) == factionModel.faction && player.GetData(EntityData.PLAYER_RANK) == factionModel.rank)
                {
                    switch (factionModel.faction)
                    {
                        case Constants.FACTION_POLICE:
                            faction = Messages.GEN_POLICE_FACTION;
                            break;
                        case Constants.FACTION_EMERGENCY:
                            faction = Messages.GEN_EMERGENCY_FACTION;
                            break;
                        case Constants.FACTION_NEWS:
                            faction = Messages.GEN_NEWS_FACTION;
                            break;
                        case Constants.FACTION_TOWNHALL:
                            faction = Messages.GEN_TOWNHALL_FACTION;
                            break;
                        case Constants.FACTION_TAXI_DRIVER:
                            faction = Messages.GEN_TRANSPORT_FACTION;
                            break;
                    }

                    // Set player's rank
                    rank = player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? factionModel.descriptionMale : factionModel.descriptionFemale;
                    break;
                }
            }

            // Check if the player has any rented house
            if (player.GetSharedData(EntityData.PLAYER_RENT_HOUSE) > 0)
            {
                houses += " " + player.GetSharedData(EntityData.PLAYER_RENT_HOUSE);
            }

            // Get player's owned houses
            foreach (HouseModel house in House.houseList)
            {
                if (house.owner == player.Name)
                {
                    houses += " " + house.id;
                }
            }

            // Check for the player's owned vehicles
            foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
            {
                if (vehicle.GetData(EntityData.VEHICLE_OWNER) == player.Name)
                {
                    ownedVehicles += " " + vehicle.GetData(EntityData.VEHICLE_ID);
                }
            }

            foreach (ParkedCarModel parkedVehicle in Parking.parkedCars)
            {
                if (parkedVehicle.vehicle.owner == player.Name)
                {
                    ownedVehicles += " " + parkedVehicle.vehicle.id;
                }
            }

            // Show all the information
            asker.SendChatMessage(Constants.COLOR_INFO + Messages.INF_BASIC_DATA);
            asker.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_NAME + player.Name + "; " + Messages.GEN_SEX + sex + "; " + Messages.GEN_AGE + age + "; " + Messages.GEN_MONEY + money + "; " + Messages.GEN_BANK + bank);
            asker.SendChatMessage(Constants.COLOR_INFO + " ");
            asker.SendChatMessage(Constants.COLOR_INFO + Messages.INF_JOB_DATA);
            asker.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_JOB + job + "; " + Messages.GEN_FACTION + faction + "; " + Messages.GEN_RANK + rank);
            asker.SendChatMessage(Constants.COLOR_INFO + " ");
            asker.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PROPERTIES);
            asker.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_HOUSES + houses);
            asker.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_OWNED_VEHICLES + ownedVehicles);
            asker.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_LENT_VEHICLES + lentVehicles);
            asker.SendChatMessage(Constants.COLOR_INFO + " ");
            asker.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ADDITIONAL_DATA);
            asker.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PLAYED_TIME + (int)played.TotalHours + "h " + played.Minutes + "m; " + Messages.GEN_ROLE_POINTS + rolePoints);
        }

        private int GetPlayerLevel(Client player)
        {
            float playedHours = player.GetData(EntityData.PLAYER_PLAYED) / 100;
            return (int)Math.Round(Math.Log(playedHours) * Constants.LEVEL_MULTIPLIER);
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            //NAPI.Native.SendNativeToPlayer(player, Hash.SET_PED_HELMET, player, false);
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            scoreList = new List<ScoreModel>();
            adminTicketList = new List<AdminTicketModel>();
            fastFoodOrderList = new List<FastFoodOrderModel>();

            // Area in the lobby to change the character
            NAPI.TextLabel.CreateTextLabel(Messages.GEN_CHARACTER_HELP, new Vector3(152.2911f, -1001.088f, -99f), 20.0f, 0.75f, 4, new Color(255, 255, 255), false);

            // Add car dealer's interior
            NAPI.World.RequestIpl("shr_int");
            NAPI.World.RequestIpl("shr_int_lod");
            NAPI.World.RemoveIpl("fakeint");
            NAPI.World.RemoveIpl("fakeint_lod");
            NAPI.World.RemoveIpl("fakeint_boards");
            NAPI.World.RemoveIpl("fakeint_boards_lod");
            NAPI.World.RemoveIpl("shutter_closed");

            // Add clubhouse's door
            NAPI.World.RequestIpl("hei_bi_hw1_13_door");

            // Avoid player's respawn
            NAPI.Server.SetAutoRespawnAfterDeath(false);
            NAPI.Server.SetAutoSpawnOnConnect(false);

            // Disable global server chat
            NAPI.Server.SetGlobalServerChat(false);

            foreach (InteriorModel interior in Constants.INTERIOR_LIST)
            {
                if (interior.blipId > 0)
                {
                    interior.blip = NAPI.Blip.CreateBlip(interior.entrancePosition);
                    interior.blip.Sprite = (uint)interior.blipId;
                    interior.blip.Name = interior.blipName;
                    interior.blip.ShortRange = true;
                }

                if (interior.captionMessage != string.Empty)
                {
                    interior.textLabel = NAPI.TextLabel.CreateTextLabel(interior.captionMessage, interior.entrancePosition, 20.0f, 0.75f, 4, new Color(255, 255, 255), false, 0);
                }
            }

            // Fastfood orders
            Random rnd = new Random();
            orderGenerationTime = GetTotalSeconds() + rnd.Next(0, 1) * 60;

            // Permanent timers
            playersCheckTimer = new Timer(UpdatePlayerList, null, 500, 500);
            minuteTimer = new Timer(OnMinuteSpent, null, 60000, 60000);
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (player.HasData(EntityData.PLAYER_PLAYING) == true)
            {
                // Disconnect from the server
                player.ResetData(EntityData.PLAYER_PLAYING);

                // Remove player from players list
                scoreList.RemoveAll(score => score.playerId == player.Value);

                // Remove opened ticket
                adminTicketList.RemoveAll(ticket => ticket.playerId == player.Value);

                // Other classes' disconnect function
                Chat.OnPlayerDisconnected(player, type, reason);
                DrivingSchool.OnPlayerDisconnected(player, type, reason);
                FastFood.OnPlayerDisconnected(player, type, reason);
                Fishing.OnPlayerDisconnected(player, type, reason);
                Garbage.OnPlayerDisconnected(player, type, reason);
                Hooker.OnPlayerDisconnected(player, type, reason);
                Police.OnPlayerDisconnected(player, type, reason);
                Thief.OnPlayerDisconnected(player, type, reason);
                Vehicles.OnPlayerDisconnected(player, type, reason);
                Weapons.OnPlayerDisconnected(player, type, reason);

                // Delete items in the hand
                if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
                {
                    int itemId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                    ItemModel item = GetItemModelFromId(itemId);
                    if (item != null && item.objectHandle != null && item.objectHandle.Exists)
                    {
                        item.objectHandle.Detach();
                        item.objectHandle.Delete();
                    }
                }

                PlayerModel character = new PlayerModel();

                character.position = player.Position;
                character.rotation = player.Rotation;
                character.health = player.Health;
                character.armor = player.Armor;
                character.id = player.GetData(EntityData.PLAYER_SQL_ID);
                character.phone = player.GetData(EntityData.PLAYER_PHONE);
                character.radio = player.GetData(EntityData.PLAYER_RADIO);
                character.killed = player.GetData(EntityData.PLAYER_KILLED);
                character.faction = player.GetData(EntityData.PLAYER_FACTION);
                character.job = player.GetData(EntityData.PLAYER_JOB);
                character.rank = player.GetData(EntityData.PLAYER_RANK);
                character.duty = player.GetData(EntityData.PLAYER_ON_DUTY);
                character.carKeys = player.GetData(EntityData.PLAYER_VEHICLE_KEYS);
                character.documentation = player.GetData(EntityData.PLAYER_DOCUMENTATION);
                character.licenses = player.GetData(EntityData.PLAYER_LICENSES);
                character.insurance = player.GetData(EntityData.PLAYER_MEDICAL_INSURANCE);
                character.weaponLicense = player.GetData(EntityData.PLAYER_WEAPON_LICENSE);
                character.houseRent = player.GetData(EntityData.PLAYER_RENT_HOUSE);
                character.houseEntered = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                character.businessEntered = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                character.employeeCooldown = player.GetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN);
                character.jobCooldown = player.GetData(EntityData.PLAYER_JOB_COOLDOWN);
                character.jobDeliver = player.GetData(EntityData.PLAYER_JOB_DELIVER);
                character.jobPoints = player.GetData(EntityData.PLAYER_JOB_POINTS);
                character.rolePoints = player.GetData(EntityData.PLAYER_ROLE_POINTS);
                character.played = player.GetData(EntityData.PLAYER_PLAYED);
                character.jailed = player.GetData(EntityData.PLAYER_JAIL_TYPE) + "," + player.GetData(EntityData.PLAYER_JAILED);

                character.money = player.GetSharedData(EntityData.PLAYER_MONEY);
                character.bank = player.GetSharedData(EntityData.PLAYER_BANK);

                // Warnt the players near to the disconnected one
                string message = string.Format(Messages.INF_PLAYER_DISCONNECTED, player.Name, reason);
                Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_DISCONNECT, 10.0f);

                Task.Factory.StartNew(() =>
                {
                    // Save player into database
                    Database.SaveCharacterInformation(character);
                });
            }
        }

        [RemoteEvent("checkPlayerEventKeyStopAnim")]
        public void CheckPlayerEventKeyStopAnimEvent(Client player)
        {
            if (!player.HasData(EntityData.PLAYER_ANIMATION) && player.GetData(EntityData.PLAYER_KILLED) == 0)
            {
                player.StopAnimation();
            }
        }

        [RemoteEvent("checkPlayerInventoryKey")]
        public void CheckPlayerInventoryKeyEvent(Client player)
        {
            if (GetPlayerInventoryTotal(player) > 0)
            {
                List<InventoryModel> inventory = GetPlayerInventory(player);
                player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_SELF);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_ITEMS_INVENTORY);
            }
        }

        [RemoteEvent("checkPlayerEventKey")]
        public void CheckPlayerEventKeyEvent(Client player)
        {
            if (player.HasData(EntityData.PLAYER_PLAYING) == true)
            {
                // Check if the player's close to an ATM
                for (int i = 0; i < Constants.ATM_LIST.Count; i++)
                {
                    if (player.Position.DistanceTo(Constants.ATM_LIST[i]) <= 1.5f)
                    {
                        player.TriggerEvent("showATM");
                        return;
                    }
                }

                // Check if the player's in any business
                foreach (BusinessModel business in Business.businessList)
                {
                    if (player.Position.DistanceTo(business.position) <= 1.5f && player.Dimension == business.dimension)
                    {
                        if (!Business.HasPlayerBusinessKeys(player, business) && business.locked)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_BUSINESS_LOCKED);
                        }
                        else
                        {
                            NAPI.World.RequestIpl(business.ipl);
                            player.Position = GetBusinessIplExit(business.ipl);
                            player.Dimension = Convert.ToUInt32(business.id);
                            player.SetData(EntityData.PLAYER_IPL, business.ipl);
                            player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, business.id);
                        }
                        return;
                    }
                    else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) == business.id)
                    {
                        Vector3 exitPosition = Business.GetBusinessExitPoint(business.ipl);
                        if (player.Position.DistanceTo(exitPosition) < 2.5f)
                        {
                            if (!Business.HasPlayerBusinessKeys(player, business) && business.locked)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_BUSINESS_LOCKED);
                            }
                            else if (player.HasData(EntityData.PLAYER_ROBBERY_START) == true)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_STEALING_PROGRESS);
                            }
                            else
                            {
                                player.Position = business.position;
                                player.Dimension = business.dimension;
                                player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                                player.ResetData(EntityData.PLAYER_IPL);

                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.HasData(EntityData.PLAYER_IPL) && target != player)
                                    {
                                        if (target.GetData(EntityData.PLAYER_IPL) == business.ipl)
                                        {
                                            return;
                                        }
                                    }
                                }
                                NAPI.World.RemoveIpl(business.ipl);
                            }
                        }
                        return;
                    }
                }

                // Check if the player's in any house
                foreach (HouseModel house in House.houseList)
                {
                    if (player.Position.DistanceTo(house.position) <= 1.5f && player.Dimension == house.dimension)
                    {
                        if (!House.HasPlayerHouseKeys(player, house) && house.locked)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_LOCKED);
                        }
                        else
                        {
                            NAPI.World.RequestIpl(house.ipl);
                            player.Position = GetHouseIplExit(house.ipl);
                            player.Dimension = Convert.ToUInt32(house.id);
                            player.SetData(EntityData.PLAYER_IPL, house.ipl);
                            player.SetData(EntityData.PLAYER_HOUSE_ENTERED, house.id);
                        }
                        return;
                    }
                    else if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) == house.id)
                    {
                        Vector3 exitPosition = House.GetHouseExitPoint(house.ipl);
                        if (player.Position.DistanceTo(exitPosition) < 2.5f)
                        {
                            if (!House.HasPlayerHouseKeys(player, house) && house.locked)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_LOCKED);
                            }
                            else if (player.HasData(EntityData.PLAYER_ROBBERY_START) == true)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_STEALING_PROGRESS);
                            }
                            else
                            {
                                player.Position = house.position;
                                player.Dimension = house.dimension;
                                player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                                player.ResetData(EntityData.PLAYER_IPL);

                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.HasData(EntityData.PLAYER_IPL) && target != player)
                                    {
                                        if (target.GetData(EntityData.PLAYER_IPL) == house.ipl)
                                        {
                                            return;
                                        }
                                    }
                                }
                                NAPI.World.RemoveIpl(house.ipl);
                            }
                        }
                        return;
                    }
                }

                // Check if the player's in any interior
                foreach (InteriorModel interior in Constants.INTERIOR_LIST)
                {
                    if (player.Position.DistanceTo(interior.entrancePosition) < 1.5f)
                    {
                        NAPI.World.RequestIpl(interior.iplName);
                        player.Position = interior.exitPosition;
                        return;
                    }
                    else if (player.Position.DistanceTo(interior.exitPosition) < 1.5f)
                    {
                        player.Position = interior.entrancePosition;
                        return;
                    }
                }
            }
            else
            {
                Vector3 lobbyExit = new Vector3(151.3791f, -1007.905f, -99f);

                if (lobbyExit.DistanceTo(player.Position) < 1.25f)
                {
                    // Player must have a character selected
                    if (player.HasData(EntityData.PLAYER_SQL_ID) == false)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_CHARACTER_SELECTED);
                    }
                    else
                    {
                        int playerSqlId = player.GetData(EntityData.PLAYER_SQL_ID);
                        ItemModel rightHand = GetItemInEntity(playerSqlId, Constants.ITEM_ENTITY_RIGHT_HAND);
                        ItemModel leftHand = GetItemInEntity(playerSqlId, Constants.ITEM_ENTITY_LEFT_HAND);

                        // Give the weapons to the player
                        Weapons.GivePlayerWeaponItems(player);

                        if (rightHand != null)
                        {
                            BusinessItemModel businessItem = Business.GetBusinessItemFromHash(rightHand.hash);

                            if (businessItem == null || businessItem.type == Constants.ITEM_TYPE_WEAPON)
                            {
                                WeaponHash weapon = NAPI.Util.WeaponNameToModel(rightHand.hash);
                                player.GiveWeapon(weapon, rightHand.amount);
                            }
                            else
                            {
                                rightHand.objectHandle = NAPI.Object.CreateObject(uint.Parse(rightHand.hash), rightHand.position, new Vector3(0.0f, 0.0f, 0.0f), (byte)rightHand.dimension);
                                rightHand.objectHandle.AttachTo(player, "PH_R_Hand", businessItem.position, businessItem.rotation);
                                player.GiveWeapon(WeaponHash.Unarmed, 1);
                            }
                            player.SetData(EntityData.PLAYER_RIGHT_HAND, rightHand.id);
                        }

                        if (leftHand != null)
                        {
                            BusinessItemModel businessItem = Business.GetBusinessItemFromHash(leftHand.hash);
                            leftHand.objectHandle = NAPI.Object.CreateObject(uint.Parse(leftHand.hash), leftHand.position, new Vector3(0.0f, 0.0f, 0.0f), (byte)leftHand.dimension);
                            leftHand.objectHandle.AttachTo(player, "PH_L_Hand", businessItem.position, businessItem.rotation);
                            player.SetSharedData(EntityData.PLAYER_LEFT_HAND, leftHand.id);
                        }

                        // Calculate spawn dimension
                        if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0)
                        {
                            int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                            HouseModel house = House.GetHouseById(houseId);
                            player.Dimension = Convert.ToUInt32(house.id);
                            NAPI.World.RequestIpl(house.ipl);
                        }
                        else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                        {
                            int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                            BusinessModel business = Business.GetBusinessById(businessId);
                            player.Dimension = Convert.ToUInt32(business.id);
                            NAPI.World.RequestIpl(business.ipl);
                        }
                        else
                        {
                            player.Dimension = 0;
                        }

                        // Add player into connected list
                        ScoreModel scoreModel = new ScoreModel(player.Value, player.Name, player.Ping);
                        scoreList.Add(scoreModel);

                        // Spawn the player into the world
                        player.Name = player.GetData(EntityData.PLAYER_NAME);
                        player.Position = player.GetData(EntityData.PLAYER_SPAWN_POS);
                        player.Rotation = player.GetData(EntityData.PLAYER_SPAWN_ROT);
                        player.Health = player.GetData(EntityData.PLAYER_HEALTH);
                        player.Armor = player.GetData(EntityData.PLAYER_ARMOR);

                        if (player.GetData(EntityData.PLAYER_KILLED) != 0)
                        {
                            Vector3 deathPosition = null;
                            string deathPlace = string.Empty;
                            string deathHour = DateTime.Now.ToString("h:mm:ss tt");

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

                            // Creamos the report for the emergency department
                            FactionWarningModel factionWarning = new FactionWarningModel(Constants.FACTION_EMERGENCY, player.Value, deathPlace, deathPosition, -1, deathHour);
                            Faction.factionWarningList.Add(factionWarning);

                            string warnMessage = string.Format(Messages.INF_EMERGENCY_WARNING, Faction.factionWarningList.Count - 1);

                            foreach (Client target in NAPI.Pools.GetAllPlayers())
                            {
                                if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY && target.GetData(EntityData.PLAYER_ON_DUTY) == 0)
                                {
                                   target.SendChatMessage(Constants.COLOR_INFO + warnMessage);
                                }
                            }

                            player.Invincible =true;
                            player.SetData(EntityData.TIME_HOSPITAL_RESPAWN, GetTotalSeconds() + 240);
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_EMERGENCY_WARN);
                        }

                        // Toggle connection flag
                        player.SetData(EntityData.PLAYER_PLAYING, true);
                    }
                }
                else if (player.Position.DistanceTo(new Vector3(152.2911f, -1001.088f, -99f)) < 1.5f)
                {
                    Task.Factory.StartNew(() =>
                    {
                        // Show character menu
                        List<string> playerList = Database.GetAccountCharacters(player.SocialClubName);
                        player.TriggerEvent("showPlayerCharacters", NAPI.Util.ToJson(playerList));
                    });
                }
            }
        }

        [RemoteEvent("processMenuAction")]
        public void ProcessMenuActionEvent(Client player, int itemId, string action)
        {
            string message = string.Empty;
            ItemModel item = GetItemModelFromId(itemId);
            BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

            switch (action.ToLower())
            {
                case Messages.COM_CONSUME:
                    item.amount--;
                    message = string.Format(Messages.INF_PLAYER_INVENTORY_CONSUME, businessItem.description.ToLower());
                    player.SendChatMessage(Constants.COLOR_INFO + message);

                    // Check if it grows alcohol level
                    if (businessItem.alcoholLevel > 0)
                    {
                        float currentAlcohol = 0;
                        if (player.HasData(EntityData.PLAYER_DRUNK_LEVEL) == true)
                        {
                            currentAlcohol = player.GetData(EntityData.PLAYER_DRUNK_LEVEL);
                        }
                        player.SetData(EntityData.PLAYER_DRUNK_LEVEL, currentAlcohol + businessItem.alcoholLevel);

                        if (currentAlcohol + businessItem.alcoholLevel > Constants.WASTED_LEVEL)
                        {
                            player.SetSharedData(EntityData.PLAYER_WALKING_STYLE, "move_m@drunk@verydrunk");
                            NAPI.ClientEvent.TriggerClientEventForAll("changePlayerWalkingStyle", player.Handle, "move_m@drunk@verydrunk");
                        }
                    }

                    // Check if it changes the health
                    if (businessItem.health != 0)
                    {
                        player.Health += businessItem.health;
                    }

                    // Check if it was the last one remaining
                    if (item.amount == 0)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            // Remove the item from the database
                            Database.RemoveItem(item.id);
                            itemList.Remove(item);
                        });
                    }
                    else
                    {
                        Task.Factory.StartNew(() =>
                        {
                            // Update the item into the inventory
                            Database.UpdateItem(item);
                        });
                    }

                    // Update the inventory
                    List<InventoryModel> inventory = GetPlayerInventory(player);
                    player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_SELF);
                    break;
                case Messages.ARG_OPEN:
                    switch (item.hash)
                    {
                        case Constants.ITEM_HASH_PACK_BEER_AM:
                            ItemModel itemModel = GetPlayerItemModelFromHash(player.GetData(EntityData.PLAYER_SQL_ID), Constants.ITEM_HASH_BOTTLE_BEER_AM);
                            if (itemModel == null)
                            {
                                // Create the item
                                itemModel = new ItemModel();
                                itemModel.hash = Constants.ITEM_HASH_BOTTLE_BEER_AM;
                                itemModel.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                                itemModel.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);
                                itemModel.amount = Constants.ITEM_OPEN_BEER_AMOUNT;
                                itemModel.position = new Vector3(0.0f, 0.0f, 0.0f);
                                itemModel.dimension = player.Dimension;


                                Task.Factory.StartNew(() =>
                                {
                                    // Create the new item
                                    itemModel.id = Database.AddNewItem(itemModel);
                                    itemList.Add(itemModel);
                                });
                            }
                            else
                            {
                                // Add the amount to the current item
                                itemModel.amount += Constants.ITEM_OPEN_BEER_AMOUNT;


                                Task.Factory.StartNew(() =>
                                {
                                    // Update the amount into the database
                                    Database.UpdateItem(item);
                                });
                            }
                            break;
                    }

                    // Substract container amount
                    SubstractPlayerItems(item);

                    message = string.Format(Messages.INF_PLAYER_INVENTORY_OPEN, businessItem.description.ToLower());
                    player.SendChatMessage(Constants.COLOR_INFO + message);

                    // Update the inventory
                    inventory = GetPlayerInventory(player);
                    player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_SELF);
                    break;
                case Messages.ARG_EQUIP:
                    if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RIGHT_HAND_OCCUPIED);
                    }
                    else
                    {
                        // Set the item into the hand
                        item.ownerEntity = Constants.ITEM_ENTITY_RIGHT_HAND;
                        item.objectHandle = NAPI.Object.CreateObject(uint.Parse(item.hash), item.position, new Vector3(0.0f, 0.0f, 0.0f), (byte)player.Dimension);
                        item.objectHandle.AttachTo(player, "PH_R_Hand", businessItem.position, businessItem.rotation);
                        player.SetData(EntityData.PLAYER_RIGHT_HAND, itemId);

                        message = string.Format(Messages.INF_PLAYER_INVENTORY_EQUIP, businessItem.description.ToLower());
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    break;
                case Messages.COM_DROP:
                    item.amount--;

                    // Check if there are items of the same type near
                    ItemModel closestItem = GetClosestItemWithHash(player, item.hash);

                    if (closestItem != null)
                    {
                        closestItem.amount++;

                        Task.Factory.StartNew(() =>
                        {
                            // Update the item into the database
                            Database.UpdateItem(item);
                        });
                    }
                    else
                    {
                        closestItem = item.Copy();
                        closestItem.amount = 1;
                        closestItem.ownerEntity = Constants.ITEM_ENTITY_GROUND;
                        closestItem.dimension = player.Dimension;
                        closestItem.position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.8f);
                        closestItem.objectHandle = NAPI.Object.CreateObject(uint.Parse(closestItem.hash), closestItem.position, new Vector3(0.0f, 0.0f, 0.0f), (byte)closestItem.dimension);


                        Task.Factory.StartNew(() =>
                        {
                            // Create the new item
                            closestItem.id = Database.AddNewItem(closestItem);
                            itemList.Add(closestItem);
                        });
                    }

                    // Check if it was the last one
                    if (item.amount == 0)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            Database.RemoveItem(item.id);
                            itemList.Remove(item);
                        });
                    }
                    else
                    {
                        Task.Factory.StartNew(() =>
                        {
                            // Update the item into the database
                            Database.UpdateItem(item);
                        });
                    }

                    message = string.Format(Messages.INF_PLAYER_INVENTORY_DROP, businessItem.description.ToLower());
                    player.SendChatMessage(Constants.COLOR_INFO + message);

                    // Update the inventory
                    inventory = GetPlayerInventory(player);
                    player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_SELF);
                    break;
                case Messages.ARG_CONFISCATE:
                    Client target = player.GetData(EntityData.PLAYER_SEARCHED_TARGET);

                    // Transfer the item from the target to the player
                    item.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                    item.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);

                    Task.Factory.StartNew(() =>
                    {
                        // Update the amount into the database
                        Database.UpdateItem(item);
                    });

                    string playerMessage = string.Format(Messages.INF_POLICE_RETIRED_ITEMS_TO, target.Name);
                    string targetMessage = string.Format(Messages.INF_POLICE_RETIRED_ITEMS_FROM, player.Name);
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                   target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                    // Update the inventory
                    inventory = GetPlayerInventoryAndWeapons(target);
                    player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_PLAYER);
                    break;
                case Messages.ARG_STORE:
                    Vehicle targetVehicle = player.GetData(EntityData.PLAYER_OPENED_TRUNK);

                    // Transfer the item from the player to the vehicle
                    item.ownerEntity = Constants.ITEM_ENTITY_VEHICLE;
                    item.ownerIdentifier = targetVehicle.GetData(EntityData.VEHICLE_ID);

                    // Remove the weapon if it's a weapon
                    foreach (WeaponHash weapon in player.Weapons)
                    {
                        if (weapon.ToString() == item.hash)
                        {
                            player.RemoveWeapon(weapon);
                            break;
                        }
                    }

                    Task.Factory.StartNew(() =>
                    {
                        // Update the amount into the database
                        Database.UpdateItem(item);
                    });

                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_TRUNK_STORED_ITEMS);

                    // Update the inventory
                    inventory = GetPlayerInventoryAndWeapons(player);
                    player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_VEHICLE_PLAYER);
                    break;
                case Messages.ARG_WITHDRAW:
                    Vehicle sourceVehicle = player.GetData(EntityData.PLAYER_OPENED_TRUNK);

                    WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(item.hash);

                    if (weaponHash != 0)
                    {
                        // Give the weapon to the player
                        item.ownerEntity = Constants.ITEM_ENTITY_WHEEL;
                        player.GiveWeapon(weaponHash, 0);
                        player.SetWeaponAmmo(weaponHash, item.amount);
                    }
                    else
                    {
                        // Place the item into the inventory
                        item.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                    }

                    // Transfer the item from the vehicle to the player
                    item.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);

                    Task.Factory.StartNew(() =>
                    {
                        // Update the amount into the database
                        Database.UpdateItem(item);
                    });

                    Chat.SendMessageToNearbyPlayers(player, Messages.INF_TRUNK_ITEM_WITHDRAW, Constants.MESSAGE_ME, 20.0f);
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_TRUNK_WITHDRAW_ITEMS);

                    // Update the inventory
                    inventory = GetVehicleTrunkInventory(sourceVehicle);
                    player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_VEHICLE_TRUNK);
                    break;
            }
        }

        [RemoteEvent("closeVehicleTrunk")]
        public void CloseVehicleTrunkEvent(Client player)
        {
            Vehicle vehicle = player.GetData(EntityData.PLAYER_OPENED_TRUNK);
            vehicle.CloseDoor(Constants.VEHICLE_TRUNK);
            player.ResetData(EntityData.PLAYER_OPENED_TRUNK);
        }

        [RemoteEvent("getPlayerTattoos")]
        public void GetPlayerTattoosEvent(Client player, Client targetPlayer)
        {
            int targetId = targetPlayer.GetData(EntityData.PLAYER_SQL_ID);
            List<TattooModel> playerTattooList = tattooList.Where(t => t.player == targetId).ToList();
            player.TriggerEvent("updatePlayerTattoos", NAPI.Util.ToJson(playerTattooList), targetPlayer);
        }

        [Command(Messages.COM_STORE)]
        public void StoreCommand(Client player)
        {
            if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
            {
                int itemId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                ItemModel item = GetItemModelFromId(itemId);

                if (item.objectHandle.IsNull)
                {
                    player.GiveWeapon(WeaponHash.Unarmed, 1);
                }
                else
                {
                    item.objectHandle.Detach();
                    item.objectHandle.Delete();
                }

                item.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                player.ResetData(EntityData.PLAYER_RIGHT_HAND);

                Task.Factory.StartNew(() =>
                {
                    // Update the amount into the database
                    Database.UpdateItem(item);
                });
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RIGHT_HAND_EMPTY);
            }
        }

        [Command(Messages.COM_CONSUME)]
        public void ConsumeCommand(Client player)
        {
            if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
            {
                // Get the item in the right hand
                int itemId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                ItemModel item = GetItemModelFromId(itemId);
                BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

                // Check if it's consumable
                if (businessItem.type == Constants.ITEM_TYPE_CONSUMABLE)
                {
                    string message = string.Format(Messages.INF_PLAYER_INVENTORY_CONSUME, businessItem.description.ToLower());

                    item.amount--;

                    if (businessItem.health != 0)
                    {
                        player.Health += businessItem.health;
                    }

                    if (businessItem.alcoholLevel > 0)
                    {
                        float currentAlcohol = 0;
                        if (player.HasData(EntityData.PLAYER_DRUNK_LEVEL) == true)
                        {
                            currentAlcohol = player.GetData(EntityData.PLAYER_DRUNK_LEVEL);
                        }
                        player.SetData(EntityData.PLAYER_DRUNK_LEVEL, currentAlcohol + businessItem.alcoholLevel);

                        if (currentAlcohol + businessItem.alcoholLevel > Constants.WASTED_LEVEL)
                        {
                            player.SetSharedData(EntityData.PLAYER_WALKING_STYLE, "move_m@drunk@verydrunk");
                            NAPI.ClientEvent.TriggerClientEventForAll("changePlayerWalkingStyle", player.Handle, "move_m@drunk@verydrunk");
                        }
                    }

                    if (item.amount == 0)
                    {
                        player.ResetData(EntityData.PLAYER_RIGHT_HAND);
                        item.objectHandle.Detach();
                        item.objectHandle.Delete();

                        Task.Factory.StartNew(() =>
                        {
                            // Remove the item from the database
                            Database.UpdateItem(item);
                        });
                    }
                    else
                    {
                        Task.Factory.StartNew(() =>
                        {
                            // Update the amount into the database
                            Database.UpdateItem(item);
                        });
                    }

                    player.SendChatMessage(Constants.COLOR_INFO + message);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ITEM_NOT_CONSUMABLE);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RIGHT_HAND_EMPTY);
            }
        }

        [Command(Messages.COM_INVENTORY)]
        public void InventoryCommand(Client player)
        {
            if (GetPlayerInventoryTotal(player) > 0)
            {
                List<InventoryModel> inventory = GetPlayerInventory(player);
                player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_SELF);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_ITEMS_INVENTORY);
            }
        }

        [Command(Messages.COM_PURCHASE)]
        public void PurchaseCommand(Client player, int amount = 0)
        {
            // Check if the player is inside a business
            if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
            {
                int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                BusinessModel business = Business.GetBusinessById(businessId);
                int playerSex = player.GetData(EntityData.PLAYER_SEX);

                switch (business.type)
                {
                    case Constants.BUSINESS_TYPE_CLOTHES:
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ABOUT_COMPLEMENTS);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_FOR_AVOID_CLIPPING1);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_FOR_AVOID_CLIPPING2);
                        player.TriggerEvent("showClothesBusinessPurchaseMenu", business.name, business.multiplier);
                        break;
                    case Constants.BUSINESS_TYPE_BARBER_SHOP:
                        // Load the players skin model
                        string skinModel = NAPI.Util.ToJson(player.GetData(EntityData.PLAYER_SKIN_MODEL));
                        player.TriggerEvent("showHairdresserMenu", playerSex, skinModel, business.name);
                        break;
                    case Constants.BUSINESS_TYPE_TATTOO_SHOP:
                        int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

                        // Remove player's clothes
                        player.SetClothes(11, 15, 0);
                        player.SetClothes(3, 15, 0);
                        player.SetClothes(8, 15, 0);

                        if (playerSex == 0)
                        {
                            player.SetClothes(4, 61, 0);
                            player.SetClothes(6, 34, 0);
                        }
                        else
                        {
                            player.SetClothes(4, 15, 0);
                            player.SetClothes(6, 35, 0);
                        }

                        // Load tattoo list
                        List<TattooModel> tattooList = Globals.tattooList.Where(t => t.player == playerId).ToList();
                        player.TriggerEvent("showTattooMenu", player.GetData(EntityData.PLAYER_SEX), NAPI.Util.ToJson(tattooList), NAPI.Util.ToJson(Constants.TATTOO_LIST), business.name, business.multiplier);

                        break;
                    default:
                        List<BusinessItemModel> businessItems = Business.GetBusinessSoldItems(business.type);
                        player.TriggerEvent("showBusinessPurchaseMenu", NAPI.Util.ToJson(businessItems), business.name, business.multiplier);
                        break;
                }
            }
            else
            {
                // Get all the houses
                foreach (HouseModel house in House.houseList)
                {
                    if (player.Position.DistanceTo(house.position) <= 1.5f && player.Dimension == house.dimension)
                    {
                        House.BuyHouse(player, house);
                        return;
                    }
                }

                // Check if the player's in the scrapyard
                foreach (ParkingModel parking in Parking.parkingList)
                {
                    if (player.Position.DistanceTo(parking.position) < 2.5f && parking.type == Constants.PARKING_TYPE_SCRAPYARD)
                    {
                        if (amount > 0)
                        {
                            int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);
                            if (playerMoney >= amount)
                            {
                                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                                ItemModel item = GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                                if (item == null)
                                {
                                    item = new ItemModel();
                                    item.amount = amount;
                                    item.dimension = 0;
                                    item.position = new Vector3(0.0f, 0.0f, 0.0f);
                                    item.hash = Constants.ITEM_HASH_BUSINESS_PRODUCTS;
                                    item.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                                    item.ownerIdentifier = playerId;
                                    item.objectHandle = null;

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Add the item into the database
                                        item.id = Database.AddNewItem(item);
                                        itemList.Add(item);
                                    });
                                }
                                else
                                {
                                    item.amount += amount;

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Update the amount into the database
                                        Database.UpdateItem(item);
                                    });
                                }

                                player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - amount);

                                string message = string.Format(Messages.INF_PRODUCTS_BOUGHT, amount, amount);
                                player.SendChatMessage(Constants.COLOR_INFO + message);
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ENOUGH_MONEY);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_COMMAND_PURCHASE);
                        }
                        return;
                    }
                }
            }

        }

        [Command(Messages.COM_SELL, Messages.GEN_SELL_COMMAND, GreedyArg = true)]
        public void SellCommand(Client player, string args)
        {
            string[] arguments = args.Split(' ');
            int price = 0;
            int targetId = 0;
            int objectId = 0;
            Client target = null;
            string priceString = string.Empty;
            if (arguments.Length > 0)
            {
                switch (arguments[0].ToLower())
                {
                    case Messages.ARG_VEHICLE:
                        if (arguments.Length > 3)
                        {
                            if (int.TryParse(arguments[2], out targetId) == true)
                            {
                                target = GetPlayerById(targetId);
                                priceString = arguments[3];
                            }
                            else if (arguments.Length == 5)
                            {
                                target = NAPI.Player.GetPlayerFromName(arguments[2] + " " + arguments[3]);
                                priceString = arguments[4];
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_SELL_VEH_COMMAND);
                                return;
                            }

                            if (int.TryParse(priceString, out price) == true)
                            {
                                if (price > 0)
                                {
                                    if (int.TryParse(arguments[1], out objectId) == true)
                                    {
                                        Vehicle vehicle = Vehicles.GetVehicleById(objectId);

                                        if (vehicle == null)
                                        {
                                            VehicleModel vehModel = Vehicles.GetParkedVehicleById(objectId);

                                            if (vehModel != null)
                                            {
                                                if (vehModel.owner == player.Name)
                                                {
                                                    string playerString = string.Format(Messages.INF_VEHICLE_SELL, vehModel.model, target.Name, price);
                                                    string targetString = string.Format(Messages.INF_VEHICLE_SOLD, player.Name, vehModel.model, price);

                                                    target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                                                    target.SetData(EntityData.PLAYER_SELLING_PRICE, price);
                                                    target.SetData(EntityData.PLAYER_SELLING_HOUSE, objectId);

                                                    player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                                   target.SendChatMessage(Constants.COLOR_INFO + targetString);
                                                }
                                                else
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_VEH_OWNER);
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_NOT_EXISTS);
                                            }
                                        }
                                        else
                                        {
                                            foreach (Vehicle veh in NAPI.Pools.GetAllVehicles())
                                            {
                                                if (veh.GetData(EntityData.VEHICLE_ID) == objectId)
                                                {
                                                    if (vehicle.GetData(EntityData.VEHICLE_OWNER) == player.Name)
                                                    {
                                                        string vehicleModel = vehicle.GetData(EntityData.VEHICLE_MODEL);
                                                        string playerString = string.Format(Messages.INF_VEHICLE_SELL, vehicleModel, target.Name, price);
                                                        string targetString = string.Format(Messages.INF_VEHICLE_SOLD, player.Name, vehicleModel, price);

                                                        target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                                                        target.SetData(EntityData.PLAYER_SELLING_PRICE, price);
                                                        target.SetData(EntityData.PLAYER_SELLING_VEHICLE, objectId);

                                                        player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                                       target.SendChatMessage(Constants.COLOR_INFO + targetString);
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_VEH_OWNER);
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_SELL_VEH_COMMAND);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PRICE_POSITIVE);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_SELL_VEH_COMMAND);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_SELL_VEH_COMMAND);
                        }
                        break;
                    case Messages.ARG_HOUSE:
                        if (arguments.Length < 2)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.GEN_SELL_HOUSE_COMMAND);
                        }
                        else
                        {
                            if (int.TryParse(arguments[1], out objectId) == true)
                            {
                                HouseModel house = House.GetHouseById(objectId);
                                if (house != null)
                                {
                                    if (house.owner == player.Name)
                                    {
                                        foreach (Client rndPlayer in NAPI.Pools.GetAllPlayers())
                                        {
                                            if (rndPlayer.HasData(EntityData.PLAYER_PLAYING) && rndPlayer.GetData(EntityData.PLAYER_HOUSE_ENTERED) == house.id)
                                            {
                                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_OCCUPIED);
                                                return;
                                            }
                                        }
                                        if (arguments.Length == 2)
                                        {
                                            int sellValue = (int)Math.Round(house.price * 0.7);
                                            string playerString = string.Format(Messages.INF_HOUSE_SELL_STATE, sellValue);
                                            player.SetData(EntityData.PLAYER_SELLING_HOUSE_STATE, objectId);
                                            player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                        }
                                        else
                                        {
                                            if (int.TryParse(arguments[2], out targetId) == true)
                                            {
                                                target = GetPlayerById(targetId);
                                                priceString = arguments[3];
                                            }
                                            else if (arguments.Length == 5)
                                            {
                                                target = NAPI.Player.GetPlayerFromName(arguments[2] + " " + arguments[3]);
                                                priceString = arguments[4];
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_SELL_HOUSE_COMMAND);
                                                return;
                                            }

                                            if (int.TryParse(priceString, out price) == true)
                                            {
                                                if (price > 0)
                                                {
                                                    string playerString = string.Format(Messages.INF_HOUSE_SELL, target.Name, price);
                                                    string targetString = string.Format(Messages.INF_HOUSE_SOLD, player.Name, price);

                                                    target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                                                    target.SetData(EntityData.PLAYER_SELLING_PRICE, price);
                                                    target.SetData(EntityData.PLAYER_SELLING_HOUSE, objectId);

                                                    player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                                   target.SendChatMessage(Constants.COLOR_INFO + targetString);
                                                }
                                                else
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PRICE_POSITIVE);
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_SELL_VEH_COMMAND);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_HOUSE_OWNER);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_NOT_EXISTS);
                                }

                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.GEN_SELL_HOUSE_COMMAND);
                            }
                        }
                        break;
                    case Messages.ARG_WEAPON:
                        // Pending TODO
                        break;
                    case Messages.ARG_FISH:
                        if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                        {
                            int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                            BusinessModel business = Business.GetBusinessById(businessId);

                            if (business != null && business.type == Constants.BUSINESS_TYPE_FISHING)
                            {
                                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                                ItemModel fishModel = GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_FISH);

                                if (fishModel == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_FISH_SELLABLE);
                                }
                                else
                                {
                                    int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);
                                    int amount = (int)Math.Round(fishModel.amount * Constants.PRICE_FISH / 1000.0);

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Remove the item from the database
                                        Database.RemoveItem(fishModel.id);
                                        itemList.Remove(fishModel);
                                    });

                                    player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney + amount);

                                    string message = string.Format(Messages.INF_FISHING_WON, amount);
                                    player.SendChatMessage(Constants.COLOR_INFO + message);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_FISHING_BUSINESS);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_FISHING_BUSINESS);
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_SELL_COMMAND);
                        break;
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.GEN_SELL_COMMAND);
            }
        }

        [Command(Messages.COM_HELP)]
        public void HelpCommand(Client player)
        {
            player.SendChatMessage(Constants.COLOR_ERROR + "Command not implemented.");
            //player.TriggerEvent("helptext");
        }

        [Command(Messages.COM_WELCOME)]
        public void WelcomeCommand(Client player)
        {
            player.SendChatMessage(Constants.COLOR_ERROR + "Command not implemented.");
            //player.TriggerEvent("welcomeHelp");
        }

        [Command(Messages.COM_SHOW, Messages.GEN_SHOW_DOC_COMMAND)]
        public void ShowCommand(Client player, string targetString, string documentation)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                int currentLicense = 0;
                string message = string.Empty;
                string nameChar = player.GetData(EntityData.PLAYER_NAME);
                int age = player.GetData(EntityData.PLAYER_AGE);
                string sexDescription = player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? Messages.GEN_SEX_MALE : Messages.GEN_SEX_FEMALE;

                Client target = int.TryParse(targetString, out int targetId) ? GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                switch (documentation.ToLower())
                {
                    case Messages.ARG_LICENSES:
                        string licenseMessage = string.Empty;
                        string playerLicenses = player.GetData(EntityData.PLAYER_LICENSES);
                        string[] playerLicensesArray = playerLicenses.Split(',');

                        message = string.Format(Messages.INF_LICENSES_SHOW, target.Name);
                        Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, 20.0f);

                        foreach (string license in playerLicensesArray)
                        {
                            int currentLicenseStatus = int.Parse(license);
                            switch (currentLicense)
                            {
                                case Constants.LICENSE_CAR:
                                    switch (currentLicenseStatus)
                                    {
                                        case -1:
                                           target.SendChatMessage(Constants.COLOR_HELP + Messages.INF_CAR_LICENSE_NOT_AVAILABLE);
                                            break;
                                        case 0:
                                           target.SendChatMessage(Constants.COLOR_HELP + Messages.INF_CAR_LICENSE_PRACTICAL_PENDING);
                                            break;
                                        default:
                                            licenseMessage = string.Format(Messages.INF_CAR_LICENSE_POINTS, currentLicenseStatus);
                                           target.SendChatMessage(Constants.COLOR_HELP + licenseMessage);
                                            break;
                                    }
                                    break;
                                case Constants.LICENSE_MOTORCYCLE:
                                    switch (currentLicenseStatus)
                                    {
                                        case -1:
                                           target.SendChatMessage(Constants.COLOR_HELP + Messages.INF_MOTORCYCLE_LICENSE_NOT_AVAILABLE);
                                            break;
                                        case 0:
                                           target.SendChatMessage(Constants.COLOR_HELP + Messages.INF_MOTORCYCLE_LICENSE_PRACTICAL_PENDING);
                                            break;
                                        default:
                                            licenseMessage = string.Format(Messages.INF_MOTORCYCLE_LICENSE_POINTS, currentLicenseStatus);
                                           target.SendChatMessage(Constants.COLOR_HELP + licenseMessage);
                                            break;
                                    }
                                    break;
                                case Constants.LICENSE_TAXI:
                                    if (currentLicenseStatus == -1)
                                    {
                                       target.SendChatMessage(Constants.COLOR_HELP + Messages.INF_TAXI_LICENSE_NOT_AVAILABLE);
                                    }
                                    else
                                    {
                                       target.SendChatMessage(Constants.COLOR_HELP + Messages.INF_TAXI_LICENSE_UP_TO_DATE);
                                    }
                                    break;
                            }
                            currentLicense++;
                        }
                        break;
                    case Messages.ARG_INSURANCE:
                        int playerMedicalInsurance = player.GetData(EntityData.PLAYER_MEDICAL_INSURANCE);
                        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                        dateTime = dateTime.AddSeconds(playerMedicalInsurance);

                        if (playerMedicalInsurance > 0)
                        {
                            message = string.Format(Messages.INF_INSURANCE_SHOW, target.Name);
                            Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, 20.0f);

                           target.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_NAME + nameChar);
                           target.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_AGE + age);
                           target.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_SEX + sexDescription);
                           target.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_EXPIRY + dateTime.ToShortDateString());
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_MEDICAL_INSURANCE);
                        }

                        break;
                    case Messages.ARG_IDENTIFICATION:
                        int playerDocumentation = player.GetData(EntityData.PLAYER_DOCUMENTATION);
                        if (playerDocumentation > 0)
                        {
                            message = string.Format(Messages.INF_IDENTIFICATION_SHOW, target.Name);
                            Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, 20.0f);

                           target.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_NAME + nameChar);
                           target.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_AGE + age);
                           target.SendChatMessage(Constants.COLOR_INFO + Messages.GEN_SEX + sexDescription);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_UNDOCUMENTED);
                        }
                        break;
                }
            }
        }

        [Command(Messages.COM_PAY, Messages.GEN_PAY_COMMAND)]
        public void PayCommand(Client player, string targetString, int price)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);
                if (target == player)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOOKER_OFFERED_HIMSELF);
                }
                else
                {
                    target.SetData(EntityData.PLAYER_PAYMENT, player);
                    target.SetData(EntityData.JOB_OFFER_PRICE, price);

                    string playerMessage = string.Format(Messages.INF_PAYMENT_OFFER, price, target.Name);
                    string targetMessage = string.Format(Messages.INF_PAYMENT_RECEIVED, player.Name, price);
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                   target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                }
            }
        }

        [Command(Messages.COM_GIVE, Messages.GEN_GIVE_COMMAND)]
        public void GiveCommand(Client player, string targetString)
        {
            if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
            {
                Client target = int.TryParse(targetString, out int targetId) ? GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
                else if (player.Position.DistanceTo(target.Position) > 2.0f)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_TOO_FAR);
                }
                else if (target.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_TARGET_RIGHT_HAND_NOT_EMPTY);
                }
                else
                {
                    string playerMessage = string.Empty;
                    string targetMessage = string.Empty;

                    int itemId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                    ItemModel item = GetItemModelFromId(itemId);

                    // Check if it's a weapon
                    WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(item.hash);

                    if (weaponHash != 0)
                    {
                        target.GiveWeapon(weaponHash, 0);
                        target.SetWeaponAmmo(weaponHash, item.amount);
                        target.RemoveWeapon(weaponHash);

                        playerMessage = string.Format(Messages.INF_ITEM_GIVEN, item.hash.ToLower(), target.Name);
                        targetMessage = string.Format(Messages.INF_ITEM_RECEIVED, player.Name, item.hash.ToLower());
                    }
                    else
                    {
                        BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);
                        item.objectHandle.Detach();
                        item.objectHandle.AttachTo(target, "PH_R_Hand", businessItem.position, businessItem.rotation);

                        playerMessage = string.Format(Messages.INF_ITEM_GIVEN, businessItem.description.ToLower(), target.Name);
                        targetMessage = string.Format(Messages.INF_ITEM_RECEIVED, player.Name, businessItem.description.ToLower());
                    }

                    // Change item's owner
                    player.ResetData(EntityData.PLAYER_RIGHT_HAND);
                    target.SetData(EntityData.PLAYER_RIGHT_HAND, item.id);
                    item.ownerIdentifier = target.GetData(EntityData.PLAYER_SQL_ID);

                    Task.Factory.StartNew(() =>
                    {
                        // Update the amount into the database
                        Database.UpdateItem(item);
                    });

                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                   target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RIGHT_HAND_EMPTY);
            }
        }

        [Command(Messages.COM_CANCEL, Messages.GEN_GLOBALS_CANCEL_COMMAND)]
        public void CancelCommand(Client player, string cancel)
        {
            switch (cancel.ToLower())
            {
                case Messages.ARG_INTERVIEW:
                    if (player.HasData(EntityData.PLAYER_ON_AIR) == true)
                    {
                        player.ResetData(EntityData.PLAYER_ON_AIR);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ON_AIR_CANCELED);
                    }
                    break;
                case Messages.ARG_SERVICE:
                    if (player.HasData(EntityData.PLAYER_ALREADY_FUCKING) == false)
                    {
                        player.ResetData(EntityData.PLAYER_ALREADY_FUCKING);
                        player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                        player.ResetData(EntityData.HOOKER_TYPE_SERVICE);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_HOOKER_SERVICE_CANCELED);
                    }
                    break;
                case Messages.ARG_MONEY:
                    if (player.HasData(EntityData.PLAYER_PAYMENT) == true)
                    {
                        player.ResetData(EntityData.PLAYER_PAYMENT);
                        player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PAYMENT_CANCELED);
                    }
                    break;
                case Messages.ARG_ORDER:
                    if (player.HasData(EntityData.PLAYER_DELIVER_ORDER) == true)
                    {
                        player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                        player.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
                        player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                        player.ResetData(EntityData.PLAYER_JOB_WON);

                        // Remove the checkpoints
                        player.TriggerEvent("fastFoodDeliverFinished");

                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_DELIVERER_ORDER_CANCELED);
                    }
                    break;
                case Messages.ARG_REPAINT:
                    if (player.HasData(EntityData.PLAYER_REPAINT_VEHICLE) == true)
                    {
                        // Get the mechanic and the vehicle
                        Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                        Vehicle vehicle = player.GetData(EntityData.PLAYER_REPAINT_VEHICLE);

                        // Get old colors
                        int vehicleColorType = vehicle.GetData(EntityData.VEHICLE_COLOR_TYPE);
                        string primaryVehicleColor = vehicle.GetData(EntityData.VEHICLE_FIRST_COLOR);
                        string secondaryVehicleColor = vehicle.GetData(EntityData.VEHICLE_SECOND_COLOR);
                        int vehiclePearlescentColor = vehicle.GetData(EntityData.VEHICLE_PEARLESCENT_COLOR);

                        if (vehicleColorType == Constants.VEHICLE_COLOR_TYPE_PREDEFINED)
                        {
                            vehicle.PrimaryColor = int.Parse(primaryVehicleColor);
                            vehicle.SecondaryColor = int.Parse(secondaryVehicleColor);
                            vehicle.PearlescentColor = vehiclePearlescentColor;
                        }
                        else
                        {
                            string[] primaryColor = primaryVehicleColor.Split(',');
                            string[] secondaryColor = secondaryVehicleColor.Split(',');
                            vehicle.CustomPrimaryColor = new Color(int.Parse(primaryColor[0]), int.Parse(primaryColor[1]), int.Parse(primaryColor[2]));
                            vehicle.CustomSecondaryColor = new Color(int.Parse(secondaryColor[0]), int.Parse(secondaryColor[1]), int.Parse(secondaryColor[2]));
                        }

                        player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                        player.ResetData(EntityData.PLAYER_REPAINT_VEHICLE);
                        player.ResetData(EntityData.PLAYER_REPAINT_COLOR_TYPE);
                        player.ResetData(EntityData.PLAYER_REPAINT_FIRST_COLOR);
                        player.ResetData(EntityData.PLAYER_REPAINT_SECOND_COLOR);
                        player.ResetData(EntityData.JOB_OFFER_PRICE);

                        // Remove repaint window
                        target.TriggerEvent("closeRepaintWindow");

                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_REPAINT_CANCELED);
                    }
                    break;
                default:
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.GEN_GLOBALS_CANCEL_COMMAND);
                    break;
            }
        }

        [Command(Messages.COM_ACCEPT, Messages.GEN_GLOBALS_ACCEPT_COMMAND)]
        public void AcceptCommand(Client player, string accept)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                switch (accept.ToLower())
                {
                    case Messages.ARG_REPAIR:
                        if (player.HasData(EntityData.PLAYER_REPAIR_VEHICLE) == true)
                        {
                            Client mechanic = player.GetData(EntityData.PLAYER_JOB_PARTNER);

                            if (mechanic != null && mechanic.Position.DistanceTo(player.Position) < 5.0f)
                            {
                                int price = player.GetData(EntityData.JOB_OFFER_PRICE);
                                int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);

                                if (playerMoney >= price)
                                {
                                    // Get the vehicle to repair and the broken part
                                    string type = player.GetData(EntityData.PLAYER_REPAIR_TYPE);
                                    Vehicle vehicle = player.GetData(EntityData.PLAYER_REPAIR_VEHICLE);

                                    int mechanicId = mechanic.GetData(EntityData.PLAYER_SQL_ID);
                                    int mechanicMoney = mechanic.GetSharedData(EntityData.PLAYER_MONEY);
                                    ItemModel item = GetPlayerItemModelFromHash(mechanicId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                                    switch (type.ToLower())
                                    {
                                        case Messages.ARG_CHASSIS:
                                            vehicle.Repair();
                                            break;
                                        case Messages.ARG_DOORS:
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (vehicle.IsDoorBroken(i) == true)
                                                {
                                                    vehicle.FixDoor(i);
                                                }
                                            }
                                            break;
                                        case Messages.ARG_TYRES:
                                            for (int i = 0; i < 4; i++)
                                            {
                                                if (vehicle.IsTyrePopped(i) == true)
                                                {
                                                    vehicle.FixTyre(i);
                                                }
                                            }
                                            break;
                                        case Messages.ARG_WINDOWS:
                                            for (int i = 0; i < 4; i++)
                                            {
                                                if (vehicle.IsWindowBroken(i) == true)
                                                {
                                                    vehicle.FixWindow(i);
                                                }
                                            }
                                            break;
                                    }

                                    if (player != mechanic)
                                    {
                                        player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - price);
                                        mechanic.SetSharedData(EntityData.PLAYER_MONEY, mechanicMoney + price);
                                    }

                                    item.amount -= player.GetData(EntityData.JOB_OFFER_PRODUCTS);

                                    if (item.amount == 0)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            // Remove the item from the database
                                            Database.RemoveItem(item.id);
                                            itemList.Remove(item);
                                        });
                                    }
                                    else
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            // Update the amount into the database
                                            Database.UpdateItem(item);
                                        });
                                    }

                                    player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                                    player.ResetData(EntityData.PLAYER_REPAIR_VEHICLE);
                                    player.ResetData(EntityData.PLAYER_REPAIR_TYPE);
                                    player.ResetData(EntityData.JOB_OFFER_PRODUCTS);
                                    player.ResetData(EntityData.JOB_OFFER_PRICE);

                                    string playerMessage = string.Format(Messages.INF_VEHICLE_REPAIRED_BY, mechanic.Name, price);
                                    string mechanicMessage = string.Format(Messages.INF_VEHICLE_REPAIRED_BY, player.Name, price);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                    mechanic.SendChatMessage(Constants.COLOR_INFO + mechanicMessage);

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Save the log into the database
                                        Database.LogPayment(player.Name, mechanic.Name, Messages.COM_REPAIR, price);
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ENOUGH_MONEY);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_TOO_FAR);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_REPAIR_OFFERED);

                        }
                        break;
                    case Messages.ARG_REPAINT:
                        if (player.HasData(EntityData.PLAYER_REPAINT_VEHICLE) == true)
                        {
                            Client mechanic = player.GetData(EntityData.PLAYER_JOB_PARTNER);

                            if (mechanic != null && mechanic.Position.DistanceTo(player.Position) < 5.0f)
                            {
                                int price = player.GetData(EntityData.JOB_OFFER_PRICE);
                                int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);

                                if (playerMoney >= price)
                                {
                                    Vehicle vehicle = player.GetData(EntityData.PLAYER_REPAINT_VEHICLE);
                                    int colorType = player.GetData(EntityData.PLAYER_REPAINT_COLOR_TYPE);
                                    string firstColor = player.GetData(EntityData.PLAYER_REPAINT_FIRST_COLOR);
                                    string secondColor = player.GetData(EntityData.PLAYER_REPAINT_SECOND_COLOR);
                                    int pearlescentColor = player.GetData(EntityData.PLAYER_REPAINT_PEARLESCENT);

                                    int mechanicId = mechanic.GetData(EntityData.PLAYER_SQL_ID);
                                    int mechanicMoney = mechanic.GetSharedData(EntityData.PLAYER_MONEY);
                                    ItemModel item = GetPlayerItemModelFromHash(mechanicId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                                    // Repaint the vehicle
                                    vehicle.SetData(EntityData.VEHICLE_COLOR_TYPE, colorType);
                                    vehicle.SetData(EntityData.VEHICLE_FIRST_COLOR, firstColor);
                                    vehicle.SetData(EntityData.VEHICLE_SECOND_COLOR, secondColor);
                                    vehicle.SetData(EntityData.VEHICLE_PEARLESCENT_COLOR, pearlescentColor);

                                    // Update the vehicle's color
                                    VehicleModel vehicleModel = new VehicleModel();
                                    vehicleModel.id = vehicle.GetData(EntityData.VEHICLE_ID);
                                    vehicleModel.colorType = colorType;
                                    vehicleModel.firstColor = firstColor;
                                    vehicleModel.secondColor = secondColor;
                                    vehicleModel.pearlescent = pearlescentColor;

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Update the vehicle's color into the database
                                        Database.UpdateVehicleColor(vehicleModel);
                                    });

                                    if (player != mechanic)
                                    {
                                        player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - price);
                                        mechanic.SetSharedData(EntityData.PLAYER_MONEY, mechanicMoney + price);
                                    }

                                    item.amount -= player.GetData(EntityData.JOB_OFFER_PRODUCTS);

                                    if (item.amount == 0)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            // Remove the item from the database
                                            Database.RemoveItem(item.id);
                                            itemList.Remove(item);
                                        });
                                    }
                                    else
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            // Update the amount into the database
                                            Database.UpdateItem(item);
                                        });
                                    }

                                    player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                                    player.ResetData(EntityData.PLAYER_REPAINT_VEHICLE);
                                    player.ResetData(EntityData.PLAYER_REPAINT_COLOR_TYPE);
                                    player.ResetData(EntityData.PLAYER_REPAINT_FIRST_COLOR);
                                    player.ResetData(EntityData.PLAYER_REPAINT_SECOND_COLOR);
                                    player.ResetData(EntityData.JOB_OFFER_PRODUCTS);
                                    player.ResetData(EntityData.JOB_OFFER_PRICE);

                                    string playerMessage = string.Format(Messages.INF_VEHICLE_REPAINTED_BY, mechanic.Name, price);
                                    string mechanicMessage = string.Format(Messages.INF_VEHICLE_REPAINTED_TO, player.Name, price);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                    mechanic.SendChatMessage(Constants.COLOR_INFO + mechanicMessage);

                                    // Remove repaint menu
                                    mechanic.TriggerEvent("closeRepaintWindow");

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Save the log into the database
                                        Database.LogPayment(player.Name, mechanic.Name, Messages.COM_REPAINT, price);
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ENOUGH_MONEY);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_TOO_FAR);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_REPAINT_OFFERED);
                        }

                        break;
                    case Messages.ARG_SERVICE:

                        if (player.HasData(EntityData.HOOKER_TYPE_SERVICE) == false)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_SERVICE_OFFERED);
                        }
                        else  if (player.HasData(EntityData.PLAYER_ALREADY_FUCKING) == true)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ALREADY_FUCKING);
                        }
                        else if (player.VehicleSeat != (int)VehicleSeat.Driver)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_VEHICLE_DRIVING);
                        }
                        else
                        {
                            if (player.Vehicle.EngineStatus)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ENGINE_ON);
                            }
                            else
                            {
                                Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                                if (player.HasData(EntityData.HOOKER_TYPE_SERVICE) == true)
                                {
                                    int amount = player.GetData(EntityData.JOB_OFFER_PRICE);
                                    int money = player.GetSharedData(EntityData.PLAYER_MONEY);

                                    if (target.HasData(EntityData.PLAYER_PLAYING) == true)
                                    {
                                        if (amount > money)
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ENOUGH_MONEY);
                                        }
                                        else
                                        {
                                            int targetMoney = target.GetSharedData(EntityData.PLAYER_MONEY);
                                            player.SetSharedData(EntityData.PLAYER_MONEY, money - amount);
                                            target.SetSharedData(EntityData.PLAYER_MONEY, targetMoney + amount);

                                            string playerMessage = string.Format(Messages.INF_SERVICE_PAID, amount);
                                            string targetMessage = string.Format(Messages.INF_SERVICE_RECEIVED, amount);
                                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                           target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                                            player.SetData(EntityData.PLAYER_ANIMATION, target);
                                            player.SetData(EntityData.PLAYER_ALREADY_FUCKING, target);
                                            target.SetData(EntityData.PLAYER_ALREADY_FUCKING, player);

                                            // Reset the entity data
                                            player.ResetData(EntityData.JOB_OFFER_PRICE);
                                            player.ResetData(EntityData.PLAYER_JOB_PARTNER);

                                            // Check the type of the service
                                            if (player.GetData(EntityData.HOOKER_TYPE_SERVICE) == Constants.HOOKER_SERVICE_BASIC)
                                            {
                                                player.PlayAnimation("mini@prostitutes@sexlow_veh", "low_car_bj_loop_player", (int)Constants.AnimationFlags.Loop);
                                                target.PlayAnimation("mini@prostitutes@sexlow_veh", "low_car_bj_loop_female", (int)Constants.AnimationFlags.Loop);

                                                // Timer to finish the service
                                                Timer sexTimer = new Timer(Hooker.OnSexServiceTimer, player, 120000, Timeout.Infinite);
                                                Hooker.sexTimerList.Add(player.Value, sexTimer);
                                            }
                                            else
                                            {
                                                player.PlayAnimation("mini@prostitutes@sexlow_veh", "low_car_sex_loop_player", (int)Constants.AnimationFlags.Loop);
                                                target.PlayAnimation("mini@prostitutes@sexlow_veh", "low_car_sex_loop_female", (int)Constants.AnimationFlags.Loop);

                                                // Timer to finish the service
                                                Timer sexTimer = new Timer(Hooker.OnSexServiceTimer, player, 180000, Timeout.Infinite);
                                                Hooker.sexTimerList.Add(player.Value, sexTimer);
                                            }

                                            Task.Factory.StartNew(() =>
                                            {
                                                // Save the log into the database
                                                Database.LogPayment(player.Name, target.Name, Messages.GEN_HOOKER, amount);
                                            });
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                                    }
                                }
                            }
                        }
                        break;
                    case Messages.ARG_INTERVIEW:
                        if (!player.IsInVehicle)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_VEHICLE);
                        }
                        else
                        {
                            NetHandle vehicle = player.Vehicle;
                            if (player.VehicleSeat != (int)VehicleSeat.RightRear)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_IN_RIGHT_REAR);
                            }
                            else
                            {
                                Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                                player.SetData(EntityData.PLAYER_ON_AIR, true);
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ALREADY_ON_AIR);
                               target.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_INTERVIEW_ACCEPTED);
                            }
                        }
                        break;
                    case Messages.ARG_MONEY:
                        if (player.HasData(EntityData.PLAYER_PAYMENT) == true)
                        {
                            Client target = player.GetData(EntityData.PLAYER_PAYMENT);
                            int amount = player.GetData(EntityData.JOB_OFFER_PRICE);

                            if (target.HasData(EntityData.PLAYER_PLAYING) == true)
                            {
                                int money = target.GetSharedData(EntityData.PLAYER_MONEY);

                                if (amount > 0 && money >= amount)
                                {
                                    int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);
                                    player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney + amount);
                                    target.SetSharedData(EntityData.PLAYER_MONEY, money - amount);

                                    // Reset the entity data
                                    player.ResetData(EntityData.JOB_OFFER_PRICE);
                                    player.ResetData(EntityData.PLAYER_PAYMENT);

                                    // Send the messages to both players
                                    string playerMessage = string.Format(Messages.INF_PLAYER_PAID, target.Name, amount);
                                    string targetMessage = string.Format(Messages.INF_TARGET_PAID, amount, player.Name);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                   target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Save the logs into database
                                        Database.LogPayment(target.Name, player.Name, Messages.GEN_PAYMENT_PLAYERS, amount);
                                    });
                                }
                                else
                                {
                                   target.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ENOUGH_MONEY);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                            }
                        }
                        break;
                    case Messages.ARG_VEHICLE:
                        if (player.HasData(EntityData.PLAYER_SELLING_VEHICLE) == true)
                        {
                            Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                            int amount = player.GetData(EntityData.PLAYER_SELLING_PRICE);
                            int vehicleId = player.GetData(EntityData.PLAYER_SELLING_VEHICLE);

                            if (target.HasData(EntityData.PLAYER_PLAYING) == true)
                            {
                                int money = player.GetSharedData(EntityData.PLAYER_BANK);

                                if (money >= amount)
                                {
                                    string vehicleModel = string.Empty;
                                    Vehicle vehicle = Vehicles.GetVehicleById(vehicleId);

                                    if (vehicle == null)
                                    {
                                        VehicleModel vehModel = Vehicles.GetParkedVehicleById(vehicleId);
                                        vehModel.owner = player.Name;
                                        vehicleModel = vehModel.model;
                                    }
                                    else
                                    {
                                        vehicle.SetData(EntityData.VEHICLE_OWNER, player.Name);
                                        vehicleModel = vehicle.GetData(EntityData.VEHICLE_MODEL);
                                    }

                                    int targetMoney = target.GetSharedData(EntityData.PLAYER_BANK);
                                    player.SetSharedData(EntityData.PLAYER_BANK, money - amount);
                                    target.SetSharedData(EntityData.PLAYER_BANK, targetMoney + amount);

                                    player.ResetData(EntityData.PLAYER_SELLING_VEHICLE);
                                    player.ResetData(EntityData.PLAYER_SELLING_PRICE);

                                    string playerString = string.Format(Messages.INF_VEHICLE_BUY, target.Name, vehicleModel, amount);
                                    string targetString = string.Format(Messages.INF_VEHICLE_BOUGHT, player.Name, vehicleModel, amount);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                   target.SendChatMessage(Constants.COLOR_INFO + targetString);

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Save the logs into database
                                        Database.LogPayment(target.Name, player.Name, Messages.GEN_VEHICLE_SALE, amount);
                                    });
                                }
                                else
                                {
                                    string message = string.Format(Messages.ERR_CARSHOP_NO_MONEY, amount);
                                    player.SendChatMessage(Constants.COLOR_ERROR + message);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                            }
                        }
                        break;
                    case Messages.ARG_HOUSE:
                        if (player.HasData(EntityData.PLAYER_SELLING_HOUSE) == true)
                        {
                            Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                            int amount = player.GetData(EntityData.PLAYER_SELLING_PRICE);
                            int houseId = player.GetData(EntityData.PLAYER_SELLING_HOUSE);

                            if (target.HasData(EntityData.PLAYER_PLAYING) == true)
                            {
                                int money = player.GetSharedData(EntityData.PLAYER_BANK);

                                if (money >= amount)
                                {
                                    HouseModel house = House.GetHouseById(houseId);

                                    if (house.owner == target.Name)
                                    {
                                        house.owner = player.Name;
                                        house.tenants = 2;

                                        int targetMoney = target.GetSharedData(EntityData.PLAYER_BANK);
                                        player.SetSharedData(EntityData.PLAYER_BANK, money - amount);
                                        target.SetSharedData(EntityData.PLAYER_BANK, targetMoney + amount);

                                        player.ResetData(EntityData.PLAYER_SELLING_HOUSE);
                                        player.ResetData(EntityData.PLAYER_SELLING_PRICE);

                                        string playerString = string.Format(Messages.INF_HOUSE_BUYTO, target.Name, amount);
                                        string targetString = string.Format(Messages.INF_HOUSE_BOUGHT, player.Name, amount);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetString);

                                        Task.Factory.StartNew(() =>
                                        {
                                            // Update the house
                                            Database.KickTenantsOut(house.id);
                                            Database.UpdateHouse(house);

                                            // Log the payment into database
                                            Database.LogPayment(target.Name, player.Name, Messages.GEN_HOUSE_SALE, amount);
                                        });
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Messages.ERR_HOUSE_SELL_GENERIC);
                                       target.SendChatMessage(Messages.ERR_HOUSE_SELL_GENERIC);
                                    }
                                }
                                else
                                {
                                    string message = string.Format(Messages.ERR_CARSHOP_NO_MONEY, amount);
                                   target.SendChatMessage(Constants.COLOR_ERROR + message);
                                }
                            }
                        }
                        break;
                    case Messages.ARG_STATE_HOUSE:
                        if (player.HasData(EntityData.PLAYER_SELLING_HOUSE_STATE) == true)
                        {
                            HouseModel house = House.GetHouseById(player.GetData(EntityData.PLAYER_SELLING_HOUSE_STATE));
                            int amount = (int)Math.Round(house.price * Constants.HOUSE_SALE_STATE);

                            if (player.HasData(EntityData.PLAYER_PLAYING) == true)
                            {
                                if (house.owner == player.Name)
                                {
                                    house.locked = true;
                                    house.owner = string.Empty;
                                    house.status = Constants.HOUSE_STATE_BUYABLE;
                                    house.houseLabel.Text = House.GetHouseLabelText(house);
                                    NAPI.World.RemoveIpl(house.ipl);
                                    house.tenants = 2;

                                    int playerMoney = player.GetSharedData(EntityData.PLAYER_BANK);
                                    player.SetSharedData(EntityData.PLAYER_BANK, playerMoney + amount);

                                    player.SendChatMessage(Constants.COLOR_SUCCESS + string.Format(Messages.SUC_HOUSE_SOLD, amount));

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Update the house
                                        Database.KickTenantsOut(house.id);
                                        Database.UpdateHouse(house);

                                        // Log the payment into the database
                                        Database.LogPayment(player.Name, Messages.GEN_STATE, Messages.GEN_HOUSE_SALE, amount);
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Messages.ERR_HOUSE_SELL_GENERIC);
                                }
                            }
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.GEN_GLOBALS_ACCEPT_COMMAND);
                        break;
                }
            }
        }

        [Command(Messages.COM_PICK_UP)]
        public void PickUpCommand(Client player)
        {
            if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RIGHT_HAND_OCCUPIED);
            }
            else if (player.HasSharedData(EntityData.PLAYER_WEAPON_CRATE) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_BOTH_HAND_OCCUPIED);
            }
            else
            {
                ItemModel item = GetClosestItem(player);
                if (item != null)
                {
                    // Get the item on the ground
                    ItemModel playerItem = GetPlayerItemModelFromHash(player.Value, item.hash);

                    if (playerItem != null)
                    {
                        item.objectHandle.Delete();
                        playerItem.amount += item.amount;

                        Task.Factory.StartNew(() =>
                        {
                            Database.RemoveItem(item.id);
                            itemList.Remove(item);
                        });
                    }
                    else
                    {
                        playerItem = item;
                    }

                    // Get the new owner of the item
                    playerItem.ownerEntity = Constants.ITEM_ENTITY_RIGHT_HAND;
                    playerItem.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);

                    // Play the animation
                    player.PlayAnimation("random@domestic", "pickup_low", 0);

                    BusinessItemModel businessItem = Business.GetBusinessItemFromHash(playerItem.hash);
                    playerItem.objectHandle = NAPI.Object.CreateObject(uint.Parse(playerItem.hash), playerItem.position, new Vector3(0.0f, 0.0f, 0.0f), (byte)playerItem.dimension);
                    playerItem.objectHandle.AttachTo(player, "PH_R_Hand", businessItem.position, businessItem.rotation);
                    player.SetData(EntityData.PLAYER_RIGHT_HAND, playerItem.id);

                    Task.Factory.StartNew(() =>
                    {
                        // Update the item's owner
                        Database.UpdateItem(item);
                    });

                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_PICKED_ITEM);
                }
                else
                {
                    WeaponCrateModel weaponCrate = Weapons.GetClosestWeaponCrate(player);
                    if (weaponCrate != null)
                    {
                        int index = Weapons.weaponCrateList.IndexOf(weaponCrate);
                        weaponCrate.carriedEntity = Constants.ITEM_ENTITY_PLAYER;
                        weaponCrate.carriedIdentifier = player.Value;
                        player.PlayAnimation("anim@heists@box_carry@", "idle", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.OnlyAnimateUpperBody | Constants.AnimationFlags.AllowPlayerControl));
                        weaponCrate.crateObject.AttachTo(player, "PH_R_Hand", new Vector3(0.0f, -0.5f, -0.25f), new Vector3(0.0f, 0.0f, 0.0f));
                        player.SetSharedData(EntityData.PLAYER_WEAPON_CRATE, index);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_ITEMS_NEAR);
                    }
                }
            }
        }

        [Command(Messages.COM_DROP)]
        public void DropCommand(Client player)
        {
            if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
            {
                int itemId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                ItemModel item = GetItemModelFromId(itemId);
                BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

                item.amount--;
                Database.UpdateItem(item);

                ItemModel closestItem = GetClosestItemWithHash(player, item.hash);

                if (closestItem != null)
                {
                    closestItem.amount++;

                    Task.Factory.StartNew(() =>
                    {
                        // Update the closest item's amount
                        Database.UpdateItem(closestItem);
                    });
                }
                else
                {
                    closestItem = item.Copy();
                    closestItem.amount = 1;
                    closestItem.ownerEntity = Constants.ITEM_ENTITY_GROUND;
                    closestItem.dimension = player.Dimension;
                    closestItem.position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.8f);
                    closestItem.objectHandle = NAPI.Object.CreateObject(uint.Parse(closestItem.hash), closestItem.position, new Vector3(0.0f, 0.0f, 0.0f), (byte)closestItem.dimension);

                    Task.Factory.StartNew(() =>
                    {
                        // Create the new item
                        closestItem.id = Database.AddNewItem(closestItem);
                        itemList.Add(closestItem);
                    }); 
                }

                if (item.amount == 0)
                {
                    // Remove the item from the hand
                    item.objectHandle.Detach();
                    item.objectHandle.Delete();
                    player.ResetData(EntityData.PLAYER_RIGHT_HAND);

                    Task.Factory.StartNew(() =>
                    {
                        // Remove the item
                        Database.RemoveItem(item.id);
                        itemList.Remove(item);
                    });
                }
                else
                {
                    Task.Factory.StartNew(() =>
                    {
                        // Update the item's amount
                        Database.UpdateItem(item);
                    });
                }

                string message = string.Format(Messages.INF_PLAYER_INVENTORY_DROP, businessItem.description.ToLower());
                player.SendChatMessage(Constants.COLOR_INFO + message);
            }
            else if (player.HasSharedData(EntityData.PLAYER_WEAPON_CRATE) == true)
            {
                WeaponCrateModel weaponCrate = Weapons.GetPlayerCarriedWeaponCrate(player.Value);

                if (weaponCrate != null)
                {
                    weaponCrate.position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 1.0f);
                    weaponCrate.carriedEntity = string.Empty;
                    weaponCrate.carriedIdentifier = 0;

                    // Place the crate on the ground
                    weaponCrate.crateObject.Detach();
                    weaponCrate.crateObject.Position = weaponCrate.position;

                    string message = string.Format(Messages.INF_PLAYER_INVENTORY_DROP, Messages.GEN_WEAPON_CRATE);
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_RIGHT_HAND_EMPTY);
            }
        }

        [Command(Messages.COM_TICKET, Messages.GEN_HELP_REQUEST, GreedyArg = true)]
        public void TicketCommand(Client player, string message)
        {
            foreach (AdminTicketModel ticket in adminTicketList)
            {
                if (player.Value == ticket.playerId)
                {
                    ticket.question = message;
                    return;
                }
            }

            // Create a new ticket
            AdminTicketModel adminTicket = new AdminTicketModel();
            adminTicket.playerId = player.Value;
            adminTicket.question = message;
            adminTicketList.Add(adminTicket);

            // Send the message to the staff online
            foreach (Client target in NAPI.Pools.GetAllPlayers())
            {
                if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_ADMIN_RANK) > 0)
                {
                   target.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_NEW_ADMIN_TICKET);
                }
                else if (target == player)
                {
                    player.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_HELP_REQUEST_SENT);
                }
            }
        }

        [Command(Messages.COM_DOOR)]
        public void DoorCommand(Client player)
        {
            // Check if the player's in his house
            foreach (HouseModel house in House.houseList)
            {
                if ((player.Position.DistanceTo(house.position) <= 1.5f && player.Dimension == house.dimension) || player.GetData(EntityData.PLAYER_HOUSE_ENTERED) == house.id)
                {
                    if (House.HasPlayerHouseKeys(player, house) == false)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_HOUSE_OWNER);
                    }
                    else
                    {
                        house.locked = !house.locked;

                        Task.Factory.StartNew(() =>
                        {
                            // Update the house
                            Database.UpdateHouse(house);
                        });

                        player.SendChatMessage(house.locked ? Constants.COLOR_INFO + Messages.INF_HOUSE_LOCKED : Constants.COLOR_INFO + Messages.INF_HOUSE_OPENED);
                    }
                    return;
                }
            }

            // Check if the player's in his business
            foreach (BusinessModel business in Business.businessList)
            {
                if ((player.Position.DistanceTo(business.position) <= 1.5f && player.Dimension == business.dimension) || player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) == business.id)
                {
                    if (Business.HasPlayerBusinessKeys(player, business) == false)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_BUSINESS_OWNER);
                    }
                    else
                    {
                        business.locked = !business.locked;

                        Task.Factory.StartNew(() =>
                        {
                            // Update the business
                            Database.UpdateBusiness(business);
                        });

                        player.SendChatMessage(business.locked ? Constants.COLOR_INFO + Messages.INF_BUSINESS_LOCKED : Constants.COLOR_INFO + Messages.INF_BUSINESS_OPENED);
                    }
                    return;
                }
            }

            // He's not in any house or business
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_HOUSE_BUSINESS);
        }

        [Command(Messages.COM_COMPLEMENT, Messages.GEN_COMPLEMENT_COMMAND)]
        public void ComplementCommand(Client player, string type, string action)
        {
            ClothesModel clothes = null;
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

            if (action.ToLower() == Messages.ARG_WEAR || action.ToLower() == Messages.ARG_REMOVE)
            {
                switch (type.ToLower())
                {
                    case Messages.ARG_MASK:
                        clothes = GetDressedClothesInSlot(playerId, 0, Constants.CLOTHES_MASK);
                        if (action.ToLower() == Messages.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.CLOTHES_MASK && c.type == 0).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_MASK_BOUGHT);
                                }
                                else
                                {
                                    player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_MASK_EQUIPED);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_MASK_EQUIPED);
                            }
                            else
                            {
                                player.SetClothes(Constants.CLOTHES_MASK, 0, 0);
                                UndressClothes(playerId, 0, Constants.CLOTHES_MASK);
                            }
                        }
                        break;
                    case Messages.ARG_BAG:
                        clothes = GetDressedClothesInSlot(playerId, 0, Constants.CLOTHES_BAGS);
                        if (action.ToLower() == Messages.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.CLOTHES_BAGS && c.type == 0).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_BAG_BOUGHT);
                                }
                                else
                                {
                                    player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_BAG_EQUIPED);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_BAG_EQUIPED);
                            }
                            else
                            {
                                player.SetClothes(Constants.CLOTHES_BAGS, 0, 0);
                                UndressClothes(playerId, 0, Constants.CLOTHES_BAGS);
                            }
                        }
                        break;
                    case Messages.ARG_ACCESSORY:
                        clothes = GetDressedClothesInSlot(playerId, 0, Constants.CLOTHES_ACCESSORIES);
                        if (action.ToLower() == Messages.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.CLOTHES_ACCESSORIES && c.type == 0).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_ACCESSORY_BOUGHT);
                                }
                                else
                                {
                                    player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ACCESSORY_EQUIPED);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_ACCESSORY_EQUIPED);
                            }
                            else
                            {
                                player.SetClothes(Constants.CLOTHES_ACCESSORIES, 0, 0);
                                UndressClothes(playerId, 0, Constants.CLOTHES_ACCESSORIES);
                            }
                        }
                        break;
                    case Messages.ARG_HAT:
                        clothes = GetDressedClothesInSlot(playerId, 1, Constants.ACCESSORY_HATS);
                        if (action.ToLower() == Messages.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.ACCESSORY_HATS && c.type == 1).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_HAT_BOUGHT);
                                }
                                else
                                {
                                    player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HAT_EQUIPED);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_HAT_EQUIPED);
                            }
                            else
                            {
                                if (player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_FEMALE)
                                {
                                    player.SetAccessories(Constants.ACCESSORY_HATS, 57, 0);
                                }
                                else
                                {
                                    player.SetAccessories(Constants.ACCESSORY_HATS, 8, 0);
                                }
                                UndressClothes(playerId, 1, Constants.ACCESSORY_HATS);
                            }
                        }
                        break;
                    case Messages.ARG_GLASSES:
                        clothes = GetDressedClothesInSlot(playerId, 1, Constants.ACCESSORY_GLASSES);
                        if (action.ToLower() == Messages.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.ACCESSORY_GLASSES && c.type == 1).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_GLASSES_BOUGHT);
                                }
                                else
                                {
                                    player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_GLASSES_EQUIPED);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_GLASSES_EQUIPED);
                            }
                            else
                            {
                                if (player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_FEMALE)
                                {
                                    player.SetAccessories(Constants.ACCESSORY_GLASSES, 5, 0);
                                }
                                else
                                {
                                    player.SetAccessories(Constants.ACCESSORY_GLASSES, 0, 0);
                                }
                                UndressClothes(playerId, 1, Constants.ACCESSORY_GLASSES);
                            }
                        }
                        break;
                    case Messages.ARG_EARRINGS:
                        clothes = GetDressedClothesInSlot(playerId, 1, Constants.ACCESSORY_EARS);
                        if (action.ToLower() == Messages.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.ACCESSORY_EARS && c.type == 1).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_EAR_BOUGHT);
                                }
                                else
                                {
                                    player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_EAR_EQUIPED);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_EAR_EQUIPED);
                            }
                            else
                            {
                                if (player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_FEMALE)
                                {
                                    player.SetAccessories(Constants.ACCESSORY_EARS, 12, 0);
                                }
                                else
                                {
                                    player.SetAccessories(Constants.ACCESSORY_EARS, 3, 0);
                                }
                                UndressClothes(playerId, 1, Constants.ACCESSORY_EARS);
                            }
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_COMPLEMENT_COMMAND);
                        break;
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_COMPLEMENT_COMMAND);
            }
        }

        [Command(Messages.COM_PLAYER)]
        public void PlayerCommand(Client player)
        {
            // Get players basic data
            GetPlayerBasicData(player, player);
        }
    }
}
