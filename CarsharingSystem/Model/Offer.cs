using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class Offer
{

    [Range(1, double.MaxValue)]
    public decimal PricePerDay { get; set; }
    [Required(AllowEmptyStrings = true)]
    public string Description { get; set; }
    [Range(14,25)]
    public int? MinimalAge { get; set; }
    public List<OfferReview> OfferReviews { get; set; } = [];
    public List<Address> Addresses { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
    [Required]
    public Vehicle Vehicle { get; set; }
    
    [JsonConstructor]
    private Offer()
    {
    }

    public Offer(decimal pricePerDay, string description, int? minimalAge, Vehicle vehicle)
    {
        PricePerDay = pricePerDay;
        Description = description;
        MinimalAge = minimalAge;
        Vehicle = vehicle;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.Add(this);
    }
}