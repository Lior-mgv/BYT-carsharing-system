using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;


namespace TestProject
{
    public class OfferTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateOffer()
        {
            var pricePerDay = 100m;
            var description = "A comfortable car for long trips.";
            var minimalAge = 18;
            var vehicle = new PassengerCar();

            var offer = new Offer(pricePerDay, description, minimalAge, vehicle);

            Assert.That(offer.PricePerDay, Is.EqualTo(pricePerDay));
            Assert.That(offer.Description, Is.EqualTo(description));
            Assert.That(offer.MinimalAge, Is.EqualTo(minimalAge));
            Assert.That(offer.Vehicle, Is.EqualTo(vehicle));
            Assert.IsEmpty(offer.OfferReviews);
            Assert.IsEmpty(offer.Addresses);
            Assert.IsEmpty(offer.Bookings);
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            // Missing PricePerDay
            Assert.Throws<ValidationException>(() => new Offer(0, "Description", 18, vehicle));

            // Missing Vehicle
            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 18, null));
        }

        [Test]
        public void Constructor_InvalidPricePerDay_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            Assert.Throws<ValidationException>(() => new Offer(-1, "Description", 18, vehicle));
        }

        [Test]
        public void Constructor_InvalidMinimalAge_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 12, vehicle));
            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 30, vehicle));
        }
    }
}
