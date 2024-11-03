using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Abstractions;

namespace CarsharingSystem.Model;

public class Offer : ClassExtent<Offer>
{
    [JsonConstructor]
    public Offer()
    {
    }

    public Offer(decimal pricePerDay, string description, int? minimalAge, Vehicle vehicle)
    {
        PricePerDay = pricePerDay;
        Description = description;
        MinimalAge = minimalAge;
        Vehicle = vehicle;
        _objects.Add(DeepCopy());
    }

    [Range(1, double.MaxValue)]
    public decimal PricePerDay { get; set; }
    [Required(AllowEmptyStrings = true)]
    public string Description { get; set; } //Can be empty but should not be null
    [Range(14,25)]
    public int? MinimalAge { get; set; }
    public List<OfferReview> OfferReviews { get; set; } = [];
    public List<Address> Addresses { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
    [Required]
    public Vehicle Vehicle { get; set; }
}