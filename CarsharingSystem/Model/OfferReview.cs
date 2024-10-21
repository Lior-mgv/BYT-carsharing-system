namespace CarsharingSystem.Model;

public class OfferReview
{
    public double CleanlinessScore { get; set; }
    public double MaintenanceScore { get; set; }
    public double ConvenienceScore { get; set; }
    public double CommunicationScore { get; set; }
    public double AverageScore => (CleanlinessScore + MaintenanceScore + ConvenienceScore + CommunicationScore) / 4;
    public string Comment { get; set; }
    public DateTime Date => DateTime.Now;
    public Renter Renter { get; set; }
    public Offer Offer { get; set; }
}