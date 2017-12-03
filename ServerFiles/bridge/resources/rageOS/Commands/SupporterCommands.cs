using System;
using System.Collections.Generic;
using GTANetworkAPI;

namespace RageOS
{
    public class SupporterCommands : Script
    {
        public SupporterCommands()
        {
            API.OnClientEventTrigger += OnClientEvent;
        }

        public void OnClientEvent(Client player, string eventName, params object[] args)
        {
            if (eventName == "TELEPORT_MARKER_CORDS")
            {
                if (args.Length < 2 || player == null)
                {
                    return;
                }
                try
                {
                    Vector3 MarkerPoint = new Vector3(float.Parse(args[0].ToString()), float.Parse(args[1].ToString()), 500);
                    player.GiveWeapon(WeaponHash.Parachute, 1, true, true);
                    API.Delay(100, true, () => { player.Position = MarkerPoint; });
                }
                catch (Exception)
                {
                }
            }
        }

        [Command("vehicle", Alias = "veh")] //Fahrzeug spawnen
        public void CreateVehicle(Client player, VehicleHash model, int color = 0)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Vector3 pos, rot;
            pos = API.GetEntityPosition(player.Handle);
            rot = API.GetEntityRotation(player.Handle);

            Vehicle veh = API.CreateVehicle(model, pos, rot.Z, color, color);
            veh.SetSyncedData("fuel", 100);
            veh.Locked = false;
            veh.SetData("KeyOwners", new List<string>() { player.SocialClubName });

            API.SetVehicleNumberPlate(veh.Handle, "RageOS");
            API.SetPlayerIntoVehicle(player, veh.Handle, -1);

            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast ein(e) '{0}' erzeugt.", model));
        }

        [Command("deletevehicle", Alias = "delveh")] //Fahrzeug löschen
        public void DeleteVehicle(Client player)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            if (!API.IsPlayerInAnyVehicle(player))
            {
                API.SendChatMessageToPlayer(player, "~r~Du musst in einem Fahrzeug sitzen!");

                return;
            }

