using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;


namespace TestProject
{
    public class UserTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateUser()
        {
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@example.com";
            var phoneNumber = "1234567890";
        
            var user = new User(firstName, lastName, email, phoneNumber);
        
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(phoneNumber, user.PhoneNumber);
            Assert.IsFalse(user.IsRenter);
            Assert.IsFalse(user.IsHost);
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new User(null, "Doe", "john.doe@example.com", "1234567890"));
            Assert.Throws<ValidationException>(() => new User("John", null, "john.doe@example.com", "1234567890"));
            Assert.Throws<ValidationException>(() => new User("John", "Doe", "john.doe@example.com", null));
        }

        [Test]
        public void Constructor_InvalidEmail_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new User("John", "Doe", "not-an-email", "1234567890"));
        }

        [Test]
        public void IsRenter_ShouldReturnTrue_WhenRenterInfoIsProvided()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890");
            var renterInfo = new Renter(user, "12345");
            user.RenterInfo = renterInfo;

            Assert.IsTrue(user.IsRenter);
        }

        [Test]
        public void IsHost_ShouldReturnTrue_WhenHostInfoIsProvided()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890");
            var hostInfo = new Host(user);
            user.HostInfo = hostInfo;

            Assert.IsTrue(user.IsHost);
        }
    }
}
