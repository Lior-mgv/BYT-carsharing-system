using System;
using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;
using CarsharingSystem.Services;
using NUnit.Framework;

namespace TestProject
{
    public class BookingTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateBooking()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(3);
            var totalPrice = 100m;
            var status = BookingStatus.Pending;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle);

            var booking = new Booking(startDate, endDate, totalPrice, status, renter, offer);

            Assert.AreEqual(startDate, booking.StartDate);
            Assert.AreEqual(endDate, booking.EndDate);
            Assert.AreEqual(totalPrice, booking.TotalPrice);
            Assert.AreEqual(status, booking.Status);
            Assert.AreEqual(renter, booking.Renter);
            Assert.AreEqual(offer, booking.Offer);
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(3);
            var totalPrice = 100m;
            var status = BookingStatus.Pending;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            
            // Missing Offer
            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, totalPrice, status, renter, null));
            
            // Missing Renter
            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, totalPrice, status, null, new Offer(100.0m, "Description", 18, vehicle)));
            
            // Missing start and end date
            Assert.Throws<ValidationException>(() => new Booking(default, default, totalPrice, status, renter, new Offer(100.0m, "Description", 18, vehicle)));
        }

        [Test]
        public void Constructor_InvalidTotalPrice_ShouldThrowValidationException()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(3);
            var status = BookingStatus.Pending;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle);

            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, -1m, status, renter, offer));
        }

        [Test]
        public void Constructor_EndDateBeforeStartDate_ShouldThrowValidationException()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(-3); // End date before start date
            var totalPrice = 100m;
            var status = BookingStatus.Pending;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle);

            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, totalPrice, status, renter, offer));
        }

        [Test]
        public void PlatformFee_ShouldBeConstantValue()
        {
            Assert.AreEqual(5, Booking.PlatformFee);
        }
    }
}
