using System;
using System.Collections.Generic;
using System.Timers;
using GTANetworkAPI;

namespace RageOS
{
    public class Paycheck : Script
    {
        public double PayCheckCiv = 100; //Paychecks für Bürger
        public double PayCheckFaction = 200; //Paychecks für Fraktionen
        public int Minutes = 5; //Intervall für PayChecks
        Timer PayCheckTimer;

        public Paycheck()
        {
            API.OnResourceStart += OnResourceStart;
        }

        public void OnResourceStart() //Timer starten
        {
            PayCheckTimer = new Timer
            {
                Interval = Minutes * 60 * 1000
            };
            PayCheckTimer.Elapsed += delegate { PayPaychecks(); }; //Funktion, wenn Timer abgelaufen ist
            PayCheckTimer.AutoReset = true; //Dauerschleife
            PayCheckTimer.Enabled = true; //Aktiviert
        }

        private void PayPaychecks()
        {
            List<Client> players = API.GetAllPlayers();
            foreach (var p in players)
            {
                if (p == null)
                    continue;

                if (p.GetData("UserLevel") == 0) //Nicht gemeldete Bürger erhalten keinen Paycheck
                    return;

                if (p.HasData("BankMoney") == false)
                    p.SetData("BankMoney", 0);

                if ((p.HasData("Company") && p.HasData("CompanyMember")) == true) //Firmenmitglieder erhalten keinen Paycheck
                    return;

                if (double.TryParse(p.GetData("BankMoney").ToString(), out double Bankmoney))
                {
                    if (p.GetData("IS_COP") || p.GetData("IS_MEDIC") || p.GetData("IS_JUSTIZ"))
                    {
                        
                        Money.GiveBankMoney(p, Math.Round(+PayCheckFaction, 2));
                        p.SendPictureNotificationToPlayer("Du hast dein Gehalt in Höhe von ~g~" + PayCheckFaction + " $~w~ erhalten.", "CHAR_BANK_MAZE", 1, 1, "Staatsbank", "Zahlungseingang");
                    }
                    else
                    {
                        
                        Money.GiveBankMoney(p, Math.Round(+PayCheckCiv, 2));
                        p.SendPictureNotificationToPlayer("Du hast eine staatliche Stütze in Höhe von ~g~" + PayCheckCiv + " $~w~ erhalten.", "CHAR_BANK_MAZE", 1, 1, "Staatsbank", "Zahlungseingang");
                    }
                }
                else
                {
                    API.ConsoleOutput("Konnte für Spieler " + p.SocialClubName + " keinen Paycheck austellen (" + p.GetData("BankMoney").ToString() + ")!");
                }
            }
        }
    }
}
