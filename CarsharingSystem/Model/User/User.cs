using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class User
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    
    public Host? HostInfo { get; set; }
    public Renter? RenterInfo { get; set; }
    
    public bool IsRenter => RenterInfo != null;
    public bool IsHost => HostInfo != null;
    private readonly List<UserReview> _userReviewsWritten = [];
    public List<UserReview> UserReviewsWritten => [.._userReviewsWritten];
    private readonly List<UserReview> _userReviewsReceived = [];
    public List<UserReview> UserReviewsReceived => [.._userReviewsReceived];
    
    [JsonConstructor]
    private User()
    {
    }

    public User(string firstName, string lastName, string email, string phoneNumber, Host? hostInfo, Renter? renterInfo)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        HostInfo = hostInfo;
        RenterInfo = renterInfo;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.AddToExtent(this);
    }

    public void AddUserReviewWritten(UserReview review)
    {
        throw new NotImplementedException();
    }
    
    public void UpdateUserReviewWritten(UserReview oldReview, UserReview newReview)
    {
        throw new NotImplementedException();
    }

    public bool DeleteUserReviewWritten(UserReview review)
    {
        throw new NotImplementedException();
    }
    public void AddUserReviewReceived(UserReview review)
    {
        throw new NotImplementedException();
    }
    
    public void UpdateUserReviewReceived(UserReview oldReview, UserReview newReview)
    {
        throw new NotImplementedException();
    }

    public bool DeleteUserReviewReceived(UserReview review)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser()
    {
        throw new NotImplementedException();
    }
}