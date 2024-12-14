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
    private readonly List<OfferReview> _offerReviews = [];
    public List<OfferReview> OfferReviews => [.._offerReviews];
    private readonly List<Address> _addresses = [];
    public List<Address> Addresses => [.._addresses];
    public List<Booking> Bookings { get; set; } = [];
    [Required]
    public Vehicle Vehicle { get; set; }
    
    [JsonConstructor]
    private Offer()
    {
    }
    public Offer(decimal pricePerDay, string description, int? minimalAge, Vehicle vehicle, List<Address> addresses)
    {
        
        PricePerDay = pricePerDay;
        Description = description;
        MinimalAge = minimalAge;
        Vehicle = vehicle;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.Add(this);
        if (addresses != null || addresses.Count != 0)
        {
            foreach (var address in addresses)
            {
                AddAddress(address);
            }
        }
        else
        {
            throw new MissingFieldException("Offer must have at least one address");
        }
    }

    public void AddAddress(Address address)
    {
        if (address == null)
        {
            throw new ArgumentNullException(nameof(address));
        }

        if (_addresses.Contains(address))
        {
            throw new InvalidOperationException("Address already contains this offer");
        }
        _addresses.Add(address);
        if (!address.Offers.Contains(this))
        {
            address.AddOffer(this);
        }
    }

    public bool DeleteAddress(Address address)
    {
        if (address == null)
        {
            throw new ArgumentNullException(nameof(address));
        }
        var res = _addresses.Remove(address);
        if (address.Offers.Contains(this))
        {
            address.DeleteOffer(this);
        }

        return res;
    }
    
    public void AddOfferReview(OfferReview offerReview)
    {
        if (offerReview == null)
        {
            throw new ArgumentNullException(nameof(offerReview));
        }

        if (_offerReviews.Contains(offerReview))
        {
            throw new InvalidOperationException("Offer already contains this review");
        }
        _offerReviews.Add(offerReview);
        if (offerReview.Offer != this)
        {
            offerReview.Offer = this;
        }
    }
    
    public bool DeleteOfferReview(OfferReview offerReview)
    {
        if (offerReview == null)
        {
            throw new ArgumentNullException(nameof(offerReview));
        }
        var res = _offerReviews.Remove(offerReview);
        if (offerReview.Offer == this)
        {
            offerReview.Offer = null;
        }

        return res;
    }

    public void DeleteOffer(Offer offer)
    {
        foreach (var offerReview in _offerReviews)
        {
            
        }
    }
}