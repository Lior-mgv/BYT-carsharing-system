using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarsharingSystem.Model;

public class Renter : IRenter
{
    public Renter(User user, string drivingLicenseNumber)
    {
        User = user;
        user.RenterInfo = this;
        DrivingLicenseNumber = drivingLicenseNumber;
    }

    [JsonConstructor]
    private Renter()
    {
    }

    [Required]
    public User User { get; set; }

    [Required]
    public string DrivingLicenseNumber { get; set; }

    private readonly List<Booking> _bookings = [];
    public List<Booking> Bookings => [.._bookings];
    
    private readonly List<OfferReview> _offerReviews = [];
    public List<OfferReview> OfferReviews => [.._offerReviews];
    
    public void AddOfferReview(OfferReview offerReview)
    {
        ArgumentNullException.ThrowIfNull(offerReview);

        if (_offerReviews.Contains(offerReview))
        {
            throw new InvalidOperationException("Offer review already contains this offer review");
        }
        _offerReviews.Add(offerReview);
        if (offerReview.Renter != this)
        {
            offerReview.Renter = this;
        }
    }

    public bool RemoveOfferReview(OfferReview review)
    {
        ArgumentNullException.ThrowIfNull(review);

        var res = _offerReviews.Remove(review);
        review.DeleteReview();
        return res;
    }

    public void AddBooking(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        if (_bookings.Contains(booking))
        {
            throw new InvalidOperationException("Offer already contains this booking");
        }
        _bookings.Add(booking);
        if (booking.Renter != this)
        {
            booking.Renter = this;
        }
    }

    public bool RemoveBooking(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);
        
        var res = _bookings.Remove(booking);
        booking.DeleteBooking();
        return res;
    }
    
    
    public void DeleteRenter()
    {
        if (this == null) throw new ArgumentNullException();
        
        foreach (var booking in Bookings.ToList())
        {
            RemoveBooking(booking);
        }

        foreach (var review in OfferReviews.ToList())
        {
            RemoveOfferReview(review);
        }
        
        User.RenterInfo = null;
    }
}