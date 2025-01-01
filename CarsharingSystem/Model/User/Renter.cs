using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class Renter
{
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
    
    
    public void DeleteRenter(Renter renter)
    {
        if (renter == null) throw new ArgumentNullException(nameof(renter));
        
        foreach (var booking in renter.Bookings.ToList())
        {
            RemoveBooking(booking);
        }

        foreach (var review in renter.OfferReviews.ToList())
        {
            RemoveOfferReview(review);
        }
        PersistenceContext.DeleteFromExtent(renter);
    }
}