using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class Address
{
    [Required]
    public string City { get; set; }
    [Required]
    public string Street { get; set; }
    [Range(1, int.MaxValue)]
    public int BuildingNumber { get; set; }
    [Required]
    public string PostalCode { get; set; }

    private readonly List<Offer> _offers = [];
    public List<Offer> Offers => [.._offers];

    public Address(string city, string street, int buildingNumber, string postalCode)
    {
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
        PostalCode = postalCode;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.Add(this);
    }

    [JsonConstructor]
    private Address()
    {
    }

    public void AddOffer(Offer offer)
    {
        _offers.Add(offer);
        if (!offer.Addresses.Contains(this))
        {
            offer.AddAddress(this);
        }
    }

    public bool DeleteOffer(Offer offer)
    {
        var res = _offers.Remove(offer);
        if (offer.Addresses.Contains(this))
        {
            offer.DeleteAddress(this);
        }
        return res;
    }
}