using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Model;

public class Renter
{
    [Required]
    public string DrivingLicenseNumber { get; set; }
    public List<OfferReview> OfferReviews { get; set; } = new List<OfferReview>();
    public List<Booking> Bookings { get; set; } = new List<Booking>();
}