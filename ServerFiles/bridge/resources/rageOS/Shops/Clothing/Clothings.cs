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
    #region DB Cloth
    public class ClothCustomization
    {
        // Clothes
        public int Draw0;
        public int Draw1;
        public int Draw2;
        public int Draw3;
        public int Draw4;
        public int Draw5;
        public int Draw6;
        public int Draw7;
        public int Draw8;
        public int Draw9;
        public int Draw10;
        public int Draw11;
        public int Tx0;
        public int Tx1;
        public int Tx2;
        public int Tx3;
        public int Tx4;
        public int Tx5;
        public int Tx6;
        public int Tx7;
        public int Tx8;
        public int Tx9;
        public int Tx10;
        public int Tx11;
        public int Propdraw0;
        public int Propdraw1;
        public int Propdraw2;
        public int Propdraw3;
        public int Propdraw4;
        public int Propdraw5;
        public int Propdraw6;
        public int Propdraw7;
        public int Propdraw8;
        public int Propdraw9;
        public int Proptx0;
        public int Proptx1;
        public int Proptx2;
        public int Proptx3;
        public int Proptx4;
        public int Proptx5;
        public int Proptx6;
        public int Proptx7;
        public int Proptx8;
        public int Proptx9;

    }
    #endregion
    public class Clothing : Script
    {
       
        public static string ClothJSON = "";
       

        public Clothing()
        {
            API.OnResourceStart += OnStart;
            API.OnClientEventTrigger += OnClientEventTrigger;
        }

        private void OnStart()
        {
            ClothJSON = File.ReadAllText(@"resources\rageOS\Shops\Clothing\clothes.json");
        }

        private void OnClientEventTrigger(Client player, string eventName, params object[] args)
        {
            if (eventName == "ClothingMenu")
            {                
                ClothesBuilder(player);                
            }
            else if (eventName == "SetClothes")
            {
                if (!int.TryParse(args[0].ToString(), out int slot)) { return; }
                if (!int.TryParse(args[1].ToString(), out int drawable)) { return; }
                if (!int.TryParse(args[2].ToString(), out int texture)) { return; }
                if (drawable == -1)
                {
                    player.SetClothes(slot, -1, 0);
                    player.SetData("Draw" + slot, -1);
                    player.SetData("Tx" + slot, 0);
                }
                else
                {
                    player.SetClothes(slot, drawable, texture);
                    player.SetData("Draw" + slot, drawable);
                    player.SetData("Tx" + slot, texture);
                }
            }
            else if (eventName == "SetAcc")
            {
                if (!int.TryParse(args[0].ToString(), out int slot)) { return; }
                if (!int.TryParse(args[1].ToString(), out int drawable)) { return; }
                if (!int.TryParse(args[2].ToString(), out int texture)) { return; }
                if (drawable == -1)
                {
                    player.ClearAccessory(slot);
                    player.SetData("Propdraw" + slot, -1);
                    player.SetData("Proptx" + slot, 0);
                }
                else
                {
                    player.SetAccessories(slot, drawable, texture);
                    player.SetData("Propdraw" + slot, drawable);
                    player.SetData("Proptx" + slot, texture);
                }
            }
        }

        private void ClothesBuilder(Client player)
        {
            API.SendChatMessageToPlayer(player, "Mach dich dich auf");

            Menu menu = new Menu("ClothingMenu", "DevMenu", "", 0, 0, Menu.MenuAnchor.MiddleRight, true)
            {
                Callback = ChooseCloth
            };
            MenuItem menuItem = new MenuItem("Kopfbedeckung", "", "Hat");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;         
            menuItem = new MenuItem("Oberkörper", "", "Jackets");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Unterhemden", "", "Underwear");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Hosen", "", "Pants");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Schuhe", "", "Shoes");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            ColoredItem coloredItem = new ColoredItem("Seite 2", "", "Next", "#32CD32", "#0000FF");
            menu.Add(coloredItem);
            coloredItem.ExecuteCallback = true;
            coloredItem = new ColoredItem("Verlassen", "", "Exit", "#FF0000", "#0000FF");
            menu.Add(coloredItem);
            coloredItem.ExecuteCallback = true;

            MenuManager.OpenMenu(player, menu);


        }

        private void ClothesBuilder2(Client player)
        {
            Menu menu = new Menu("ClothingMenu2", "Kleidungsladen", "", 0, 0, Menu.MenuAnchor.MiddleRight, true)
            {
                Callback = ChooseCloth2
            };
            MenuItem menuItem = new MenuItem("Cheap Laden 1", "", "Glasses");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Cheap Laden 2", "", "Earrings");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Cheap Laden 3", "", "Acessory");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Cheap Laden 4", "", "Watches");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Cheap Laden 5", "", "Braces");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            menuItem = new MenuItem("Cheap Laden 6", "", "Shop");
            menu.Add(menuItem);
            menuItem.ExecuteCallback = true;
            ColoredItem coloredItem = new ColoredItem("Seite 1", "", "Back", "#32CD32", "#0000FF");
            menu.Add(coloredItem);
            coloredItem.ExecuteCallback = true;
            coloredItem = new ColoredItem("Verlassen", "", "Exit", "#FF0000", "#0000FF");
            menu.Add(coloredItem);
            coloredItem.ExecuteCallback = true;

            MenuManager.OpenMenu(player, menu);
        }
        private void ChooseCloth(Client player, Menu menu, MenuItem menuItem, int itemIndex, dynamic data)
        {
            
            if (menu.Id == "ClothingMenu" && menuItem.Id == "Hat")
            {
                HatMenu(player);
                MenuManager.CloseMenu(player);
                
            }
            if (menu.Id == "ClothingMenu" && menuItem.Id == "Underwear")
            {
                UnderwearMenu(player);
                MenuManager.CloseMenu(player);
                
            }
            if (menu.Id == "ClothingMenu" && menuItem.Id == "Jackets")
            {
                JacketMenu(player);
                MenuManager.CloseMenu(player);
                
            }
            if (menu.Id == "ClothingMenu" && menuItem.Id == "Pants")
            {
                PantsMenu(player);
                MenuManager.CloseMenu(player);
                
            }
            if (menu.Id == "ClothingMenu" && menuItem.Id == "Shoes")
            {
                ShoesMenu(player);
                MenuManager.CloseMenu(player);
                
            }
            if (menu.Id == "ClothingMenu" && menuItem.Id == "Next")
            {
                ClothesBuilder2(player);
            }
            if (menu.Id == "ClothingMenu" && menuItem.Id == "Exit")
            {
                player.SendNotification("Juhu");
                MenuManager.CloseMenu(player);
                
            }
        }

        private void ChooseCloth2(Client player, Menu menu, MenuItem menuItem, int itemIndex, dynamic data)
        {
            if (menu.Id == "ClothingMenu2" && menuItem.Id == "Watches")
            {
                API.Delay(100, true, () => { player.Position = new Vector3(75.69604, -1387.704, 29.37612); });
            }
            if (menu.Id == "ClothingMenu2" && menuItem.Id == "Braces")
            {
                API.Delay(100, true, () => { player.Position = new Vector3(-817.7388, -1071.229, 11.3281); });
            }
            if (menu.Id == "ClothingMenu2" && menuItem.Id == "Earrings")
            {
                API.Delay(100, true, () => { player.Position = new Vector3(1201.899, 2709.882, 38.22258); });
            }
            if (menu.Id == "ClothingMenu2" && menuItem.Id == "Acessory")
            {
                API.Delay(100, true, () => { player.Position = new Vector3(-1097.362, 2713.798, 19.10784); });
            }
            if (menu.Id == "ClothingMenu2" && menuItem.Id == "Glasses")
            {
                API.Delay(100, true, () => { player.Position = new Vector3(1694.28, 4817.615, 42.06314); });
            }
            if (menu.Id == "ClothingMenu2" && menuItem.Id == "Shop")
            {
                API.Delay(100, true, () => { player.Position = new Vector3(0.8381513, 6508.932, 31.87783); });
            }
            if (menu.Id == "ClothingMenu2" && menuItem.Id == "Back")
            {
                ClothesBuilder(player);
               
            }
            if (menu.Id == "ClothingMenu2" && menuItem.Id == "Exit")
            {
                player.SendNotification("Juhu");
                MenuManager.CloseMenu(player);
               
            }
        }
        
        public void HatMenu(Client player)
        {
            API.TriggerClientEvent(player, "HatMenu", ClothJSON, player.GetData("Gender"));
        }
        public void UnderwearMenu(Client player)
        {
            API.TriggerClientEvent(player, "UnderwearMenu", ClothJSON, player.GetData("Gender"));
        }     
        public void JacketMenu(Client player)
        {
            API.TriggerClientEvent(player, "JacketMenu", ClothJSON, player.GetData("Gender"));
        }
        public void PantsMenu(Client player)
        {
            API.TriggerClientEvent(player, "PantsMenu", ClothJSON, player.GetData("Gender"));
        }
        public void ShoesMenu(Client player)
        {
            API.TriggerClientEvent(player, "ShoesMenu", ClothJSON, player.GetData("Gender"));
        }
    }
}
