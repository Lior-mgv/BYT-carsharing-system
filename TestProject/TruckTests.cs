using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class TruckTests
    {
        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var model = "Ford F-150";
            var numOfSeats = 3;
            var numOfDoors = 2;
            var transmissionType = TransmissionType.Manual;
            var electricVehicleInfo = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 10 };
            var gasVehicleInfo = new GasVehicle { FuelType = "Diesel", FuelConsumption = 15 };
            var offer = new Offer(80, "Heavy-duty Truck Rental", 21, new Truck());
            var host = new Host();
            var bedLength = 6.5;

            // Missing Model
            Assert.Throws<ValidationException>(() => new Truck(null, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host, bedLength));
            
            // Missing NumOfSeats
            Assert.Throws<ValidationException>(() => new Truck(model, 0, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host, bedLength));
            
            // Missing NumOfDoors
            Assert.Throws<ValidationException>(() => new Truck(model, numOfSeats, 0, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host, bedLength));
            
            // Missing TransmissionType
            Assert.Throws<ValidationException>(() => new Truck(model, numOfSeats, numOfDoors, default, electricVehicleInfo, gasVehicleInfo, offer, host, bedLength));
            
            // Missing Host
            Assert.Throws<ValidationException>(() => new Truck(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, null, bedLength));
            
            // Missing BedLength
            Assert.Throws<ValidationException>(() => new Truck(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host, 0));
        }
        
        [Test]
        public void Constructor_ValidParameters_ShouldCreateTruck()
        {
            var model = "Ford F-150";
            var numOfSeats = 3;
            var numOfDoors = 2;
            var transmissionType = TransmissionType.Manual;
            var electricVehicleInfo = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 10 };
            var gasVehicleInfo = new GasVehicle { FuelType = "Diesel", FuelConsumption = 15 };
            var offer = new Offer(80, "Heavy-duty Truck Rental", 21, new Truck());
            var host = new Host();
            var bedLength = 6.5;

            var truck = new Truck(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host, bedLength);

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
    }
}
