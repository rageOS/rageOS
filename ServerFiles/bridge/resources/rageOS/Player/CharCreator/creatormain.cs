using System;
using System.IO;
using System.Collections.Generic;
using GTANetworkAPI;
using Newtonsoft.Json;


namespace RageOS
{
    #region ParentData
    public class ParentData
    {
        public int Father;
        public int Mother;
        public float Similarity;
        public float SkinSimilarity;

        public ParentData(int father, int mother, float similarity, float skinsimilarity)
        {
            Father = father;
            Mother = mother;
            Similarity = similarity;
            SkinSimilarity = skinsimilarity;
        }
    }
    #endregion

    #region AppearanceItem
    public class AppearanceItem
    {
        public int Value;
        public float Opacity;

        public AppearanceItem(int value, float opacity)
        {
            Value = value;
            Opacity = opacity;
        }
    }
    #endregion

    #region HairData
    public class HairData
    {
        public int Hair;
        public int Color;
        public int HighlightColor;

        public HairData(int hair, int color, int highlightcolor)
        {
            Hair = hair;
            Color = color;
            HighlightColor = highlightcolor;
        }
    }
    #endregion

    #region PlayerCustomization Class
    public class PlayerCustomization
    {
        // Player
        public int Gender;

        // Parents
        public ParentData Parents;

        // Features
        public float[] Features = new float[20];

        // Appearance
        public AppearanceItem[] Appearance = new AppearanceItem[10];

        // Hair & Colors
        public HairData Hair;

        public int EyebrowColor;
        public int BeardColor;
        public int EyeColor;
        public int BlushColor;
        public int LipstickColor;
        public int ChestHairColor;

        public PlayerCustomization()
        {
            Gender = 0;
            Parents = new ParentData(0, 0, 1.0f, 1.0f);
            for (int i = 0; i < Features.Length; i++) Features[i] = 0f;
            for (int i = 0; i < Appearance.Length; i++) Appearance[i] = new AppearanceItem(255, 1.0f);
            Hair = new HairData(0, 0, 0);
        }
    }
    #endregion

    public class Charcreator : Script
    {
        public static Dictionary<NetHandle, PlayerCustomization> CustomPlayerData = new Dictionary<NetHandle, PlayerCustomization>();

        public static Vector3 CreatorCharPos = new Vector3(402.8664, -996.4108, -99.00027);
        public static Vector3 CreatorPos = new Vector3(402.8664, -997.5515, -98.5);
        public static Vector3 CameraLookAtPos = new Vector3(402.8664, -996.4108, -98.5);
        public static float FacingAngle = -185.0f;
        public static uint DimensionID = 1;

        public Charcreator()
        {
            API.OnResourceStart += CharCreator_Init;

            API.OnPlayerConnected += CharCreator_PlayerJoin;
            API.OnClientEventTrigger += CharCreator_EventTrigger;
            API.OnPlayerDisconnected += CharCreator_PlayerLeave;

            API.OnResourceStop += CharCreator_Exit;
        }
        
        #region Methods
        public static void LoadCharacter(Client player)
        {
            if (CustomPlayerData.ContainsKey(player.Handle)) return;
            
            string char_data = player.GetData("CharDataJSON");
            if (char_data != null)
            {
                CustomPlayerData.Add(player.Handle, JsonConvert.DeserializeObject<PlayerCustomization>(char_data));
            }
            
            ApplyCharacter(player);
        }

