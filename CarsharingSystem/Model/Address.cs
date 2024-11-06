using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class Address
{
    [Required]
    public string City { get; set; }
    [Range(1, int.MaxValue)]
    public int BuildingNumber { get; set; }
    [Required]
    public string PostalCode { get; set; }
    public List<Offer> Offers { get; set; } = [];

    public Address(string city, int buildingNumber, string postalCode)
    {
        City = city;
        BuildingNumber = buildingNumber;
        PostalCode = postalCode;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.Add(this);
    }

    [JsonConstructor]
    private Address()
    {
    }
}