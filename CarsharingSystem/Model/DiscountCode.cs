using System.ComponentModel.DataAnnotations;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class DiscountCode
{
    private static int _minPercentage = 5;
    private static int _maxPercentage = 50;
    [Required]
    private string _code;
    public string Code {
        get => _code;
        set
        {
            var oldCode = _code;
            _code = value;
            foreach (var booking in _bookings.Where(b => b.DiscountCodes.ContainsKey(oldCode)))
            {
                booking.UpdateDiscountCode(oldCode, this);
            }
        }
    }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime ExpirationDate { get; set; }

    public Host Host { get; set; }

    private readonly List<Booking> _bookings = [];
    public List<Booking> Bookings => [.._bookings];
    public DiscountCode(string code, DateTime startDate, DateTime expirationDate, Host host)
    {
        _code = code;
        StartDate = startDate;
        ExpirationDate = expirationDate;
        Host = host;
        ValidationHelpers.ValidateObject(this);
        host.AddDiscountCode(this);
        
        PersistenceContext.AddToExtent(this);
    }

    public void AddBooking(Booking booking)
    {
        throw new NotImplementedException();
    }
    
    public bool DeleteBooking(Booking booking)
    {
        throw new NotImplementedException();
    }
    
    public void UpdateBooking(Booking oldBooking, Booking newBooking)
    {
        throw new NotImplementedException();
    }
    public void DeleteDiscountCode()
    {
        throw new NotImplementedException();
    }
}