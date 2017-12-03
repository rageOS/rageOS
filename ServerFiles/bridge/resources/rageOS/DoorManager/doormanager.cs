using System.Collections.Generic;
using System.IO;
using System.Linq;
using GTANetworkAPI;

namespace RageOS
{
    public class DoorManager : Script
    {
        private class ObjDoor
        {
            public double X { get; }
            public double Y { get; }
            public double Z { get; }
            public int Hash { get; }
            public Vector3 V3 { get; }
            public int ID { get; set; }

            public ObjDoor(double X, double Y, double Z, int Hash)
            {
                this.X = X;
                this.Y = Y;
                this.Z = Z;
                this.Hash = Hash;
                this.V3 = new Vector3(X, Y, Z);

                ID = int.MaxValue;      //Falls Tür nicht registriert wurde
            }
        }

        List<ObjDoor> doorList = new List<ObjDoor>()
        {
            //Info:         X           Y       Z      ModelHash
            //Clothing Shop Doors
            new ObjDoor(88.21735, -1390.482, 29.22895, 868499217),
            new ObjDoor(88.21735, -1390.482, 29.22895, -1148826190),
            //Load TEQUL-LA_LA
            new ObjDoor(-565.1712f, 276.6259f, 83.28626f, 993120320),
            new ObjDoor(-561.2866f, 293.5044f, 87.77851f, 993120320),
            //Mission Driven 24/7
            new ObjDoor(1392.927, 3599.469, 35.13078, -1212951353),
            new ObjDoor(1395.371, 3600.358, 35.13078, -1212951353),
            //Ammunition doors
            new ObjDoor(842.7685, -1024.539, 28.34478, -8873588),
            new ObjDoor(845.3694, -1024.539, 28.34478, 97297972),
            new ObjDoor(-662.6415, -944.325, 21.97915, -8873588),
            new ObjDoor(-665.2424, -944.3256, 21.97915, 97297972),
            new ObjDoor(810.5769, -2148.27, 29.76892, -8873588),
            new ObjDoor(813.1779, -2148.27, 29.76892, 97297972),
            new ObjDoor(18.572, -1115.495, 29.94694, -8873588),
            new ObjDoor(16.12787, -1114.606, 29.94694, 97297972),
            new ObjDoor(243.8379, -46.52324, 70.09098, -8873588),
            new ObjDoor(244.7275, -44.07911, 70.09098, 97297972),
            new ObjDoor(-1112.071, 2691.505, 18.70407, -8873588),
            new ObjDoor(-1114.009, 2689.77, 18.70407, 97297972),
            new ObjDoor(-3163.812, 1083.779, 20.98866, -8873588),
            new ObjDoor(-3164.845, 1081.392, 20.98866, 97297972),
            new ObjDoor(-324.273, 6077.109, 31.6047, -8873588),
            new ObjDoor(-326.1122, 6075.27, 31.6047, 97297972),
            new ObjDoor(1699.937, 3753.42, 34.85526, -8873588),
            new ObjDoor(1698.176, 3751.506, 34.85526, 97297972),
            new ObjDoor(2568.304, 303.3556, 108.8848, -8873588),
            new ObjDoor(2570.905, 303.3556, 108.8848, 97297972),
            //Barber
            new ObjDoor(-822.4442, -188.3924, 37.81895, 145369505),
            new ObjDoor(-823.2001, -187.0831, 37.81895, -1663512092),
            new ObjDoor(-29.86917, -148.1571, 57.22648, -1844444717),
            new ObjDoor(1932.952, 3725.154, 32.9944, -1844444717),
            new ObjDoor(132.5569, -1710.996, 29.44157, -1844444717),
            new ObjDoor(1207.873, -470.0363, 66.358, -1844444717),
            new ObjDoor(-280.7851, 6232.782, 31.84548, -1844444717),
            new ObjDoor(-1287.857, -1115.742, 7.140073, -1844444717),
            //Autohändler
            new ObjDoor(-37.33113, -1108.873, 26.7198, 1417577297),
            new ObjDoor(-39.13366, -1108.218, 26.7198, 2059227086),
            new ObjDoor(-60.54582, -1094.749, 26.88872, 1417577297),
            new ObjDoor(-59.89302, -1092.952, 26.88362, 2059227086),
            //Kleidungsläden
            new ObjDoor(82.38156, -1390.476, 29.52609, -1148826190),
            new ObjDoor(82.38156, -1390.752, 29.52609, 868499217),
            new ObjDoor(-1201.435, -776.8566, 17.99184, 1780022985),
            new ObjDoor(127.8201, -211.8274, 55.22751, 1780022985),
            new ObjDoor(617.2458, 2751.022, 42.75777, 1780022985),
            new ObjDoor(-3167.75, 1055.536, 21.53288, 1780022985),
            new ObjDoor(-715.6154, -157.2561, 37.67493, -1922281023),
            new ObjDoor(-716.6755, -155.42, 37.67493, -1922281023),
            new ObjDoor(-1456.201, -233.3682, 50.05648, -1922281023),
            new ObjDoor(-1454.782, -231.7927, 50.05649, -1922281023),
            new ObjDoor(-156.439, -304.4294, 39.99308, -1922281023),
            new ObjDoor(-157.1293, -306.4341, 39.99308, -1922281023),
            new ObjDoor(-816.7932, -1078.406, 11.47806, -1148826190),
            new ObjDoor(-818.7643, -1079.544, 11.47806, 868499217),
            new ObjDoor(418.5713, -808.674, 29.64108, -1148826190),
            new ObjDoor(418.5713, -806.3979, 29.64108, 868499217),
            new ObjDoor(1199.101, 2703.221, 38.37257, -1148826190),
            new ObjDoor(1196.825, 2703.221, 38.37257, 868499217),
            new ObjDoor(-1094.965, 2706.964, 19.25781, -1148826190),
            new ObjDoor(-1096.661, 2705.446, 19.25781, 868499217),
            new ObjDoor(1687.282, 4819.485, 42.21305, -1148826190),
            new ObjDoor(1686.983, 4821.741 , 42.21305, 868499217),
            new ObjDoor(-1.725257, 6515.914 , 32.02779, -1148826190),
            new ObjDoor(-0.05637074 , 6517.461, 32.02779, 868499217),
            // Tattoo
            new ObjDoor(-3167.789, 1074.867, 20.92086, 543652229),
            new ObjDoor(321.8085, 178.3599, 103.6782, 543652229),
            //Tuningwerkstatt
            new ObjDoor(-356.0905, -134.7714, 40.01295, -550347177),
            //Los Santos Customs Airport
            new ObjDoor(-1145.898, -1991.144, 14.18357, -550347177),
            //Los Santons Customs La Mesa
            new ObjDoor(723.116, -1088.831, 23.23201, 270330101),
            //Lesters Fabrik
            new ObjDoor(719.3818, -975.4185, 25.00572, -681066206),
            new ObjDoor(716.7808, -975.4207, 25.00572, 245182344),
            //Michales Haus
            new ObjDoor(-843.924, 159.0499, 66.80453, -2125423493),
            new ObjDoor(-848.8483, 178.7819, 69.85393, -1568354151),
            new ObjDoor(-815.4046, 185.6964, 73.97892, 30769481),
            new ObjDoor(-807.1749, 186.0868, 72.4251, -156364173),
            new ObjDoor(-816.4758, 178.5034, 72.22686, 159994461),
            new ObjDoor(-816.4114, 178.3506, 72.22687, -1686014385),
            new ObjDoor(-794.6723, 177.7837, 72.83529, 1245831483),
            new ObjDoor(-796.2883, 177.1557, 72.83533, -1454760130),
            new ObjDoor(-793.8602, 182.2625, 72.83525, 1245831483),
            new ObjDoor(-793.3856, 180.8772, 72.83527, -1454760130),
            //Bennys Garage
            new ObjDoor(-205.6828, -1310.683, 30.29572, -427498890),
        };

