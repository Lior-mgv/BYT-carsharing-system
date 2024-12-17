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
    
    private readonly List<Booking> _bookings = [];
    public List<Booking> Bookings => [.._bookings];
    
    [Required]
    public Vehicle Vehicle { get; set; }

    [Required]
    public Host Host { get; set; }

    [JsonConstructor]
    private Offer()
    {
    }
    public Offer(decimal pricePerDay, string description, int? minimalAge, Vehicle vehicle, List<Address> addresses, Host host)
    {
        PricePerDay = pricePerDay;
        Description = description;
        MinimalAge = minimalAge;
        Vehicle = vehicle;
        Host = host;
        ValidationHelpers.ValidateObject(this);
        if (addresses != null && addresses.Count != 0)
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
        Host.AddOffer(this);
        Vehicle.Offer = this;
        PersistenceContext.AddToExtent(this);
    }

    public void AddAddress(Address address)
    {
        ArgumentNullException.ThrowIfNull(address);

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

    public bool RemoveAddress(Address address)
    {
        ArgumentNullException.ThrowIfNull(address);

        var removed = _addresses.Remove(address);
        if (removed && address.Offers.Contains(this))
        {
            address.RemoveOffer(this);
        }
        return removed;
    }

    public void UpdateAddress(Address oldAddress, Address newAddress)
    {
        if(!RemoveAddress(oldAddress)) return;
        AddAddress(newAddress);
    }
    
    public void AddOfferReview(OfferReview offerReview)
    {
        ArgumentNullException.ThrowIfNull(offerReview);

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
    
    public bool RemoveOfferReview(OfferReview review)
    {
        ArgumentNullException.ThrowIfNull(review);

        var res = _offerReviews.Remove(review);
        review.DeleteReview();
        return res;
    }

    public void UpdateOfferReview(OfferReview oldOfferReview, OfferReview newOfferReview)
    {
        if(!RemoveOfferReview(oldOfferReview)) return;
        AddOfferReview(newOfferReview);
    }
    
    public void AddBooking(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        if (_bookings.Contains(booking))
        {
            throw new InvalidOperationException("Offer already contains this booking");
        }
        _bookings.Add(booking);
        if (booking.Offer != this)
        {
            booking.Offer = this;
        }
    }

    public bool RemoveBooking(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);
        
        var res = _bookings.Remove(booking);
        booking.DeleteBooking();
        return res;
    }
    
    public void UpdateBooking(Booking oldBooking, Booking newBooking)
    {
        if(!RemoveBooking(oldBooking)) return;
        AddBooking(newBooking);
    }
    
    public void DeleteOffer(Offer offer)
    {
        foreach (var address in offer._addresses.ToList())
        {
            RemoveAddress(address);
        }

        foreach (var review in offer._offerReviews.ToList())
        {
            RemoveOfferReview(review);
        }

        foreach (var booking in offer._bookings.ToList())
        {
            RemoveBooking(booking);
        }

        Vehicle.Offer = null;
        Host.DeleteOffer(this);

        PersistenceContext.DeleteFromExtent(this);
    }
}