using System;
using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Model;

namespace TestProject
{
    public class UserReviewTests
    {
        [Test]
        public void Reviewer_AddingUserReview_ShouldIncludeReviewInUserReviews()
        {
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var reviewee = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var review = new UserReview(DateTime.Now, 5, "Great service!", reviewer, reviewee);

            reviewer.AddUserReview(review);

            Assert.That(reviewer.UserReviews, Contains.Item(review));
        }

        [Test]
        public void Reviewee_AddingUserReview_ShouldIncludeReviewInUserReviews()
        {
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var reviewee = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var review = new UserReview(DateTime.Now, 5, "Great service!", reviewer, reviewee);

            reviewee.AddUserReview(review);

            Assert.That(reviewee.UserReviews, Contains.Item(review));
        }

        [Test]
        public void DeleteUserReview_ShouldRemoveReviewFromBothUsers()
        {
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var reviewee = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var review = new UserReview(DateTime.Now, 5, "Great service!", reviewer, reviewee);


            review.DeleteUserReview(review);

            Assert.That(reviewer.UserReviews, Does.Not.Contain(review));
            Assert.That(reviewee.UserReviews, Does.Not.Contain(review));
        }

        [Test]
        public void AddingDuplicateUserReview_ShouldThrowException()
        {
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var reviewee = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var review = new UserReview(DateTime.Now, 5, "Great service!", reviewer, reviewee);

            reviewer.AddUserReview(review);

            Assert.Throws<InvalidOperationException>(() => reviewer.AddUserReview(review));
        }

        [Test]
        public void RemovingNonexistentUserReview_ShouldNotThrowException()
        {
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var reviewee = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var review = new UserReview(DateTime.Now, 5, "Great service!", reviewer, reviewee);

            Assert.DoesNotThrow(() => reviewer.RemoveUserReview(review));
        }

        [Test]
        public void Reviewee_RemoveUserReview_ShouldRemoveFromUserReviews()
        {
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var reviewee = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var review = new UserReview(DateTime.Now, 5, "Great service!", reviewer, reviewee);

            reviewee.AddUserReview(review);
            reviewee.RemoveUserReview(review);

            Assert.That(reviewee.UserReviews, Does.Not.Contain(review));
        }

        [Test]
        public void Reviewer_UpdateUserReview_ShouldUpdateInUserReviews()
        {
            var reviewer = new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null);
            var reviewee = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var oldReview = new UserReview(DateTime.Now, 4, "Good service.", reviewer, reviewee);
            var newReview = new UserReview(DateTime.Now, 5, "Excellent service!", reviewer, reviewee);

            reviewer.AddUserReview(oldReview);
            reviewer.UpdateUserReview(oldReview, newReview);

            Assert.That(reviewer.UserReviews, Does.Not.Contain(oldReview));
            Assert.That(reviewer.UserReviews, Contains.Item(newReview));
        }
    }
}