        public DoorManager()
        {
            API.OnEntityEnterColShape += ColShapeTrigger;
            API.OnClientEventTrigger += ClientEventTrigger;
            API.OnResourceStart += API_onResourceStart;
            API.OnUpdate += OnUpdate;
        }

        private void API_onResourceStart()
        {
            InitDoors();
        }

        private void OnUpdate()
        {
            UpdateDoors();
        }

        private void RegisterDoors()
        {
            foreach (ObjDoor door in doorList)
            {
                door.ID = RegisterDoor(door.Hash, door.V3);
            }

        }

        private void InitDoors()
        {
            RegisterDoors();

            foreach (ObjDoor door in doorList)
            {
                SetDoorState(door.ID, false, 0);
            }
        }

        private void UpdateDoors()
        {
            foreach (ObjDoor door in doorList)
            {
                RefreshDoorState(door.ID);
            }
        }

        private int _doorCounter;
        private Dictionary<int, ColShape> _doorColShapes = new Dictionary<int, ColShape>();

        private bool _debugStatus = true;

        public const ulong SET_STATE_OF_CLOSEST_DOOR_OF_TYPE = 0xF82D8F1926A02C3D;

        [Command("getdoor")]
        public void Debug_GetDoorCMD(Client sender)
        {
            if (!_debugStatus) return;

            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            sender.TriggerEvent("doormanager_debug");
        }

