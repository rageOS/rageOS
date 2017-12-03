using System;
using System.Collections.Generic;
using GTANetworkAPI;

namespace RageOS
{
    class Carwash : Script
    {
        private double toPay = 50;

        private List<Vector3> wash = new List<Vector3>()
        {
            new Vector3(20.90365f, -1391.978f, 28.9f),
            new Vector3(-699.8218f, -934.9742f, 18.5f)
        };

        public Carwash()
        {
            API.OnResourceStart += OnResourceStart;
            API.OnClientEventTrigger += OnClientEventTrigger;
        }

        public void OnResourceStart()
        {
            foreach (Vector3 Carwash in wash)
            {
                ColShape carwash = API.CreateSphereColShape(new Vector3(Carwash.X, Carwash.Y, Carwash.Z), 5f);
                API.CreateMarker(75, new Vector3(Carwash.X, Carwash.Y, Carwash.Z - 1), new Vector3(), new Vector3(5f, 5f, 1f), 1f, new Color(0, 0, 153));
                carwash.OnEntityEnterColShape += OnEntityEnterColShapeHandler;
                carwash.OnEntityExitColShape += OnEntityExitColShapeHandler;
            }
        }

        private void OnClientEventTrigger(Client player, string eventname, params object[] arguments)
        {
            switch (eventname)
            {
                case "waschen":
                    player.SetSyncedData("IsInCarwash", false);
                    if (player.Vehicle.EngineStatus == false)
                    {
                        API.SendPictureNotificationToPlayer(player, "~g~Glückwunsch, dein Fahrzeug sieht aus wie neu!", "CHAR_STRIPPER_JULIET", 1, 1, "Juliet", "Wasch Nixe");
                        API.RepairVehicle(player.Vehicle); //entfernt den groben schmutz
                        Money.TakeMoney(player, toPay);
                    }
                    else
                    {
                        API.SendNotificationToPlayer(player, "~r~Waschen mit laufendem Motor ist verboten!");
                    }
                    break;
            }
        }

        private void OnEntityEnterColShapeHandler(ColShape shape, NetHandle entity)
        {
            Client player = API.GetPlayerFromHandle(entity);
            if (player == null || shape == null || !player.IsInVehicle || player.VehicleSeat != -1)
                return;

            player.SetSyncedData("IsInCarwash", true);
            if (player.GetData("CashMoney") < toPay)
            {
                API.SendPictureNotificationToPlayer(player, "Eine Wäsche kostet ~r~" + toPay.ToString() + "$~w~." + "\nAber dafür reicht dein Geld nicht.", "CHAR_STRIPPER_JULIET", 1, 1, "Juliet", "Wasch Nixe");
            }
            else
            {
                API.SendPictureNotificationToPlayer(player, "Eine Wäsche kostet ~r~" + toPay.ToString() + "$~w~." + "\nDu bekommst auch 'nen gut riechenden Duftbaum dazu.", "CHAR_STRIPPER_JULIET", 1, 1, "Juliet", "Wasch Nixe");
            }
        }

        private void OnEntityExitColShapeHandler(ColShape shape, NetHandle entity)
        {
            Client player = API.GetPlayerFromHandle(entity);
            if (player == null || shape == null)
                return;

            player.SetSyncedData("IsInCarwash", false);
        }
    }
}