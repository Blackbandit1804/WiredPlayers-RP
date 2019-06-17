using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System;

namespace WiredPlayers.house
{
    public class Furniture : Script
    {
        private static List<FurnitureModel> furnitureList;

        public void LoadDatabaseFurniture()
        {
            furnitureList = Database.LoadAllFurniture();
            foreach (FurnitureModel furnitureModel in furnitureList)
            {
                furnitureModel.handle = NAPI.Object.CreateObject(furnitureModel.hash, furnitureModel.position, furnitureModel.rotation, (byte)furnitureModel.house);
            }
        }

        public List<FurnitureModel> GetFurnitureInHouse(int houseId)
        {
            List<FurnitureModel> list = new List<FurnitureModel>();
            foreach (FurnitureModel furniture in furnitureList)
            {
                if (furniture.house == houseId)
                {
                    list.Add(furniture);
                }
            }
            return list;
        }

        public FurnitureModel GetFurnitureById(int id)
        {
            FurnitureModel furniture = null;
            foreach (FurnitureModel furnitureModel in furnitureList)
            {
                if (furnitureModel.id == id)
                {
                    furniture = furnitureModel;
                    break;
                }
            }
            return furniture;
        }
        
        [Command(Messages.COM_FURNITURE, Messages.GEN_FURNITURE_COMMAND)]
        public void FurnitureCommand(Client player, string action)
        {
            if (player.HasData(EntityData.PLAYER_HOUSE_ENTERED) == true)
            {
                int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                HouseModel house = House.GetHouseById(houseId);

                if (house != null && house.owner == player.Name)
                {
                    switch (action.ToLower())
                    {
                        case Messages.ARG_PLACE:
                            FurnitureModel furniture = new FurnitureModel();
                            furniture.hash = 1251197000;
                            furniture.house = Convert.ToUInt32(houseId);
                            furniture.position = player.Position;
                            furniture.rotation = player.Rotation;
                            furniture.handle = NAPI.Object.CreateObject(furniture.hash, furniture.position, furniture.rotation, (byte)furniture.house);
                            furnitureList.Add(furniture);
                            break;
                        case Messages.ARG_MOVE:
                            string furnitureJson = NAPI.Util.ToJson(GetFurnitureInHouse(houseId));
                            player.SetSharedData(EntityData.PLAYER_MOVING_FURNITURE, true);
                            player.TriggerEvent("moveFurniture", furnitureJson);
                            break;
                        case Messages.ARG_REMOVE:
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_FURNITURE_COMMAND);
                            break;
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