        [Command("getdoorex")]
        public void Debug_GetDoorExCMD(Client sender, string modelName)
        {
            if (!_debugStatus) return;

            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            var pos = API.GetEntityPosition(sender);

            sender.TriggerEvent("doormanager_finddoor_return", API.GetHashKey(modelName), pos);
        }

        [Command("createDoor")]
        public void Debug_CreateDoorCMD(Client sender, int model)
        {
            if (!_debugStatus) return;

            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            var pos = API.GetEntityPosition(sender);

            var id = RegisterDoor(model, pos);

            API.SendChatMessageToPlayer(sender, "Your door id is " + id);
        }

        [Command("createDoorEx")]
        public void Debug_CreateDoorExCMD(Client sender, string modelName, float x, float y, float z)
        {
            if (!_debugStatus) return;

            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            var pos = new Vector3(x, y, z);
            var model = API.GetHashKey(modelName);

            var id = RegisterDoor(model, pos);

            API.SendChatMessageToPlayer(sender, "Your door id is " + id);
        }

        [Command("setdoorstate")]
        public void Debug_SetDoorStateCMD(Client sender, int doorId, bool locked, float heading)
        {
            if (!_debugStatus) return;

            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            SetDoorState(doorId, locked, heading);
        }

        [Command("findobj")]
        public void Debug_FindDoorCMD(Client sender, int model)
        {
            if (!_debugStatus) return;

            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            var pos = API.GetEntityPosition(sender);

            sender.TriggerEvent("doormanager_finddoor", model, pos);
        }

