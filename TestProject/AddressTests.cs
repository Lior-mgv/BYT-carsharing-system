using System;
using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;
using CarsharingSystem.Services;
using NUnit.Framework;

namespace TestProject
{
    public class AddressTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateAddress()
        {
            var city = "New York";
            var buildingNumber = 123;
            var postalCode = "10001";

            var address = new Address(city, buildingNumber, postalCode);

            Assert.AreEqual(city, address.City);
            Assert.AreEqual(buildingNumber, address.BuildingNumber);
            Assert.AreEqual(postalCode, address.PostalCode);
            Assert.IsEmpty(address.Offers);
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new Address(null, 123, "10001"));
            Assert.Throws<ValidationException>(() => new Address("New York", 0, "10001"));
            Assert.Throws<ValidationException>(() => new Address("New York", 123, null));
        }

        [Test]
        public void Constructor_InvalidBuildingNumber_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new Address("New York", -1, "10001"));
        }

        [Test]
        public void Constructor_ValidOffer_ShouldAddToOffersList()
        {
            var city = "New York";
            var buildingNumber = 123;
            var postalCode = "10001";
            var address = new Address(city, buildingNumber, postalCode);

            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle);
            address.Offers.Add(offer);

            Assert.Contains(offer, address.Offers);
        }
    }
}