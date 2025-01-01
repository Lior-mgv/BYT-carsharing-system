using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class OfferReviewTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateOfferReview()
        {
            var date = DateTime.Now;
            var cleanlinessScore = 4;
            var maintenanceScore = 5;
            var convenienceScore = 3;
            var communicationScore = 4;
            var comment = "Great experience!";
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host());

            var offerReview = new OfferReview(date, cleanlinessScore, maintenanceScore, convenienceScore, communicationScore, comment, renter, offer);

            Assert.That(offerReview.Date, Is.EqualTo(date));
            Assert.That(offerReview.CleanlinessScore, Is.EqualTo(cleanlinessScore));
            Assert.That(offerReview.MaintenanceScore, Is.EqualTo(maintenanceScore));
            Assert.That(offerReview.ConvenienceScore, Is.EqualTo(convenienceScore));
            Assert.That(offerReview.CommunicationScore, Is.EqualTo(communicationScore));
            Assert.That(offerReview.Comment, Is.EqualTo(comment));
            Assert.That(offerReview.Renter, Is.EqualTo(renter));
            Assert.That(offerReview.Offer, Is.EqualTo(offer));
            Assert.That(offerReview.AverageScore, Is.EqualTo(4.0));
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var date = DateTime.Now;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host());

            // Missing Offer
            Assert.Throws<ValidationException>(() => new OfferReview(date, 4, 4, 4, 4, "Good service", renter, null));

            // Missing Renter
            Assert.Throws<ValidationException>(() => new OfferReview(date, 4, 4, 4, 4, "Good service", null, offer));
        }

        [Test]
        public void Constructor_InvalidScore_ShouldThrowValidationException()
        {
            var date = DateTime.Now;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host());

            // Invalid CleanlinessScore
            Assert.Throws<ValidationException>(() => new OfferReview(date, 0, 4, 4, 4, "Comment", renter, offer));
            Assert.Throws<ValidationException>(() => new OfferReview(date, 6, 4, 4, 4, "Comment", renter, offer));

            // Invalid MaintenanceScore
            Assert.Throws<ValidationException>(() => new OfferReview(date, 4, 0, 4, 4, "Comment", renter, offer));
            Assert.Throws<ValidationException>(() => new OfferReview(date, 4, 6, 4, 4, "Comment", renter, offer));

            // Invalid ConvenienceScore
            Assert.Throws<ValidationException>(() => new OfferReview(date, 4, 4, 0, 4, "Comment", renter, offer));
            Assert.Throws<ValidationException>(() => new OfferReview(date, 4, 4, 6, 4, "Comment", renter, offer));

            // Invalid CommunicationScore
            Assert.Throws<ValidationException>(() => new OfferReview(date, 4, 4, 4, 0, "Comment", renter, offer));
            Assert.Throws<ValidationException>(() => new OfferReview(date, 4, 4, 4, 6, "Comment", renter, offer));
        }

        [Test]
        public void AverageScore_ShouldCalculateCorrectly()
        {
            var date = DateTime.Now;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host());

            var offerReview = new OfferReview(date, 5, 4, 3, 2, "Feedback", renter, offer);

            Assert.That(offerReview.AverageScore, Is.EqualTo(3.5));
        }
        
        [Test]
        public void DeleteReview_ShouldRemoveAssociations()
        {
            // Arrange
            var date = DateTime.Now;
            var renter = new Renter();
            var vehicle = new PassengerCar();
            var offer = new Offer(100, "Description", 18, vehicle, new List<Address> { new Address("city", "Street", 1, "PostalCode") }, new Host());
            var offerReview = new OfferReview(date, 5, 4, 3, 2, "Great experience", renter, offer);

            // Act
            offerReview.DeleteReview();

            // Assert
            Assert.That(offer.OfferReviews, Does.Not.Contain(offerReview));
            Assert.That(renter.OfferReviews, Does.Not.Contain(offerReview));
        }

    }
}
