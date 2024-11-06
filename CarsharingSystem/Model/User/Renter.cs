using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Model;

public class Renter
{
    [Required]
    public string DrivingLicenseNumber { get; set; }
    public List<OfferReview> OfferReviews { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
}