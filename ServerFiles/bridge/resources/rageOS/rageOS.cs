using System;
using GTANetworkAPI;

namespace RageOS
{
    public class RageOS : Script
    {
        public RageOS()
        {
            API.OnResourceStart += API_OnResourceStart;
        }

        private void API_OnResourceStart()
        {
            
        }
    }
}
