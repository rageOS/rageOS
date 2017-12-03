using System.Collections.Generic;
using GTANetworkAPI;

namespace RageOS
{
    class Taxidriver : Script
    {
        public ColShape Infotaxi;
        public List<TaxiJob> jobs = new List<TaxiJob>();
        public List<Client> drivers = new List<Client>();

        public Taxidriver()
        {
            API.OnResourceStart += onResourceStart;
        }

        public void onResourceStart()
        {
            API.CreateMarker(1, new Vector3(895.6638, -179.3419, 73.70026), new Vector3(), new Vector3(), 1f, new Color(253,201,90));
            Blip Taxi = API.CreateBlip(new Vector3(895.6638, -179.3419, 74.70034));
            API.SetBlipSprite(Taxi, 56);
            API.SetBlipColor(Taxi, 5);
            API.SetBlipName(Taxi, "Taxizentrale");
            Infotaxi = API.CreateCylinderColShape(new Vector3(895.6638, -179.3419, 73.70026), 2f, 3f);

            Infotaxi.OnEntityEnterColShape += (shape, entity) =>
            {
                Client player;
                if ((player = API.GetPlayerFromHandle(entity)) != null)
                {
                    if (player.GetData("IS_COP") == true || int.Parse(player.GetData("TaxiLevel").ToString()) == 0)
                    {
                        player.SendNotification("Du bist kein Taxifahrer!");
                    }
                    else
                    {
                        player.SendNotification("Schreibe /job um deine Schicht anzufangen!");
                    }
                }
            };

        }

        public void useTaxis(TaxiJob job)
        {
            foreach (Client driver in drivers)
            {
                if (driver.GetData("TAXI") != null && driver.GetData("TASK") != 1.623482)
                {
                    API.SendPictureNotificationToPlayer(driver, job.sender.Name + " hat ein Taxi gerufen, wollen sie den Job annehmen?", "CHAR_TAXI", 0, 1, "Taxizentrale Morgan", "Job");
                }
            }
        }

        public void accepted(Client sender, TaxiJob job)
        {
            foreach (Client driver in drivers)
            {
                if (driver.GetData("TASK") == job.id)
                {
                    driver.SendNotification("~r~Dieser Auftrag ist bereits vergeben");
                }
            }
            sender.SendNotification("Du hast den Auftrag erhalten, benutze /done wenn Du fertig bist!");
            sender.TriggerEvent("markonmap", job.pos);
            sender.SetData("TASK", job.id);
            API.SendPictureNotificationToPlayer(job.sender, sender.Name + " ist auf dem Weg um sie abzuholen. Bitte bleiben sie wo sie sind!", "CHAR_TAXI", 0, 1, "Taxizentrale Morgan", "Message");
        }
        public bool isincircle(NetHandle lit)
        {
            var player = API.GetEntityFromHandle<Client>(lit);
            return Infotaxi.IsPointWithin(player.Position);

        }

        [Command("taxi")]
        public void calltaxi(Client sender)
        {
            TaxiJob j = new TaxiJob();
            j.id = API.Random();
            j.pos = sender.Position;
            j.sender = sender;
            j.status = 0;
            jobs.Add(j);
            useTaxis(j);
            sender.SendNotification("Ein Taxi ist auf dem Weg zu Ihnen!");
        }

        [Command("job")]
        public void startjob(Client sender)
        {
            if (isincircle((NetHandle)sender.Handle))
            {
                VehicleHash taxi = API.VehicleNameToModel("Taxi");
                Vehicle myvehicle = API.CreateVehicle(taxi, new Vector3(917.0233, -163.6854, 74.70861), 143.9855f, new Color(253,201,90), new Color(253, 201, 90));
                API.SetPlayerIntoVehicle(sender, myvehicle, -1);
                sender.SetData("TAXI", true);
                myvehicle.SetSyncedData("fuel", 100);
                myvehicle.SetData("TaxiOwner", sender.GetData("Id"));

                myvehicle.SetSyncedData("fuel", 100);
                myvehicle.SetData("RESPAWNABLE", true);
                myvehicle.SetData("SPAWN_POS", myvehicle.Position);
                myvehicle.SetData("SPAWN_ROT", myvehicle.Rotation);
                myvehicle.SetData("OWNER", sender.SocialClubName);
                myvehicle.SetSyncedData("OWNER", sender.SocialClubName);
                myvehicle.SetData("CLIENT", sender);
                myvehicle.SetData("vehicleMotorhaube", false);
                myvehicle.SetData("vehicleKofferraum", false);
                myvehicle.SetData("isAdminCar", false);
                myvehicle.SetSyncedData("DoorsLocked", true);
                myvehicle.Locked = true;
                myvehicle.EngineStatus = false;

                sender.SendNotification("Du hat einen Schlüssel zu deinem Taxi erhalten!");
                myvehicle.SetData("KeyOwners", new List<string>() { sender.SocialClubName });

                drivers.Add(sender);
                sender.SendNotification("Du bist jetzt Taxifahrer. /accept um einen Auftrag anzunehmen.");
            }
            else
            {
                //API.Shared.SetEntityPosition(sender, new Vector3(917.0233, -163.6854, 74.70861));
                sender.TriggerEvent("display_subtitle", "~r~Du bist nicht in der nähe eines Arbeitgebers!", 3000);
            }
        }

        [Command("accept")]
        public void acceptthetask(Client sender)
        {
            if (!jobs.Exists(X=>X.status == 0))
            {
                API.SendPictureNotificationToPlayer(sender, "Es liegen aktuell keine Aufträge vor!", "CHAR_TAXI", 0, 1, "Taxizentrale Morgan", "Message");
                return;
            }

            if (sender.HasData("TAXI") && bool.Parse(sender.GetData("TAXI").ToString()))
            {
                TaxiJob j = jobs.Find(X=>X.status == 0);
                j.status = 1;
                accepted(sender, j);
            }
            else
            {
                sender.SendNotification("Du bist nicht verfügbar!");
            }
        }

        [Command("done")]
        public void finishthetask(Client sender)
        {
            foreach (TaxiJob job in jobs)
            {
                if (job.id == sender.GetData("TASK"))
                {
                    job.status = 2;
                    sender.TriggerEvent("cleartaximarkers");
                    break;
                }
            }
            sender.SetData("TASK", 1.623482);
            sender.SendNotification("Du bist nun wieder frei!");
        }
    }

    public class TaxiJob
    {
        public double id { get; set; }
        public Vector3 pos { get; set; }
        public Client sender { get; set; }
        public int status { get; set; }
    }
}
