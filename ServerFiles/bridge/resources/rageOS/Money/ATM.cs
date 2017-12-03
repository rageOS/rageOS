using System;
using System.Collections.Generic;
using GTANetworkAPI;

namespace RageOS
{
    public class ATM : Script
    {
        public Vector3 Position { get; set; }
        public Boolean Savezone { get; set; }
        private List<ATM> atms = new List<ATM>();

        public ATM()
        {
            PopulateCashpoints();

            API.OnClientEventTrigger += OnClientEventTrigger;
        }

        private void OnClientEventTrigger(Client player, string eventName, params object[] arguments)
        {
            switch (eventName)
            {
                case "OpenATMOverlay":
                    if (player.HasData("atminrange"))
                    {
                        player.TriggerEvent("CEF_CREATE", "atm.html");
                    }
                    break;
                case "GetBankMoney":
                    player.TriggerEvent("BankMoneyReceiver", player.GetData("BankMoney"));
                    break;
                case "depositMoney":
                    API.SendChatMessageToPlayer(player, "Hi Na?");
                    break;
                case "withdrawMoney":
                    API.SendChatMessageToPlayer(player, "Hi CockSucker");
                    break;
            }
        }

        public void PopulateCashpoints() //Bool = Show Safezone?
        {
            // Normale ATM (Off. Banken)
            atms.Add(new ATM(new Vector3(-1410.736, -98.9279, 51.397), false));
            atms.Add(new ATM(new Vector3(-1410.183, -100.6454, 51.3965), false));
            atms.Add(new ATM(new Vector3(-2295.853, 357.9348, 173.6014), false));
            atms.Add(new ATM(new Vector3(-2295.069, 356.2556, 173.6014), false));
            atms.Add(new ATM(new Vector3(-2294.3, 354.6056, 173.6014), false));
            atms.Add(new ATM(new Vector3(-282.7141, 6226.43, 30.4965), false));
            atms.Add(new ATM(new Vector3(-386.4596, 6046.411, 30.474), false));
            atms.Add(new ATM(new Vector3(24.5933, -945.543, 28.333), false));
            atms.Add(new ATM(new Vector3(5.686, -919.9551, 28.4809), false));
            atms.Add(new ATM(new Vector3(296.1756, -896.2318, 28.2901), true));
            atms.Add(new ATM(new Vector3(296.8775, -894.3196, 28.2615), true));
            atms.Add(new ATM(new Vector3(-712.9357, -818.4827, 22.7407), false));
            atms.Add(new ATM(new Vector3(-710.0828, -818.4756, 22.7363), false));
            atms.Add(new ATM(new Vector3(289.53, -1256.788, 28.4406), false));
            atms.Add(new ATM(new Vector3(289.2679, -1282.32, 28.6552), false));
            atms.Add(new ATM(new Vector3(-1569.84, -547.0309, 33.9322), false));
            atms.Add(new ATM(new Vector3(-1570.765, -547.7035, 33.9322), false));
            atms.Add(new ATM(new Vector3(-1305.708, -706.6881, 24.3145), false));
            atms.Add(new ATM(new Vector3(-2071.928, -317.2862, 12.3181), false));
            atms.Add(new ATM(new Vector3(-821.8936, -1081.555, 10.1366), false));
            atms.Add(new ATM(new Vector3(-867.013, -187.9928, 36.8822), false));
            atms.Add(new ATM(new Vector3(-867.9745, -186.3419, 36.8822), false));
            atms.Add(new ATM(new Vector3(-3043.835, 594.1639, 6.7328), false));
            atms.Add(new ATM(new Vector3(-3241.455, 997.9085, 11.5484), false));
            atms.Add(new ATM(new Vector3(-204.0193, -861.0091, 29.2713), false));
            atms.Add(new ATM(new Vector3(118.6416, -883.5695, 30.1395), false));
            atms.Add(new ATM(new Vector3(-256.6386, -715.8898, 32.7883), false));
            atms.Add(new ATM(new Vector3(-259.2767, -723.2652, 32.7015), false));
            atms.Add(new ATM(new Vector3(-254.5219, -692.8869, 32.5783), false));
            atms.Add(new ATM(new Vector3(89.8134, 2.8803, 67.3521), false));
            atms.Add(new ATM(new Vector3(-617.8035, -708.8591, 29.0432), false));
            atms.Add(new ATM(new Vector3(-617.8035, -706.8521, 29.0432), false));
            atms.Add(new ATM(new Vector3(-614.5187, -705.5981, 30.224), false));
            atms.Add(new ATM(new Vector3(-611.8581, -705.5981, 30.224), false));
            atms.Add(new ATM(new Vector3(-537.8052, -854.9357, 28.2754), false));
            atms.Add(new ATM(new Vector3(-526.7791, -1223.374, 17.4527), false));
            atms.Add(new ATM(new Vector3(-1315.416, -834.431, 15.9523), false));
            atms.Add(new ATM(new Vector3(-1314.466, -835.6913, 15.9523), false));
            atms.Add(new ATM(new Vector3(-1205.378, -326.5286, 36.851), false));
            atms.Add(new ATM(new Vector3(-1206.142, -325.0316, 36.851), false));
            atms.Add(new ATM(new Vector3(148.42, -1033.451, 29.34324), false));
            atms.Add(new ATM(new Vector3(146.9807, -1032.727, 29.34487), false));
            atms.Add(new ATM(new Vector3(-2975.11, 380.049, 14.9987), false));
            atms.Add(new ATM(new Vector3(155.9548, 6642.825, 31.60165), false));
            atms.Add(new ATM(new Vector3(-96.3399, 6456.214, 31.46018), false));
            atms.Add(new ATM(new Vector3(1138.766, -469.0414, 66.73122), false));

            // 24/7 ATMs
            atms.Add(new ATM(new Vector3(33.14966, -1348.255, 29.49702), false));
            atms.Add(new ATM(new Vector3(-56.96103, -1752.081, 29.42099), false));
            atms.Add(new ATM(new Vector3(-717.6124, -915.5986, 19.21559), false));
            atms.Add(new ATM(new Vector3(380.6667, 323.4697, 103.5664), false));
            atms.Add(new ATM(new Vector3(1153.753, -326.7956, 69.20506), false));
            atms.Add(new ATM(new Vector3(2558.438, 389.3324, 108.6229), false));
            atms.Add(new ATM(new Vector3(2683.117, 3286.611, 55.24113), false));
            atms.Add(new ATM(new Vector3(1967.987, 3743.592, 32.34375), false));
            atms.Add(new ATM(new Vector3(1702.804, 4933.617, 42.06367), false));
            atms.Add(new ATM(new Vector3(1735.283, 6410.588, 35.03722), false));
            atms.Add(new ATM(new Vector3(540.336, 2671.079, 42.15651), false));
            atms.Add(new ATM(new Vector3(-1827.188, 784.9083, 138.3025), false));
            atms.Add(new ATM(new Vector3(-3240.629, 1008.62, 12.83071), false));
            atms.Add(new ATM(new Vector3(-3040.871, 593.2568, 7.90893), false));

            // Savezone ATMs
            atms.Add(new ATM(new Vector3(296.1756, -896.2318, 28.2901), true));
            atms.Add(new ATM(new Vector3(296.8775, -894.3196, 28.2615), true));
            atms.Add(new ATM(new Vector3(-846.6537, -341.509, 37.6685), true));
            atms.Add(new ATM(new Vector3(-847.204, -340.4291, 37.6793), true));
            atms.Add(new ATM(new Vector3(-282.7141, 6226.43, 30.4965), true));

            // Others
            atms.Add(new ATM(new Vector3(-1391.006, -590.3349, 30.31955), false));
        }

