using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class BoxVanTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateBoxVan()
        {
            var model = "Ford Transit";
            var numOfSeats = 3;
            var numOfDoors = 4;
            var transmissionType = TransmissionType.Manual;
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var boxVolume = 20.0;

            var boxVan = new BoxVan(model, numOfSeats, numOfDoors, transmissionType, null, null, null, host, boxVolume);

            Assert.That(boxVan.Model, Is.EqualTo(model));
            Assert.That(boxVan.NumOfSeats, Is.EqualTo(numOfSeats));
            Assert.That(boxVan.NumOfDoors, Is.EqualTo(numOfDoors));
            Assert.That(boxVan.TransmissionType, Is.EqualTo(transmissionType));
            Assert.That(boxVan.Host, Is.EqualTo(host));
            Assert.That(boxVan.BoxVolume, Is.EqualTo(boxVolume));
        }

        [Test]
        public void Constructor_InvalidBoxVolume_ShouldThrowValidationException()
        {
            var model = "Ford Transit";
            var numOfSeats = 3;
            var numOfDoors = 4;
            var transmissionType = TransmissionType.Manual;
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var invalidBoxVolume = 0.0; // Invalid box volume, must be greater than 0

            Assert.Throws<ValidationException>(() =>
                new BoxVan(model, numOfSeats, numOfDoors, transmissionType, null, null, null, host, invalidBoxVolume));
        }
    }
}