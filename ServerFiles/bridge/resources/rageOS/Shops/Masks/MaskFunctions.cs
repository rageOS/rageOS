using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GTANetworkAPI;

namespace RageOS
{
    public class MaskFunctions : Script
    {
        public static string maskJSON = "";
        public MaskFunctions()
        {
            API.OnResourceStart += OnResourceStart;
            API.OnClientEventTrigger += OnClientEventTrigger;
        }  
        
        public void OnResourceStart()
        {
            API.CreatePed((PedHash)1846684678, new Vector3(-1336.748, -1276.614, 4.888119), 141.4498f);
            Blip blip = API.CreateBlip(new Vector3(-1336.748, -1276.614, 4.888119));
            API.SetBlipSprite(blip, 362);
            blip.ShortRange = true;
            maskJSON = File.ReadAllText(@"resources\rageOS\Shops\Masks\mask.json");
            ColShape mask = API.CreateSphereColShape(new Vector3(-1336.748, -1276.614, 4.888119), 2f);
            mask.SetData("maskbuy", this);
            mask.OnEntityEnterColShape += MaskEnter;
            mask.OnEntityExitColShape += MaskExit;            
        }

        public void OnClientEventTrigger(Client player, string eventname, params object[] args)
        {
            switch (eventname)
            {                
                case "BuyingMask":
                    if (player.GetData("CashMoney") < 900)
                    {
                        API.SendNotificationToPlayer(player, "Du hast leider nicht genug Geld dabei.");
                        return;
                    }
                    else
                    {
                        Money.TakeMoney(player, 900);                        
                        player.SendPictureNotificationToPlayer("~g~Du hast dir erfolgreich die Maske gekauft. Benutze [O] um deine Maske aufzuziehen", "CHAR_DEFAULT", 1, 1, "", "Maskenladen");
                        
                        if (!int.TryParse(args[0].ToString(), out int drawable)) { return; }
                        if (!int.TryParse(args[1].ToString(), out int texture)) { return; }
                        if (drawable == -1)
                        {
                            player.SetClothes(1, -1, 0);
                            player.SetData("Draw1", -1);
                            player.SetData("Dx1", 0);
                        }
                        else
                        {
                            player.SetClothes(1, drawable, texture);
                            player.SetData("Draw1", drawable);
                            player.SetData("Tx1", texture);
                        }
                        Account.SavePlayerCharacter(player);
                    }
                    break;
                case "SetPlayerMask":
                    if (!player.HasData("Draw1")) return;
                    if (player.InFreefall || player.IsInCover || player.IsOnLadder || player.IsShooting || player.Spectating || player.GetData("IS_COP")) return;
                    if ((bool)args[0] == true)
                    {
                        player.SetClothes(1, player.GetData("Draw1"), player.GetData("Tx1"));
                    }
                    else
                    {
                        player.SetClothes(1, 0, 0);
                    }
                    break;               
            }
        }

        public static void MaskMenu(Client player)
        {
            API.Shared.TriggerClientEvent(player, "masksMenu", maskJSON, player.GetData("Gender"));
        }

        public void MaskEnter(ColShape shape, NetHandle entity)
        {
            Client player = API.GetPlayerFromHandle(entity);
            if (player == null)
                return;
            if (shape == null)
                return;

            player.SetSyncedData("maskbuy", true);
            player.SendPictureNotificationToPlayer("Drücke ~r~[E]~w~ um dir Masken anzuschauen", "CHAR_DEFAULT", 1, 1, "", "Maskenladen");
        }

        public void MaskExit(ColShape shape, NetHandle entity)
        {
            Client player = API.GetPlayerFromHandle(entity);
            if (player == null)
                return;
            if (shape == null)
                return;            
            player.ResetSyncedData("maskbuy");
            player.SetClothes(1, 0, 0);
            API.TriggerClientEvent(player, "leaveMaskmenu");
        }
    }
}