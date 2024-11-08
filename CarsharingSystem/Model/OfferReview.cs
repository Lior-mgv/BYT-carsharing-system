using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class OfferReview
{
    [Range(1,5)]
    public double CleanlinessScore { get; set; }
    [Range(1,5)]
    public double MaintenanceScore { get; set; }
    [Range(1,5)]
    public double ConvenienceScore { get; set; }
    [Range(1,5)]
    public double CommunicationScore { get; set; }
    public double AverageScore => (CleanlinessScore + MaintenanceScore + ConvenienceScore + CommunicationScore) / 4;
    [Required(AllowEmptyStrings = true)] 
    public string Comment { get; set; } = null!;
    [Required]
    public DateTime Date;
    [Required]
    public Renter Renter { get; set; } = null!;

    [Required]
    public Offer Offer { get; set; } = null!;

    public OfferReview(DateTime date, double cleanlinessScore, double maintenanceScore, double convenienceScore, 
        double communicationScore, string comment, Renter renter, Offer offer)
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
        PersistenceContext.Add(this);
        
        if (Date == default)
        {
            throw new ValidationException("Date is not set.");
        }
    }

    [JsonConstructor]
    private OfferReview()
    {
    }
}