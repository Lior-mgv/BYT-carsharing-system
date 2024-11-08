using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class GasVehicleTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateGasVehicle()
        {
            var fuelType = "Gasoline";
            var fuelConsumption = 8;

            var gasVehicle = new GasVehicle
            {
                FuelType = fuelType,
                FuelConsumption = fuelConsumption
            };

            Assert.That(gasVehicle.FuelType, Is.EqualTo(fuelType));
            Assert.That(gasVehicle.FuelConsumption, Is.EqualTo(fuelConsumption));
        }

        [Test]
        public void FuelType_Missing_ShouldThrowValidationException()
        {
            var gasVehicle = new GasVehicle { FuelConsumption = 8 }; // Missing FuelType, should trigger validation error

            var context = new ValidationContext(gasVehicle);
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(gasVehicle, context, validateAllProperties: true));
        }

        [Test]
        public void FuelConsumption_InvalidValue_ShouldThrowValidationException()
        {
            var gasVehicle = new GasVehicle { FuelType = "Gasoline", FuelConsumption = 0 }; // Invalid, should be greater than 0

            var context = new ValidationContext(gasVehicle);
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(gasVehicle, context, validateAllProperties: true));
        }
    }
}