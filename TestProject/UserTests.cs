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
            Assert.Throws<ValidationException>(() => new User("John", "Doe", "john.doe@example.com", null, null,null));
        }

        [Test]
        public void Constructor_InvalidEmail_ShouldThrowValidationException()
        {
            Assert.Throws<ValidationException>(() => new User("John", "Doe", "not-an-email", "1234567890", null, null));
        }

        [Test]
        public void IsRenter_ShouldReturnTrue_WhenRenterInfoIsProvided()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var renterInfo = new Renter(user, "12345");
            user.RenterInfo = renterInfo;

            Assert.IsTrue(user.IsRenter);
        }

        [Test]
        public void IsHost_ShouldReturnTrue_WhenHostInfoIsProvided()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var hostInfo = new Host(user);
            user.HostInfo = hostInfo;

            Assert.IsTrue(user.IsHost);
        }
        
        [Test]
        public void RemoveUserReview_ValidReview_ShouldRemoveFromList()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var review = new UserReview(DateTime.Now, 5, "Great service!", reviewer, user);

            user.AddUserReview(review);
            var result = user.RemoveUserReview(review);

            Assert.IsTrue(result);
            Assert.That(user.UserReviews, Does.Not.Contain(review));
        }
        
        [Test]
        public void DeleteUser_ShouldRemoveAllAssociatedData()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var hostInfo = new Host(user);
            var renterInfo = new Renter(user, "ABC12345");
            user.RenterInfo = renterInfo;
            user.HostInfo = hostInfo;
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var review = new UserReview(DateTime.Now, 5, "Great service!", reviewer, user);

            user.AddUserReview(review);

            user.DeleteUser(user);

            Assert.IsEmpty(user.UserReviews);
            Assert.IsNull(user.HostInfo);
            Assert.IsNull(user.RenterInfo);
        }
        
    }
}
