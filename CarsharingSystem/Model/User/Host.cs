namespace CarsharingSystem.Model;

public class Host
{
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

        if (vehicle.Host != this)
        {
            vehicle.Host = this;
        }
    }

    public bool DeleteVehicle(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        var res = _vehicles.Remove(vehicle);
        vehicle.DeleteVehicle();
        return res;
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
            throw new InvalidOperationException("Host already contains this vehicle");
        }

        if (offer.Host != this)
        {
            offer.Host = this;
        }
    }

    public bool DeleteOffer(Offer offer)
    {
        ArgumentNullException.ThrowIfNull(offer);
        var res = _offers.Remove(offer);
        offer.DeleteOffer();
        return res;
    }

    public void UpdateVehicle(Offer oldOffer, Offer newOffer)
    {
        if(!DeleteOffer(oldOffer)) return;
        AddOffer(newOffer);
    }

    public void AddDiscountCode(DiscountCode discountCode)
    {
        throw new NotImplementedException();
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
}