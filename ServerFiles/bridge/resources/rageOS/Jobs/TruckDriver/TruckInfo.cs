using System.Linq;
using GTANetworkAPI;

namespace RageOS
{
    public class TruckInfo : Script
    {
        public Vehicle Vehicle { get; }

        public TrailerInfo Trailer { get; set; }

        public delegate void DeletedHandler(TruckInfo truckInfo);
        public event DeletedHandler Deleted;

        public TruckInfo()
        {

        }

        public TruckInfo(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }

        public void Delete()
        {
            if (!Vehicle.Exists) return;

            // Remove occupants
            foreach (var occupant in Vehicle.Occupants)
            {
                occupant.WarpOutOfVehicle();
            }

            if (Vehicle.Exists)
            {
                Vehicle.Delete();
            }

            Deleted?.Invoke(this);
        }


        public Client GetDriver()
        {
            return Vehicle.Occupants.FirstOrDefault(o => o.VehicleSeat == -1);
        }
    }
}
