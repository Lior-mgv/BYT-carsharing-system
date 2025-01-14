namespace CarsharingSystem.Model;

public interface IHost
{
    public void AddVehicle(Vehicle vehicle);

    public bool DeleteVehicle(Vehicle vehicle);
    
    public void UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle);

    public void AddOffer(Offer offer);

    public bool DeleteOffer(Offer offer);

    public void AddDiscountCode(DiscountCode discountCode);

    public bool DeleteDiscountCode(DiscountCode discountCode);

    public void DeleteHost();
}