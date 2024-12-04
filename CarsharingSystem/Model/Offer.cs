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
    
    private readonly List<Address> _addresses = [];
    public List<Address> Addresses => [.._addresses];
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

    public void AddAddress(Address address)
    {
        _addresses.Add(address);
        if (!address.Offers.Contains(this))
        {
            address.AddOffer(this);
        }
    }

    public bool DeleteAddress(Address address)
    {
        var res = _addresses.Remove(address);
        if (address.Offers.Contains(this))
        {
            address.DeleteOffer(this);
        }

        return res;
    }
}