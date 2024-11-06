using System;
using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;
using CarsharingSystem.Services;
using NUnit.Framework;

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

            Assert.AreEqual(pricePerDay, offer.PricePerDay);
            Assert.AreEqual(description, offer.Description);
            Assert.AreEqual(minimalAge, offer.MinimalAge);
            Assert.AreEqual(vehicle, offer.Vehicle);
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

        [Test]
        public void Constructor_ValidOfferReview_ShouldAddToOfferReviews()
        {
            var vehicle = new PassengerCar();
            var offer = new Offer(100, "Description", 18, vehicle);

            var offerReview = new OfferReview(default, 5,5,5,5, "Great car!", new Renter(), offer);
            offer.OfferReviews.Add(offerReview);

            Assert.Contains(offerReview, offer.OfferReviews);
        }

        [Test]
        public void Constructor_ValidAddress_ShouldAddToAddresses()
        {
            var vehicle = new PassengerCar();
            var offer = new Offer(100, "Description", 18, vehicle);

            var address = new Address("New York", 123, "10001");
            offer.Addresses.Add(address);

            Assert.Contains(address, offer.Addresses);
        }

        [Test]
        public void Constructor_ValidBooking_ShouldAddToBookings()
        {
            var vehicle = new PassengerCar();
            var offer = new Offer(100, "Description", 18, vehicle);

            var booking = new Booking(DateTime.Now, DateTime.Now.AddDays(3), 300m, BookingStatus.Pending, new Renter(), offer);
            offer.Bookings.Add(booking);

            Assert.Contains(booking, offer.Bookings);
        }
    }
}
