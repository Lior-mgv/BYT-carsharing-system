using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Model;

public class Booking
{
    public static readonly int PlatformFee = 5;
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Range(1,double.MaxValue)]
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    [Required]
    public Renter Renter { get; set; }
    [Required]
    public Offer Offer { get; set; }
}