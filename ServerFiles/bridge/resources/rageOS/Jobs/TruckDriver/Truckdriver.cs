using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;

namespace RageOS
{
    class Truckdriver : Script
    {
        private const string ClientSetDestinationEventName = "SetDestination";
        private const int UnhookedTrailerSpawnCount = 100;
        private const float SpawnAreaClearanceRange = 15f;
        private const string errorAdmin = "~r~[FEHLER]~w~ Du besitzt nicht die benötigten Rechte oder du bist nicht im Admindienst.";

        private List<TruckInfo> _trucks = new List<TruckInfo>();
        private List<TrailerInfo> _trailers = new List<TrailerInfo>();

        private Random _random = new Random();

        private VehicleHash[] _truckHashes = new[] {
            VehicleHash.Hauler,
            VehicleHash.Packer,
            VehicleHash.Phantom
        };

        private VehicleHash[] _trailerHashes = new[] {
            VehicleHash.ArmyTanker,
            VehicleHash.ArmyTrailer,
            VehicleHash.DockTrailer,
            VehicleHash.TR4,
            VehicleHash.TVTrailer,
            VehicleHash.Tanker,
            VehicleHash.Tanker2,
            VehicleHash.TrailerLogs,
            VehicleHash.Trailers,
            VehicleHash.Trailers2,
            VehicleHash.Trailers3
        };

