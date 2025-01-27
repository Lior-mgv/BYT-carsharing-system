﻿using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;


namespace TestProject
{
    public class BookingTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateBooking()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(3);
            var status = BookingStatus.Pending;
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var renter = new Renter(user,"12345");
            var vehicle = new PassengerCar();
            var host = new Host(user);
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host);

            var booking = new Booking(startDate, endDate, status, renter, offer);

            Assert.That(booking.StartDate, Is.EqualTo(startDate));
            Assert.That(booking.EndDate, Is.EqualTo(endDate));
            Assert.That(booking.Status, Is.EqualTo(status));
            Assert.That(booking.Renter, Is.EqualTo(renter));
            Assert.That(booking.Offer, Is.EqualTo(offer));
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(3);
            var totalPrice = 100m;
            var status = BookingStatus.Pending;
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var renter = new Renter(user, "12345");
            var vehicle = new PassengerCar();
            var host = new Host(user);
            
            // Missing offer
            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, status, renter, null));
            
            // Missing Renter
            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, status, null, new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("City", "Street", 1, "PostalCode")}, host)));
            
            // Missing start and end date
            Assert.Throws<ValidationException>(() => new Booking(default, default, status, renter, new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host)));
        }
        

        [Test]
        public void Constructor_EndDateBeforeStartDate_ShouldThrowValidationException()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(-3); // End date before start date
            var status = BookingStatus.Pending;
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var renter = new Renter(user, "12345");
            var vehicle = new PassengerCar();
            var host = new Host(user);
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host);

            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, status, renter, offer));
        }

        [Test]
        public void PlatformFee_ShouldBeConstantValue()
        {
            Assert.That(Booking.PlatformFee, Is.EqualTo(5));
        }
        
        [Test]
        public void TotalPrice_ShouldBeCalculatedCorrectly()
        {
            var startDate = new DateTime(2023, 10, 1);
            var endDate = new DateTime(2023, 10, 5); // 4 days
            var pricePerDay = 100m;
            var status = BookingStatus.Pending;
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var renter = new Renter(user, "12345");
            var vehicle = new PassengerCar();
            var host = new Host(user);
            var offer = new Offer(pricePerDay, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host);

            var booking = new Booking(startDate, endDate, status, renter, offer);

            var expectedTotalPrice = (pricePerDay * 4) + Booking.PlatformFee;
            Assert.That(booking.TotalPrice, Is.EqualTo(expectedTotalPrice));
        }
        

        [Test]
        public void TotalPrice_ShouldThrowValidationException_WhenEndDateBeforeStartDate()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(-3); // End date before start date
            var status = BookingStatus.Pending;
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var renter = new Renter(user, "12345");
            var vehicle = new PassengerCar();
            var host = new Host(user);
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>{new Address("city", "Street", 1, "PostalCode")}, host);

            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, status, renter, offer));
        }

        [Test]
        public void DeleteBookingShouldDeleteAssociatdStuff()
        {
            var user = new User("Cool", "Guy", "coolguy@cool.com", "+484448292", null, null);
            var renter = new Renter(user, "123123");
            var host = new Host(user);
            var offer = new Offer(12, "some description shi", null, new PassengerCar(),
                new List<Address> { new Address("some city", "some street", 2, "213") }, host);
            var booking = new Booking(DateTime.Now, DateTime.MaxValue, BookingStatus.Confirmed, renter, offer);
            Assert.That(offer.Bookings, Does.Contain(booking));
            Assert.That(renter.Bookings, Does.Contain(booking));
            
            booking.DeleteBooking();
            
            Assert.That(offer.Bookings, Does.Not.Contain(booking));
            Assert.That(renter.Bookings, Does.Not.Contain(booking));
        }
    }
}
