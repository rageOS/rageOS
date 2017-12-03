using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using GTANetworkAPI;

namespace RageOS
{
    public class AdminCommands : Script
    {
        [Command("kill")] //Spieler töten
        public void Kill(Client player, string target1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            target.Kill();

            if (target == player)
            {
                API.SendChatMessageToPlayer(player, "~b~Du hast dich selbst getötet.");
            }
            else
            {
                API.SendChatMessageToPlayer(target, string.Format("~b~Du wurdest von {0} getötet.", player.Name));
                API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0} getötet.", target.Name));
            }
        }

        [Command("money", "~y~Benutzung: ~w~/money [Spieler] [Betrag]")] //Geld geben
        public void Money(Client player, string target1, int amount = 0)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            double oldMoney = double.Parse(target.GetData("CashMoney").ToString());

            if (amount == 0)
            {
                API.SendChatMessageToPlayer(player, string.Format("~b~{0} hat ${1} in der Tasche.", target.Name, oldMoney));
                return;
            }

            double newMoney = 0;
            if (amount < 0)
            {
                newMoney = oldMoney - amount;
                if (newMoney < 0) newMoney = 0;
            }
            else if (amount > 0)
                newMoney = oldMoney + amount;

            target.SetData("CashMoney", newMoney);
            target.TriggerEvent("update_money_display", target.GetData("CashMoney").ToString());

            player.SendChatMessage("~b~[System]~w~ Du hast " + target.Name + " " + amount + "$ gegeben");
        }

        [Command("setweather", "~y~Benutzung: ~w~/setweather [WetterID]")] //Wetter ändern
        public void Setworldweather(Client player, int w1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            if (w1 < 0 || w1 > 13)
            {
                API.SendChatMessageToPlayer(player, "~b~[System]~w~ Falsche Wetter ID.");
                API.SendChatMessageToPlayer(player, "~b~[System]~w~ Wetter IDs gehen von 0 bis 13");
            }
            else
            {
                string weather = "";
                switch (w1)
                {
                    case 0: weather = "Extra Sunny"; break;
                    case 1: weather = "Clear"; break;
                    case 2: weather = "Clouds"; break;
                    case 3: weather = "Smog"; break;
                    case 4: weather = "Foggy"; break;
                    case 5: weather = "Overcast"; break;
                    case 6: weather = "Rain"; break;
                    case 7: weather = "Thunderstorm"; break;
                    case 8: weather = "Light rain (Clearing)"; break;
                    case 9: weather = "Smoggy light rain (Neutral)"; break;
                    case 10: weather = "Very light snow (Snowing)"; break;
                    case 11: weather = "Windy light snow (Blizzard)"; break;
                    case 12: weather = "Light snow (Snowlight)"; break;
                    case 13: weather = "Unknown (No Effect)"; break;
                }
                API.SetWeather(weather);
                API.SendChatMessageToPlayer(player, "~b~[System]~w~ Du hast das Wetter zu " + weather + " geändert");
            }
        }

        [Command("ip", "~y~Benutzung: ~w~/ip [Spieler]")] //IP anzeigen
        public void IPAdresse(Client player, string target1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;


            API.SendChatMessageToPlayer(player, String.Format("~b~* IP Adresse von {0}: {1}", target.Name, API.GetPlayerAddress(target)));
        }

        [Command("rgc")] //Alle Fahrzeuge einparken
        public void ResetGarageCarsCommand(Client player)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            API.SendNotificationToAll("~o~[Warnung]~w~ Alle unbenutzen Fahrzeuge an einer Garage werden in 10 Sekunden eingeparkt.");
            Timer timer = new Timer(10000);
            timer.Elapsed += (sender, e) => ResetGarageCars(sender, e, player);
            timer.Start();
        }
        public void ResetGarageCars(object source, ElapsedEventArgs e, Client player)
        {
            API.Call("GarageFunctions", "ParkAllVehiclesInGarage", player);
            API.SendNotificationToAll("~g~[Warnung]~w~ Alle unbenutzen Fahrzeuge an einer Garage wurden eingeparkt.");
            Timer t = source as Timer;
            t.Stop();
        }

        [Command("settime")] //Uhrzeit ändern
        public void Settime(Client player, int hour, int minute)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            if (hour < 0 || hour > 23)
            {
                player.SendChatMessage("~r~Fehlerhafte Zeitangabe in Stunde");
                return;
            }

            if (minute < 0 || minute > 59)
            {
                player.SendChatMessage("~r~Fehlerhafte Zeitangabe in Minute");
                return;
            }

            API.SetWorldSyncedData("DAYNIGHT_HOUR", hour);
            API.SetWorldSyncedData("DAYNIGHT_MINUTE", minute);
            API.SetTime(API.GetWorldSyncedData("DAYNIGHT_HOUR"), API.GetWorldSyncedData("DAYNIGHT_MINUTE"), 0);
            //ServerTime.DayNightPrepareText();

            player.SendChatMessage("~b~[System]~w~ Du hast die Serverzeit auf " + hour + ":" + minute + " Uhr gestellt");
        }

        [Command("createcompany", Alias = "newcompany", GreedyArg = true)] //Neue Firma anlegen
        public void Createcompany(Client player, Database.Models.CompanyType Type, string FirmenName)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            using (var db = new DBContext())
            {
                int CharacterID = player.GetData("character.Id");
                var IdentificationNumber = CompaniesManager.GenerateIdentificationnumber();

                var company = new Database.Models.Company
                {
                    CharacterId = CharacterID,
                    Name = FirmenName,
                    BankMoney = 0,
                    Type = Type,
                    LocationX = player.Position.X,
                    LocationY = player.Position.Y,
                    LocationZ = player.Position.Z
                };
                db.Company.Add(company);
                db.SaveChanges();
                player.SendNotification("~g~Firma wurde erfolgreich hinzugefügt!");
                API.ConsoleOutput("Firma " + FirmenName + " angelegt");
            }
        }

        [Command("deletecompany", Alias = "removecompany", GreedyArg = true)] //Firma löschen
        public void Deletecompany(Client player, string FirmenName)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            using (var db = new DBContext())
            {
                var company = db.Company.First(X => X.Name == FirmenName);
                db.Company.Remove(company);
                db.SaveChanges();
                player.SendNotification("~g~Firma wurde erfolgreich gelöscht!");
            }
        }

        [Command("AddCompanyOwner", Alias = "companyowner", GreedyArg = true)] //Inhaber setzen
        public void AddCompanyOwner(Client player, int FirmenID, Client InvPlayer)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 4))
                return;

            using (var db = new DBContext())
            {
                var company = db.Company.First(X => X.Id == FirmenID);
                company.CharacterId = player.GetData("CharacterId");
                db.SaveChanges();
                player.SendNotification("~g~Inhaber erfolgreich hinzugefügt!");
            }
        }

    }
}
