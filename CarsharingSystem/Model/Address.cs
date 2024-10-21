namespace CarsharingSystem.Model;

public class Address
{
    public string City { get; set; }
    public int BuildingNumber { get; set; }
    public string PostalCode { get; set; }
    public List<Offer> Offers { get; set; } = new List<Offer>();
}