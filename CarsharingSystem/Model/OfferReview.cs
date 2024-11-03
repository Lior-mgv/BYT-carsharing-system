using System.ComponentModel.DataAnnotations;

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
}