using System;
using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class PassengerCarTests
    {
        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var model = "Toyota Corolla";
            var numOfSeats = 5;
            var numOfDoors = 4;
            var transmissionType = TransmissionType.Automatic;
            var gasVehicleInfo = new GasVehicle { FuelType = "Gasoline", FuelConsumption = 6 };
            var offer = new Offer(50, "Rental Offer", 18, new PassengerCar());
            var host = new Host();
            
            // Missing Model
            Assert.Throws<ValidationException>(() => new PassengerCar(null, numOfSeats, numOfDoors, transmissionType, null, gasVehicleInfo, offer, host));
            
            // Missing NumOfSeats
            Assert.Throws<ValidationException>(() => new PassengerCar(model, 0, numOfDoors, transmissionType, null, gasVehicleInfo, offer, host));
            
            // Missing NumOfDoors
            Assert.Throws<ValidationException>(() => new PassengerCar(model, numOfSeats, 0, transmissionType, null, gasVehicleInfo, offer, host));
            
            // Missing TransmissionType
            Assert.Throws<ValidationException>(() => new PassengerCar(model, numOfSeats, numOfDoors, default, null, gasVehicleInfo, offer, host)); 
            
            // Missing Host
            Assert.Throws<ValidationException>(() => new PassengerCar(model, numOfSeats, numOfDoors, transmissionType, null, gasVehicleInfo, offer, null));
        }
        

        [Test]
        public void Constructor_ValidParameters_ShouldCreatePassengerCar()
        {
            var model = "Toyota Corolla";
            var numOfSeats = 5;
            var numOfDoors = 4;
            var transmissionType = TransmissionType.Automatic;
            var gasVehicleInfo = new GasVehicle { FuelType = "Gasoline", FuelConsumption = 6 };
            var offer = new Offer(50, "Rental Offer", 18, new PassengerCar());
            var host = new Host();
            
            var passengerCar = new PassengerCar(model, numOfSeats, numOfDoors, transmissionType, null, gasVehicleInfo, offer, host);

            Assert.That(passengerCar.Model, Is.EqualTo(model));
            Assert.That(passengerCar.NumOfSeats, Is.EqualTo(numOfSeats));
            Assert.That(passengerCar.NumOfDoors, Is.EqualTo(numOfDoors));
            Assert.That(passengerCar.TransmissionType, Is.EqualTo(transmissionType));
            Assert.That(passengerCar.ElectricVehicleInfo, Is.EqualTo(null));
            Assert.That(passengerCar.GasVehicleInfo, Is.EqualTo(gasVehicleInfo));
            Assert.That(passengerCar.Offer, Is.EqualTo(offer));
            Assert.That(passengerCar.Host, Is.EqualTo(host));
        }
    }
}
