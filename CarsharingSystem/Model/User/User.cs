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

    public IHost? HostInfo { get; set; }
    public IRenter? RenterInfo { get; set; }

    public bool IsRenter => RenterInfo != null;
    public bool IsHost => HostInfo != null;
    private readonly List<UserReview> _userReviews = [];
    public List<UserReview> UserReviews => [.._userReviews];
    
    [JsonConstructor]
    private User()
    {
    }

    public override bool Equals(object obj)
    {
        if (obj is User other)
        {
            return Email == other.Email;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Email.GetHashCode();
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

        if (!review.Reviewer.Equals(this))
        {
            review.Reviewer = this;
        }
    }

    public bool RemoveUserReview(UserReview review)
    {
        ArgumentNullException.ThrowIfNull(review);
        var res = _userReviews.Remove(review);
        if (res)
        {
            if (review.Reviewer.Equals(this))
            {
                review.Reviewee?.RemoveUserReview(review);
            }
            else if (review.Reviewee.Equals(this))
            {
                review.Reviewer?.RemoveUserReview(review);
            }
        }
        return res;
    }

    public void UpdateUserReview(UserReview oldReview, UserReview newReview)
    {
        if (!RemoveUserReview(oldReview))
        {
            return;
        }

        AddUserReview(newReview);
    }

    public void DeleteUser(User user)
    {
        if (user.IsHost)
        {
            user.HostInfo?.DeleteHost();
        }

        if (user.IsRenter)
        {
            user.RenterInfo?.DeleteRenter();
        }
        foreach (var review in user._userReviews.ToList())
        {
            RemoveUserReview(review);
        }

        PersistenceContext.DeleteFromExtent(user);
    }

    #region Host functionality
    
    public void AddVehicle(Vehicle vehicle)
    {
        if(!IsHost) throw new InvalidOperationException("The user is not a host");
        HostInfo?.AddVehicle(vehicle);
    }

    public bool DeleteVehicle(Vehicle vehicle)
    {
        if(!IsHost) throw new InvalidOperationException("The user is not a host");
        return HostInfo?.DeleteVehicle(vehicle) ?? false;
    }

    public void UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle)
    {
        if(!IsHost) throw new InvalidOperationException("The user is not a host");
        HostInfo?.UpdateVehicle(oldVehicle, newVehicle);
    }

    public void AddOffer(Offer offer)
    {
        if(!IsHost) throw new InvalidOperationException("The user is not a host");
        HostInfo?.AddOffer(offer);
    }

    public bool DeleteOffer(Offer offer)
    {
        if(!IsHost) throw new InvalidOperationException("The user is not a host");
        return HostInfo?.DeleteOffer(offer) ?? false;
    }

    public void AddDiscountCode(DiscountCode discountCode)
    {
        if(!IsHost) throw new InvalidOperationException("The user is not a host");
        HostInfo?.AddDiscountCode(discountCode);
    }

    public bool DeleteDiscountCode(DiscountCode discountCode)
    {
        if(!IsHost) throw new InvalidOperationException("The user is not a host");
        return HostInfo?.DeleteDiscountCode(discountCode) ?? false;
    }

    public void DeleteHost()
    {
        if(!IsHost) throw new InvalidOperationException("The user is not a host");
        HostInfo?.DeleteHost();
    }
    #endregion

    #region Renter functionality

    public void AddOfferReview(OfferReview offerReview)
    {
        if(!IsRenter) throw new InvalidOperationException("The user is not a renter");
        RenterInfo?.AddOfferReview(offerReview);
    }

    public bool RemoveOfferReview(OfferReview review)
    {
        if(!IsRenter) throw new InvalidOperationException("The user is not a renter");
        return RenterInfo?.RemoveOfferReview(review) ?? false;
    }

    public void AddBooking(Booking booking)
    {
        if(!IsRenter) throw new InvalidOperationException("The user is not a renter");
        RenterInfo?.AddBooking(booking);
    }

    public bool RemoveBooking(Booking booking)
    {
        if(!IsRenter) throw new InvalidOperationException("The user is not a renter");
        return RenterInfo?.RemoveBooking(booking) ?? false;
    }

    public void DeleteRenter(Renter renter)
    {
        if(!IsRenter) throw new InvalidOperationException("The user is not a renter");
        RenterInfo?.DeleteRenter();
    }

    #endregion
    
}