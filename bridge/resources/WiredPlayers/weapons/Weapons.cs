using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace WiredPlayers.weapons
{
    public class Weapons : Script
    {
        private static Timer weaponTimer;
        private static List<Timer> vehicleWeaponTimer;
        public static List<WeaponCrateModel> weaponCrateList;

        public static void GivePlayerWeaponItems(Client player)
        {
            int itemId = 0;
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            foreach (ItemModel item in Globals.itemList)
            {
                if (!int.TryParse(item.hash, out itemId) && item.ownerIdentifier == playerId && item.ownerEntity == Constants.ITEM_ENTITY_WHEEL)
                {
                    WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(item.hash);
                    player.GiveWeapon(weaponHash, 0);
                    player.SetWeaponAmmo(weaponHash, item.amount);
                }
            }
        }

        public static void GivePlayerNewWeapon(Client player, WeaponHash weapon, int bullets, bool licensed)
        {
            // Create weapon model
            ItemModel weaponModel = new ItemModel();
            weaponModel.hash = weapon.ToString();
            weaponModel.amount = bullets;
            weaponModel.ownerEntity = Constants.ITEM_ENTITY_WHEEL;
            weaponModel.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);
            weaponModel.position = new Vector3(0.0f, 0.0f, 0.0f);
            weaponModel.dimension = 0;

            Task.Factory.StartNew(() =>
            {
                weaponModel.id = Database.AddNewItem(weaponModel);
                Globals.itemList.Add(weaponModel);
            });

            player.GiveWeapon(weapon, 0);
            player.SetWeaponAmmo(weapon, bullets);
            
            if (licensed)
            {
                Task.Factory.StartNew(() =>
                {
                    // We add the weapon as a registered into database
                    Database.AddLicensedWeapon(weaponModel.id, player.Name);
                });
            }
        }

        public static string GetGunAmmunitionType(WeaponHash weapon)
        {
            string type = string.Empty;
            foreach (GunModel gun in Constants.GUN_LIST)
            {
                if (weapon == gun.weapon)
                {
                    type = gun.ammunition;
                    break;
                }
            }
            return type;
        }

        public static int GetGunAmmunitionCapacity(WeaponHash weapon)
        {
            int amount = 0;
            foreach (GunModel gun in Constants.GUN_LIST)
            {
                if (weapon == gun.weapon)
                {
                    amount = gun.capacity;
                    break;
                }
            }
            return amount;
        }

        public static ItemModel GetEquippedWeaponItemModelByHash(int playerId, WeaponHash weapon)
        {
            ItemModel item = null;
            foreach (ItemModel itemModel in Globals.itemList)
            {
                if (itemModel.ownerIdentifier == playerId && (itemModel.ownerEntity == Constants.ITEM_ENTITY_WHEEL || itemModel.ownerEntity == Constants.ITEM_ENTITY_RIGHT_HAND) && weapon.ToString() == itemModel.hash)
                {
                    item = itemModel;
                    break;
                }
            }
            return item;
        }

        public static WeaponCrateModel GetClosestWeaponCrate(Client player, float distance = 1.5f)
        {
            WeaponCrateModel weaponCrate = null;
            foreach (WeaponCrateModel weaponCrateModel in weaponCrateList)
            {
                if (player.Position.DistanceTo(weaponCrateModel.position) < distance && weaponCrateModel.carriedEntity == string.Empty)
                {
                    weaponCrate = weaponCrateModel;
                    break;
                }
            }
            return weaponCrate;
        }

        public static WeaponCrateModel GetPlayerCarriedWeaponCrate(int playerId)
        {
            WeaponCrateModel weaponCrate = null;
            foreach (WeaponCrateModel weaponCrateModel in weaponCrateList)
            {
                if (weaponCrateModel.carriedEntity == Constants.ITEM_ENTITY_PLAYER && weaponCrateModel.carriedIdentifier == playerId)
                {
                    weaponCrate = weaponCrateModel;
                    break;
                }
            }
            return weaponCrate;
        }

        public static void WeaponsPrewarn()
        {
            // Send the warning message to all factions
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasData(EntityData.PLAYER_PLAYING) && player.GetData(EntityData.PLAYER_FACTION) > Constants.LAST_STATE_FACTION)
                {
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_WEAPON_PREWARN);
                }
            }

            // Timer for the next warning
            weaponTimer = new Timer(OnWeaponPrewarn, null, 600000, Timeout.Infinite);
        }

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            WeaponCrateModel weaponCrate = GetPlayerCarriedWeaponCrate(player.Value);

            if (weaponCrate != null)
            {
                weaponCrate.position = new Vector3(player.Position.X, player.Position.Y, player.Position.X - 1.0f);
                weaponCrate.carriedEntity = string.Empty;
                weaponCrate.carriedIdentifier = 0;

                // Place the crate on the floor
                weaponCrate.crateObject.Detach();
                weaponCrate.crateObject.Position = weaponCrate.position;
            }
        }

        private static List<Vector3> GetRandomWeaponSpawns(int spawnPosition)
        {
            Random random = new Random();
            List<Vector3> weaponSpawns = new List<Vector3>();
            List<CrateSpawnModel> cratesInSpawn = GetSpawnsInPosition(spawnPosition);

            while (weaponSpawns.Count < Constants.MAX_CRATES_SPAWN)
            {
                Vector3 crateSpawn = cratesInSpawn[random.Next(cratesInSpawn.Count)].position;
                if (weaponSpawns.Contains(crateSpawn) == false)
                {
                    weaponSpawns.Add(crateSpawn);
                }
            }
            return weaponSpawns;
        }

        private static List<CrateSpawnModel> GetSpawnsInPosition(int spawnPosition)
        {
            List<CrateSpawnModel> crateSpawnList = new List<CrateSpawnModel>();
            foreach (CrateSpawnModel crateSpawn in Constants.CRATE_SPAWN_LIST)
            {
                if (crateSpawn.spawnPoint == spawnPosition)
                {
                    crateSpawnList.Add(crateSpawn);
                }
            }
            return crateSpawnList;
        }

        private static CrateContentModel GetRandomCrateContent(int type, int chance)
        {
            CrateContentModel crateContent = new CrateContentModel();
            
            foreach (WeaponProbabilityModel weaponAmmo in Constants.WEAPON_CHANCE_LIST)
            {
                if (weaponAmmo.type == type && weaponAmmo.minChance <= chance && weaponAmmo.maxChance >= chance)
                {
                    crateContent.item = weaponAmmo.hash;
                    crateContent.amount = weaponAmmo.amount;
                    break;
                }
            }

            return crateContent;
        }

        private static void OnWeaponPrewarn(object unused)
        {
            weaponTimer.Dispose();

            int currentSpawn = 0;
            weaponCrateList = new List<WeaponCrateModel>();
            
            Random random = new Random();
            int spawnPosition = random.Next(Constants.MAX_WEAPON_SPAWNS);

            // Get crates' spawn points
            List<Vector3> weaponSpawns = GetRandomWeaponSpawns(spawnPosition);
            
            foreach (Vector3 spawn in weaponSpawns)
            {
                // Calculate weapon or ammunition crate
                int type = currentSpawn % 2;
                int chance = random.Next(type == 0 ? Constants.MAX_WEAPON_CHANCE : Constants.MAX_AMMO_CHANCE);
                CrateContentModel crateContent = GetRandomCrateContent(type, chance);

                // We create the crate
                WeaponCrateModel weaponCrate = new WeaponCrateModel();
                weaponCrate.contentItem = crateContent.item;
                weaponCrate.contentAmount = crateContent.amount;
                weaponCrate.position = spawn;
                weaponCrate.carriedEntity = string.Empty;
                weaponCrate.crateObject = NAPI.Object.CreateObject(481432069, spawn, new Vector3(0.0f, 0.0f, 0.0f), 0);
                
                weaponCrateList.Add(weaponCrate);                
                currentSpawn++;
            }

            // Warn all the factions about the place
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasData(EntityData.PLAYER_PLAYING) && player.GetData(EntityData.PLAYER_FACTION) > Constants.LAST_STATE_FACTION)
                {
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_WEAPON_SPAWN_ISLAND);
                }
            }

            // Timer to warn the police
            weaponTimer = new Timer(OnPoliceCalled, null, 240000, Timeout.Infinite);
        }

        private static void OnPoliceCalled(object unused)
        {
            weaponTimer.Dispose();

            // Send the warning message to all the police members
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasData(EntityData.PLAYER_PLAYING) && player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE)
                {
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_WEAPON_SPAWN_ISLAND);
                }
            }

            // Finish the event
            weaponTimer = new Timer(OnWeaponEventFinished, null, 3600000, Timeout.Infinite);
        }

        private static void OnVehicleUnpackWeapons(object vehicleObject)
        {
            Vehicle vehicle = (Vehicle)vehicleObject;
            int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
            
            foreach (WeaponCrateModel weaponCrate in weaponCrateList)
            {
                if (weaponCrate.carriedEntity == Constants.ITEM_ENTITY_VEHICLE && weaponCrate.carriedIdentifier == vehicleId)
                {
                    // Unpack the weapon in the crate
                    ItemModel item = new ItemModel();
                    item.hash = weaponCrate.contentItem;
                    item.amount = weaponCrate.contentAmount;
                    item.ownerEntity = Constants.ITEM_ENTITY_VEHICLE;
                    item.ownerIdentifier = vehicleId;

                    // Delete the crate
                    weaponCrate.carriedIdentifier = 0;
                    weaponCrate.carriedEntity = string.Empty;

                    Task.Factory.StartNew(() =>
                    {
                        item.id = Database.AddNewItem(item);
                        Globals.itemList.Add(item);
                    });
                }
            }

            // Warn driver about unpacked crates
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.GetData(EntityData.PLAYER_VEHICLE) == vehicle)
                {
                    player.ResetData(EntityData.PLAYER_VEHICLE);
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_WEAPONS_UNPACKED);
                    break;
                }
            }

            vehicle.ResetData(EntityData.VEHICLE_WEAPON_UNPACKING);
        }

        private static void OnWeaponEventFinished(object unused)
        {
            weaponTimer.Dispose();

            foreach (WeaponCrateModel crate in weaponCrateList)
            {
                if (crate.crateObject.Exists)
                {
                    crate.crateObject.Delete();
                }
            }

            // Destroy weapon crates
            weaponCrateList = new List<WeaponCrateModel>();
            weaponTimer = null;
        }

        private int GetVehicleWeaponCrates(int vehicleId)
        {
            int crates = 0;
            foreach (WeaponCrateModel weaponCrate in weaponCrateList)
            {
                if (weaponCrate.carriedEntity == Constants.ITEM_ENTITY_VEHICLE && weaponCrate.carriedIdentifier == vehicleId)
                {
                    crates++;
                }
            }
            return crates;
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            vehicleWeaponTimer = new List<Timer>();
            weaponCrateList = new List<WeaponCrateModel>();
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.HasData(EntityData.VEHICLE_ID) && player.VehicleSeat == (int)VehicleSeat.Driver)
            {
                int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                if (!vehicle.HasData(EntityData.VEHICLE_WEAPON_UNPACKING) && GetVehicleWeaponCrates(vehicleId) > 0)
                {
                    // Mark the delivery point
                    Vector3 weaponPosition = new Vector3(-2085.543f, 2600.857f, -0.4712417f);
                    Checkpoint weaponCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, weaponPosition, new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                    player.SetData(EntityData.PLAYER_JOB_COLSHAPE, weaponCheckpoint);
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_WEAPON_POSITION_MARK);
                    player.TriggerEvent("showWeaponCheckpoint", weaponPosition);
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (vehicle.HasData(EntityData.VEHICLE_ID) == true)
            {
                int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                if (player.HasData(EntityData.PLAYER_JOB_COLSHAPE) && GetVehicleWeaponCrates(vehicleId) > 0)
                {
                    player.TriggerEvent("deleteWeaponCheckpoint");
                }
            }
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Client player)
        {
            if (player.HasData(EntityData.PLAYER_JOB_COLSHAPE) == true)
            {
                if (checkpoint == player.GetData(EntityData.PLAYER_JOB_COLSHAPE) && player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    Vehicle vehicle = player.Vehicle;
                    int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                    if (GetVehicleWeaponCrates(vehicleId) > 0)
                    {
                        // Delete the checkpoint
                        Checkpoint weaponCheckpoint = player.GetData(EntityData.PLAYER_JOB_COLSHAPE);
                        player.ResetData(EntityData.PLAYER_JOB_COLSHAPE);
                        player.TriggerEvent("deleteWeaponCheckpoint");
                        weaponCheckpoint.Delete();

                        // Freeze the vehicle
                        vehicle.EngineStatus = false;
                        player.SetData(EntityData.PLAYER_VEHICLE, vehicle);
                        vehicle.SetData(EntityData.VEHICLE_WEAPON_UNPACKING, true);

                        vehicleWeaponTimer.Add(new Timer(OnVehicleUnpackWeapons, vehicle, 60000, Timeout.Infinite));
                        
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_WAIT_FOR_WEAPONS);
                    }
                }
            }
        }

        [ServerEvent(Event.PlayerWeaponSwitch)]
        public void OnPlayerWeaponSwitch(Client player, WeaponHash oldWeapon, WeaponHash newWeapon)
        {
            if (player.HasData(EntityData.PLAYER_PLAYING) == true)
            {
                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

                if (player.HasData(EntityData.PLAYER_RIGHT_HAND) == true)
                {
                    int itemId = player.GetData(EntityData.PLAYER_RIGHT_HAND);
                    ItemModel item = Globals.GetItemModelFromId(itemId);
                    if (int.TryParse(item.hash, out int itemHash) == true)
                    {
                        ItemModel weaponItem = GetEquippedWeaponItemModelByHash(playerId, newWeapon);
                        player.GiveWeapon(WeaponHash.Unarmed, 1);
                        return;
                    }
                }

                // Get previous and new weapon models
                ItemModel oldWeaponModel = GetEquippedWeaponItemModelByHash(playerId, oldWeapon);
                ItemModel currentWeaponModel = GetEquippedWeaponItemModelByHash(playerId, newWeapon);

                if (oldWeaponModel != null)
                {
                    // Unequip previous weapon
                    oldWeaponModel.ownerEntity = Constants.ITEM_ENTITY_WHEEL;

                    Task.Factory.StartNew(() =>
                    {
                        // Update the weapon into the database
                        Database.UpdateItem(oldWeaponModel);
                    });
                }

                if (currentWeaponModel != null)
                {
                    // Equip new weapon
                    currentWeaponModel.ownerEntity = Constants.ITEM_ENTITY_RIGHT_HAND;

                    Task.Factory.StartNew(() =>
                    {
                        // Update the weapon into the database
                        Database.UpdateItem(currentWeaponModel);
                    });
                }

                // Check if it's armed
                if (newWeapon == WeaponHash.Unarmed)
                {
                    player.ResetData(EntityData.PLAYER_RIGHT_HAND);
                }
                else
                {
                    player.SetData(EntityData.PLAYER_RIGHT_HAND, currentWeaponModel.id);
                }
            }
        }

        [RemoteEvent("reloadPlayerWeapon")]
        public void ReloadPlayerWeaponEvent(Client player)
        {
            WeaponHash weapon = player.CurrentWeapon;
            int maxCapacity = GetGunAmmunitionCapacity(weapon);
            int currentBullets = player.GetWeaponAmmo(weapon);
            if (currentBullets < maxCapacity)
            {
                string bulletType = GetGunAmmunitionType(weapon);
                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                ItemModel bulletItem = Globals.GetPlayerItemModelFromHash(playerId, bulletType);
                if (bulletItem != null)
                {
                    int bulletsLeft = maxCapacity - currentBullets;
                    if (bulletsLeft >= bulletItem.amount)
                    {
                        currentBullets += bulletItem.amount;

                        Task.Factory.StartNew(() =>
                        {
                            Database.RemoveItem(bulletItem.id);
                            Globals.itemList.Remove(bulletItem);
                        });
                    }
                    else
                    {
                        currentBullets += bulletsLeft;
                        bulletItem.amount -= bulletsLeft;

                        Task.Factory.StartNew(() =>
                        {
                            // Update the remaining bullets
                            Database.UpdateItem(bulletItem);
                        });
                    }

                    // Add ammunition to the weapon
                    ItemModel weaponItem = GetEquippedWeaponItemModelByHash(playerId, weapon);
                    weaponItem.amount = currentBullets;

                    Task.Factory.StartNew(() =>
                    {
                        // Update the bullets in the weapon
                        Database.UpdateItem(weaponItem);
                    });

                    // Reload the weapon
                    player.SetWeaponAmmo(weapon, currentBullets);
                    //NAPI.Native.SendNativeToPlayer(player, Hash.MAKE_PED_RELOAD, player);
                }
            }
        }

        [Command(Messages.COM_WEAPONS_EVENT)]
        public void WeaponsEventCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_S_GAME_MASTER)
            {
                if (weaponTimer == null)
                {
                    WeaponsPrewarn();
                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + Messages.ADM_WEAPON_EVENT_STARTED);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_WEAPON_EVENT_ON_COURSE);
                }
            }
        }
    }
}
