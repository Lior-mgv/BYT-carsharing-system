using System.ComponentModel.DataAnnotations;
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
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")});

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
            var renter = new Renter();
            var vehicle = new PassengerCar();
            
            // Missing Offer
            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, status, renter, null));
            
            // Missing Renter
            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, status, null, new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("City", "Street", 1, "PostalCode")})));
            
            // Missing start and end date
            Assert.Throws<ValidationException>(() => new Booking(default, default, status, renter, new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")})));
        }
        

        [Test]
        public void Constructor_EndDateBeforeStartDate_ShouldThrowValidationException()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(-3); // End date before start date
            var status = BookingStatus.Pending;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")});

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
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(pricePerDay, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")});

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
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")});

            Assert.Throws<ValidationException>(() => new Booking(startDate, endDate, status, renter, offer));
        }
    }
}
