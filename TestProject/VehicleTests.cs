using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;
using NUnit.Framework;

namespace TestProject
{
    public class VehicleTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateVehicle()
        {
            var model = "Tesla Model S";
            var numOfSeats = 4;
            var numOfDoors = 4;
            var transmissionType = TransmissionType.Automatic;
            var electricVehicleInfo = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5 };
            GasVehicle? gasVehicleInfo = null;
            var offer = new Offer(120.5m, "Electric car rental", 25, new PassengerCar(), new List<Address>(){new Address("city", "Street", 1, "PostalCode")});
            var host = new Host();

            var vehicle = new Truck(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host, 10.0);

            Assert.That(vehicle.Model, Is.EqualTo(model));
            Assert.That(vehicle.NumOfSeats, Is.EqualTo(numOfSeats));
            Assert.That(vehicle.NumOfDoors, Is.EqualTo(numOfDoors));
            Assert.That(vehicle.TransmissionType, Is.EqualTo(transmissionType));
            Assert.That(vehicle.ElectricVehicleInfo, Is.EqualTo(electricVehicleInfo));
            Assert.IsTrue(vehicle.IsElectric);
            Assert.IsFalse(vehicle.IsGas);
            Assert.That(vehicle.Offer, Is.EqualTo(offer));
            Assert.That(vehicle.Host, Is.EqualTo(host));
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var numOfSeats = 4;
            var numOfDoors = 4;
            var transmissionType = TransmissionType.Automatic;
            var electricVehicleInfo = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5 };
            var host = new Host();

            // Missing Model
            Assert.Throws<ValidationException>(() => new Truck(null, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, null, null, host, 10.0));

            // Missing NumOfSeats
            Assert.Throws<ValidationException>(() => new Truck("Model X", 0, numOfDoors, transmissionType, electricVehicleInfo, null, null, host, 10.0));

            // Missing NumOfDoors
            Assert.Throws<ValidationException>(() => new Truck("Model X", numOfSeats, 0, transmissionType, electricVehicleInfo, null, null, host, 10.0));

            // Missing Host
            Assert.Throws<ValidationException>(() => new Truck("Model X", numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, null, null, null, 10.0));
        }

        [Test]
        public void IsElectric_ShouldReturnTrue_WhenElectricVehicleInfoIsNotNull()
        {
            var vehicle = new Truck("Tesla Model S", 4, 4, TransmissionType.Automatic, new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5 }, null, null, new Host(), 10.0);
            Assert.IsTrue(vehicle.IsElectric);
        }

        [Test]
        public void IsGas_ShouldReturnTrue_WhenGasVehicleInfoIsNotNull()
        {
            var vehicle = new Truck("Toyota Camry", 4, 4, TransmissionType.Automatic, null, new GasVehicle { FuelType = "Petrol", FuelConsumption = 10 }, null, new Host(), 10.0);
            Assert.IsTrue(vehicle.IsGas);
        }

        [Test]
        public void AdditionalFeatures_ShouldBeEmptyByDefault()
        {
            var vehicle = new Truck("Tesla Model S", 4, 4, TransmissionType.Automatic, null, null, null, new Host(), 10.0);
            Assert.IsEmpty(vehicle.AdditionalFeatures);
        }

        [Test]
        public void AddFeature_ShouldAddFeatureToList()
        {
            var vehicle = new Truck("Tesla Model S", 4, 4, TransmissionType.Automatic, null, null, null, new Host(), 10.0);
            vehicle.AdditionalFeatures.Add("GPS");
            Assert.Contains("GPS", vehicle.AdditionalFeatures);
        }
    }
}
