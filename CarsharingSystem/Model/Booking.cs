namespace CarsharingSystem.Model;

public class Booking
{
    public const int PlatformFee = 5;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }

    public Renter Renter { get; set; }
    public Offer Offer { get; set; }
}