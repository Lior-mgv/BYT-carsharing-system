using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class Booking
{
    public static readonly int PlatformFee = 5;
    [Required]
    public DateTime? StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
    public decimal TotalPrice
    {
        get
        {
            var rentalDays = (EndDate - StartDate)!.Value.Days;
            rentalDays = rentalDays == 0 ? 1 : rentalDays;
            return Offer.PricePerDay * rentalDays + PlatformFee;
        }
    }
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
        ValidationHelpers.ValidateObject(this);
        if (endDate <= startDate)
        {
            throw new ValidationException("End date must be after start date");
        }
        PersistenceContext.Add(this);
    }

    [JsonConstructor]
    private Booking()
    {
    }
}