            API.DeleteEntity(API.GetPlayerVehicle(player));
        }

        [Command("repairveh", Alias = "repair")] //Fahrzeug reparieren
        public void RepairVehicle(Client player)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            if (!API.IsPlayerInAnyVehicle(player))
            {
                API.SendChatMessageToPlayer(player, "~r~Du musst in einem Fahrzeug sitzen!");

                return;
            }

            player.Vehicle.Repair();
            API.SetVehicleHealth(player.Vehicle, 1000);
        }

        [Command("teleporttomarker", Alias = "ttm")] //Zum Marker teleportieren
        public void Teleporttomarker(Client player)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            API.TriggerClientEvent(player, "GET_USER_MARKER_TELEPORT");
        }

        [Command("teleport", Alias = "tp", GreedyArg = true)] //Zum Spieler teleportieren
        public void Teleport(Client player, string target1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            if (target.IsInVehicle)
            {
                player.SetIntoVehicle(target.Vehicle, -2);
            }
            else
            {
                player.Position = target.Position;
            }
            
            player.SendChatMessage("~b~Du hast dich zu " + target.Name + " teleportiert");
        }

        [Command("warp", GreedyArg = true)] //Spieler zu sich teleportieren
        public void Warp(Client player, string target1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            if (player == target)
            {
                API.SendChatMessageToPlayer(player, "~r~Du kannst dich nicht selbst zu dir teleportieren!");
                return;
            }

            target.Position = player.Position;
            player.SendChatMessage("~b~Du hast " + target.Name + " zu dir teleportiert.");
        }

        [Command("godmode", Alias = "god")] //Gott Modus
        public void GodMode(Client player, string target1, bool enabled)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetEntityInvincible(target.Handle, enabled);

            if (target == player)
            {
                if (enabled)
                    API.SendChatMessageToPlayer(player, "~b~Dein Godmode wurde aktiviert.");
                else
                    API.SendChatMessageToPlayer(player, "~b~Dein Godmode wurde deaktiviert.");

                return;
            }

            if (enabled)
            {
                API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat deinen Godmode aktiviert.", player.Name));
                API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0}'s Godmode aktiviert.", target.Name));
            }
            else
            {
                API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat deinen Godmode deaktiviert.", player.Name));
                API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0}'s Godmode deaktiviert.", target.Name));
            }
        }

        [Command("setpos", Alias = "goto")] //Teleport zu Coord
        public void SetPosition(Client player, float x, float y, float z)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            API.SetEntityPosition(player.Handle, new Vector3(x, y, z));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast deine Position auf '{0} {1} {2}' geändert.", x, y, z));
        }

        [Command("freeze", "~y~Benutzung: ~w~/freeze [Spieler] [Freezed]")] //Spieler einfrieren
        public void FreezePlayer(Client player, string target1, bool freeze)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetEntityPositionFrozen(target.Handle, freeze);

            if (target == player)
            {
                if (freeze)
                    API.SendChatMessageToPlayer(player, "~r~Du hast dich gefreezed.");
                else
                    API.SendChatMessageToPlayer(player, "~r~Du hast dich entfreezed.");

                return;
            }

            if (freeze)
            {
                API.SendChatMessageToPlayer(target, string.Format("~b~Du wurdest von {0} gefreezed.", player.Name));
                API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0} gefreezed.", target.Name));
            }
            else
            {
                API.SendChatMessageToPlayer(target, string.Format("~b~Du wurdest von {0} entfreezed.", player.Name));
                API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0} entfreezed.", target.Name));
            }
        }

        [Command("kick")] //Spieler kicken
        public void Kick(Client player, string target1, string reason)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.KickPlayer(target, reason);

            API.SendChatMessageToPlayer(player, "~r~Du hast {1} gekickt. (Grund: {2})");
        }

        [Command("sethealth", Alias = "health")] //Gesundheit setzen
        public void SetHealth(Client player, string target1, int health)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            if (health < 0) health = 0;
            if (health > 100) health = 100;
            API.SetPlayerHealth(target, health);

            API.SendChatMessageToPlayer(target, String.Format("~b~Dein Leben wurde von {0} auf {1}% geändert.", player.Name, health));
            API.SendChatMessageToPlayer(player, String.Format("~b~Du hast das Leben von {0} auf {1}% geändert.", target.Name, health));
        }

        [Command("setskin", Alias = "skin")] //Skin ändern
        public void Skin(Client player, string target1, PedHash skin)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetPlayerSkin(target, skin);

            API.SendChatMessageToPlayer(target, string.Format("~b~Dein Skin wurde von {0} zu {1} geändert.", player.Name, skin));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast den Skin von {0} zu {1} geändert.", target.Name, skin));
        }

        [Command("spectate")] //Spionieren
        public void SpectatePlayer(Client player, string target1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            if (target == player)
            {
                API.SendChatMessageToPlayer(player, "~r~Du kannst dich nicht selber beobachten!");

                return;
            }

            API.SetPlayerToSpectatePlayer(player, target);

            API.SendChatMessageToPlayer(player, string.Format("~b~Du beobachtest nun {0}.", target.Name));
        }

        [Command("spectator")] //Spionieren
        public void Spectator(Client player, string target1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.SetPlayerToSpectator(target);

            API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat deinen Zuschauermodus aktiviert.", player.Name));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0}'s Zuschauermodus aktiviert.", target.Name));
        }

        [Command("unspectate")] //Spionieren beenden
        public void Unspectate(Client player, string target1)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            Client target = PlayerCommands.FindPlayer(player, target1);
            if (target == null) return;

            API.UnspectatePlayer(target);

            API.SendChatMessageToPlayer(target, string.Format("~b~{0} hat deinen Zuschauermodus deaktiviert.", player.Name));
            API.SendChatMessageToPlayer(player, string.Format("~b~Du hast {0}'s Zuschauermodus deaktiviert.", target.Name));
        }

        [Command("invisible", Alias = "inv")] //Unsichtbar werden
        public void AdminVisibilityToggle(Client player)
        {
            if (!PlayerCommands.CheckAdminPermission(player, 1))
                return;

            int vis = player.Transparency;
            if (vis > 0)
            {
                player.Transparency = 0;
                player.SendNotification("Du bist jetzt unsichtbar!");
            }
            else
            {
                player.Transparency = 255;
                player.SendNotification("Du bist jetzt wieder sichtbar!");
            }
        }

        [Command("rundfunk", GreedyArg = true)] //Nachricht an alle Spieler
        public void SendMessageToAllPlayers(Client sender, string text)
        {
            if (!PlayerCommands.CheckAdminPermission(sender, 1))
                return;

            if (String.IsNullOrEmpty(text))
                return;

            API.SendChatMessageToAll("~g~Bürgermeister: ", "~w~" + text);
        }
    }
}
