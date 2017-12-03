using GTANetworkAPI;

namespace RageOS
{
    class KeyManager: Script
    {
        public KeyManager()
        {
            API.OnClientEventTrigger += OnClientEventTrigger;
        }

        private void OnClientEventTrigger(Client player, string eventName, params object[] arguments)
        {
            if (eventName == "onHotkeyPress")
            {
                //Vehicle Engine Toogle
                if ((int)arguments[0] == 0)
                {
                    VehicleManager.ToogleVehicleEngine(player);
                }
                // Unlock / Lock Vehicle
                else if ((int)arguments[0] == 1)
                {
                    VehicleManager.VehicleLock(player);
                }
                // Interaction
                else if ((int)arguments[0] == 2)
                {
                    if (player.HasSyncedData("maskbuy") == true)
                    {
                        MaskFunctions.MaskMenu(player);
                    }
                    if (player.HasSyncedData("CheapCloth") == true)
                    {
                        CheapCloth.CheapClothMenu(player);
                    }
                }
               
            }
        }
    }
}
