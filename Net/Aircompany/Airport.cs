using Aircompany.Models;
using Aircompany.Planes;
using System.Collections.Generic;
using System.Linq;

namespace Aircompany
{
    public class Airport
    {
        private List<Plane> _planes;

        public Airport(IEnumerable<Plane> planes) => _planes = planes.ToList();

        public List<Plane> Planes => _planes;

        public List<PassengerPlane> GetPassengerPlanes()
        {
            var passengerPlanes = new List<PassengerPlane>();
            foreach (var plane in _planes)
                if (plane is PassengerPlane)
                    passengerPlanes.Add(plane as PassengerPlane);
            return passengerPlanes;
        }

        public List<MilitaryPlane> GetMilitaryPlanes()
        {
            var militaryPlanes = new List<MilitaryPlane>();
            foreach (var plane in _planes)
                if (plane is MilitaryPlane)
                    militaryPlanes.Add(plane as MilitaryPlane);
            return militaryPlanes;
        }

        public List<MilitaryPlane> GetMilitaryPlanesWithType(MilitaryType militaryPlaneType)
        {
            var militaryPlanesWithType = new List<MilitaryPlane>();
            var militaryPlanes = GetMilitaryPlanes();

            foreach (var plane in militaryPlanes)
                if (plane.Type == militaryPlaneType)
                    militaryPlanesWithType.Add(plane);

            return militaryPlanesWithType;
        }

        public PassengerPlane GetPassengerPlaneWithMaxPassengersCapacity() =>
           GetPassengerPlanes().Aggregate((m, c) => m.PassengersCapacity > c.PassengersCapacity ? m : c);

        public Airport SortByMaxDistance() => new Airport(_planes.OrderBy(p => p.MaxFlightDistance));

        public Airport SortByMaxSpeed() => new Airport(_planes.OrderBy(p => p.MaxSpeed));

        public Airport SortByMaxLoadCapacity() => new Airport(_planes.OrderBy(p => p.MaxLoadCapacity));

        public override string ToString() => $"Airport: planes = {string.Join(", ", _planes.Select(x => x.Model))}\n";
    }
}
