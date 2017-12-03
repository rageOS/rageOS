using System.Linq;
using GTANetworkAPI;

namespace RageOS
{
    public class TrailerInfo : Script
    {
        private const float CollisionShapeSize = 10;

        private Blip _blip;
        private ColShape _destinationCollision;

        public Vehicle Vehicle { get; }

        public Vector3 Destination { get; }

        public bool IsOnDestination { get; private set; }
        public double Money { get; set; }

        public delegate void DeletedHandler(TrailerInfo trailerInfo);
        public event DeletedHandler Deleted;

        public delegate void EnteredDestinationHandler(TrailerInfo trailerInfo);
        public event EnteredDestinationHandler EnteredDestination;

        public delegate void DetachedOnDestinationHandler(TruckInfo truck, TrailerInfo trailerInfo);
        public event DetachedOnDestinationHandler DetachedOnDestination;

        private void ShowBlip(bool show)
        {
            if (!show && _blip == null) return;
            if (show && _blip != null) return;
            if (show && Vehicle == null || !Vehicle.Exists) return;

            if (show)
            {
                _blip = API.CreateBlip(Vehicle);
                _blip.Sprite = 479;
                _blip.ShortRange = true;
            }
            else
            {
                _blip.Delete();
                _blip = null;
            }
        }

        public TrailerInfo()
        {

        }

        public TrailerInfo(Vehicle vehicle, Vector3 destination, int money)
        {
            Vehicle = vehicle;
            Destination = destination;
            Money = money;

            _destinationCollision = API.CreateCylinderColShape(
                destination,
                CollisionShapeSize,
                CollisionShapeSize);

            _destinationCollision.OnEntityEnterColShape += EntityEnteredDestination;
            _destinationCollision.OnEntityExitColShape += EntityExitedDestination;

            ShowBlip(true);
        }

        private void EntityExitedDestination(ColShape shape, NetHandle entity)
        {
            if (entity != Vehicle || !Vehicle.Exists || Vehicle.Health <= 0) return;

            IsOnDestination = false;
        }

        private void EntityEnteredDestination(ColShape shape, NetHandle entity)
        {
            if (entity != Vehicle || !Vehicle.Exists || Vehicle.Health <= 0) return;

            IsOnDestination = true;
            EnteredDestination?.Invoke(this);
        }

        public void NotifyTraileredBy(TruckInfo truck)
        {
            ShowBlip(true);
        }

        public void NotifyTrailerDetached(TruckInfo truck)
        {
            if (IsOnDestination) DetachedOnDestination?.Invoke(truck, this);

            ShowBlip(false);
        }

        public void Delete()
        {
            API.DeleteColShape(_destinationCollision);
            ShowBlip(false);

            if (Vehicle.Exists)
            {
                Vehicle.Delete();
            }

            Deleted?.Invoke(this);
        }

        public Client GetTrailerDriver()
        {
            return Vehicle.TraileredBy?.Occupants.FirstOrDefault(o => o.VehicleSeat == -1);
        }
    }
}
