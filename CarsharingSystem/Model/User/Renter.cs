namespace CarsharingSystem.Model;

public class Renter
{
    public string DrivingLicenseNumber { get; set; }
    public List<OfferReview> OfferReviews { get; set; } = new List<OfferReview>();
    public List<Booking> Bookings { get; set; } = new List<Booking>();
}