        [Command("door")]
        public void IPL(Client sender)
        {
            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            //Hopefully all door objects :D
            int[] Doors =
            {
            -1884098102,
            -218019333,
            -850860783,
            357195902,
            34120519,
            -368655288,
            621101123,
            -31919505,
            1263238661,
            -1934393132,
            -1469164005,
            394409025,
            -1436200562,
            1853479348,
            1890297615,
            -1920147247,
            -894594569,
            -812777085,
            -63539571,
            110411286,
            2096238007,
            -667009138,
            1640157877,
            116180164,
            -415922858,
            2116359305,
            46734799,
            393167779,
            -1562944903,
            782871627,
            1356853431,
            -1483545996,
            -2009193533,
            -2051450263,
            1424372521,
            -1232996765,
            -1874351633,
            -2881618,
            224975209,
            330294775,
            -645206502,
            -284254006,
            -1290268385,
            -498077814,
            1450215542,
            1293907652,
            -134415992,
            -165117488,
            -459350339,
            1523529669,
            1596276849,
            -1454760130,
            1245831483,
            -403433025,
            1308911070,
            1071105235,
            -1635579193,
            277255495,
            -699424554,
            674546851,
            -204842037,
            -655196089,
            1713150633,
            -44475594,
            1183182250,
            1764111426,
            -1082334994,
            1056781042,
            -264464292,
            1291867081,
            913904359,
            -26664553,
            914592203,
            -582278602,
            1343686600,
            1742849246,
            2052512905,
            -1848368739,
            -1035763073,
            -190780785,
            -550347177,
            1500925016,
            1437126442,
            519594446,
            1413187371,
            1342464176,
            -948829372,
            338220432,
            1075555701,
            1107966991,
            -405152626,
            885756908,
            1781429436,
            -1248359543,
            1930882775,
            -1023447729,
            776184575,
            -1776185420,
            -197147162,
            368191321,
            254309271,
            2026076529,
            207200483,
            -2045308299,
            -42303174,
            393888353,
            -893114122,
            -1652821467,
            1013329911,
            -1212275031,
            -1223237597,
            -982531572,
            -728539053,
            -910962270,
            1946625558,
            2051508718,
            239492112,
            920306374,
            1888438146,
            272205552,
            -1992828732,
            1316648054,
            711901167,
            -227061755,
            1991494706,
            -1463743939,
            -1429437264,
            -1677789234,
            27033010,
            889818406,
            2120130511,
            838685283,
            -1020431159,
            -1447681559,
            1543931499,
            1701450624,
            340291898,
            -2041685008,
            -642608865,
            -1264354268,
            668467214,
            -502195954,
            1011598562,
            -891120940,
            -18398025,
            -872784146,
            -1306074314,
            -1890952940,
            1774846173,
            546827942,
            -2062889184,
            -176635891,
            130962589,
            1609617895,
            -752497691,
            1737094319,
            -1204251591,
            1373390714,
            996499903,
            1739173235,
            -5479653,
            761708175,
            456661554,
            -1428622127,
            -992845609,
            -1231371160,
            720693755,
            703855057,
            24002365,
            1833539318,
            -1902553960,
            1564471782,
            -1536154964,
            -725970636,
            827574885,
            308262790,
            -1904897132,
            -48868009,
            1818395686,
            2049718375,
            216030657,
            -2076287065,
            -374527357,
            -1857663329,
            -1867159867,
            911651337,
            -1116041313,
            1015012981,
            -1461908217,
            36810380,
            1427153555,
            283948267,
            -1859992197,
            320854256,
            -439452078,
            -1897431054,
            645774080,
            2120038965,
            899635523,
            -453852320,
            -684382235,
            -815851463,
            1795767067,
            -1042390945,
            645231946,
            1645674613,
            923341943,
            -1298716645,
            1350616857,
            1714199852,
            1425919976,
            9467943,
            1924030334,
            1150266519,
            1536367999,
            -849772278,
            -106323690,
            -2042007659,
            1755793225,
            239858268,
            749848321,
            -1666470363,
            -353187150,
            560831900,
            612934610,
            1956494919,
            964838196,
            961976194,
            1878909644,
            1709395619,
            19193616,
            -1572101598,
            161378502,
            -421709054,
            1282049587,
            -1844444717,
            -822900180,
            -1184592117,
            -1185205679,
            1438783233,
            -551608542,
            -311575617,
            -519068795,
            -1789571019,
            -1716946115,
            -1922281023,
            1780022985,
            -710818483,
            14722111,
            -283574096,
            -770740285,
            1653893025,
            2059227086,
            1417577297,
            1693207013,
            -664582244,
            868499217,
            -1148826190,
            -1207991715,
            -2083448347,
            -1726331785,
            1248599813,
            -1421582160,
            -1037226769,
            -2069558801,
            -543490328,
            -1474383439,
            -1881825907,
            -495720969,
            -1230442770,
            -1091549377,
            1770281453,
            520341586,
            -610054759,
            2000998394,
            -431157263,
            56642071,
            -1679881977,
            -1045015371,
            1104171198,
            -1425071302,
            969847031,
            1980513646,
            -359451089,
            -64988855,
            781635019,
            1413743677,
            308207762,
            -1154592059,
            -90456267,
            -1517873911,
            -2051651622,
            1709345781,
            -1821777087,
            -538477509,
            -1083130717,
            -1225363909,
            1219957182,
            -853859998,
            -662750590,
            -726591477,
            -1932297301,
            -1375589668,
            -1240156945,
            452874391,
            -8873588,
            97297972,
            -1152174184,
            73386408,
            -129553421,
            1242124150,
            -1033001619,
            -340230128,
            -1663512092,
            145369505,
            1436076651,
            1335309163,
            486670049,
            1145337974,
            -1647153464,
            190770132,
            747286790,
            -352193203,
            -995467546,
            -1815392278,
            2088680867,
            -1563640173,
            1204471037,
            159994461,
            -1686014385,
            -794543736,
            -384976104,
            -1663022887,
            574422567,
            1558432213,
            -320948292,
            458025182,
            320433149,
            -1215222675,
            -522504255,
            -1320876379,
            -543497392,
            1557126584,
            185711165,
            -131296141,
            1378348636,
            -1032171637,
            -52575179,
            736699661,
            -228773386,
            1504256620,
            262671971,
            174080689,
            362975687,
            812467272,
            -2023754432,
            1099436502,
            -1627599682,
            -1586611409,
            -199073634,
            202981272,
            1117236368,
            -626684119,
            1289778077,
            993120320,
            757543979,
            1083279016,
            -1501157055,
            -1765048490,
            -2030220382,
            1544229216,
            1388116908,
            551491569,
            933053701,
            1173348778,
            -658747851,
            1335311341,
            -681066206,
            245182344,
            1804626822,
            865041037,
            -1775213343,
            426403179,
            543652229,
            1243635233,
            464151082,
            -1977105237,
            -522980862,
            -1128607325,
            1575804630,
            -607040053,
            -122922994,
            -267021114,
            -1743257725,
            -1241212535,
            878161517,
            192682307,
            1196685123,
            997554217,
            -952356348,
            374758529,
            1415151278,
            580361003,
            1859711902,
            1194028902,
            479144380,
            995767216,
            -868672903,
            2065277225,
            825720073,
            -1212951353,
            -251167274,
            582134182,
            270330101,
            -1246222793,
            -427498890

        };
            foreach (int Door in Doors)
            {
                sender.TriggerEvent("doormanager_finddoor_ex", Door);
            }
        }
        object[] currargs;
        [Command("y")]
        public void Save(Client sender)
        {
            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            File.AppendAllText("doors.txt", currargs[0].ToString() + " | " + currargs[1].ToString() + "\n  ");
            API.SendChatMessageToPlayer(sender, "Model " + currargs[0].ToString() + " saved");
        }