        public static void ApplyCharacter(Client player)
        {
            if (!CustomPlayerData.ContainsKey(player.Handle)) return;
            player.SetSkin((CustomPlayerData[player.Handle].Gender == 0) ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);
            player.SetData("SkinC",(CustomPlayerData[player.Handle].Gender == 0) ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);
            Clothes.LoadClothing(player);            
            player.SetClothes(2, CustomPlayerData[player.Handle].Hair.Hair, 0);

            API.Shared.SendNativeToAllPlayers(
                Hash.SET_PED_HEAD_BLEND_DATA,
                player.Handle,

                CustomPlayerData[player.Handle].Parents.Mother,
                CustomPlayerData[player.Handle].Parents.Father,
                0,

                CustomPlayerData[player.Handle].Parents.Mother,
                CustomPlayerData[player.Handle].Parents.Father,
                0,

                CustomPlayerData[player.Handle].Parents.Similarity,
                CustomPlayerData[player.Handle].Parents.SkinSimilarity,
                0,

                false
            );

            for (int i = 0; i < CustomPlayerData[player.Handle].Features.Length; i++) API.Shared.SendNativeToAllPlayers(Hash._SET_PED_FACE_FEATURE, player.Handle, i, CustomPlayerData[player.Handle].Features[i]);
            for (int i = 0; i < CustomPlayerData[player.Handle].Appearance.Length; i++) API.Shared.SendNativeToAllPlayers(Hash.SET_PED_HEAD_OVERLAY, player.Handle, i, CustomPlayerData[player.Handle].Appearance[i].Value, CustomPlayerData[player.Handle].Appearance[i].Opacity);

            // apply colors
            API.Shared.SendNativeToAllPlayers(Hash._SET_PED_HAIR_COLOR, player.Handle, CustomPlayerData[player.Handle].Hair.Color, CustomPlayerData[player.Handle].Hair.HighlightColor);
            API.Shared.SendNativeToAllPlayers(Hash._SET_PED_EYE_COLOR, player.Handle, CustomPlayerData[player.Handle].EyeColor);

            API.Shared.SendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.Handle, 1, 1, CustomPlayerData[player.Handle].BeardColor, 0);
            API.Shared.SendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.Handle, 2, 1, CustomPlayerData[player.Handle].EyebrowColor, 0);
            API.Shared.SendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.Handle, 5, 2, CustomPlayerData[player.Handle].BlushColor, 0);
            API.Shared.SendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.Handle, 8, 2, CustomPlayerData[player.Handle].LipstickColor, 0);
            API.Shared.SendNativeToAllPlayers(Hash._SET_PED_HEAD_OVERLAY_COLOR, player.Handle, 10, 1, CustomPlayerData[player.Handle].ChestHairColor, 0);

            player.SetSyncedData("CustomCharacter", API.Shared.ToJson(CustomPlayerData[player.Handle]));
        }

        public void SaveCharacter(Client player)
        {
            player.SetData("CharDataJSON", JsonConvert.SerializeObject(CustomPlayerData[player.Handle], Formatting.Indented));
        }

        public static void SendToCreator(Client player)
        {
            player.SetData("Creator_PrevPos", player.Position);

            player.Dimension = DimensionID;
            player.Rotation = new Vector3(0f, 0f, FacingAngle);
            player.Position = CreatorCharPos;

            if (CustomPlayerData.ContainsKey(player.Handle))
            {
                SetCreatorClothes(player, CustomPlayerData[player.Handle].Gender);
                player.TriggerEvent("UpdateCreator", API.Shared.ToJson(CustomPlayerData[player.Handle]));
            }
            else
            {
                CustomPlayerData.Add(player.Handle, new PlayerCustomization());
                SetDefaultFeatures(player, 0);
            }

            player.TriggerEvent("CreatorCamera", CreatorPos, CameraLookAtPos, FacingAngle);
            DimensionID++;
        }

        public void SendBackToWorld(Client player)
        {
            player.Dimension = 0;
            player.Position = (Vector3)player.GetData("Creator_PrevPos");

            player.TriggerEvent("DestroyCamera");
            player.ResetData("Creator_PrevPos");
            API.TriggerClientEvent(player, "createPlayerHUD");
            API.TriggerClientEvent(player, "update_money_display", player.GetData("CashMoney").ToString());
        }

        public static void SetDefaultFeatures(Client player, int gender, bool reset = false)
        {
            if (reset)
            {
                CustomPlayerData[player.Handle] = new PlayerCustomization
                {
                    Gender = gender
                };

                CustomPlayerData[player.Handle].Parents.Father = 0;
                CustomPlayerData[player.Handle].Parents.Mother = 21;
                CustomPlayerData[player.Handle].Parents.Similarity = (gender == 0) ? 1.0f : 0.0f;
                CustomPlayerData[player.Handle].Parents.SkinSimilarity = (gender == 0) ? 1.0f : 0.0f;
            }

            // will apply the resetted data
            ApplyCharacter(player);

            // clothes
            SetCreatorClothes(player, gender);
        }

        public static void SetCreatorClothes(Client player, int gender)
        {
            if (!CustomPlayerData.ContainsKey(player.Handle)) return;

            // clothes            
            for (int i = 0; i < 10; i++) player.ClearAccessory(i);

            if (gender == 0)
            {
                player.SetClothes(3, 15, 0);
                player.SetClothes(4, 21, 0);
                player.SetClothes(6, 1, 0);
                player.SetClothes(8, 15, 0);
                player.SetClothes(11, 15, 0);
            }
            else
            {
                player.SetClothes(3, 15, 0);
                player.SetClothes(4, 10, 0);
                player.SetClothes(6, 35, 0);
                player.SetClothes(8, 15, 0);
                player.SetClothes(11, 15, 0);
            }

            player.SetClothes(2, CustomPlayerData[player.Handle].Hair.Hair, 0);
        }
        #endregion

        #region Events
        public void CharCreator_Init()
        {
            API.ConsoleOutput("started");
        }

        public void CharCreator_PlayerJoin(Client player)
        {
            LoadCharacter(player);
        }

        public void CharCreator_EventTrigger(Client player, string event_name, params object[] args)
        {
            switch (event_name)
            {
                case "SetGender":
                {
                    if (args.Length < 1 || !CustomPlayerData.ContainsKey(player.Handle)) return;

                    int gender = Convert.ToInt32(args[0]);
                    player.SetSkin((gender == 0) ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);
                    player.SetData("ChangedGender", true);
                    SetDefaultFeatures(player, gender, true);
                    break;
                }

                case "SaveCharacter":
                {
                    if (args.Length < 8 || !CustomPlayerData.ContainsKey(player.Handle)) return;

                    Clothes.Load(player);

                    // gender
                    CustomPlayerData[player.Handle].Gender = Convert.ToInt32(args[0]);

                    // parents
                    CustomPlayerData[player.Handle].Parents.Father = Convert.ToInt32(args[1]);
                    CustomPlayerData[player.Handle].Parents.Mother = Convert.ToInt32(args[2]);
                    CustomPlayerData[player.Handle].Parents.Similarity = (float)Convert.ToDouble(args[3]);
                    CustomPlayerData[player.Handle].Parents.SkinSimilarity = (float)Convert.ToDouble(args[4]);

                    // features
                    float[] feature_data = JsonConvert.DeserializeObject<float[]>(args[5].ToString());
                    CustomPlayerData[player.Handle].Features = feature_data;

                    // appearance
                    AppearanceItem[] appearance_data = JsonConvert.DeserializeObject<AppearanceItem[]>(args[6].ToString());
                    CustomPlayerData[player.Handle].Appearance = appearance_data;

                    // hair & colors
                    int[] hair_and_color_data = JsonConvert.DeserializeObject<int[]>(args[7].ToString());
                    for (int i = 0; i < hair_and_color_data.Length; i++)
                    {
                        switch (i)
                        {
                            // Hair
                            case 0:
                            {
                                CustomPlayerData[player.Handle].Hair.Hair = hair_and_color_data[i];
                                break;
                            }

                            // Hair Color
                            case 1:
                            {
                                CustomPlayerData[player.Handle].Hair.Color = hair_and_color_data[i];
                                break;
                            }

                            // Hair Highlight Color
                            case 2:
                            {
                                CustomPlayerData[player.Handle].Hair.HighlightColor = hair_and_color_data[i];
                                break;
                            }

                            // Eyebrow Color
                            case 3:
                            {
                                CustomPlayerData[player.Handle].EyebrowColor = hair_and_color_data[i];
                                break;
                            }

                            // Beard Color
                            case 4:
                            {
                                CustomPlayerData[player.Handle].BeardColor = hair_and_color_data[i];
                                break;
                            }

                            // Eye Color
                            case 5:
                            {
                                CustomPlayerData[player.Handle].EyeColor = hair_and_color_data[i];
                                break;
                            }

                            // Blush Color
                            case 6:
                            {
                                CustomPlayerData[player.Handle].BlushColor = hair_and_color_data[i];
                                break;
                            }

                            // Lipstick Color
                            case 7:
                            {
                                CustomPlayerData[player.Handle].LipstickColor = hair_and_color_data[i];
                                break;
                            }

                            // Chest Hair Color
                            case 8:
                            {
                                CustomPlayerData[player.Handle].ChestHairColor = hair_and_color_data[i];
                                break;
                            }
                        }
                    }

                    if (player.HasData("ChangedGender")) player.ResetData("ChangedGender");
                    ApplyCharacter(player);
                    SaveCharacter(player);
                    SendBackToWorld(player);
                    //ResetChar(player);
                    break;
                }

                case "LeaveCreator":
                {
                    if (!CustomPlayerData.ContainsKey(player.Handle)) return;
                    string char_data = player.GetData("CHAR_DATA");
                        // revert back to last save data
                    if (player.HasData("ChangedGender"))
                    {
                            if (char_data != null)
                            {
                                CustomPlayerData.Add(player.Handle, JsonConvert.DeserializeObject<PlayerCustomization>(char_data));

                            }
                            player.ResetData("ChangedGender");
                    }

                    ApplyCharacter(player);

                    // bye
                    SendBackToWorld(player);
                    break;
                }
            }
        }

        public void CharCreator_PlayerLeave(Client player, string reason)
        {
            if (CustomPlayerData.ContainsKey(player.Handle)) CustomPlayerData.Remove(player.Handle);
        }

        public void CharCreator_Exit()
        {
            foreach (Client player in API.GetAllPlayers())
            {
                if (player.HasData("Creator_PrevPos"))
                {
                    player.Dimension = 0;
                    player.Position = (Vector3)player.GetData("Creator_PrevPos");

                    player.TriggerEvent("DestroyCamera");
                    player.ResetData("Creator_PrevPos");
                }
            }
            CustomPlayerData.Clear();
        }
        #endregion

        #region Commands
        [Command("creator")]
        public void CMD_EnableCreator(Client player)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;
            if (!(player.Model == int.Parse(PedHash.FreemodeMale01.ToString()) || player.Model == int.Parse(PedHash.FreemodeFemale01.ToString())))
            {
                player.SendChatMessage("You need to have a freemode character skin.");
                return;
            }

            SendToCreator(player);
        }
        
        public void ResetChar(Client player)
        {
            
            for (var i = 3; i <= 11; i++)
            {
                player.SetClothes(i, 0, 0);
                player.SetData("Draw" + i, -1);
                player.SetData("Tx" + i, 0);
            }
            for (var i = 0; i <= 9; i++)
            {
                player.ClearAccessory(i);
                player.SetData("Propdraw" + i, -1);
                player.SetData("Proptx" + i, 0);
            }
        }
        #endregion
    }
}