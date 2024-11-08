using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class RenterTests
    {
        [Test]
        public void Constructor_ValidDrivingLicenseNumber_ShouldCreateRenter()
        {
            var drivingLicenseNumber = "ABC12345";
            var renter = new Renter { DrivingLicenseNumber = drivingLicenseNumber };

            Assert.That(renter.DrivingLicenseNumber, Is.EqualTo(drivingLicenseNumber));
            Assert.IsNotNull(renter.OfferReviews);
            Assert.IsNotNull(renter.Bookings);
            Assert.IsEmpty(renter.OfferReviews);
            Assert.IsEmpty(renter.Bookings);
        }

        [Test]
        public void DrivingLicenseNumber_Missing_ShouldThrowValidationException()
        {
            var renter = new Renter();

            var context = new ValidationContext(renter);
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(renter, context, validateAllProperties: true));
        }
    }
}