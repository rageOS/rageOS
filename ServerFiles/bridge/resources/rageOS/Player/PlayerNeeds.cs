using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Timers;

namespace RageOS
{
    public class PlayerNeeds : Script
    {
        public double PlayerNeedsHunger = 2.5; //Hunger
        public double PlayerNeedsThirst = 2.65; //Durst
        public double PlayerNeedsDrunk = 0.3; //Alkohol
        public int Minutes = 8; //Intervall für PlayerNeeds
        Timer PlayerNeedsTimer;

        public PlayerNeeds()
        {
            API.OnResourceStart += OnResourceStart;
        }

        public void OnResourceStart() //Timer starten
        {
            PlayerNeedsTimer = new Timer
            {
                Interval = Minutes * 60 * 1000
            };
            PlayerNeedsTimer.Elapsed += delegate { PlayerNeedsCounter(); }; //Funktion, wenn Timer abgelaufen ist
            PlayerNeedsTimer.AutoReset = true; //Dauerschleife
            PlayerNeedsTimer.Enabled = true; //Aktiviert
        }

        private void PlayerNeedsCounter()
        {
            List<Client> players = API.GetAllPlayers();
            foreach (var p in players)
            {
                if (!p.HasData("IS_ONLINE") || p.GetData("IS_ONLINE") != true)
                    continue;

                try
                {
                    double FoodLevel = (double)p.GetData("FoodLevel") - PlayerNeedsHunger;
                    double DrinkLevel = (double)p.GetData("DrinkLevel") - PlayerNeedsThirst;
                    double DrunkLevel = (double)p.GetData("DrunkLevel") - PlayerNeedsDrunk;

                    if (FoodLevel <= 0 || DrinkLevel <= 0 || DrunkLevel >= 100)
                    {
                        p.SetData("FoodLevel", 100);
                        p.SetData("DrinkLevel", 100);
                        p.SetData("DrunkLevel", 0);
                        p.SetSyncedData("DrunkLevel", 0);

                        p.Kill();
                        return;
                    }

                    if (DrunkLevel < 0)
                    {
                        DrunkLevel = 0;
                    }

                    // == Change Movestyle ==
                    if (p.GetData("DrunkLevel") >= 75)
                    {
                        API.TriggerClientEventForAll("Drunk", p.Handle, "move_m@drunk@verydrunk");
                    }
                    else if (p.GetData("DrunkLevel") >= 50)
                    {
                        API.TriggerClientEventForAll("Drunk", p.Handle, "move_m@drunk@moderatedrunk");
                    }
                    else if (p.GetData("DrunkLevel") >= 25)
                    {
                        API.TriggerClientEventForAll("Drunk", p.Handle, "move_m@drunk@slightlydrunk");
                    }
                    else if (p.GetData("DrunkLevel") >= 0)
                    {
                        API.TriggerClientEventForAll("ResetDrunk", p.Handle);
                    }
                    // == Change Movestyle ==

                    p.SetData("FoodLevel", FoodLevel); //Hunger
                    p.SetData("DrinkLevel", DrinkLevel); //Durst
                    p.SetData("DrunkLevel", DrunkLevel); //Alkohol
                    p.SetSyncedData("FoodLevel", FoodLevel); //Hunger
                    p.SetSyncedData("DrinkLevel", DrinkLevel); //Durst
                    p.SetSyncedData("DrunkLevel", DrunkLevel); //Alkohol
                }
                catch (Exception)
                {
                    API.ConsoleOutput("Fehler - PlayerNeeds.cs");
                }
            }
        }
    }
}
