using System.ComponentModel.DataAnnotations;

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
}