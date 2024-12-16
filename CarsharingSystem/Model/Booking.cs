using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class Booking
{
    public static readonly int PlatformFee = 5;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice
    {
        get
        {
            var rentalDays = (EndDate - StartDate)!.Days;
            rentalDays = rentalDays == 0 ? 1 : rentalDays;
            return Offer.PricePerDay * rentalDays + PlatformFee;
        }
    }
    public BookingStatus Status { get; set; }
    [Required]
    public Renter Renter { get; set; }
    [Required]
    public Offer Offer { get; set; }

    private readonly Dictionary<string, DiscountCode> _discountCodes = [];
    public Dictionary<string, DiscountCode> DiscountCodes => new (_discountCodes);

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

        offer.AddBooking(this);
        renter.AddBooking(this);
        PersistenceContext.AddToExtent(this);
    }

    [JsonConstructor]
    private Booking()
    {
    }

    public void AddDiscountCode(DiscountCode discountCode)
    {
        ArgumentNullException.ThrowIfNull(discountCode);

        if (!_discountCodes.TryAdd(discountCode.Code, discountCode))
        {
            throw new InvalidOperationException("Booking already contains this discount code");
        }
    }

    public bool DeleteDiscountCode(string code)
    {
        throw new NotImplementedException();
    }

    public void UpdateDiscountCode(string oldCode, DiscountCode newDiscountCode)
    {
        throw new NotImplementedException();
    }

    public void DeleteBooking()
    {
        if (Offer.Bookings.Contains(this))
        {
            Offer.RemoveBooking(this);
        }

        if (Renter.Bookings.Contains(this))
        {
            Renter.RemoveBooking(this);
        }

        PersistenceContext.DeleteFromExtent(this);
    }
}