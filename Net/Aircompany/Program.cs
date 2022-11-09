using Aircompany.Planes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Aircompany
{
    public class Program
    {
        public static void Main()
        {
            //using (StreamWriter sw = new StreamWriter("../../JSON/militaryPlanes.json"))
            //{
            //    string json = JsonConvert.SerializeObject(planesPassenger, Formatting.Indented);
            //    Console.WriteLine(json);
            //    sw.Write(json);
            //}
            //using (var sr = new StreamReader("../../JSON/passengerplanes.json"))
            //{
            //    string json = sr.ReadToEnd();
            //    Console.WriteLine(json);
            //    planes = JsonConvert.DeserializeObject<List<Plane>>(json);
            //}

            var planes = new List<Plane>();
            var passengerPlanes = new List<PassengerPlane>();
            var militaryPlanes = new List<MilitaryPlane>();

            using (var streamReaderPassengerPlanesJson = new StreamReader("../../JSON/passengerPlanes.json"))
            {
                passengerPlanes = JsonConvert.DeserializeObject<List<PassengerPlane>>(streamReaderPassengerPlanesJson.ReadToEnd());
            }
            using (var streamReaderMilitaryPlanesJson = new StreamReader("../../JSON/militaryPlanes.json"))
            {
                militaryPlanes = JsonConvert.DeserializeObject<List<MilitaryPlane>>(streamReaderMilitaryPlanesJson.ReadToEnd());
            }

            foreach (var plane in passengerPlanes)
                planes.Add(plane);
            foreach (var plane in militaryPlanes)
                planes.Add(plane);

            var airport = new Airport(planes);
            var militaryAirport = new Airport(airport.GetMilitaryPlanes());
            var passengerAirport = new Airport(airport.GetPassengerPlanes());

            Console.WriteLine(militaryAirport.SortByMaxDistance());
            Console.WriteLine(passengerAirport.SortByMaxSpeed());
            Console.WriteLine(passengerAirport.GetPassengerPlaneWithMaxPassengersCapacity());
        }
    }
}
