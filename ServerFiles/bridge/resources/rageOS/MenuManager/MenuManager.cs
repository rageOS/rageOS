using System.Collections.Generic;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace MenuManagement
{
    class MenuManager : Script
    {
        #region Private static properties
        private static Dictionary<Client, Menu> ClientMenus = new Dictionary<Client, Menu>();
        #endregion

        #region Constructor
        public MenuManager()
        {
            API.OnClientEventTrigger += OnClientEventTriggerHandler;
            API.OnPlayerDisconnected += OnPlayerDisconnectedHandler;
        }
        #endregion

        #region Private API triggers
        private static void OnClientEventTriggerHandler(Client sender, string eventName, params object[] arguments)
        {
            if (eventName == "MenuManager_ExecuteCallback")
            {
                Menu menu = ClientMenus[sender];

                if (menu != null && menu.Callback != null)
                {
                    string menuId = (string)arguments[0];
                    string itemId = (string)arguments[1];
                    int itemIndex = (int)arguments[2];
                    dynamic data = API.Shared.FromJson((string)arguments[3]);
                    menu.Callback(sender, menu, menu.Items[itemIndex], itemIndex, data);
                }
            }
            else if (eventName == "MenuManager_ClosedMenu")
            {
                if (ClientMenus.ContainsKey(sender) == true)
                {
                    Menu menu = ClientMenus[sender];

                    if (menu.BackCloseMenu == false)
                        menu.Callback(sender, menu, null, -1, null);
                    else
                        ClientMenus.Remove(sender);
                }
            }
        }

        private static void OnPlayerDisconnectedHandler(Client player, string reason)
        {
            if (ClientMenus.ContainsKey(player) == true)
                ClientMenus.Remove(player);
        }
        #endregion

        #region Public static methods
        public static void CloseMenu(Client client)
        {
            if (ClientMenus.ContainsKey(client) == true)
            {
                ClientMenus[client].Finalizer?.Invoke(client, ClientMenus[client]);
                ClientMenus.Remove(client);
                API.Shared.TriggerClientEvent(client, "MenuManager_CloseMenu");
            }
        }

        public static Menu GetMenu(Client client)
        {
            return ClientMenus[client];
        }

        public static void OpenMenu(Client client, Menu menu)
        {
            if (ClientMenus.ContainsKey(client) == true)
            {
                CloseMenu(client);
            }

            ClientMenus.Add(client, menu);

            string json = JsonConvert.SerializeObject(menu, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            API.Shared.TriggerClientEvent(client, "MenuManager_OpenMenu", json);
        }
        #endregion
    }
}
