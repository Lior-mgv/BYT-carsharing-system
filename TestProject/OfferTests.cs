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
            var addresses = new List<Address>{new ("city", "Street", 1, "PostalCode")};
            var host = new Host();

            var offer = new Offer(pricePerDay, description, minimalAge, vehicle, addresses, host);

            Assert.That(offer.PricePerDay, Is.EqualTo(pricePerDay));
            Assert.That(offer.Description, Is.EqualTo(description));
            Assert.That(offer.MinimalAge, Is.EqualTo(minimalAge));
            Assert.That(offer.Vehicle, Is.EqualTo(vehicle));
            Assert.IsEmpty(offer.OfferReviews);
            Assert.That(offer.Addresses, Is.EqualTo(addresses));
            Assert.That(offer.Host, Is.EqualTo(host));
            Assert.IsEmpty(offer.Bookings);
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            // Missing PricePerDay
            Assert.Throws<ValidationException>(() => new Offer(0, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));

            // Missing Vehicle
            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 18, null, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));
            
            //Missing Host
            Assert.Throws<ValidationException>(() => new Offer(0, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, null));
        }

        [Test]
        public void Constructor_InvalidPricePerDay_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            Assert.Throws<ValidationException>(() => new Offer(-1, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));
        }

        [Test]
        public void Constructor_InvalidMinimalAge_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 12, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));
            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 30, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));
        }
    }
}
