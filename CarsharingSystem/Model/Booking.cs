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
    public decimal TotalPrice => Offer.PricePerDay * (EndDate - StartDate).Days + PlatformFee;
    public BookingStatus Status { get; set; }
    [Required]
    public Renter Renter { get; set; }
    [Required]
    public Offer Offer { get; set; }

    public Booking(DateTime startDate, DateTime endDate, BookingStatus status, Renter renter, Offer offer)
    {
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        Renter = renter;
        Offer = offer;
        if (startDate == default || endDate == default)
        {
            throw new ValidationException("Start and end date must be provided");
        }
        if (endDate < startDate)
        {
            throw new ValidationException("End date must be after start date");
        }
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.Add(this);
    }

    [JsonConstructor]
    private Booking()
    {
    }
}