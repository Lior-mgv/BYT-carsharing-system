using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class Renter
{
    public Renter(User user, string drivingLicenseNumber)
    {
        this.User = user;
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
            throw new InvalidOperationException("Offer review already contains this renter");
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
    
    public void UpdateOfferReview(OfferReview oldReview, OfferReview newReview)
    {
        if(!RemoveOfferReview(oldReview)) return;
        AddOfferReview(newReview);
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
    
    public void UpdateBooking(Booking oldBooking, Booking newBooking)
    {
        if(!RemoveBooking(oldBooking)) return;
        AddBooking(newBooking);
    }
    
    public void DeleteRenter(Renter renter)
    {
        foreach (var booking in _bookings)
        {
            RemoveBooking(booking);
        }

        foreach (var review in _offerReviews)
        {
            RemoveOfferReview(review);
        }
        
        User.RenterInfo = null;
    }
}