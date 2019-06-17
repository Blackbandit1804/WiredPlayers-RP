using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WiredPlayers.house
{
    public class House : Script
    {
        public static List<HouseModel> houseList;

        public void LoadDatabaseHouses()
        {
            houseList = Database.LoadAllHouses();
            foreach (HouseModel houseModel in houseList)
            {
                string houseLabelText = GetHouseLabelText(houseModel);
                houseModel.houseLabel = NAPI.TextLabel.CreateTextLabel(houseLabelText, houseModel.position, 20.0f, 0.75f, 4, new Color(255, 255, 255), false, houseModel.dimension);
            }
        }

        public static HouseModel GetHouseById(int id)
        {
            HouseModel house = null;
            foreach (HouseModel houseModel in houseList)
            {
                if (houseModel.id == id)
                {
                    house = houseModel;
                    break;
                }
            }
            return house;
        }

        public static HouseModel GetClosestHouse(Client player, float distance = 1.5f)
        {
            HouseModel house = null;
            foreach (HouseModel houseModel in houseList)
            {
                if (player.Position.DistanceTo(houseModel.position) < distance)
                {
                    house = houseModel;
                    distance = player.Position.DistanceTo(houseModel.position);
                }
            }
            return house;
        }

        public static Vector3 GetHouseExitPoint(string ipl)
        {
            Vector3 exit = null;
            foreach (HouseIplModel houseIpl in Constants.HOUSE_IPL_LIST)
            {
                if (houseIpl.ipl == ipl)
                {
                    exit = houseIpl.position;
                    break;
                }
            }
            return exit;
        }

        public static bool HasPlayerHouseKeys(Client player, HouseModel house)
        {
            return (player.Name == house.owner || player.GetData(EntityData.PLAYER_RENT_HOUSE) == house.id);
        }

        public static string GetHouseLabelText(HouseModel house)
        {
            string label = string.Empty;

            switch (house.status)
            {
                case Constants.HOUSE_STATE_NONE:
                    label = house.name + "\n" + Messages.GEN_STATE_OCCUPIED;
                    break;
                case Constants.HOUSE_STATE_RENTABLE:
                    label = house.name + "\n" + Messages.GEN_STATE_RENT + "\n" + house.rental + "$";
                    break;
                case Constants.HOUSE_STATE_BUYABLE:
                    label = house.name + "\n" + Messages.GEN_STATE_SALE + "\n" + house.price + "$";
                    break;
            }
            return label;
        }

        public static void BuyHouse(Client player, HouseModel house)
        {
            if (house.status == Constants.HOUSE_STATE_BUYABLE)
            {
                if (player.GetSharedData(EntityData.PLAYER_BANK) >= house.price)
                {
                    int bank = player.GetSharedData(EntityData.PLAYER_BANK) - house.price;
                    string message = string.Format(Messages.INF_HOUSE_BUY, house.name, house.price);
                    string labelText = GetHouseLabelText(house);
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                    player.SetSharedData(EntityData.PLAYER_BANK, bank);
                    house.status = Constants.HOUSE_STATE_NONE;
                    house.houseLabel.Text = GetHouseLabelText(house);
                    house.owner = player.Name;
                    house.locked = true;

                    Task.Factory.StartNew(() =>
                    {
                        // Update the house
                        Database.UpdateHouse(house);
                    });
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_NOT_MONEY);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_NOT_BUYABLE);
            }
        }

        [RemoteEvent("getPlayerPurchasedClothes")]
        public void GetPlayerPurchasedClothesEvent(Client player, int type, int slot)
        {
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            int sex = player.GetData(EntityData.PLAYER_SEX);

            List<ClothesModel> clothesList = Globals.GetPlayerClothes(playerId).Where(c => c.type == type && c.slot == slot).ToList();

            if(clothesList.Count > 0)
            {
                List<string> clothesNames = Globals.GetClothesNames(clothesList);

                // Show player's clothes
                player.TriggerEvent("showPlayerClothes", NAPI.Util.ToJson(clothesList), NAPI.Util.ToJson(clothesNames));
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_CLOTHES_IN_WARDROBE);
            }
        }

        [RemoteEvent("wardrobeClothesItemSelected")]
        public void WardrobeClothesItemSelectedEvent(Client player, int clothesId)
        {
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

            // Replace player clothes for the new ones
            foreach (ClothesModel clothes in Globals.clothesList)
            {
                if (clothes.id == clothesId)
                {
                    clothes.dressed = true;
                    if (clothes.type == 0)
                    {
                        player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                    }
                    else
                    {
                        player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                    }

                    Task.Factory.StartNew(() =>
                    {
                        // Update dressed clothes into database
                        Database.UpdateClothes(clothes);
                    });
                }
                else if (clothes.id != clothesId && clothes.dressed)
                {
                    clothes.dressed = false;

                    Task.Factory.StartNew(() =>
                    {
                        // Update dressed clothes into database
                        Database.UpdateClothes(clothes);
                    });
                }
            }
        }

        [Command(Messages.COM_RENTABLE, Messages.GEN_RENTABLE_COMMAND)]
        public void RentableCommand(Client player, int amount = 0)
        {
            if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_IN_HOUSE);
            }
            else
            {
                int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                HouseModel house = GetHouseById(houseId);
                if (house == null || house.owner != player.Name)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_HOUSE_OWNER);
                }
                else if (amount > 0)
                {
                    house.rental = amount;
                    house.status = Constants.HOUSE_STATE_RENTABLE;
                    house.tenants = 2;

                    // Update house's textlabel
                    house.houseLabel.Text = GetHouseLabelText(house);

                    Task.Factory.StartNew(() =>
                    {
                        // Update the house
                        Database.UpdateHouse(house);
                    });

                    // Message sent to the player
                    string message = string.Format(Messages.INF_HOUSE_STATE_RENT, amount);
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                }
                else if (house.status == Constants.HOUSE_STATE_RENTABLE)
                {
                    house.status = Constants.HOUSE_STATE_NONE;
                    house.tenants = 2;

                    // Update house's textlabel
                    house.houseLabel.Text = GetHouseLabelText(house);

                    Task.Factory.StartNew(() =>
                    {
                        // Update the house
                        Database.KickTenantsOut(house.id);
                        Database.UpdateHouse(house);
                    });

                    // Message sent to the player
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_HOUSE_RENT_CANCEL);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PRICE_POSITIVE);
                }
            }
        }

        [Command(Messages.COM_RENT)]
        public void RentCommand(Client player)
        {
            foreach (HouseModel house in houseList)
            {
                if (player.Position.DistanceTo(house.position) <= 1.5 && player.Dimension == house.dimension)
                {
                    if (player.GetData(EntityData.PLAYER_RENT_HOUSE) == 0)
                    {
                        if (house.status != Constants.HOUSE_STATE_RENTABLE)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_HOUSE_NOT_RENTABLE);
                        }
                        else if (player.GetSharedData(EntityData.PLAYER_MONEY) < house.rental)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_RENT_MONEY);
                        }
                        else
                        {
                            int money = player.GetSharedData(EntityData.PLAYER_MONEY) - house.rental;
                            string message = string.Format(Messages.INF_HOUSE_RENT, house.name, house.rental);

                            player.SetData(EntityData.PLAYER_RENT_HOUSE, house.id);
                            player.SetSharedData(EntityData.PLAYER_MONEY, money);
                            house.tenants--;

                            if (house.tenants == 0)
                            {
                                house.status = Constants.HOUSE_STATE_NONE;
                                house.houseLabel.Text = GetHouseLabelText(house);
                            }

                            Task.Factory.StartNew(() =>
                            {
                                // Update house's tenants
                                Database.UpdateHouse(house);
                            });

                            // Send the message to the player
                            player.SendChatMessage(Constants.COLOR_INFO + message);
                        }
                        break;
                    }
                    else if (player.GetData(EntityData.PLAYER_RENT_HOUSE) == house.id)
                    {
                        // Remove player's rental
                        player.SetData(EntityData.PLAYER_RENT_HOUSE, 0);
                        house.tenants++;

                        Task.Factory.StartNew(() =>
                        {
                            // Update house's tenants
                            Database.UpdateHouse(house);
                        });

                        // Send the message to the player
                        player.SendChatMessage(Constants.COLOR_INFO + string.Format(Messages.INF_HOUSE_RENT_STOP, house.name));
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_HOUSE_RENTED);
                    }
                }
            }
        }

        [Command(Messages.COM_WARDROBE)]
        public void WardrobeCommand(Client player)
        {
            int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
            if (houseId > 0)
            {
                HouseModel house = GetHouseById(houseId);
                if (HasPlayerHouseKeys(player, house) == true)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

                    if (Globals.GetPlayerClothes(playerId).Count > 0)
                    {
                        player.TriggerEvent("showPlayerWardrobe");
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_CLOTHES_IN_WARDROBE);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_HOUSE_OWNER);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_IN_HOUSE);
            }
        }
    }
}
