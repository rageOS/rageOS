using System.IO;
using GTANetworkAPI;

namespace RageOS
{
    public class DevelopmentCommands : Script
    {
        [Command("giveweapon", Alias = "weapon")] //Waffe geben
        public void GiveWeapon(Client player, string target1, WeaponHash weapon, int ammo = 0, bool equip = false, bool loaded = false)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.GivePlayerWeapon(target, weapon, ammo, equip, loaded);

            API.SendChatMessageToPlayer(target, string.Format("~b~Du hast von {0} ein(e) {1} mit {2} Munition erhalten.", player.Name, weapon, ammo));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0} ein(e) {1} mit {2} Munition gegeben.", target.Name, weapon, ammo));
        }

        [Command("giveweaponcomponent", Alias = "component")] //Waffen Komponente geben
        public void GiveWeaponComponent(Client player, string target1, WeaponHash weapon, WeaponComponent component)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.GivePlayerWeaponComponent(target, weapon, component);

            API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat dir ein(e) {1} für dein(e) {2} gegeben.", player.Name, component, weapon));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0} ein(e) {1} für sein(e) {2} gegeben.", target.Name, component, weapon));
        }

        [Command("setammo", Alias = "ammo")] //Munition geben
        public void SetAmmo(Client player, string target1, WeaponHash weapon, int ammo)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetPlayerWeaponAmmo(target, weapon, ammo);

            API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat die Munition von {1} auf {2} geändert.", player.Name, weapon, ammo));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0}'s Munition von {1} auf {2} geändert.", target.Name, weapon, ammo));
        }

        [Command("settint", Alias = "tint")] //Waffen Farbe ändern
        public void SetTint(Client player, string target1, WeaponHash weapon, WeaponTint tint)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetPlayerWeaponTint(target, weapon, tint);

            API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat die Färbung von {1} auf {2} geändert.", player.Name, weapon, tint));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0}'s Färbung von {1} auf {2} geändert.", target.Name, weapon, tint));
        }

        [Command("setaccessory", Alias = "accessory")] //Accessoir ändern
        public void SetAccessory(Client player, string target1, int slot, int drawable, int texture = 0)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetPlayerAccessory(target, slot, drawable, texture);

            API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat deine Accessoires geändert.", player.Name));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0} seine Accessoires geändert.", target.Name));
        }

        [Command("setclothes", Alias = "clothes")] //Kleidung ändern
        public void SetClothes(Client player, string target1, int slot, int drawable, int texture)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetPlayerClothes(target, slot, drawable, texture);

            API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat deine Kleidung geändert.", player.Name));
            API.SendChatMessageToPlayer(target, string.Format("~b~Du hast {0}'s Kleidung geändert.", target.Name));
        }

        [Command("resetclothes")] //Kleidung zurücksetzen
        public void DefaultClothes(Client player, string target1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetPlayerDefaultClothes(target);

            API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat deine Kleidung zurück gesetzt.", player.Name));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0}'s Kleidung zurück gesetzt.", target.Name));
        }

        [Command("setvehcolor")] //Fahrzeug Farben ändern
        public void Collroveh(Client player, int color1, int color2)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            if (API.IsPlayerInAnyVehicle(player) == false)
                return;

            var veh = API.GetPlayerVehicle(player);
            API.SetVehiclePrimaryColor(veh, color1);
            API.SetVehicleSecondaryColor(veh, color2);
        }

        [Command("setvehiclemod", Alias = "vehiclemod")] //Fahrzeug Mod ändern
        public void Setvehiclemod(Client player, int modType, int mod)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            if (API.IsPlayerInAnyVehicle(player) == false)
                return;

            API.SetVehicleMod(API.GetPlayerVehicle(player), modType, mod);
        }

        [Command("vehiclehash")] //Fahrzeug Hash anzeigen
        public void Vehiclehash(Client player)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            if (API.IsPlayerInAnyVehicle(player) == false)
                return;

            var vehicle = API.GetEntityModel(API.GetPlayerVehicle(player));
            API.SendChatMessageToPlayer(player, vehicle.ToString());
        }

        [Command("changeseat")] //Sitzplatz ändern
        public void Changeseat(Client player, int seat)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            if (API.IsPlayerInAnyVehicle(player) == false)
                return;

            API.SetPlayerIntoVehicle(player, player.Vehicle, seat);
        }

        [Command("power")] //PS erhöhen
        public void PowerCommand(Client player, int multi)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            if (API.IsPlayerInAnyVehicle(player) == false)
                return;

            API.SetVehicleEnginePowerMultiplier(API.GetPlayerVehicle(player), multi);
            API.SendChatMessageToPlayer(player, "~b~[System]~w~ Das Fahrzeug ist jetzt mit einem Engine Multiplier von " + multi + " ausgestattet");
        }

        [Command("power2")] //Beschleunigung erhöhen
        public void Power2Command(Client player, int multi)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            if (API.IsPlayerInAnyVehicle(player) == false)
                return;

            API.SetVehicleEngineTorqueMultiplier(API.GetPlayerVehicle(player), multi);
            API.SendChatMessageToPlayer(player, "~b~[System]~w~ Das Fahrzeug ist jetzt mit einem Engine Multiplier von " + multi + " ausgestattet");
        }

        [Command("savecoords")] //Koordinate speichern
        public void SaveCoords(Client player, string coordName)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Vector3 playerPos = API.GetEntityPosition(player); Vector3 playerRot = API.GetEntityRotation(player);
            API.SendChatMessageToPlayer(player, "Position: " + playerPos.X.ToString().Replace(",", ".") + ", " + playerPos.Y.ToString().Replace(",", ".") + ", " + playerPos.Z.ToString().Replace(",", "."), " Rotation: " + playerRot.Z.ToString().Replace(",", "."));
            System.IO.StreamWriter coordsFile;
            if (!File.Exists("SavedCoords.txt"))
            {
                coordsFile = new StreamWriter("SavedCoords.txt");
            }
            else
            {
                coordsFile = File.AppendText("SavedCoords.txt");
            }
            API.SendChatMessageToPlayer(player, "~r~Koordinate wurde gespeichert!");
            coordsFile.WriteLine("| " + coordName + " | " + playerPos.X.ToString().Replace(",", ".") + ", " + playerPos.Y.ToString().Replace(",", ".") + ", " + playerPos.Z.ToString().Replace(",", ".") + " Saved Rotation - " + playerRot.Z.ToString().Replace(",", "."));
            coordsFile.Close();
        }

        [Command("position")] //Aktuelle Position anzeigen
        public void Coords(Client player)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 2))
                return;

            Vector3 playerPos = API.GetEntityPosition(player);
            Vector3 playerRot = API.GetEntityRotation(player);

            API.SendChatMessageToPlayer(player, "Position: " + playerPos.X.ToString().Replace(",", ".") + ", " + playerPos.Y.ToString().Replace(",", ".") + ", " + playerPos.Z.ToString().Replace(",", "."), " Rotation: " + playerRot.Z.ToString().Replace(",", "."));
        }
    }
}
