using CarsharingSystem.Model;

namespace TestProject;

public class HostTests
    {

        [Test]
        public void AddVehicle_ValidVehicle_ShouldAddVehicleToList()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null); 
            var host = new Host(user);
            var vehicle = new PassengerCar();
            host.AddVehicle(vehicle);

            Assert.Contains(vehicle, host.Vehicles);
            Assert.That(vehicle.Host, Is.EqualTo(host));
        }

        [Test]
        public void AddVehicle_NullVehicle_ShouldThrowArgumentNullException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            Assert.Throws<ArgumentNullException>(() => host.AddVehicle(null));
        }

        [Test]
        public void AddVehicle_DuplicateVehicle_ShouldThrowInvalidOperationException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var vehicle = new PassengerCar();
            host.AddVehicle(vehicle);

            Assert.Throws<InvalidOperationException>(() => host.AddVehicle(vehicle));
        }

        [Test]
        public void DeleteVehicle_ValidVehicle_ShouldRemoveVehicleFromList()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var vehicle = new PassengerCar();
            host.AddVehicle(vehicle);

            var result = host.DeleteVehicle(vehicle);

            Assert.IsTrue(result);
            Assert.IsFalse(host.Vehicles.Contains(vehicle));
        }
        
        [Test]
        public void DeleteVehicle_NotContainedVehicle_ShouldThrowInvalidCastException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var vehicle = new PassengerCar();
            Assert.Throws<InvalidCastException>(() => host.DeleteVehicle(vehicle));
        }

        [Test]
        public void DeleteVehicle_NullVehicle_ShouldThrowArgumentNullException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            Assert.Throws<ArgumentNullException>(() => host.DeleteVehicle(null));
        }

        [Test]
        public void UpdateVehicle_ValidVehicles_ShouldReplaceOldVehicleWithNewOne()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var oldVehicle = new PassengerCar();
            var newVehicle = new PassengerCar();
            host.AddVehicle(oldVehicle);

            host.UpdateVehicle(oldVehicle, newVehicle);

            Assert.IsFalse(host.Vehicles.Contains(oldVehicle));
            Assert.Contains(newVehicle, host.Vehicles);
            Assert.That(newVehicle.Host, Is.EqualTo(host));
        }

        [Test]
        public void AddOffer_ValidOffer_ShouldAddOfferToList()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host);

            Assert.Contains(offer, host.Offers);
            Assert.AreEqual(host, offer.Host);
        }

        [Test]
        public void AddOffer_NullOffer_ShouldThrowArgumentNullException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            Assert.Throws<ArgumentNullException>(() => host.AddOffer(null));
        }

        [Test]
        public void AddOffer_DuplicateOffer_ShouldThrowInvalidOperationException()
        {var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host);

            Assert.Throws<InvalidOperationException>(() => host.AddOffer(offer));
        }

        [Test]
        public void DeleteOffer_ValidOffer_ShouldRemoveOfferFromList()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var vehicle = new PassengerCar();
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host);

            var result = host.DeleteOffer(offer);

            Assert.IsTrue(result);
            Assert.IsFalse(host.Offers.Contains(offer));
        }

        [Test]
        public void DeleteOffer_NullOffer_ShouldThrowArgumentNullException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            Assert.Throws<ArgumentNullException>(() => host.DeleteOffer(null));
        }

        [Test]
        public void AddDiscountCode_ValidDiscountCode_ShouldAddDiscountCodeToList()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var discountCode = new DiscountCode("Code", DateTime.Now, DateTime.MaxValue, host);

            Assert.Contains(discountCode, host.DiscountCodes);
            Assert.AreEqual(host, discountCode.Host);
        }

        [Test]
        public void AddDiscountCode_NullDiscountCode_ShouldThrowArgumentNullException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            Assert.Throws<ArgumentNullException>(() => host.AddDiscountCode(null));
        }

        [Test]
        public void AddDiscountCode_DuplicateDiscountCode_ShouldThrowInvalidOperationException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var discountCode = new DiscountCode("Code", DateTime.Now, DateTime.MaxValue, host);


            Assert.Throws<InvalidOperationException>(() => host.AddDiscountCode(discountCode));
        }

        [Test]
        public void DeleteDiscountCode_ValidDiscountCode_ShouldRemoveDiscountCodeFromList()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var discountCode = new DiscountCode("Code", DateTime.Now, DateTime.MaxValue, host);

            var result = host.DeleteDiscountCode(discountCode);

            Assert.IsTrue(result);
            Assert.IsFalse(host.DiscountCodes.Contains(discountCode));
        }

        [Test]
        public void DeleteDiscountCode_NullDiscountCode_ShouldThrowArgumentNullException()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            Assert.Throws<ArgumentNullException>(() => host.DeleteDiscountCode(null));
        }

        [Test]
        public void DeleteHost_ShouldRemoveAllAssociationsAndDeleteHost()
        {
            var vehicle = new PassengerCar();
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var host = new Host(user);
            var offer = new Offer(100.0m, "Description", 18, vehicle, new List<Address>(){new Address("city", "Street", 1, "PostalCode")}, host);
            var discountCode = new DiscountCode("Code", DateTime.Now, DateTime.MaxValue, host);

            host.DeleteHost();

            Assert.IsEmpty(host.Vehicles);
            Assert.IsEmpty(host.Offers);
            Assert.IsEmpty(host.DiscountCodes);
        }
    }