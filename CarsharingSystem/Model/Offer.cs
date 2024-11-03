using CarsharingSystem.Abstractions;

namespace CarsharingSystem.Model;

public class Offer : ClassExtent<Offer>
{
    public decimal PricePerDay { get; set; }
    public string Description { get; set; }
    public int? MinimalAge { get; set; }
    public List<OfferReview> OfferReviews { get; set; } = [];
    public List<Address> Addresses { get; set; } = [];// Have to ensure that the address is not null
    public List<Booking> Bookings { get; set; } = [];
    public Vehicle Vehicle { get; set; }
}