        [Command("findobjname")]
        public void Debug_FindDoorByNameCMD(Client sender, string modelName)
        {
            if (!_debugStatus) return;

            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            var model = API.GetHashKey(modelName);

            var pos = API.GetEntityPosition(sender);

            sender.TriggerEvent("doormanager_finddoor", model, pos);
        }

        [Command("transition")]
        public void Debug_TransitionDoorCMD(Client sender, int doorid, float target, int time)
        {
            if (!PlayerCommands.CheckAdminPermission(sender, 2))
                return;

            TransitionDoor(doorid, target, time);
        }

        private void ClientEventTrigger(Client sender, string eventName, object[] args)
        {
            if (eventName == "doormanager_debug_createdoor")
            {
                if (!_debugStatus) return;

                var model = (int)args[0];
                var pos = (Vector3)args[1];

                var id = RegisterDoor(model, pos);

                API.SendChatMessageToPlayer(sender, "Your door id is " + id);
            }
            else if (eventName == "SaveModel")
            {
                currargs = args;
            }
        }
               

        private void ColShapeTrigger(ColShape colshape, NetHandle entity)
        {
            var player = API.GetPlayerFromHandle(entity);

            if (player == null) return;

            if (colshape != null && colshape.GetData("IS_DOOR_TRIGGER") == true)
            {
                var id = colshape.GetData("DOOR_ID");
                var info = colshape.GetData("DOOR_INFO");

                float heading = 0f;

                if (info.State != null) heading = info.State;

                API.SendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                    info.Hash, info.Position.X, info.Position.Y, info.Position.Z,
                    info.Locked, heading, false);
            }
        }

