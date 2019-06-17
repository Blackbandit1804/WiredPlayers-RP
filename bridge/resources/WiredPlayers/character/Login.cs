using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.globals;
using System.Threading.Tasks;
using System;

namespace WiredPlayers.character
{
    public class Login : Script
    {
        private void InitializePlayerData(Client player)
        {
            Vector3 worldSpawn = new Vector3(200.6641f, -932.0939f, 30.68681f);
            Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
            player.Position = new Vector3(152.26, -1004.47, -99.00);
            player.Dimension = Convert.ToUInt32(player.Value);

            player.Health = 100;
            player.Armor = 0;

            // Clear weapons
            player.RemoveAllWeapons();

            // Initialize shared entity data
            player.SetData(EntityData.PLAYER_SEX, 0);
            player.SetSharedData(EntityData.PLAYER_MONEY, 0);
            player.SetSharedData(EntityData.PLAYER_BANK, 3500);

            // Initialize entity data
            player.SetData(EntityData.PLAYER_NAME, string.Empty);
            player.SetData(EntityData.PLAYER_SPAWN_POS, worldSpawn);
            player.SetData(EntityData.PLAYER_SPAWN_ROT, rotation);
            player.SetData(EntityData.PLAYER_ADMIN_NAME, string.Empty);
            player.SetData(EntityData.PLAYER_ADMIN_RANK, 0);
            player.SetData(EntityData.PLAYER_AGE, 18);
            player.SetData(EntityData.PLAYER_HEALTH, 100);
            player.SetData(EntityData.PLAYER_ARMOR, 0);
            player.SetData(EntityData.PLAYER_PHONE, 0);
            player.SetData(EntityData.PLAYER_RADIO, 0);
            player.SetData(EntityData.PLAYER_KILLED, 0);
            player.SetData(EntityData.PLAYER_JAILED, 0);
            player.SetData(EntityData.PLAYER_JAIL_TYPE, 0);
            player.SetData(EntityData.PLAYER_FACTION, 0);
            player.SetData(EntityData.PLAYER_JOB, 0);
            player.SetData(EntityData.PLAYER_RANK, 0);
            player.SetData(EntityData.PLAYER_ON_DUTY, 0);
            player.SetData(EntityData.PLAYER_RENT_HOUSE, 0);
            player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
            player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
            player.SetData(EntityData.PLAYER_DOCUMENTATION, 0);
            player.SetData(EntityData.PLAYER_VEHICLE_KEYS, "0,0,0,0,0");
            player.SetData(EntityData.PLAYER_JOB_POINTS, "0,0,0,0,0,0,0");
            player.SetData(EntityData.PLAYER_LICENSES, "-1,-1,-1");
            player.SetData(EntityData.PLAYER_ROLE_POINTS, 0);
            player.SetData(EntityData.PLAYER_MEDICAL_INSURANCE, 0);
            player.SetData(EntityData.PLAYER_WEAPON_LICENSE, 0);
            player.SetData(EntityData.PLAYER_JOB_COOLDOWN, 0);
            player.SetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN, 0);
            player.SetData(EntityData.PLAYER_JOB_DELIVER, 0);
            player.SetData(EntityData.PLAYER_PLAYED, 0);
            player.SetData(EntityData.PLAYER_STATUS, 0);
        }
        
