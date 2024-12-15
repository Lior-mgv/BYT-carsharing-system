using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Model;

public class Renter
{
    [Required]
    public string DrivingLicenseNumber { get; set; }
    public List<Booking> Bookings { get; set; } = [];
    
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
    
    public void UpdateOfferReview(UserReview oldReview, UserReview newReview)
    {
        throw new NotImplementedException();
    }

    public bool DeleteOfferReview(UserReview review)
    {
        throw new NotImplementedException();
    }

    public void AddBooking(Booking booking)
    {
        throw new NotImplementedException();
    }
    
    public void UpdateBooking(Booking oldBooking, Booking newBooking)
    {
        throw new NotImplementedException();
    }

    public bool DeleteBooking(Booking booking)
    {
        throw new NotImplementedException();
    }
}