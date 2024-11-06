using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

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

    public Booking(DateTime startDate, DateTime endDate, decimal totalPrice, BookingStatus status, Renter renter, Offer offer)
    {
        StartDate = startDate;
        EndDate = endDate;
        TotalPrice = totalPrice;
        Status = status;
        Renter = renter;
        Offer = offer;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.Add(this);
    }

    [JsonConstructor]
    private Booking()
    {
    }
}