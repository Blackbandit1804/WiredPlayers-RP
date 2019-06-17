using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WiredPlayers.character
{
    public class Customization : Script
    {
        public static void ApplyPlayerCustomization(Client player, SkinModel skinModel, int sex)
        {
            // Populate the head
            HeadBlend headBlend = new HeadBlend();
            headBlend.ShapeFirst = Convert.ToByte(skinModel.firstHeadShape);
            headBlend.ShapeSecond = Convert.ToByte(skinModel.secondHeadShape);
            headBlend.SkinFirst = Convert.ToByte(skinModel.firstSkinTone);
            headBlend.SkinSecond = Convert.ToByte(skinModel.secondSkinTone);
            headBlend.ShapeMix = skinModel.headMix;
            headBlend.SkinMix = skinModel.skinMix;

            // Get the hair and eyes colors
            byte eyeColor = Convert.ToByte(skinModel.eyesColor);
            byte hairColor = Convert.ToByte(skinModel.firstHairColor);
            byte hightlightColor = Convert.ToByte(skinModel.secondHairColor);

            // Add the face features
            float[] faceFeatures = new float[]
            {
                skinModel.noseWidth, skinModel.noseHeight, skinModel.noseLength, skinModel.noseBridge, skinModel.noseTip, skinModel.noseShift, skinModel.browHeight,
                skinModel.browWidth, skinModel.cheekboneHeight, skinModel.cheekboneWidth, skinModel.cheeksWidth, skinModel.eyes, skinModel.lips, skinModel.jawWidth,
                skinModel.jawHeight, skinModel.chinLength, skinModel.chinPosition, skinModel.chinWidth, skinModel.chinShape, skinModel.neckWidth
            };

            // Populate the head overlays
            Dictionary<int, HeadOverlay> headOverlays = new Dictionary<int, HeadOverlay>();

            for (int i = 0; i < Constants.MAX_HEAD_OVERLAYS; i++)
            {
                // Get the overlay model and color
                int[] overlayData = GetOverlayData(skinModel, i);

                // Create the overlay
                HeadOverlay headOverlay = new HeadOverlay();
                headOverlay.Index = Convert.ToByte(overlayData[0]);
                headOverlay.Color = Convert.ToByte(overlayData[1]);
                headOverlay.SecondaryColor = 0;
                headOverlay.Opacity = 1.0f;

                // Add the overlay
                headOverlays[i] = headOverlay;
            }

            // Update the character's skin
            player.SetCustomization(sex == Constants.SEX_MALE, headBlend, eyeColor, hairColor, hightlightColor, faceFeatures, headOverlays, new Decoration[] { });
            player.SetClothes(2, skinModel.hairModel, 0);
        }

        public static void ApplyPlayerClothes(Client player)
        {
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            foreach (ClothesModel clothes in Globals.clothesList)
            {
                if (clothes.player == playerId && clothes.dressed)
                {
                    if (clothes.type == 0)
                    {
                        player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                    }
                    else
                    {
                        player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                    }
                }
            }
        }

        public static void ApplyPlayerTattoos(Client player)
        {
            // Get the tattoos from the player
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            List<TattooModel> playerTattoos = Globals.tattooList.Where(t => t.player == playerId).ToList();

            foreach (TattooModel tattoo in playerTattoos)
            {
                // Add each tattoo to the player
                Decoration decoration = new Decoration();
                decoration.Collection = NAPI.Util.GetHashKey(tattoo.library);
                decoration.Overlay = NAPI.Util.GetHashKey(tattoo.hash);
                player.SetDecoration(decoration);
            }
        }

        private static int[] GetOverlayData(SkinModel skinModel, int index)
        {
            int[] overlayData = new int[2];

            switch (index)
            {
                case 0:
                    overlayData[0] = skinModel.blemishesModel;
                    overlayData[1] = 0;
                    break;
                case 1:
                    overlayData[0] = skinModel.beardModel;
                    overlayData[1] = skinModel.beardColor;
                    break;
                case 2:
                    overlayData[0] = skinModel.eyebrowsModel;
                    overlayData[1] = skinModel.eyebrowsColor;
                    break;
                case 3:
                    overlayData[0] = skinModel.ageingModel;
                    overlayData[1] = 0;
                    break;
                case 4:
                    overlayData[0] = skinModel.makeupModel;
                    overlayData[1] = 0;
                    break;
                case 5:
                    overlayData[0] = skinModel.blushModel;
                    overlayData[1] = skinModel.blushColor;
                    break;
                case 6:
                    overlayData[0] = skinModel.complexionModel;
                    overlayData[1] = 0;
                    break;
                case 7:
                    overlayData[0] = skinModel.sundamageModel;
                    overlayData[1] = 0;
                    break;
                case 8:
                    overlayData[0] = skinModel.lipstickModel;
                    overlayData[1] = skinModel.lipstickColor;
                    break;
                case 9:
                    overlayData[0] = skinModel.frecklesModel;
                    overlayData[1] = 0;
                    break;
                case 10:
                    overlayData[0] = skinModel.chestModel;
                    overlayData[1] = skinModel.chestColor;
                    break;
            }

            return overlayData;
        }
    }
}