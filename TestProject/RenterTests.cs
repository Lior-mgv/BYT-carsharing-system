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
    }
}