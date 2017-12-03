using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using MenuManagement;
using GTANetworkAPI;

namespace RageOS
{
  
    public class CheapCloth : Script
    {
        public static string lowJSON = "";
        private List<Shop> shops = new List<Shop>();
        

        public CheapCloth()
        {
            API.OnResourceStart += OnStart;
            API.OnClientEventTrigger += OnTrigger;
           
        }

        public void OnStart()
        {
            ShopPosition();
        }
        
       
        private void OnTrigger(Client player, string eventName, params object [] args)
        {

        }

        public static void CheapClothMenu(Client player)
        {
            Menu menu = new Menu("CheapCloth", "Discount Store", "Alles günstig einkaufen", 0, 0, Menu.MenuAnchor.MiddleRight, true)
            {
                Callback = CheapClothManager
            };
            MenuItem menuItem = new MenuItem("Oberteile", "Wähle dein Oberteil aus", "Body");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Unterhemden", "", "Underwear");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            ColoredItem coloredItem = new ColoredItem("Verlassen", "", "Exit", "#FF0000", "#0000FF");
            menu.Add(coloredItem);
            coloredItem.ExecuteCallback = true;

            MenuManager.OpenMenu(player, menu);
        }

        private static void CheapClothManager(Client player, Menu menu, MenuItem menuItem, int itemIndex, dynamic dat)
        {
            if (menuItem.Id == "Exit")
            {
                player.SendNotification("Auf Wiedersehen");
                MenuManager.CloseMenu(player);

            }
        }


        #region Shops 
        public class Shop
        {
            public Vector3 Position { get; set; }
            public float Rotation { get; set; }

            public Shop(Vector3 pos, float rot)
            {
                Position = pos;
                Rotation = rot;

                Ped ShopPed = API.Shared.CreatePed(PedHash.ShopLowSFY, pos, rot);
                ColShape LowClothCol = API.Shared.CreateCylinderColShape(pos, 2f, 1f);
                LowClothCol.SetData("CheapCloth", this);
                LowClothCol.OnEntityEnterColShape += EnterCheapCloth;
                LowClothCol.OnEntityExitColShape += ExitCheapCloth;

            }
            private void EnterCheapCloth(ColShape shape, NetHandle entity)
            {
                Client player = API.Shared.GetPlayerFromHandle(entity);
                if (player == null)
                    return;
                if (shape == null)
                    return;
               
                player.SetSyncedData("CheapCloth", true);
                player.SendPictureNotificationToPlayer("Drücke ~r~[E]~w~ um dir Kleidung anzuschauen", "CHAR_DEFAULT", 1, 1, "", "");
            }

            private void ExitCheapCloth(ColShape shape, NetHandle entity)
            {
                Client player = API.Shared.GetPlayerFromHandle(entity);
                if (player == null)
                    return;
                if (shape == null)
                    return;
                player.ResetSyncedData("CheapCloth");
                MenuManager.CloseMenu(player);
            }
        }
        public void ShopPosition()
        {
            //shops.Add(new Shop(new Vector3(X, Y, Z), float heading);
            shops.Add(new Shop(new Vector3(1692.647, 4817.384, 42.06306), 13.40282f));
            shops.Add(new Shop(new Vector3(1201.892, 2708.262, 38.22259), 94.26429f));
            shops.Add(new Shop(new Vector3(77.40495, -1387.688, 29.37613), -173.8607f));
            shops.Add(new Shop(new Vector3(-816.8969, -1072.627, 11.3281), 122.5078f));
            shops.Add(new Shop(new Vector3(-1096.213, 2712.512, 19.10784), 134.2202f));
            shops.Add(new Shop(new Vector3(-0.3973338, 6510.409, 31.87783), -45.02671f));
        }
        #endregion
    }
}