        private void LoadCharacterData(Client player, PlayerModel character)
        {
            string[] jail = character.jailed.Split(',');

            player.SetSharedData(EntityData.PLAYER_MONEY, character.money);
            player.SetSharedData(EntityData.PLAYER_BANK, character.bank);
            player.SetData(EntityData.PLAYER_SEX, character.sex);

            player.SetData(EntityData.PLAYER_SQL_ID, character.id);
            player.SetData(EntityData.PLAYER_NAME, character.realName);
            player.SetData(EntityData.PLAYER_HEALTH, character.health);
            player.SetData(EntityData.PLAYER_ARMOR, character.armor);
            player.SetData(EntityData.PLAYER_AGE, character.age);
            player.SetData(EntityData.PLAYER_ADMIN_RANK, character.adminRank);
            player.SetData(EntityData.PLAYER_ADMIN_NAME, character.adminName);
            player.SetData(EntityData.PLAYER_SPAWN_POS, character.position);
            player.SetData(EntityData.PLAYER_SPAWN_ROT, character.rotation);
            player.SetData(EntityData.PLAYER_PHONE, character.phone);
            player.SetData(EntityData.PLAYER_RADIO, character.radio);
            player.SetData(EntityData.PLAYER_KILLED, character.killed);
            player.SetData(EntityData.PLAYER_JAIL_TYPE, int.Parse(jail[0]));
            player.SetData(EntityData.PLAYER_JAILED, int.Parse(jail[1]));
            player.SetData(EntityData.PLAYER_FACTION, character.faction);
            player.SetData(EntityData.PLAYER_JOB, character.job);
            player.SetData(EntityData.PLAYER_RANK, character.rank);
            player.SetData(EntityData.PLAYER_ON_DUTY, character.duty);
            player.SetData(EntityData.PLAYER_VEHICLE_KEYS, character.carKeys);
            player.SetData(EntityData.PLAYER_DOCUMENTATION, character.documentation);
            player.SetData(EntityData.PLAYER_LICENSES, character.licenses);
            player.SetData(EntityData.PLAYER_MEDICAL_INSURANCE, character.insurance);
            player.SetData(EntityData.PLAYER_WEAPON_LICENSE, character.weaponLicense);
            player.SetData(EntityData.PLAYER_RENT_HOUSE, character.houseRent);
            player.SetData(EntityData.PLAYER_HOUSE_ENTERED, character.houseEntered);
            player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, character.businessEntered);
            player.SetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN, character.employeeCooldown);
            player.SetData(EntityData.PLAYER_JOB_COOLDOWN, character.jobCooldown);
            player.SetData(EntityData.PLAYER_JOB_DELIVER, character.jobDeliver);
            player.SetData(EntityData.PLAYER_JOB_POINTS, character.jobPoints);
            player.SetData(EntityData.PLAYER_ROLE_POINTS, character.rolePoints);
            player.SetData(EntityData.PLAYER_PLAYED, character.played);
            player.SetData(EntityData.PLAYER_STATUS, character.status);
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {
            // Set the default skin and transparency
            NAPI.Player.SetPlayerSkin(player, PedHash.Strperf01SMM);
            player.Transparency = 255;

            // Initialize the player data
            InitializePlayerData(player);

            Task.Factory.StartNew(() =>
            {
                AccountModel account = Database.GetAccount(player.SocialClubName);

                switch (account.status)
                {
                    case -1:
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ACCOUNT_DISABLED);
                        player.Kick(Messages.INF_ACCOUNT_DISABLED);
                        break;
                    case 0:
                        // Show the register window
                        player.TriggerEvent("showRegisterWindow");
                        break;
                    default:
                        // Welcome message
                        string welcomeMessage = string.Format(Messages.GEN_WELCOME_MESSAGE, player.SocialClubName);
                        player.SendChatMessage(welcomeMessage);
                        player.SendChatMessage(Messages.GEN_WELCOME_HINT);
                        player.SendChatMessage(Messages.GEN_HELP_HINT);
                        player.SendChatMessage(Messages.GEN_TICKET_HINT);

                        if (account.lastCharacter > 0)
                        {
                            // Load selected character
                            PlayerModel character = Database.LoadCharacterInformationById(account.lastCharacter);
                            SkinModel skinModel = Database.GetCharacterSkin(account.lastCharacter);
                            
                            player.Name = character.realName;
                            player.SetData(EntityData.PLAYER_SKIN_MODEL, skinModel);
                            NAPI.Player.SetPlayerSkin(player, character.sex == 0 ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);

                            LoadCharacterData(player, character);
                            Customization.ApplyPlayerCustomization(player, skinModel, character.sex);
                            Customization.ApplyPlayerClothes(player);
                            Customization.ApplyPlayerTattoos(player);
                        }

                        // Activate the login window
                        player.SetSharedData(EntityData.SERVER_TIME, DateTime.Now.ToString("HH:mm:ss"));

                        break;
                }
            });
        }

        [RemoteEvent("loginAccount")]
        public void LoginAccountEvent(Client player, string password)
        {
            Task.Factory.StartNew(() =>
            {
                bool login = Database.LoginAccount(player.SocialClubName, password);
                player.TriggerEvent(login ? "clearLoginWindow" : "showLoginError");
            });
        }

        [RemoteEvent("registerAccount")]
        public void RegisterAccountEvent(Client player, string password)
        {
            Task.Factory.StartNew(() =>
            {
                Database.RegisterAccount(player.SocialClubName, password);
                player.TriggerEvent("clearRegisterWindow");
            });
        }

        [RemoteEvent("changeCharacterSex")]
        public void ChangeCharacterSexEvent(Client player, int sex)
        {
            // Set the model of the player
            NAPI.Player.SetPlayerSkin(player, sex == 0 ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);

            // Remove player's clothes
            player.SetClothes(11, 15, 0);
            player.SetClothes(3, 15, 0);
            player.SetClothes(8, 15, 0);

            // Save sex entity shared data
            player.SetData(EntityData.PLAYER_SEX, sex);
        }

        [RemoteEvent("createCharacter")]
        public void CreateCharacterEvent(Client player, string playerName, int playerAge, int playerSex, string skinJson)
        {
            PlayerModel playerModel = new PlayerModel();
            SkinModel skinModel = NAPI.Util.FromJson<SkinModel>(skinJson);

            playerModel.realName = playerName;
            playerModel.age = playerAge;
            playerModel.sex = playerSex;

            // Apply the skin to the character
            player.SetData(EntityData.PLAYER_SKIN_MODEL, skinModel);
            Customization.ApplyPlayerCustomization(player, skinModel, playerSex);

            Task.Factory.StartNew(() =>
            {
                int playerId = Database.CreateCharacter(player, playerModel, skinModel);

                if (playerId > 0)
                {
                    InitializePlayerData(player);
                    player.Transparency = 255;
                    player.SetData(EntityData.PLAYER_SQL_ID, playerId);
                    player.SetData(EntityData.PLAYER_NAME, playerName);
                    player.SetData(EntityData.PLAYER_AGE, playerAge);
                    player.SetData(EntityData.PLAYER_SEX, playerSex);
                    player.SetSharedData(EntityData.PLAYER_SPAWN_POS, new Vector3(200.6641f, -932.0939f, 30.6868f));
                    player.SetSharedData(EntityData.PLAYER_SPAWN_ROT, new Vector3(0.0f, 0.0f, 0.0f));

                    Database.UpdateLastCharacter(player.SocialClubName, playerId);

                    player.TriggerEvent("characterCreatedSuccessfully");
                }
            });
        }

        [RemoteEvent("setCharacterIntoCreator")]
        public void SetCharacterIntoCreatorEvent(Client player)
        {
            // Change player's skin
            NAPI.Player.SetPlayerSkin(player, PedHash.FreemodeMale01);

            // Remove clothes
            player.SetClothes(11, 15, 0);
            player.SetClothes(3, 15, 0);
            player.SetClothes(8, 15, 0);

            // Set player's position
            player.Transparency = 255;
            player.Rotation = new Vector3(0.0f, 0.0f, 180.0f);
            player.Position = new Vector3(152.3787f, -1000.644f, -99f);
        }

        [RemoteEvent("loadCharacter")]
        public void LoadCharacterEvent(Client player, string name)
        {
            Task.Factory.StartNew(() =>
            {
                PlayerModel playerModel = Database.LoadCharacterInformationByName(name);
                SkinModel skinModel = Database.GetCharacterSkin(playerModel.id);

                // Load player's model
                player.Name = playerModel.realName;
                player.SetData(EntityData.PLAYER_SKIN_MODEL, skinModel);
                NAPI.Player.SetPlayerSkin(player, playerModel.sex == 0 ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);

                LoadCharacterData(player, playerModel);
                Customization.ApplyPlayerCustomization(player, skinModel, playerModel.sex);
                Customization.ApplyPlayerClothes(player);
                Customization.ApplyPlayerTattoos(player);

                // Update last selected character
                Database.UpdateLastCharacter(player.SocialClubName, playerModel.id);
            });
        }
    }
}