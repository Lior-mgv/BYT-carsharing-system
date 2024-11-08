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
            var host = new Host();
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
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var model = "Ford Transit";
            var numOfSeats = 3;
            var numOfDoors = 4;
            var transmissionType = TransmissionType.Manual;
            var host = new Host();
            var offer = new Offer(100.0m, "Description", 18, new BoxVan());
            var boxVolume = 20.0;

            // Missing Model
            Assert.Throws<ValidationException>(() => new BoxVan(null, numOfSeats, numOfDoors, transmissionType, null, null, offer, host, boxVolume));
            
            // Missing NumOfSeats
            Assert.Throws<ValidationException>(() => new BoxVan(model, 0, numOfDoors, transmissionType, null, null, offer, host, boxVolume));
            
            // Missing NumOfDoors
            Assert.Throws<ValidationException>(() => new BoxVan(model, numOfSeats, 0, transmissionType, null, null, offer, host, boxVolume));
            
            // Missing TransmissionType
            Assert.Throws<ValidationException>(() => new BoxVan(model, numOfSeats, numOfDoors, default, null, null, offer, host, boxVolume));
            
            // Missing Host
            Assert.Throws<ValidationException>(() => new BoxVan(model, numOfSeats, numOfDoors, transmissionType, null, null, offer, null, boxVolume));
            
            // Missing BoxVolume (Invalid BoxVolume - 0)
            Assert.Throws<ValidationException>(() => new BoxVan(model, numOfSeats, numOfDoors, transmissionType, null, null, offer, host, 0));
        }

        [Test]
        public void Constructor_InvalidBoxVolume_ShouldThrowValidationException()
        {
            var model = "Ford Transit";
            var numOfSeats = 3;
            var numOfDoors = 4;
            var transmissionType = TransmissionType.Manual;
            var host = new Host();
            var invalidBoxVolume = 0.0; // Invalid box volume, must be greater than 0

            Assert.Throws<ValidationException>(() =>
                new BoxVan(model, numOfSeats, numOfDoors, transmissionType, null, null, null, host, invalidBoxVolume));
        }
    }
}