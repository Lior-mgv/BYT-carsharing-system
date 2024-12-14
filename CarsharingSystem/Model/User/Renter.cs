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
        if (offerReview == null)
        {
            throw new ArgumentNullException(nameof(offerReview));
        }

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

    public void DeleteOfferReview(OfferReview offerReview)
    {
        
    }
}