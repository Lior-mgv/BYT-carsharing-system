using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;


namespace TestProject
{
    public class UserReviewTests
    {
        [Test]
        public void Constructor_ValidParameters_ShouldCreateUserReview()
        {
            var date = DateTime.Now;
            var score = 4;
            var comment = "Excellent user!";
            var reviewer = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var reviewee = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);

            var userReview = new UserReview(date, score, comment, reviewer, reviewee);

            Assert.That(userReview.Date, Is.EqualTo(date));
            Assert.That(userReview.Score, Is.EqualTo(score));
            Assert.That(userReview.Comment, Is.EqualTo(comment));
            Assert.That(userReview.Reviewer, Is.EqualTo(reviewer));
            Assert.That(userReview.Reviewee, Is.EqualTo(reviewee));
        }

        [Test]
        public void Constructor_MissingRequiredFields_ShouldThrowValidationException()
        {
            var date = DateTime.Now;
            var reviewer = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var reviewee = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);

            // Missing Reviewee
            Assert.Throws<ValidationException>(() => new UserReview(date, 4, "Good user", reviewer, null!));

            // Missing Reviewer
            Assert.Throws<ValidationException>(() => new UserReview(date, 4, "Good user", null!, reviewee));
        }

        [Test]
        public void Constructor_InvalidScore_ShouldThrowValidationException()
        {
            var date = DateTime.Now;
            var reviewer = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var reviewee = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);

            // Score less than 1
            Assert.Throws<ValidationException>(() => new UserReview(date, 0, "Too low score", reviewer, reviewee));

            // Score greater than 5
            Assert.Throws<ValidationException>(() => new UserReview(date, 6, "Too high score", reviewer, reviewee));
        }
    }
}
