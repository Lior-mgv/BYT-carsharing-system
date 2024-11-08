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
        
            var user = new User(firstName, lastName, email, phoneNumber, null, null);
        
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
            Assert.Throws<ValidationException>(() => new User(null, "Doe", "john.doe@example.com", "1234567890", null, null));
            Assert.Throws<ValidationException>(() => new User("John", null, "john.doe@example.com", "1234567890", null, null));
            Assert.Throws<ValidationException>(() => new User("John", "Doe", "john.doe@example.com", null, null, null));
        }

        [Test]
        public void Constructor_InvalidEmail_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new User("John", "Doe", "not-an-email", "1234567890", null, null));
        }

        [Test]
        public void IsRenter_ShouldReturnTrue_WhenRenterInfoIsProvided()
        {
            var renterInfo = new Renter();
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, renterInfo);

            Assert.IsTrue(user.IsRenter);
        }

        [Test]
        public void IsHost_ShouldReturnTrue_WhenHostInfoIsProvided()
        {
            var hostInfo = new Host();
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", hostInfo, null);

            Assert.IsTrue(user.IsHost);
        }
    }
}
