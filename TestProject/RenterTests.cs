using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class RenterTests
    {
        [Test]
        public void Constructor_ValidDrivingLicenseNumber_ShouldCreateRenter()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890");
            var drivingLicenseNumber = "ABC12345";
            var host = new Host(user);
            var renter = new Renter(user, drivingLicenseNumber);

            Assert.That(renter.DrivingLicenseNumber, Is.EqualTo(drivingLicenseNumber));
            Assert.IsNotNull(renter.OfferReviews);
            Assert.IsNotNull(renter.Bookings);
            Assert.IsEmpty(renter.OfferReviews);
            Assert.IsEmpty(renter.Bookings);
        }

        [Test]
        public void DrivingLicenseNumber_Missing_ShouldThrowValidationException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890");
            var renter = new Renter(user, null);

            var context = new ValidationContext(renter);
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(renter, context, validateAllProperties: true));
        }

        [Test]
        public void AddOfferReview_ValidReview_ShouldAddToList()
        {
            var renter = new Renter { DrivingLicenseNumber = "ABC12345" };
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address>() { new Address("City", "Street", 1, "PostalCode") }, new Host());
            var review = new OfferReview(DateTime.Now, 5, 4, 4, 4, "Great!", renter, offer);
            

            Assert.That(renter.OfferReviews, Contains.Item(review));
            Assert.That(review.Renter, Is.EqualTo(renter));
        }

        [Test]
        public void RemoveOfferReview_ValidReview_ShouldRemoveFromList()
        {
            var renter = new Renter { DrivingLicenseNumber = "ABC12345" };
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address>() { new Address("City", "Street", 1, "PostalCode") }, new Host());
            var review = new OfferReview(DateTime.Now, 5, 4, 4, 4, "Great!", renter, offer);
            
            var result = renter.RemoveOfferReview(review);

            Assert.IsTrue(result);
            Assert.That(renter.OfferReviews, Does.Not.Contain(review));
        }
        

        [Test]
        public void AddBooking_ValidBooking_ShouldAddToList()
        {
            var renter = new Renter { DrivingLicenseNumber = "ABC12345" };
            var vehicle = new PassengerCar();
            var address = new Address("city", "street", 1, "postalCode");
            var offer = new Offer(100, "Description", 18, vehicle, new List<Address>{address}, new Host());
            var booking = new Booking(DateTime.Now, DateTime.Now.AddDays(1),BookingStatus.Confirmed, renter, offer);

            Assert.That(renter.Bookings, Contains.Item(booking));
            Assert.That(booking.Renter, Is.EqualTo(renter));
        }

        [Test]
        public void RemoveBooking_ValidBooking_ShouldRemoveFromList()
        {
            var renter = new Renter { DrivingLicenseNumber = "ABC12345" };
            var vehicle = new PassengerCar();
            var address = new Address("city", "street", 1, "postalCode");
            var booking = new Booking(DateTime.Now, DateTime.Now.AddDays(1),BookingStatus.Confirmed, renter, new Offer(100, "Description", 18, vehicle, new List<Address>{address}, new Host()));
            
            var result = renter.RemoveBooking(booking);

            Assert.IsTrue(result);
            Assert.That(renter.Bookings, Does.Not.Contain(booking));
        }
        

        [Test]
        public void DeleteRenter_ShouldRemoveAllBookingsAndReviews()
        {
            var renter = new Renter { DrivingLicenseNumber = "ABC12345" };
            var vehicle = new PassengerCar();
            var address = new Address("city", "street", 1, "postalCode");
            var offer = new Offer(100, "Description", 18, vehicle, new List<Address> {address}, new Host());
            var booking = new Booking(DateTime.Now, DateTime.Now.AddDays(1), BookingStatus.Confirmed, renter, offer);
            var review = new OfferReview(DateTime.Now, 5, 4, 3, 4, "Great!", renter, offer);
            

            renter.DeleteRenter(renter);

            Assert.That(renter.Bookings, Is.Empty);
            Assert.That(renter.OfferReviews, Is.Empty);
        }
    }
}
