using GTANetworkAPI;

namespace RageOS
{
    public class SpawnPoint
    {
        public SpawnPoint(Vector3 position, double rotation, SpawnPointTypes type)
        {
            Type = type;
            Position = position;
            Rotation = new Vector3(0, 0, rotation);
        }
        public SpawnPointTypes Type { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Rotation { get; set; }

    }
}
