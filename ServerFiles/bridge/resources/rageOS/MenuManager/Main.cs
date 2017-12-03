using System;
using System.Collections.Generic;
using System.Globalization;
using GTANetworkAPI;

namespace MenuManagement
{
    class Main : Script
    {
        private MenuManager MenuManager = new MenuManager();
        private Menu savedMenu = null;

        public Main()
        {
            API.OnResourceStart += OnResourceStart;
        }

        private void OnResourceStart()
        {
            API.ConsoleOutput("MenuManager started");
        }
        [Command("TestMenuBuilder",Alias = "test1")]
        public void TestMenuBuilder(Client client)
        {
            Menu menu = savedMenu;

            if (menu == null)
            {
                // Create the menu
                menu = new Menu("TestMenu", "Test Menu", "This is a subtitle", 0, 0, Menu.MenuAnchor.Middle, true)
                {
                    BannerColor = new Color(0, 255, 255, 64),

                    // Set menu callback function that will be executed when a menu item with ExecuteCallback property is selected
                    Callback = TestMenuManager
                };

                // Add a simple MenuItem
                MenuItem menuItem = new MenuItem("Simple MenuItem");
                menu.Add(menuItem);

                // Add a MenuItem with commentary
                menuItem = new MenuItem("MenuItem with commentary", "This is the commentary");
                menu.Add(menuItem);

                // Add a MenuItem with left badge
                menuItem = new MenuItem("MenuItem with Left badge")
                {
                    LeftBadge = BadgeStyle.Star
                };
                menu.Add(menuItem);

                // Add an selectable MenuItem which opens a sub-menu and display a right badge
                menuItem = new MenuItem("Open submenu")
                {
                    ExecuteCallback = true,
                    RightBadge = BadgeStyle.Trevor
                };
                menu.Add(menuItem);

                // Add an selectable MenuItem which opens a sub-menu
                menuItem = new MenuItem("Shop menu", "Open a shop menu with advanced features")
                {
                    ExecuteCallback = true
                };
                menu.Add(menuItem);

                // Add a MenuItem with a right label
                menuItem = new MenuItem("MenuItem with right label")
                {
                    RightLabel = "BlaBlaBla"
                };
                menu.Add(menuItem);

                // Add a ListItem with 3 items and a commentary
                List<string> values = new List<string>() { "Item 1", "Item 2", "Item 3" };
                menuItem = new ListItem("A ListItem control", "Select the item you want", "List", values, 0);
                menu.Add(menuItem);

                // Add a CheckboxItem with checkbox unselected by default
                menuItem = new CheckboxItem("This is a CheckboxItem", "", "Checkbox", false);
                menu.Add(menuItem);

                // Add a MenuItem which will ask user to input text when selected. Default text is set to "My default text"
                menuItem = new MenuItem("MenuItem with input", "Text input", "Input");
                menuItem.SetInput("My Default text", 30, InputType.Text);
                menu.Add(menuItem);

                // Add a coloredItem with right label which will act as a "Submit" button
                // When submitted all input field will transmited to server
                ColoredItem coloredItem = new ColoredItem("Submit data", "Submit all data of editable MenuItems", "Submit", "#FF0000", "#0000FF")
                {
                    RightLabel = "Also with right label",
                    ExecuteCallback = true
                };
                menu.Add(coloredItem);
            }

            // Open the menu client side
            MenuManager.OpenMenu(client, menu);
        }

        private void TestMenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, dynamic data)
        {
            if (itemIndex == 3)
            {
                // Save menu data so that we will not loose them when we will come back from sub-menu
                menu.SaveData(data);
                savedMenu = menu;

                // Create sub-menu and send it to client
                TestSubMenuBuilder(client);
            }
            else if (itemIndex == 4)
            {
                // Save menu data so that we will not loose them when we will come back from sub-menu
                menu.SaveData(data);
                savedMenu = menu;

                // Create sub-menu and send it to client
                ShopMenuBuilder(client);
            }
            else if (itemIndex == 9)
            {
                int listIndex = data["List"]["Index"];
                string listValue = data["List"]["Value"];
                bool checkboxValue = data["Checkbox"];
                string inputValue = data["Input"];
            }
        }

        private void ShopMenuBuilder(Client client)
        {
            // Create the menu
            Menu menu = new Menu("ShopMenu", "", "Select objects to buy", 0, 0, Menu.MenuAnchor.Middle)
            {

                // Set menu banner from picture
                //menu.BannerTexture = "./Client/MyMenu.png";

                // Set javascript to execute when the OnItemSelect event is raised
                OnItemSelect = "resource.Shop.CalculatePrice(menu);",

                // Set menu callback
                Callback = ShopMenuManager
            };

            // Add sandwich item
            MenuItem menuItem = new MenuItem("Sandwich", "", "Sandwich");
            menuItem.SetInput("", 4, InputType.UNumber);
            menu.Add(menuItem);

            // Add water bottle item
            menuItem = new MenuItem("Water bottle", "", "WaterBottle");
            menuItem.SetInput("", 4, InputType.UNumber);
            menu.Add(menuItem);

            // Submit button with right label displaying price
            menuItem = new MenuItem("~y~Buy~s~ goods", "", "BuyItems")
            {
                RightLabel = "~g~$~s~0",
                ExecuteCallback = true
            };
            menu.Add(menuItem);

            // Back button
            menuItem = new MenuItem("~c~Back to main menu", "", "Back")
            {
                ExecuteCallback = true
            };
            menu.Add(menuItem);

            // Open menu and set finalizer method that will be executed when menu will close
            MenuManager.OpenMenu(client, menu);
        }

        private void ShopMenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, dynamic data)
        {
            if (itemIndex == -1 || menuItem.Id == "Back")
                TestMenuBuilder(client);
            else if (menuItem.Id == "BuyItems")
            {
                int sandwichs = 0;
                int waterBottles = 0;

                if (data["Sandwich"] != null)
                    sandwichs = (int)data["Sandwich"];

                if (data["WaterBottle"] != null)
                    waterBottles = (int)data["WaterBottle"];

                int price = sandwichs * 5 + waterBottles * 10;
                client.SendNotification("You bought goods for: ~g~$~s~" + price);
            }
        }

        private void TestSubMenuBuilder(Client client)
        {
            // Create menu
            Menu menu = new Menu("TestSubMenu", "Sub Menu", "Another subtitle", 0, 0, Menu.MenuAnchor.Middle, true)
            {

                // Set banner sprite from ingame banners' list
                BannerSprite = Banner.Guns,

                // Set callback
                Callback = TestSubMenuManager
            };

            // Add menu items
            menu.Add(new MenuItem("Line 1"));
            menu.Add(new MenuItem("Line 2"));
            menu.Add(new MenuItem("Line 3"));
            ColoredItem coloredItem = new ColoredItem("Back", "", "Back", "#FF00FF", "#00FF00")
            {
                ExecuteCallback = true
            };
            menu.Add(coloredItem);
            MenuManager.OpenMenu(client, menu);
        }

        private void TestSubMenuManager(Client client, Menu menu, MenuItem menuItem, int itemIndex, dynamic data)
        {
            if (menu.Id == "TestSubMenu" && menuItem.Id == "Back")
                TestMenuBuilder(client);
        }
    }
}
