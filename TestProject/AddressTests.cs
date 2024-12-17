using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;


namespace TestProject
{
    public class AddressTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateAddress()
        {
            var city = "New York";
            var street = "Main Street";
            var buildingNumber = 123;
            var postalCode = "10001";

            var address = new Address(city, street, buildingNumber, postalCode);

            Assert.AreEqual(city, address.City);
            Assert.AreEqual(street, address.Street);
            Assert.AreEqual(buildingNumber, address.BuildingNumber);
            Assert.AreEqual(postalCode, address.PostalCode);
            Assert.IsEmpty(address.Offers);
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new Address(null, "Main Street",123, "10001"));
            Assert.Throws<ValidationException>(() => new Address("New York", null, 123, "10001"));
            Assert.Throws<ValidationException>(() => new Address("New York", "Main Street", 0, "10001"));
            Assert.Throws<ValidationException>(() => new Address("New York", "Main Street", 123, null));
        }

        [Test]
        public void Constructor_InvalidBuildingNumber_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new Address("New York", "Main Street",  -1, "10001"));
        }

        [Test]
        public void Constructor_ValidOffer_ShouldAddToOffersList()
        {
            var city = "New York";
            var street = "Main Street";
            var buildingNumber = 123;
            var postalCode = "10001";
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890");
            var host = new Host(user);
            var address = new Address(city, street, buildingNumber, postalCode);

            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host);
            address.AddOffer(offer);

            Assert.Contains(offer, address.Offers);
        }
    }
}