        public ATM(Vector3 position, Boolean savezone)
        {
            Position = position;
            Savezone = savezone;
            ColShape ATMCol = API.Shared.CreateSphereColShape(Position, 2);
            ATMCol.SetData("ATM", true);
            ATMCol.OnEntityEnterColShape += OnEntityEnterColShape;
            ATMCol.OnEntityExitColShape += OnEntityExitColShape;

            if (Savezone) //Wenn Bool = true
            {
                Blip ATMBlip = API.Shared.CreateBlip(Position);
                ATMBlip.Sprite = 108;
                ATMBlip.ShortRange = true;
                ATMBlip.Color = 25;
            }
        }

        public void OnEntityEnterColShape(ColShape shape, NetHandle entity)
        {
            Client player = API.GetPlayerFromHandle(entity);
            if (player == null || shape == null || player.HasData("atminrange"))
                return;

            if (shape.HasData("ATM"))
            {
                player.SetData("atminrange", true);
                API.SendNotificationToPlayer(player, "~b~Drücke die Taste [E] um den Bankautomaten zu benutzen!");
                API.TriggerClientEvent(player, "TriggerATMOverlay", true);
            }
        }

        private void OnEntityExitColShape(ColShape shape, NetHandle entity)
        {
            Client player = API.GetPlayerFromHandle(entity);
            if (player == null || shape == null)
                return;

            player.SetData("atminrange", null);
            player.ResetData("atminrange");
        }
    }
}
