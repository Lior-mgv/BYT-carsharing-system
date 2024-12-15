using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class OfferReview
{
    [Range(1,5)]
    public int CleanlinessScore { get; set; }
    [Range(1,5)]
    public int MaintenanceScore { get; set; }
    [Range(1,5)]
    public int ConvenienceScore { get; set; }
    [Range(1,5)]
    public int CommunicationScore { get; set; }
    public double AverageScore => (CleanlinessScore + MaintenanceScore + ConvenienceScore + CommunicationScore) / 4.0;
    [Required(AllowEmptyStrings = true)] 
    public string Comment { get; set; } = null!;
    
    public DateTime Date;
    [Required]
    public Renter Renter { get; set; } = null!;

    [Required]
    public Offer Offer { get; set; } = null!;

    public OfferReview(DateTime date, int cleanlinessScore, int maintenanceScore, int convenienceScore, 
        int communicationScore, string comment, Renter renter, Offer offer)
    {
        Date = date;
        CleanlinessScore = cleanlinessScore;
        MaintenanceScore = maintenanceScore;
        ConvenienceScore = convenienceScore;
        CommunicationScore = communicationScore;
        Comment = comment;
        Renter = renter;
        Offer = offer;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.AddToExtent(this);
        Offer.AddOfferReview(this);
        Renter.AddOfferReview(this);
    }

    [JsonConstructor]
    private OfferReview()
    {
    }
    
    public void DeleteOfferReview()
    {
        throw new NotImplementedException();
    }
}