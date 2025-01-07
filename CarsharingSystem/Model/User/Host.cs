using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class Host
{
    public Host(User user)
    {
        User = user;
        ValidationHelpers.ValidateObject(this);
    }

    [JsonConstructor]
    private Host()
    {
    }

    [Required]
    public User User { get; set; }
    private readonly List<Vehicle> _vehicles = [];
    public List<Vehicle> Vehicles => [.._vehicles];

    private readonly List<Offer> _offers = [];
    public List<Offer> Offers => [.._offers];
    
    private readonly List<DiscountCode> _discountCodes = [];
    public List<DiscountCode> DiscountCodes => [.._discountCodes];
 
    public void AddVehicle(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        if (_vehicles.Contains(vehicle))
        {
            throw new InvalidOperationException("Host already contains this vehicle");
        }
        _vehicles.Add(vehicle);
        if (vehicle.Host != this)
        {
            vehicle.Host = this;
        }
    }

    public bool DeleteVehicle(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        if (_vehicles.Contains(vehicle))
        {
            var res = _vehicles.Remove(vehicle);
            vehicle.DeleteVehicle();
            
            return res;
        }else
        {
            throw new InvalidCastException("Host does not contain this vehicle");
        }
        
    }

    public void UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle)
    {
        if(!DeleteVehicle(oldVehicle)) return;
        AddVehicle(newVehicle);
    }
    
    public void AddOffer(Offer offer)
    {
        ArgumentNullException.ThrowIfNull(offer);

        if (_offers.Contains(offer))
        {
            throw new InvalidOperationException("Host already contains this offer");
        }
        _offers.Add(offer);
        if (offer.Host != this)
        {
            offer.Host = this;
        }
    }

    public bool DeleteOffer(Offer offer)
    {
        ArgumentNullException.ThrowIfNull(offer);
        var res = _offers.Remove(offer);
        offer.DeleteOffer(offer);
        return res;
    }
    

    public void AddDiscountCode(DiscountCode discountCode)
    {
        ArgumentNullException.ThrowIfNull(discountCode);

        if (_discountCodes.Contains(discountCode))
        {
            throw new InvalidOperationException("Host already contains this discount code");
        }
        _discountCodes.Add(discountCode);
        if (discountCode.Host != this)
        {
            discountCode.Host = this;
        }
    }
    
    public void UpdateDiscountCode(DiscountCode oldDiscountCode, DiscountCode newDiscountCode)
    {
        if(!DeleteDiscountCode(oldDiscountCode)) return;
        AddDiscountCode(newDiscountCode);
    }
    
    public bool DeleteDiscountCode(DiscountCode discountCode)
    {
        ArgumentNullException.ThrowIfNull(discountCode);
        var res = _discountCodes.Remove(discountCode);
        discountCode.DeleteDiscountCode();
        return res;
    }

    public void DeleteHost(Host host)
    {
        foreach (var offer in host._offers.ToList())
        {
            DeleteOffer(offer);
        }

        foreach (var code in host._discountCodes.ToList())
        {
            DeleteDiscountCode(code);
        }

        foreach (var vehicle in _vehicles.ToList())
        {
            DeleteVehicle(vehicle);
        }

        User.HostInfo = null;
    }
}