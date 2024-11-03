namespace CarsharingSystem.Model;

public class BoxVan : Vehicle
{
    public double BoxVolume { get; set; }

    public BoxVan(string model, int numOfSeats, int numOfDoors, TransmissionType transmissionType, 
        ElectricVehicle? electricVehicleInfo, GasVehicle? gasVehicleInfo, Offer? offer, Host host, double boxVolume)
        : base(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host)
    {
        BoxVolume = boxVolume;
    }
}