        List<SpawnPoint> Trailers = new List<SpawnPoint>()
        {
            new SpawnPoint(new Vector3(790.254456,1289.39587,359.8711),-141.968491, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(230.545074,1224.53552,225.035431),-166.329117, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-416.90448,1216.10889,325.224),161.58905, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-47.2153664,1877.83154,196.005219),127.285393, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-128.333878,1927.02222,196.662033),-0.730141342, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(190.469208,2476.64,55.1393738),-72.35381, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(258.830475,2578.776,44.715168),102.04245, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(193.409637,2760.55737,43.002388),-176.824585, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(209.5766,2758.335,43.0019264),177.221649, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(328.176636,2871.95654,43.028923),154.840256, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(647.429932,3501.03979,33.56954),-81.58661, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(924.795349,3650.26123,32.16458),177.531067, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1696.44434,3605.7644,34.99804),178.256165, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1981.67236,3782.55786,31.7568855),169.6424, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2893.11426,4378.322,49.9184952),-66.13225, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2907.85547,4344.66943,49.878994),-68.82769, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2553.44727,4673.94873,33.5053368),17.1116, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2109.28442,4768.65771,40.76526),104.521347, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(712.7401,4175.403,40.2856636),-86.55055, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(370.088562,4424.547,62.071106),20.5307732, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(352.016541,4417.724,63.4372635),10.3068972, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(309.999237,3386.64038,35.9828033),-30.2712631, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(315.6289,3383.3335,35.98043),-19.0491123, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-261.492126,6039.58154,31.44624),50.6569061, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-254.880157,6046.65527,31.6400261),45.7133522, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(206.264771,6385.833,30.9860668),22.1681385, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(184.55278,6394.809,30.9587841),-60.6373978, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(47.8882141,6457.464,30.9867077),-134.805511, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(428.632172,6469.18,28.3532867),53.78863, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1686.06079,6434.4873,31.95531),-170.954315, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2978.41138,3502.47656,70.95871),-77.1671753, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(3630.56787,3754.59644,28.09227),-18.4712811, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(3624.53467,3756.635,28.0936623),-29.0750484, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2672.44458,3517.94116,52.280407),69.82709, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2675.06934,3525.49756,52.1106453),76.73255, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2832.2688,1584.257,24.132309),73.72464, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2830.41943,1578.38879,24.1357346),76.74688, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(2826.31226,1571.97253,24.1359234),71.79269, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1403.04224,1058.66528,113.908905),-89.2880554, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1401.23218,1053.521,113.910278),-85.68043, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-68.33613,-2224.607,7.878508),-90.11711, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-65.71204 , -2232.175 , 7.879158),-90.73766, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(28.86705 , -1739.357 , 29.37089 ),50.09919, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(547.8154 , -1924.765 , 24.87827),-56.72981, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(554.7697 , -1844.62 , 25.39528),28.78527, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(566.196 , -1824.973 , 25.39945),91.34718, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1199.431 , -1494.437 , 34.77433),178.901, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1197.614 , -1401.296 , 35.29277),-178.5533, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1213.967 , -1071.6 , 39.73382),117.8777, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(632.3493 , 119.7442 , 92.57867),160.627, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-1196.831 , -1481.707 , 4.447226),-144.7754, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-1172.812 , -1743.705 , 4.096878),-146.7612, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(211.6134 , -2887.569 , 6.076221),-86.81203, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-220.0313 , -2515.73 , 6.068573),88.26366, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-430.6153 , -2712.398 , 6.068156),-135.0924, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-510.5136 , -2800.219 , 6.068237),134.1403, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(835.7764 , -1934.948 , 29.02222),174.538, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(894.937 , -1549.915 , 30.64512),-143.2548, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(1008.5 , -1223.733 , 25.21399),89.71461, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(-686.2279 , -1404.939 , 5.067984),136.538, SpawnPointTypes.Trailer),
            new SpawnPoint(new Vector3(145.4991 , -2500.564 , 6.066085),-125.0349, SpawnPointTypes.Trailer),
        };
        List<SpawnPoint> Destinations = new List<SpawnPoint>()
        {
            new SpawnPoint(new Vector3(676.0584,1277.75464,359.872467),-85.35305, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(179.210342,1244.96228,223.814148),-75.12576, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(-83.04313,1879.9856,196.846237),-98.53121, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(166.606689,2283.752,92.94659),-113.75589, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(216.50296,2607.16846,45.7766724),16.7768421, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(174.626373,2741.22681,43.0022659),-82.77837, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(284.8022,2827.33252,42.9953346),-64.669075, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(442.655182,3559.0835,32.8161736),-102.229431, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(885.662964,3608.51245,32.3911819),-176.73407, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(1976.32446,3760.596,31.75947),-144.190018, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(2512.96533,4221.701,39.46959),-121.786392, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(2588.28174,4683.3833,33.6419563),47.0469742, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(1956.62256,4649.503,40.3212852),-120.927948, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(734.851,4176.27637,40.2883034),-16.8779831, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(366.151031,3412.61914,35.98142),67.59108, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(-299.451019,6034.95264,31.0406723),-51.2346344, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(109.7262,6378.268,30.8014412),16.08805, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(59.3331223,6468.19531,30.9926949),-138.981781, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(435.2525,6509.68848,27.9843674),57.5816879, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(2982.25366,3489.27148,70.95835),-72.26721, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(3522.14258,3674.56348,33.4618645),81.387085, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(2702.66919,3454.74341,55.2373734),157.526016, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(2860.55835,1473.95862,24.1339264),74.38523, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(-260.9233 , -2501.198 , 6.067829),151.9755, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(-407.1918 , -2743.407 , 6.067881),-112.3195, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(911.6865 , -1171.891 , 25.45843),-62.4661, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(98.47039 , -1403.709 , 29.26324),37.79456, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(-741.298 , -1491.988 , 5.067977),122.0627, SpawnPointTypes.Destination),
            new SpawnPoint(new Vector3(272.209 , -2502.654 , 6.509565),105.4382, SpawnPointTypes.Destination),
        };


        public Truckdriver()
        {
            API.OnResourceStart += OnResourceStart;
            API.OnVehicleTrailerChange += OnVehicleTrailerChange;
            API.OnPlayerEnterVehicle += OnPlayerEnterVehicle;
            API.OnPlayerExitVehicle += OnPlayerExitVehicle;
        }

        private void OnResourceStart()
        {
            SpawnTrailers();
            API.Delay(300000, false, () =>
            {
                foreach (Client player in API.GetAllPlayers())
                {
                    if (API.IsPlayerInAnyVehicle(player))
                    {
                        var truck = player.Vehicle;
                        var truckInfo = GetTruckInfoByNetHandle(truck);
                        if (truckInfo == null) return;
                        if (player.VehicleSeat != -1) return;


                        var bankmoney = player.GetData("BankMoney");
                        var miete = 100;
                        var konto = bankmoney - miete;
                        player.SetData("BankMoney", konto);
                        API.SendPictureNotificationToPlayer(player, "LKW Miete:\n Summe: $" + miete.ToString() + " - \n Kontostand: $" + konto.ToString(), "CHAR_BANK_MAZE", 1, 1, "Spedition Perez", "Abbuchung");

                    }
                }
            });
        }

        private void OnVehicleTrailerChange(NetHandle tower, NetHandle trailer)
        {
            var truckInfo = GetTruckInfoByNetHandle(tower);
            if (truckInfo == null) return;

            var player = truckInfo.GetDriver();
            var trailerInfo = GetTrailerInfoByNetHandle(trailer);

            // Attached to unknown trailer
            if (trailerInfo == null)
            {
                TogglePlayerDestinationBlip(player, truckInfo, false);
                return;
            }

            truckInfo.Trailer = trailerInfo;
            trailerInfo.NotifyTraileredBy(truckInfo);

            TogglePlayerDestinationBlip(player, truckInfo, true);
        }

        private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
        {
            var truckInfo = GetTruckInfoByNetHandle(vehicle);
            if (truckInfo == null)
            {
                return;
            }

            if (truckInfo.Trailer == null) return;

            TogglePlayerDestinationBlip(player, truckInfo, true);
        }

        private void OnPlayerExitVehicle(Client player, NetHandle vehicle)
        {
            var truckInfo = GetTruckInfoByNetHandle(vehicle);
            if (truckInfo == null || truckInfo.Trailer == null) return;

            TogglePlayerDestinationBlip(player, truckInfo, false);
        }

        private TruckInfo GetTruckInfoByNetHandle(NetHandle netHandle)
        {
            return _trucks.FirstOrDefault(t => t.Vehicle == netHandle);
        }

        private TrailerInfo GetTrailerInfoByNetHandle(NetHandle netHandle)
        {
            return _trailers.FirstOrDefault(t => t.Vehicle == netHandle);
        }

        private void TogglePlayerDestinationBlip(Client player, TruckInfo truck, bool showBlip)
        {
            API.TriggerClientEvent(player,ClientSetDestinationEventName, showBlip ? truck.Trailer.Destination : null);
        }

        private void SpawnTrailers()
        {
            var unhookedTrailerCount = _trailers.Count(t => t.Vehicle.TraileredBy == null);

            for (var i = unhookedTrailerCount; i < UnhookedTrailerSpawnCount; i++)
            {
                SpawnTrailerRandomly();
            }
        }

        private void SpawnTrailerRandomly()
        {
            var spawnPoint = Trailers.Shuffle().FirstOrDefault<SpawnPoint>(sp => sp.Type == SpawnPointTypes.Trailer && !AreVehiclesInRange(sp.Position, SpawnAreaClearanceRange));
            if (spawnPoint == null)
            {
                return;
            }

            var targetPoint = Destinations[_random.Next(0, Destinations.Count())];
            if (targetPoint == null)
            {
                return;
            }

            var trailerHash = _trailerHashes[_random.Next(0, _trailerHashes.Length)];
            int money = new Random().Next(1000, 2000);
            var trailerInfo = new TrailerInfo(API.CreateVehicle(trailerHash, spawnPoint.Position, spawnPoint.Rotation.Z, new Color(0,0,0), new Color(0, 0, 0), "RageOS"), targetPoint.Position, money);
            //trailerInfo.Deleted += TrailerDeleted;
            //trailerInfo.EnteredDestination += TrailerEnteredDestination;
            //trailerInfo.DetachedOnDestination += TrailerDetachedOnDestination;

            _trailers.Add(trailerInfo);
        }

        private void TrailerDeleted(TrailerInfo trailerInfo)
        {
            API.ConsoleOutput("TrailerDeleted");
            _trailers.Remove(trailerInfo);
        }

        private void TrailerEnteredDestination(TrailerInfo trailerInfo)
        {
            var driver = trailerInfo.GetTrailerDriver();
            API.SendNotificationToPlayer(driver, "Drücke H um den Anhänger abzukoppeln!");
        }

        private void TrailerDetachedOnDestination(TruckInfo truckInfo, TrailerInfo trailerInfo)
        {
            API.ConsoleOutput("TrailerDetachedOnDestination");
            var driver = truckInfo.GetDriver();
            if (driver == null) return;
            if (driver.VehicleSeat != -1) return;
            double bankmoney = double.Parse(driver.GetData("Money").ToString());
            double cash = bankmoney + trailerInfo.Money;
            driver.SetData("Money", cash);
            API.SendPictureNotificationToPlayer(driver, "Von: Spedition Perez \n Summe: $" + trailerInfo.Money.ToString() + " - \n Bargeld: $" + cash.ToString(), "CHAR_BANK_MAZE", 1, 1, "Spedition Perez", "Zahlungseingang");
            trailerInfo.Delete();
        }

        private bool AreVehiclesInRange(Vector3 position, float range)
        {
            return API.GetAllVehicles().Any(v => API.Shared.GetEntityPosition(v).DistanceTo(position) <= range);
        }

        [Command("truck")]
        public void SpawnAdminTruck(Client player)
        {
            if (!checkAdminPermission(player, 1))
                return;
            SpawnTruck(player);
        }

        public void SpawnTruck(Client player)
        {
            var truck = CreateTruck(player.Position, player.Rotation.Z);
            truck.Vehicle.SetSyncedData("fuel", 100);
            truck.Vehicle.SetData("OWNER", player.SocialClubName);
            truck.Vehicle.SetData("isAdminCar", false);
            truck.Vehicle.SetData("OBJVEHICLE", truck.Vehicle);
            truck.Vehicle.EngineStatus = false;
            player.SetIntoVehicle(truck.Vehicle, -1);

            player.SendNotification("Du hat einen Schlüssel zu deinem Truck erhalten!");
            truck.Vehicle.SetData("KeyOwners", new List<string>() { player.SocialClubName });
        }

        public static bool checkAdminPermission(Client player, int adminlevel)
        {
            int myLevel = player.GetData("AdminLevel");
            if (myLevel >= adminlevel)
            {
                return true;
            }
            API.Shared.SendChatMessageToPlayer(player, errorAdmin);
            return false;
        }

        public TruckInfo CreateTruck(Vector3 position, float rotation)
        {
            var truckHash = _truckHashes[_random.Next(0, _truckHashes.Length)];
            var truck = new TruckInfo(API.CreateVehicle(truckHash, position, rotation, new Color(0,0,0), new Color(0,0,0)));
            truck.Deleted += TruckDeleted;
            API.SetVehicleEngineStatus(truck.Vehicle, false);

            _trucks.Add(truck);

            return truck;
        }

        private void TruckDeleted(TruckInfo truckInfo)
        {
            API.ConsoleOutput("TruckDeleted");
            _trucks.Remove(truckInfo);
        }
    }
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            return list.OrderBy(l => Guid.NewGuid());
        }
    }
}
