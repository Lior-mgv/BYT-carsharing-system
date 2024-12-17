using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class TruckTests
    {
        
        [Test]
        public void Constructor_ValidParameters_ShouldCreateTruck()
        {
            var model = "Ford F-150";
            var numOfSeats = 3;
            var numOfDoors = 2;
            var transmissionType = TransmissionType.Manual;
            var electricVehicleInfo = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 10 };
            var gasVehicleInfo = new GasVehicle { FuelType = "Diesel", FuelConsumption = 15 };
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890");
            var host = new Host(user);
            var bedLength = 6.5;

            var truck = new Truck(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, null, host, bedLength);
            var offer = new Offer(80, "Heavy-duty Truck Rental", 21, truck, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host(user));
            truck.Offer = offer;

            Assert.That(truck.Model, Is.EqualTo(model));
            Assert.That(truck.NumOfSeats, Is.EqualTo(numOfSeats));
            Assert.That(truck.NumOfDoors, Is.EqualTo(numOfDoors));
            Assert.That(truck.TransmissionType, Is.EqualTo(transmissionType));
            Assert.That(truck.ElectricVehicleInfo, Is.EqualTo(electricVehicleInfo));
            Assert.That(truck.GasVehicleInfo, Is.EqualTo(gasVehicleInfo));
            Assert.That(truck.Offer, Is.EqualTo(offer));
            Assert.That(truck.Host, Is.EqualTo(host));
            Assert.That(truck.BedLength, Is.EqualTo(bedLength));
        }
        
        [Test]
        public void Constructor_InvalidBedLength_ShouldThrowValidationException()
        {
            var model = "Ford F-150";
            var numOfSeats = 3;
            var numOfDoors = 2;
            var transmissionType = TransmissionType.Manual;
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890");
            var host = new Host(user);
            var invalidBedLength = 0.0;

            Assert.Throws<ValidationException>(() =>
                new BoxVan(model, numOfSeats, numOfDoors, transmissionType, null, null, null, host, invalidBedLength));
        }
    }
}
