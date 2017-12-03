using System;
using GTANetworkAPI;

namespace RageOS
{
    public class Money : Script
    {
        public static bool TakeMoney(Client player, double amount)
        {
            if (player.HasData("CashMoney"))
            {
                if (double.TryParse(player.GetData("CashMoney").ToString(), out double playermoney))
                {
                    if ((playermoney >= amount) && (amount >= 0))
                    {
                        player.SetData("CashMoney", Math.Round(playermoney - amount, 2));
                        API.Shared.TriggerClientEvent(player, "update_money_display", player.GetData("CashMoney").ToString());
                        return true;
                    }
                    API.Shared.SendNotificationToPlayer(player, "Du hast nicht genug Geld bei dir!");
                }
                else
                {
                    API.Shared.ConsoleOutput("Konnte für Spieler " + player.SocialClubName + " TakeMoney nicht ausführen (" + player.GetData("CashMoney").ToString() + ")!");
                }
            }
            return false;
        }

        public static void GiveMoney(Client player, double amount)
        {
            if (amount > 0 && player.HasData("CashMoney"))
            {
                if (double.TryParse(player.GetData("CashMoney").ToString(), out double playermoney))
                {
                    player.SetData("CashMoney", Math.Round(playermoney + amount, 2));
                    API.Shared.TriggerClientEvent(player, "update_money_display", player.GetData("CashMoney").ToString());
                }
                else
                {
                    API.Shared.ConsoleOutput("Konnte für Spieler " + player.SocialClubName + " GiveMoney nicht ausführen (" + player.GetData("CashMoney").ToString() + ")!");
                }
            }
        }

        //public static void GiveFirmenMoney(Client sender, double amount)
        //{
        //    if (amount > 0 && sender.GetData("Gang").gangType == 2)
        //    {
        //        using (var connection = new DB().GetOpenConnection())
        //        {
        //            ObjGang Gang = sender.GetData("Gang");
        //            ObjGang ObjGang = connection.Get<ObjGang>(Gang.Id);
        //            ObjGang.Fraktionskasse = ObjGang.Fraktionskasse + amount;
        //            connection.Update(ObjGang);
        //        }
        //    }
        //}

        public static void GiveBankMoney(Client player, double amount)
        {
            if (amount > 0 && player.HasData("BankMoney"))
            {
                if (double.TryParse(player.GetData("BankMoney").ToString(), out double bankmoney))
                {
                    player.SetData("BankMoney", Math.Round(bankmoney + amount, 2));
                }
                else
                {
                    API.Shared.ConsoleOutput("Konnte für Spieler " + player.SocialClubName + " GiveBankMoney nicht ausführen (" + player.GetData("BankMoney").ToString() + ")!");
                }
            }
        }

        public static bool TakeBankMoney(Client player, double amount)
        {
            if (amount > 0 && player.HasData("BankMoney"))
            {
                if (double.TryParse(player.GetData("BankMoney").ToString(), out double bankmoney))
                {
                    if ((bankmoney >= amount) && (amount >= 0))
                    {
                        player.SetData("BankMoney", Math.Round(bankmoney - amount, 2));
                        return true;
                    }
                }
                else
                {
                    API.Shared.ConsoleOutput("Konnte für Spieler " + player.SocialClubName + " TakeBankMoney nicht ausführen (" + player.GetData("BankMoney").ToString() + ")!");
                }
            }
            return false;
        }
    }
}