        /* EXPORTED METHODS */
        public int RegisterDoor(int modelHash, Vector3 position)
        {
            var colShapeId = ++_doorCounter;

            var info = new DoorInfo()
            {
                Hash = modelHash,
                Position = position,
                Locked = false, // Open by default;
                Id = colShapeId,
                State = 0
            };
            var colShape = API.CreateSphereColShape(position, 35f);
            colShape.SetData("DOOR_INFO", info);
            colShape.SetData("DOOR_ID", colShapeId);
            colShape.SetData("IS_DOOR_TRIGGER", true);

            _doorColShapes.Add(colShapeId, colShape);

            return colShapeId;
        }

        public void TransitionDoor(int doorId, float finish, int ms)
        {
            if (_doorColShapes.ContainsKey(doorId))
            {
                var info = _doorColShapes[doorId].GetData("DOOR_INFO");

                info.Locked = true;

                var players = API.GetPlayersInRadiusOfPosition(10, _doorColShapes[doorId].Position);

                foreach (var player in players)
                {
                    if (player == null) continue;

                    API.TriggerClientEvent(player, "doormanager_transitiondoor",
                        info.Hash, info.Position, info.State, finish, ms);
                }

                info.State = finish;
            }
        }

        public void RefreshDoorState(int doorId)
        {
            if (_doorColShapes.ContainsKey(doorId))
            {
                var info = _doorColShapes[doorId].GetData("DOOR_INFO");

                float heading = info.State;

                var players = API.GetPlayersInRadiusOfPosition(10, _doorColShapes[doorId].Position);

                foreach (var player in players)
                {
                    if (player == null) continue;

                    API.SendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                        info.Hash, info.Position.X, info.Position.Y, info.Position.Z,
                        info.Locked, heading, false);
                }
            }
        }

        public void RemoveDoor(int id)
        {
            if (_doorColShapes.ContainsKey(id))
            {
                API.DeleteColShape(_doorColShapes[id]);
                _doorColShapes.Remove(id);
            }
        }

        public void SetDoorState(int doorId, bool locked, float heading)
        {
            if (_doorColShapes.ContainsKey(doorId))
            {
                var door = _doorColShapes[doorId];
                var data = door.GetData("DOOR_INFO");
                data.Locked = locked;
                data.State = heading;

                door.SetData("DOOR_INFO", data);

                var players = API.GetPlayersInRadiusOfPosition(10, door.Position);

                foreach (var player in players)
                {
                    if (player == null) continue;

                    float cH = data.State;

                    API.SendNativeToPlayer(player, SET_STATE_OF_CLOSEST_DOOR_OF_TYPE,
                        data.Hash, data.Position.X, data.Position.Y, data.Position.Z,
                        data.Locked, cH, false);
                }
            }
        }

        public int GetCloseDoor(Client player)
        {
            var localCopy = new Dictionary<int, ColShape>(_doorColShapes);
            foreach (var sh in localCopy)
            {
                if (API.IsPointWithinColshape(sh.Value, player.Position))
                {
                    return sh.Key;
                }
            }
            return 0;
        }

        public int[] GetAllCloseDoors(Client player)
        {
            var localCopy = new Dictionary<int, ColShape>(_doorColShapes);
            var list = new List<int>();
            foreach (var sh in localCopy)
            {
                if (API.IsPointWithinColshape(sh.Value, player.Position))
                {
                    list.Add(sh.Key);
                }
            }

            return list.ToArray();
        }

        public void SetDebug(bool status)
        {
            _debugStatus = status;
        }
    }
}

public struct DoorInfo
{
    public int Hash;
    public Vector3 Position;
    public int Id;

    public bool Locked;
    public float State;
}