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
        ArgumentNullException.ThrowIfNull(booking);

        if (_bookings.Contains(booking))
        {
            throw new InvalidOperationException("Booking already exists");
        }
        
        _bookings.Add(booking);
        if (!booking.DiscountCodes.ContainsKey(Code))
        {
            booking.AddDiscountCode(this);
        }
    }
    
    public bool DeleteBooking(Booking booking)
    {
        var res = _bookings.Remove(booking);
        if (res && booking.DiscountCodes.ContainsKey(Code))
        {
            booking.DeleteDiscountCode(_code);   
        }
        return res;
    }
    
    public void UpdateBooking(Booking oldBooking, Booking newBooking)
    {
        DeleteBooking(oldBooking);
        AddBooking(newBooking);
    }
    public void DeleteDiscountCode()
    {
        Host.DeleteDiscountCode(this);
        foreach (var booking in _bookings)
        {
            booking.DeleteDiscountCode(_code);
        }
        
        PersistenceContext.DeleteFromExtent(this);
    }
}