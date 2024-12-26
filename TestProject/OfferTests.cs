using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;


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
            var addresses = new List<Address>{new ("city", "Street", 1, "PostalCode")};
            var host = new Host();

            var offer = new Offer(pricePerDay, description, minimalAge, vehicle, addresses, host);

            Assert.That(offer.PricePerDay, Is.EqualTo(pricePerDay));
            Assert.That(offer.Description, Is.EqualTo(description));
            Assert.That(offer.MinimalAge, Is.EqualTo(minimalAge));
            Assert.That(offer.Vehicle, Is.EqualTo(vehicle));
            Assert.IsEmpty(offer.OfferReviews);
            Assert.That(offer.Addresses, Is.EqualTo(addresses));
            Assert.That(offer.Host, Is.EqualTo(host));
            Assert.IsEmpty(offer.Bookings);
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            // Missing PricePerDay
            Assert.Throws<ValidationException>(() => new Offer(0, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));

            // Missing Vehicle
            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 18, null, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));
            
            //Missing Host
            Assert.Throws<ValidationException>(() => new Offer(0, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, null));
        }

        [Test]
        public void Constructor_InvalidPricePerDay_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            Assert.Throws<ValidationException>(() => new Offer(-1, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));
        }

        [Test]
        public void Constructor_InvalidMinimalAge_ShouldThrowValidationException()
        {
            var vehicle = new PassengerCar();

            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 12, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));
            Assert.Throws<ValidationException>(() => new Offer(100, "Description", 30, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, new Host()));
        }
        [Test]
        public void AddAddress_ValidAddress_ShouldAddToAddresses()
        {
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { new Address("City", "Street", 1, "PostalCode") }, new Host());
            var newAddress = new Address("NewCity", "NewStreet", 2, "NewPostalCode");

            offer.AddAddress(newAddress);

            Assert.That(offer.Addresses, Contains.Item(newAddress));
            Assert.That(newAddress.Offers, Contains.Item(offer));
        }

        [Test]
        public void AddAddress_AlreadyExistingAddress_ShouldThrowInvalidOperationException()
        {
            var address = new Address("City", "Street", 1, "PostalCode");
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { address }, new Host());

            Assert.Throws<InvalidOperationException>(() => offer.AddAddress(address));
        }

        [Test]
        public void RemoveAddress_ValidAddress_ShouldRemoveFromAddresses()
        {
            var address = new Address("City", "Street", 1, "PostalCode");
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { address }, new Host());

            var removed = offer.RemoveAddress(address);

            Assert.IsTrue(removed);
            Assert.That(offer.Addresses, Does.Not.Contain(address));
            Assert.That(address.Offers, Does.Not.Contain(offer));
        }

        [Test]
        public void RemoveAddress_NullAddress_ShouldThrowArgumentNullException()
        {
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { new Address("City", "Street", 1, "PostalCode") }, new Host());

            Assert.Throws<ArgumentNullException>(() => offer.RemoveAddress(null));
        }

        [Test]
        public void AddOfferReview_ValidReview_ShouldAddToReviews()
        {
            var renter = new Renter();
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { new Address("City", "Street", 1, "PostalCode") }, new Host());
            var review = new OfferReview(DateTime.Now, 5, 5, 5, 5, "Comment", renter, offer);

            Assert.That(offer.OfferReviews, Contains.Item(review));
            Assert.That(review.Offer, Is.EqualTo(offer));
        }

        [Test]
        public void AddOfferReview_AlreadyExistingReview_ShouldThrowInvalidOperationException()
        {
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { new Address("City", "Street", 1, "PostalCode") }, new Host());
            var renter = new Renter();
            var review = new OfferReview(DateTime.Now,5, 5,5,5,"Comment", renter, offer);

            Assert.Throws<InvalidOperationException>(() => offer.AddOfferReview(review));
        }

        [Test]
        public void RemoveOfferReview_ValidReview_ShouldRemoveFromReviews()
        {
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { new Address("City", "Street", 1, "PostalCode") }, new Host());
            var renter = new Renter();
            var review = new OfferReview(DateTime.Now,5, 5,5,5,"Comment", renter, offer);

            var removed = offer.RemoveOfferReview(review);

            Assert.IsTrue(removed);
            Assert.That(offer.OfferReviews, Does.Not.Contain(review));
        }

        [Test]
        public void RemoveOfferReview_NullReview_ShouldThrowArgumentNullException()
        {
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { new Address("City", "Street", 1, "PostalCode") }, new Host());

            Assert.Throws<ArgumentNullException>(() => offer.RemoveOfferReview(null));
        }
        
        [Test]
        public void UpdateAddress_ValidAddresses_ShouldUpdateAddresses()
        {
            var oldAddress = new Address("City", "Street", 1, "PostalCode");
            var newAddress = new Address("NewCity", "NewStreet", 2, "NewPostalCode");
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { oldAddress }, new Host());

            offer.UpdateAddress(oldAddress, newAddress);

            Assert.That(offer.Addresses, Contains.Item(newAddress));
            Assert.That(offer.Addresses, Does.Not.Contain(oldAddress));
            Assert.That(oldAddress.Offers, Does.Not.Contain(offer));
            Assert.That(newAddress.Offers, Contains.Item(offer));
        }
        
        [Test]
        public void UpdateAddress_NullOldAddress_ShouldThrowArgumentNullException()
        {
            var offer = new Offer(100, "Description", 18, new PassengerCar(), new List<Address> { new Address("City", "Street", 1, "PostalCode") }, new Host());

            Assert.Throws<ArgumentNullException>(() => offer.UpdateAddress(null, new Address("NewCity", "NewStreet", 2, "NewPostalCode")));
        }

        // [Test]
        // public void DeleteOfferShouldDeleteAssociations(Offer offer)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
