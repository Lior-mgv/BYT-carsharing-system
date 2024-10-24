using CarsharingSystem.Abstractions;

namespace CarsharingSystem.Model;

public abstract class Vehicle : ClassExtent<Vehicle>
{
    public string Model { get; set; }
    public int NumOfSeats { get; set; }
    public int NumOfDoors { get; set; }
    public TransmissionType TransmissionType { get; set; }
    public IEnumerable<string> AdditionalFeatures { get; set; } = new List<string>();

    public ElectricVehicle? ElectricVehicleInfo { get; set; }
    public GasVehicle? GasVehicleInfo { get; set; }

    public bool IsElectric => ElectricVehicleInfo != null;
    public bool IsGas => GasVehicleInfo != null;

    public Offer? Offer { get; set; }
}