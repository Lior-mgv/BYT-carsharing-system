using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Abstractions;

namespace CarsharingSystem.Model;

public class Vehicle : ClassExtent<Vehicle>
{
    [JsonConstructor]
    private Vehicle()
    {
    }

    public Vehicle(string model, int numOfSeats, int numOfDoors, TransmissionType transmissionType, 
        ElectricVehicle? electricVehicleInfo, GasVehicle? gasVehicleInfo, Offer? offer, Host host)
    {
        Model = model;
        NumOfSeats = numOfSeats;
        NumOfDoors = numOfDoors;
        TransmissionType = transmissionType;
        ElectricVehicleInfo = electricVehicleInfo;
        GasVehicleInfo = gasVehicleInfo;
        Offer = offer;
        Host = host;
        if (GetType() == typeof(Vehicle))
        {
            _objects.Add(DeepCopy());
        }
    }

    [Required]
    public string Model { get; set; }
    [Range(1,int.MaxValue)]
    public int NumOfSeats { get; set; }
    [Range(1,int.MaxValue)]
    public int NumOfDoors { get; set; }

    public TransmissionType TransmissionType { get; set; }
    public List<string> AdditionalFeatures { get; set; } = [];

    public ElectricVehicle? ElectricVehicleInfo { get; set; }
    public GasVehicle? GasVehicleInfo { get; set; }

    public bool IsElectric => ElectricVehicleInfo != null;
    public bool IsGas => GasVehicleInfo != null;

    public Offer? Offer { get; set; }
    [Required]
    public Host Host { get; set; }
}