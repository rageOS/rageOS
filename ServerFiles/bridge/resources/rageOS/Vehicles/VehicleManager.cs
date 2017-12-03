using GTANetworkAPI;
using System.Collections.Generic;

namespace RageOS
{
    public class VehicleManager : Script
    {
        public static void ToogleVehicleEngine(Client player) //Fahrzeug Motor an-/ausschalten
        {
            bool EngineStatus = API.Shared.GetVehicleEngineStatus(player.Vehicle);

            if (EngineStatus == true) //Ausschalten
            {
                player.Vehicle.EngineStatus = false;
                API.Shared.SendNotificationToPlayer(player, "~r~Motor ausgeschaltet");
            }
            else if (EngineStatus == false && HasVehicleKey(player, player.Vehicle)) //Anschalten
            {
                player.Vehicle.EngineStatus = true;
                API.Shared.SendNotificationToPlayer(player, "~g~Motor gestartet");
            }
            else
            {
                API.Shared.SendNotificationToPlayer(player, "~r~Ohne Schlüssel wird das nichts..");
            }
        }

        public static void VehicleLock(Client player) //Fahrzeug abschließen
        {
            Vehicle veh = GetNearVehicle(player, 3f);
            bool LockStatus = API.Shared.GetVehicleLocked(veh);

            if (veh == null || !HasVehicleKey(player, veh)) return;

            if (LockStatus == true) //Aufschließen
            {
                API.Shared.SetVehicleLocked(veh, false);
                API.Shared.TriggerClientEvent(player, "UnlockVehicle");
            }
            else //Abschließen
            {
                API.Shared.SetVehicleLocked(veh, true);
                API.Shared.TriggerClientEvent(player, "LockVehicle");
            }
        }

        public static Vehicle GetNearVehicle(Client player, float range) //Fahrzeug finden
        {
            var vehicle = API.Shared.GetAllVehicles();
            foreach (NetHandle v in vehicle)
            {
                Vehicle veh = API.Shared.GetEntityFromHandle<Vehicle>(v);
                if (API.Shared.GetEntityPosition(veh).DistanceTo(player.Position) <= range)
                {
                    return veh;
                }
            }
            return null;
        }

        public static bool HasVehicleKey(Client player, Vehicle veh) //Schlüssel vorhanden?
        {
            if (veh.HasData("KeyOwners"))
            {
                List<string> owners = veh.GetData("KeyOwners");
                if (owners.Contains(player.SocialClubName)) return true;
            }
            return false;
        }

        public static Vehicle CreateProbeCar(Client player, VehicleHash model, Vector3 pos, float rot, int color1, int color2)
        {
            Vehicle vehicle = API.Shared.CreateVehicle(model, pos, rot, color1, color2);

            vehicle.SetSyncedData("fuel", 100);
            vehicle.Locked = true;
            vehicle.EngineStatus = false;
            vehicle.PrimaryColor = color1;
            vehicle.SecondaryColor = color2;
            vehicle.NumberPlate = "Probe";
            vehicle.SetData("KeyOwners", new List<string>() { player.SocialClubName });

            return vehicle;
        }

    }
}