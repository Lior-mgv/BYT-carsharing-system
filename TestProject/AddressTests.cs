using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;
using NUnit.Framework;

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
            Assert.Throws<ValidationException>(() => new Address(null, "Main Street", 123, "10001"));
            Assert.Throws<ValidationException>(() => new Address("New York", null, 123, "10001"));
            Assert.Throws<ValidationException>(() => new Address("New York", "Main Street", 0, "10001"));
            Assert.Throws<ValidationException>(() => new Address("New York", "Main Street", 123, null));
        }

        [Test]
        public void Constructor_InvalidBuildingNumber_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new Address("New York", "Main Street", -1, "10001"));
        }

        [Test]
        public void AddOffer_ValidOffer_ShouldAddToOffersList()
        {
            var city = "New York";
            var street = "Main Street";
            var buildingNumber = 123;
            var postalCode = "10001";
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var address = new Address(city, street, buildingNumber, postalCode);

            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>{address}, host);


            Assert.Contains(offer, address.Offers);
        }

        [Test]
        public void AddOffer_DuplicateOffer_ShouldThrowException()
        {
            var address = new Address("New York", "Main Street", 123, "10001");
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
var host = new Host(user);
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address> {address }, host);

            Assert.Throws<InvalidOperationException>(() => address.AddOffer(offer));
        }

        [Test]
        public void RemoveOffer_ValidOffer_ShouldRemoveFromOffersList()
        {
            var address = new Address("New York", "Main Street", 123, "10001");
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
var host = new Host(user);
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>{address}, host);

            var result = address.RemoveOffer(offer);

            Assert.IsTrue(result);
            Assert.IsEmpty(address.Offers);
        }

        [Test]
        public void DeleteAddress_ShouldRemoveAllAssociatedOffers()
        {
            var address = new Address("New York", "Main Street", 123, "10001");
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
var host = new Host(user);
            var vehicle = new PassengerCar();
            var offer1 = new Offer(100.0m, "Description1", 18, vehicle, new List<Address>{address}, host);
            var offer2 = new Offer(150.0m, "Description2", 20, vehicle, new List<Address>{address}, host);


            address.DeleteAddress(address);

            Assert.IsEmpty(address.Offers);
        }
    }
}
