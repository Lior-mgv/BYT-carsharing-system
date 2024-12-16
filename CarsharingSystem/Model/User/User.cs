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
    private readonly List<UserReview> _userReviews = [];
    public List<UserReview> UserReviews => [.._userReviews];
    
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

    public void AddUserReview(UserReview review)
    {
        ArgumentNullException.ThrowIfNull(review);

        if (_userReviews.Contains(review))
        {
            throw new InvalidOperationException("Review already exists in written reviews.");
        }
        
        _userReviews.Add(review);

        if (review.Reviewer != this)
        {
            review.Reviewer = this;
        }
    }
    
    public bool RemoveUserReview(UserReview review)
    {
        ArgumentNullException.ThrowIfNull(review);
        var res = _userReviews.Remove(review);
        review.DeleteUserReview(review);
        return res;
    }
    
    public void UpdateUserReview(UserReview oldReview, UserReview newReview)
    {
        AddUserReview(newReview);
        RemoveUserReview(oldReview);
    }

    public void DeleteUser(User user)
    {
        if (user.IsHost)
        {
            user.HostInfo?.DeleteHost(user.HostInfo);
        }

        if (user.IsRenter)
        {
            user.RenterInfo?.DeleteRenter(user.RenterInfo);
        }
        foreach (var review in user._userReviews)
        {
            RemoveUserReview(review);
        }
        
        PersistenceContext.DeleteFromExtent(user);
    }
}