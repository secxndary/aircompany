using Aircompany;
using Aircompany.Models;
using Aircompany.Planes;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Newtonsoft.Json;

namespace AircompanyTests.Tests
{
    [TestFixture]
    public class AirportTest
    {
        private List<Plane> planes = new List<Plane>();

        [OneTimeSetUp]
        public void GetPlanesFromJson_OneTimeSetup()
        {
            var streamReaderPassengerPlanesJson = new StreamReader("Aircompany/JSON/passengerPlanes.json");
            var streamReaderMilitaryPlanesJson = new StreamReader("Aircompany/JSON/militaryPlanes.json");

            var passengerPlanes = JsonConvert.DeserializeObject<List<PassengerPlane>>(streamReaderPassengerPlanesJson.ReadToEnd());
            var militaryPlanes = JsonConvert.DeserializeObject<List<MilitaryPlane>>(streamReaderMilitaryPlanesJson.ReadToEnd());

            foreach (var plane in passengerPlanes)
                planes.Add(plane);
            foreach (var plane in militaryPlanes)
                planes.Add(plane);
        }

        [Test]
        public void MilitaryPlanesWithTransportType_ShouldBeContained_InMilitaryPlanesList()
        {
            var militaryPlanes = new Airport(planes);
            var transportMilitaryPlanes = militaryPlanes.GetMilitaryPlanesWithType(MilitaryType.TRANSPORT);
            Assert.IsNotEmpty(transportMilitaryPlanes);
        }

        [Test]
        public void GetPassengerPlaneWithMaxPassengersCapacity_ShouldReturn_ActualPlaneWithMaxPassengersCapacity()
        {
            var airport = new Airport(planes);
            var expectedPassengerPlaneWithMaxPassengersCapacity = new PassengerPlane("Boeing-747", 980, 16100, 70500, 242);
            var actualPassengerPlaneWithMaxPassengersCapacity = airport.GetPassengerPlaneWithMaxPassengersCapacity();           
            Assert.AreEqual(actualPassengerPlaneWithMaxPassengersCapacity, expectedPassengerPlaneWithMaxPassengersCapacity);
        }

        [Test]
        public void SortByMaxLoadCapacity_ShouldBeEqual_WithManualSorting()
        {
            var airport = new Airport(planes);
            airport = airport.SortByMaxLoadCapacity();
            var planesSortedByMaxLoadCapacity = airport.Planes;
            bool nextPlaneMaxLoadCapacityIsHigherThanCurrent = true;

            for (int i = 0; i < planesSortedByMaxLoadCapacity.Count - 1; i++)
            {
                var currentPlane = planesSortedByMaxLoadCapacity[i];
                var nextPlane = planesSortedByMaxLoadCapacity[i + 1];
                if (currentPlane.MaxLoadCapacity > nextPlane.MaxLoadCapacity)
                    nextPlaneMaxLoadCapacityIsHigherThanCurrent = false;
            }
            Assert.IsTrue(nextPlaneMaxLoadCapacityIsHigherThanCurrent);
        }
    }
}
