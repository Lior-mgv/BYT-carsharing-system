using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class ElectricVehicleTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateElectricVehicle()
        {
            var batteryCapacity = 100;
            var chargingTime = 6;

            var electricVehicle = new ElectricVehicle
            {
                BatteryCapacity = batteryCapacity,
                ChargingTime = chargingTime
            };

            Assert.That(electricVehicle.BatteryCapacity, Is.EqualTo(batteryCapacity));
            Assert.That(electricVehicle.ChargingTime, Is.EqualTo(chargingTime));
        }

        [Test]
        public void BatteryCapacity_InvalidValue_ShouldThrowValidationException()
        {
            var electricVehicle = new ElectricVehicle { BatteryCapacity = 0 }; // Invalid, should be greater than 0

            var context = new ValidationContext(electricVehicle);
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(electricVehicle, context, validateAllProperties: true));
        }

        [Test]
        public void ChargingTime_InvalidValue_ShouldThrowValidationException()
        {
            var electricVehicle = new ElectricVehicle { ChargingTime = 0 }; // Invalid, should be greater than 0

            var context = new ValidationContext(electricVehicle);
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(electricVehicle, context, validateAllProperties: true));
        }
    }
}