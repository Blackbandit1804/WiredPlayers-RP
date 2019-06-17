using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.database;
using WiredPlayers.model;
using WiredPlayers.drivingschool;
using WiredPlayers.weapons;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.factions
{
    public class Police : Script
    {
        private static Timer reinforcesTimer;
        public static List<PoliceControlModel> policeControlList;

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (player.HasData(EntityData.PLAYER_HANDCUFFED) == true)
            {
                // Remove player's cuffs
                GTANetworkAPI.Object cuff = player.GetData(EntityData.PLAYER_HANDCUFFED);
                cuff.Detach();
                cuff.Delete();
            }
        }

        private List<string> GetDifferentPoliceControls()
        {
            List<string> policeControls = new List<string>();
            foreach (PoliceControlModel policeControl in policeControlList)
            {
                if (policeControls.Contains(policeControl.name) == false && policeControl.name != string.Empty)
                {
                    policeControls.Add(policeControl.name);
                }
            }
            return policeControls;
        }

        private void RemoveClosestPoliceControlItem(Client player, int hash)
        {
            foreach (PoliceControlModel policeControl in policeControlList)
            {
                if (policeControl.controlObject.Exists && policeControl.controlObject.Position.DistanceTo(player.Position) < 2.0f && policeControl.item == hash)
                {
                    policeControl.controlObject.Delete();
                    policeControl.controlObject = null;
                    break;
                }
            }
        }

        private void UpdateReinforcesRequests(object unused)
        {
            List<ReinforcesModel> policeReinforces = new List<ReinforcesModel>();
            List<Client> policeMembers = NAPI.Pools.GetAllPlayers().Where(x => x.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE).ToList();
            
            foreach (Client police in policeMembers)
            {
                if (police.HasData(EntityData.PLAYER_REINFORCES) == true)
                {
                    ReinforcesModel reinforces = new ReinforcesModel(police.Value, police.Position);
                    policeReinforces.Add(reinforces);
                }
            }
            
            string reinforcesJsonList = NAPI.Util.ToJson(policeReinforces);

            foreach (Client police in policeMembers)
            {
                if (police.HasData(EntityData.PLAYER_PLAYING) == true)
                {
                    // Update reinforces position for each policeman
                    police.TriggerEvent("updatePoliceReinforces", reinforcesJsonList);
                }
            }
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            // Initialize reinforces updater
            reinforcesTimer = new Timer(UpdateReinforcesRequests, null, 250, 250);
        }

        [RemoteEvent("applyCrimesToPlayer")]
        public void ApplyCrimesToPlayerEvent(Client player, string crimeJson)
        {
            int fine = 0, jail = 0;
            Client target = player.GetData(EntityData.PLAYER_INCRIMINATED_TARGET);
            List<CrimeModel> crimeList = NAPI.Util.FromJson<List<CrimeModel>>(crimeJson);

            // Calculate fine amount and jail time
            foreach (CrimeModel crime in crimeList)
            {
                fine += crime.fine;
                jail += crime.jail;
            }
            
            Random random = new Random();
            target.Position = Constants.JAIL_SPAWNS[random.Next(3)];
            player.SetData(EntityData.PLAYER_INCRIMINATED_TARGET, target);

            // Remove money and jail the player
            int money = target.GetSharedData(EntityData.PLAYER_MONEY);
            target.SetSharedData(EntityData.PLAYER_MONEY, money - fine);
            target.SetData(EntityData.PLAYER_JAIL_TYPE, Constants.JAIL_TYPE_IC);
            target.SetData(EntityData.PLAYER_JAILED, jail);
        }

        [RemoteEvent("policeControlSelected")]
        public void PoliceControlSelectedEvent(Client player, string policeControl)
        {
            if (player.GetSharedData(EntityData.PLAYER_POLICE_CONTROL) == Constants.ACTION_LOAD)
            {
                foreach (PoliceControlModel policeControlModel in policeControlList)
                {
                    if (!policeControlModel.controlObject.Exists && policeControlModel.name == policeControl)
                    {
                        policeControlModel.controlObject = NAPI.Object.CreateObject(policeControlModel.item, policeControlModel.position, policeControlModel.rotation);
                    }
                }
            }
            else if (player.GetSharedData(EntityData.PLAYER_POLICE_CONTROL) == Constants.ACTION_SAVE)
            {
                List<PoliceControlModel> copiedPoliceControlModels = new List<PoliceControlModel>();
                List<PoliceControlModel> deletedPoliceControlModels = new List<PoliceControlModel>();
                foreach (PoliceControlModel policeControlModel in policeControlList)
                {
                    if (policeControlModel.controlObject.Exists && policeControlModel.name != policeControl)
                    {
                        if (policeControlModel.name != string.Empty)
                        {
                            PoliceControlModel policeControlCopy = policeControlModel;
                            policeControlCopy.name = policeControl;

                            Task.Factory.StartNew(() =>
                            {
                                policeControlCopy.id = Database.AddPoliceControlItem(policeControlCopy);
                                copiedPoliceControlModels.Add(policeControlCopy);
                            });
                        }
                        else
                        {
                            policeControlModel.name = policeControl;

                            Task.Factory.StartNew(() =>
                            {
                                // Add the new element
                                policeControlModel.id = Database.AddPoliceControlItem(policeControlModel);
                            });
                        }
                    }
                    else if (!policeControlModel.controlObject.Exists && policeControlModel.name == policeControl)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            Database.DeletePoliceControlItem(policeControlModel.id);
                            deletedPoliceControlModels.Add(policeControlModel);
                        });
                    }
                }
                policeControlList.AddRange(copiedPoliceControlModels);
                policeControlList = policeControlList.Except(deletedPoliceControlModels).ToList();
            }
            else
            {
                foreach (PoliceControlModel policeControlModel in policeControlList)
                {
                    if (policeControlModel.controlObject.Exists && policeControlModel.name == policeControl)
                    {
                        policeControlModel.controlObject.Delete();
                    }
                }
                policeControlList.RemoveAll(control => control.name == policeControl);

                Task.Factory.StartNew(() =>
                {
                    // Delete the police control
                    Database.DeletePoliceControl(policeControl);
                });
            }
        }

        [RemoteEvent("updatePoliceControlName")]
        public void UpdatePoliceControlNameEvent(Client player, string policeControlSource, string policeControlTarget)
        {
            if (player.GetSharedData(EntityData.PLAYER_POLICE_CONTROL) == Constants.ACTION_SAVE)
            {
                List<PoliceControlModel> copiedPoliceControlModels = new List<PoliceControlModel>();
                List<PoliceControlModel> deletedPoliceControlModels = new List<PoliceControlModel>();
                foreach (PoliceControlModel policeControlModel in policeControlList)
                {
                    if (policeControlModel.controlObject.Exists && policeControlModel.name != policeControlTarget)
                    {
                        if (policeControlModel.name != string.Empty)
                        {
                            PoliceControlModel policeControlCopy = policeControlModel.Copy();
                            policeControlModel.controlObject = null;
                            policeControlCopy.name = policeControlTarget;

                            Task.Factory.StartNew(() =>
                            {
                                policeControlCopy.id = Database.AddPoliceControlItem(policeControlCopy);
                                copiedPoliceControlModels.Add(policeControlCopy);
                            });
                        }
                        else
                        {
                            policeControlModel.name = policeControlTarget;

                            Task.Factory.StartNew(() =>
                            {
                                // Add new element to the control
                                policeControlModel.id = Database.AddPoliceControlItem(policeControlModel);
                            });
                        }
                    }
                }
                policeControlList.AddRange(copiedPoliceControlModels);
            }
            else
            {
                policeControlList.Where(s => s.name == policeControlSource).ToList().ForEach(t => t.name = policeControlTarget);

                Task.Factory.StartNew(() =>
                {
                    // Rename the control
                    Database.RenamePoliceControl(policeControlSource, policeControlTarget);
                });
            }
        }

        [Command(Messages.COM_CHECK)]
        public void CheckCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
            else
            {
                Vehicle vehicle = Globals.GetClosestVehicle(player, 3.5f);
                if (vehicle == null)
                {
                    int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                    string checkTitle = string.Format(Messages.GEN_VEHICLE_CHECK_TITLE, vehicleId);
                    string model = vehicle.GetData(EntityData.VEHICLE_MODEL);
                    string plate = vehicle.GetData(EntityData.VEHICLE_PLATE);
                    string owner = vehicle.GetData(EntityData.VEHICLE_OWNER);
                    player.SendChatMessage(checkTitle);
                    player.SendChatMessage(Messages.GEN_VEHICLE_MODEL + model);
                    player.SendChatMessage(Messages.GEN_VEHICLE_PLATE + plate);
                    player.SendChatMessage(Messages.GEN_OWNER + owner);

                    string message = string.Format(Messages.INF_CHECK_VEHICLE_PLATE, player.Name, model);

                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if(player != target && player.Position.DistanceTo(target.Position) < 20.0f)
                        {
                           target.SendChatMessage(Constants.COLOR_CHAT_ME + message);
                        }
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_VEHICLES_NEAR);
                }
            }
        }

        [Command(Messages.COM_FRISK, Messages.GEN_FRISK_COMMAND)]
        public void FriskCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (target == player)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_SEARCHED_HIMSELF);
                    }
                    else
                    {
                        string message = string.Format(Messages.INF_PLAYER_FRISK, player.Name, target.Name);
                        List<InventoryModel> inventory = Globals.GetPlayerInventoryAndWeapons(target);
                        player.SetData(EntityData.PLAYER_SEARCHED_TARGET, target);

                        foreach (Client nearPlayer in NAPI.Pools.GetAllPlayers())
                        {
                            if (player != nearPlayer && player.Position.DistanceTo(nearPlayer.Position) < 20.0f)
                            {
                                nearPlayer.SendChatMessage(Constants.COLOR_CHAT_ME + message);
                            }
                        }

                        // Show target's inventory to the player
                        player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_PLAYER);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_INCRIMINATE, Messages.GEN_INCRIMINATE_COMMAND)]
        public void IncriminateCommand(Client player, string targetString)
        {
            if (player.HasData(EntityData.PLAYER_JAIL_AREA) == false)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_JAIL_AREA);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (target == player)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_INCRIMINATED_HIMSELF);
                    }
                    else
                    {
                        string crimeList = NAPI.Util.ToJson(Constants.CRIME_LIST);
                        player.SetData(EntityData.PLAYER_INCRIMINATED_TARGET, target);
                        player.TriggerEvent("showCrimesMenu", crimeList);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_FINE, Messages.GEN_FINE_COMMAND)]
        public void FineCommand(Client player, string name = "", string surname = "", string amount = "", string reason = "")
        {
            if (name == string.Empty)
            {
                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_FINE_COMMAND);
            }
            else if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
            else
            {
                Client target = null;

                if (int.TryParse(name, out int targetId) == true)
                {
                    target = Globals.GetPlayerById(targetId);
                    reason = amount;
                    amount = surname;
                }
                else
                {
                    target = NAPI.Player.GetPlayerFromName(name + " " + surname);
                }
                if (target != null && target.HasData(EntityData.PLAYER_PLAYING) == true)
                {
                    if (player.Position.DistanceTo(target.Position) > 2.5f)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_TOO_FAR);
                    }
                    else if (target == player)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_FINED_HIMSELF);
                    }
                    else
                    {
                        string playerMessage = string.Format(Messages.INF_FINE_GIVEN, target.Name);
                        string targetMessage = string.Format(Messages.INF_FINE_RECEIVED, player.Name);
                        FineModel fine = new FineModel();
                        fine.officer = player.Name;
                        fine.target = target.Name;
                        fine.amount = int.Parse(amount);
                        fine.reason = reason;
                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                        Task.Factory.StartNew(() =>
                        {
                            // Insert the fine into the database
                            Database.InsertFine(fine);
                        });
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_HANDCUFF, Messages.GEN_HANDCUFF_COMMAND)]
        public void HandcuffCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (player.Position.DistanceTo(target.Position) > 1.5f)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_TOO_FAR);
                    }
                    else if (target == player)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_HANDCUFFED_HIMSELF);
                    }
                    else if (target.HasData(EntityData.PLAYER_HANDCUFFED) == false)
                    {
                        string playerMessage = string.Format(Messages.INF_CUFFED, target.Name);
                        string targetMessage = string.Format(Messages.INF_CUFFED_BY, player.Name);
                        GTANetworkAPI.Object cuff = NAPI.Object.CreateObject(-1281059971, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f));
                        cuff.AttachTo(target, "IK_R_Hand", new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f));
                        target.PlayAnimation("mp_arresting", "idle", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.OnlyAnimateUpperBody | Constants.AnimationFlags.AllowPlayerControl));
                        player.SetData(EntityData.PLAYER_ANIMATION, true);
                        target.SetData(EntityData.PLAYER_HANDCUFFED, cuff);
                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                        target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                        // Disable some player movements
                        player.TriggerEvent("toggleHandcuffed", true);
                    }
                    else
                    {
                        GTANetworkAPI.Object cuff = target.GetData(EntityData.PLAYER_HANDCUFFED);

                        cuff.Detach();
                        cuff.Delete();

                        target.StopAnimation();
                        player.ResetData(EntityData.PLAYER_ANIMATION);
                        target.ResetData(EntityData.PLAYER_HANDCUFFED);

                        string playerMessage = string.Format(Messages.INF_UNCUFFED, target.Name);
                        string targetMessage = string.Format(Messages.INF_UNCUFFED_BY, player.Name);
                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                        target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                        // Enable previously disabled player movements
                        player.TriggerEvent("toggleHandcuffed", false);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                }
            }
        }

        [Command(Messages.COM_EQUIPMENT, Messages.GEN_EQUIPMENT_COMMAND, GreedyArg = true)]
        public void EquipmentCommand(Client player, string action, string type = "")
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.HasData(EntityData.PLAYER_IN_LSPD_ROOM_LOCKERS_AREA) == false)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_IN_ROOM_LOCKERS);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE)
            {
                switch (action.ToLower())
                {
                    case Messages.ARG_BASIC:
                        player.Armor = 100;
                        Weapons.GivePlayerNewWeapon(player, WeaponHash.Flashlight, 0, false);
                        Weapons.GivePlayerNewWeapon(player, WeaponHash.Nightstick, 0, true);
                        Weapons.GivePlayerNewWeapon(player, WeaponHash.StunGun, 0, true);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_EQUIP_BASIC_RECEIVED);
                        break;
                    case Messages.ARG_AMMUNITION:
                        if (player.GetData(EntityData.PLAYER_RANK) > 1)
                        {
                            WeaponHash[] playerWeaps = player.Weapons;
                            foreach (WeaponHash playerWeap in playerWeaps)
                            {
                                string ammunition = Weapons.GetGunAmmunitionType(playerWeap);
                                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                                ItemModel bulletItem = Globals.GetPlayerItemModelFromHash(playerId, ammunition);
                                if (bulletItem != null)
                                {
                                    switch (playerWeap)
                                    {
                                        case WeaponHash.CombatPistol:
                                            bulletItem.amount += Constants.STACK_PISTOL_CAPACITY;
                                            break;
                                        case WeaponHash.SMG:
                                            bulletItem.amount += Constants.STACK_MACHINEGUN_CAPACITY;
                                            break;
                                        case WeaponHash.CarbineRifle:
                                            bulletItem.amount += Constants.STACK_ASSAULTRIFLE_CAPACITY;
                                            break;
                                        case WeaponHash.PumpShotgun:
                                            bulletItem.amount += Constants.STACK_SHOTGUN_CAPACITY;
                                            break;
                                        case WeaponHash.SniperRifle:
                                            bulletItem.amount += Constants.STACK_SNIPERRIFLE_CAPACITY;
                                            break;
                                    }

                                    Task.Factory.StartNew(() =>
                                    {
                                        // Update the bullet's amount
                                        Database.UpdateItem(bulletItem);
                                    });
                                }
                                else
                                {
                                    bulletItem = new ItemModel();
                                    bulletItem.hash = ammunition;
                                    bulletItem.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                                    bulletItem.ownerIdentifier = playerId;
                                    bulletItem.amount = 30;
                                    bulletItem.position = new Vector3(0.0f, 0.0f, 0.0f);
                                    bulletItem.dimension = 0;

                                    Task.Factory.StartNew(() =>
                                    {
                                        bulletItem.id = Database.AddNewItem(bulletItem);
                                        Globals.itemList.Add(bulletItem);
                                    });
                                }
                            }
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_EQUIP_AMMO_RECEIVED);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ENOUGH_POLICE_RANK);
                        }
                        break;
                    case Messages.ARG_WEAPON:
                        if (player.GetData(EntityData.PLAYER_RANK) > 1)
                        {
                            WeaponHash selectedWeap = new WeaponHash();
                            switch (type.ToLower())
                            {
                                case Messages.ARG_PISTOL:
                                    selectedWeap = WeaponHash.CombatPistol;
                                    break;
                                case Messages.ARG_MACHINE_GUN:
                                    selectedWeap = WeaponHash.SMG;
                                    break;
                                case Messages.ARG_ASSAULT:
                                    selectedWeap = WeaponHash.CarbineRifle;
                                    break;
                                case Messages.ARG_SNIPER:
                                    selectedWeap = WeaponHash.SniperRifle;
                                    break;
                                case Messages.ARG_SHOTGUN:
                                    selectedWeap = WeaponHash.PumpShotgun;
                                    break;
                                default:
                                    selectedWeap = 0;
                                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_EQUIPMENT_WEAP_COMMAND);
                                    break;
                            }

                            if (selectedWeap != 0)
                            {
                                Weapons.GivePlayerNewWeapon(player, selectedWeap, 0, true);
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_EQUIP_WEAP_RECEIVED);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ENOUGH_POLICE_RANK);
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_EQUIPMENT_AMMO_COMMAND);
                        break;
                }
            }
        }

        [Command(Messages.COM_CONTROL, Messages.GEN_POLICE_CONTROL_COMMAND)]
        public void ControlCommand(Client player, string action)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
            else
            {
                List<string> policeControls = GetDifferentPoliceControls();
                switch (action.ToLower())
                {
                    case Messages.ARG_LOAD:
                        if (policeControls.Count > 0)
                        {
                            player.SetSharedData(EntityData.PLAYER_POLICE_CONTROL, Constants.ACTION_LOAD);
                            player.TriggerEvent("loadPoliceControlList", NAPI.Util.ToJson(policeControls));
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_POLICE_CONTROLS);
                        }
                        break;
                    case Messages.ARG_SAVE:
                        player.SetSharedData(EntityData.PLAYER_POLICE_CONTROL, Constants.ACTION_SAVE);
                        if (policeControls.Count > 0)
                        {
                            player.TriggerEvent("loadPoliceControlList", NAPI.Util.ToJson(policeControls));
                        }
                        else
                        {
                            player.TriggerEvent("showPoliceControlName");
                        }
                        break;
                    case Messages.ARG_RENAME:
                        if (policeControls.Count > 0)
                        {
                            player.SetSharedData(EntityData.PLAYER_POLICE_CONTROL, Constants.ACTION_RENAME);
                            player.TriggerEvent("loadPoliceControlList", NAPI.Util.ToJson(policeControls));
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_POLICE_CONTROLS);
                        }
                        break;
                    case Messages.ARG_REMOVE:
                        if (policeControls.Count > 0)
                        {
                            player.SetSharedData(EntityData.PLAYER_POLICE_CONTROL, Constants.ACTION_DELETE);
                            player.TriggerEvent("loadPoliceControlList", NAPI.Util.ToJson(policeControls));
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_POLICE_CONTROLS);
                        }
                        break;
                    case Messages.ARG_CLEAR:
                        foreach (PoliceControlModel policeControl in policeControlList)
                        {
                            if (policeControl.controlObject.Exists)
                            {
                                policeControl.controlObject.Delete();
                            }
                        }
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_POLICE_CONTROL_CLEARED);
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_POLICE_CONTROL_COMMAND);
                        break;
                }
            }
        }

        [Command(Messages.COM_PUT, Messages.GEN_POLICE_PUT_COMMAND)]
        public void PutCommand(Client player, string item)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else
            {
                PoliceControlModel policeControl = null;
                if (player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE)
                {
                    switch (item.ToLower())
                    {
                        case Messages.ARG_CONE:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_CONE, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_CONE, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Messages.ARG_BEACON:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_BEACON, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_BEACON, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Messages.ARG_BARRIER:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_BARRIER, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_BARRIER, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Messages.ARG_SPIKES:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_SPIKES, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_SPIKES, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_POLICE_PUT_COMMAND);
                            break;
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
                }
            }
        }

        [Command(Messages.COM_REMOVE, Messages.GEN_POLICE_REMOVE_COMMAND)]
        public void RemoveCommand(Client player, string item)
        {
            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
            else
            {
                switch (item.ToLower())
                {
                    case Messages.ARG_CONE:
                        RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_CONE);
                        break;
                    case Messages.ARG_BEACON:
                        RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_BEACON);
                        break;
                    case Messages.ARG_BARRIER:
                        RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_BARRIER);
                        break;
                    case Messages.ARG_SPIKES:
                        RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_SPIKES);
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_POLICE_REMOVE_COMMAND);
                        break;
                }
            }
        }

        [Command(Messages.COM_REINFORCES)]
        public void ReinforcesCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_ON_DUTY);
            }
            else if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else
            {
                // Get police department's members
                List<Client> policeMembers = NAPI.Pools.GetAllPlayers().Where(x => x.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE).ToList();

                if (player.HasData(EntityData.PLAYER_REINFORCES) == true)
                {
                    string targetMessage = string.Format(Messages.INF_TARGET_REINFORCES_CANCELED, player.Name);

                    foreach (Client target in policeMembers)
                    {
                        if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_ON_DUTY) == 1)
                        {
                            // Remove the blip from the map
                            target.TriggerEvent("reinforcesRemove", player.Value);
                            
                            if (player == target)
                            {
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_REINFORCES_CANCELED);
                            }
                            else
                            {
                               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                        }
                    }

                    // Remove player's reinforces
                    player.ResetData(EntityData.PLAYER_REINFORCES);
                }
                else
                {
                    string targetMessage = string.Format(Messages.INF_TARGET_REINFORCES_ASKED, player.Name);

                    foreach (Client target in policeMembers)
                    {
                        if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_ON_DUTY) == 1)
                        {
                            if (player == target)
                            {
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PLAYER_REINFORCES_ASKED);
                            }
                            else
                            {
                               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                        }
                    }

                    // Ask for reinforces
                    player.SetData(EntityData.PLAYER_REINFORCES, true);
                }
            }
        }

        [Command(Messages.COM_LICENSE, Messages.GEN_LICENSE_COMMAND, GreedyArg = true)]
        public void LicenseCommand(Client player, string args)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE && player.GetData(EntityData.PLAYER_RANK) == 6)
            {
                string[] arguments = args.Trim().Split(' ');
                if (arguments.Length == 3 || arguments.Length == 4)
                {
                    Client target = null;

                    // Get the target player
                    if (int.TryParse(arguments[2], out int targetId) && arguments.Length == 3)
                    {
                        target = Globals.GetPlayerById(targetId);
                    }
                    else
                    {
                        target = NAPI.Player.GetPlayerFromName(arguments[2] + arguments[3]);
                    }

                    // Check whether the target player is connected
                    if (target == null || target.HasData(EntityData.PLAYER_PLAYING) == false)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_FOUND);
                    }
                    else if (player.Position.DistanceTo(target.Position) > 2.5f)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_TOO_FAR);
                    }
                    else
                    {
                        string playerMessage = string.Empty;
                        string targetMessage = string.Empty;

                        switch (arguments[0].ToLower())
                        {
                            case Messages.ARG_GIVE:
                                switch (arguments[1].ToLower())
                                {
                                    case Messages.ARG_WEAPON:
                                        // Add one month to the license
                                        target.SetData(EntityData.PLAYER_WEAPON_LICENSE, Globals.GetTotalSeconds() + 2628000);
                                        
                                        playerMessage = string.Format(Messages.INF_WEAPON_LICENSE_GIVEN, target.Name);
                                        targetMessage = string.Format(Messages.INF_WEAPON_LICENSE_RECEIVED, player.Name);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        break;
                                    default:
                                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_LICENSE_COMMAND);
                                        break;
                                }
                                break;
                            case Messages.ARG_REMOVE:
                                switch (arguments[1].ToLower())
                                {
                                    case Messages.ARG_WEAPON:
                                        // Adjust the date to the current one
                                        target.SetData(EntityData.PLAYER_WEAPON_LICENSE, Globals.GetTotalSeconds());
                                        
                                        playerMessage = string.Format(Messages.INF_WEAPON_LICENSE_REMOVED, target.Name);
                                        targetMessage = string.Format(Messages.INF_WEAPON_LICENSE_LOST, player.Name);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        break;
                                    case Messages.ARG_CAR:
                                        // Remove car license
                                        DrivingSchool.SetPlayerLicense(target, Constants.LICENSE_CAR, -1);
                                        
                                        playerMessage = string.Format(Messages.INF_CAR_LICENSE_REMOVED, target.Name);
                                        targetMessage = string.Format(Messages.INF_CAR_LICENSE_LOST, player.Name);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        break;
                                    case Messages.ARG_MOTORCYCLE:
                                        // Remove motorcycle license
                                        DrivingSchool.SetPlayerLicense(target, Constants.LICENSE_MOTORCYCLE, -1);
                                        
                                        playerMessage = string.Format(Messages.INF_MOTO_LICENSE_REMOVED, target.Name);
                                        targetMessage = string.Format(Messages.INF_MOTO_LICENSE_LOST, player.Name);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_LICENSE_COMMAND);
                                break;
                        }
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_LICENSE_COMMAND);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_POLICE_CHIEF);
            }
        }

        [Command(Messages.COM_BREATHALYZER, Messages.GEN_ALCOHOLIMETER_COMMAND)]
        public void BreathalyzerCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE && player.GetData(EntityData.PLAYER_RANK) > 0)
            {
                float alcoholLevel = 0.0f;
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target.HasData(EntityData.PLAYER_DRUNK_LEVEL) == true)
                {
                    alcoholLevel = target.GetData(EntityData.PLAYER_DRUNK_LEVEL);
                }
                
                string playerMessage = string.Format(Messages.INF_ALCOHOLIMETER_TEST, target.Name, alcoholLevel);
                string targetMessage = string.Format(Messages.INF_ALCOHOLIMETER_RECEPTOR, player.Name, alcoholLevel);
                player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_POLICE_FACTION);
            }
        }
    }
}
