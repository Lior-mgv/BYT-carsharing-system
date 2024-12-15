using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

[JsonDerivedType(typeof(BoxVan), 0)]
[JsonDerivedType(typeof(Truck), 1)]
[JsonDerivedType(typeof(PassengerCar), 2)]
public abstract class Vehicle
{
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
    public Host Host { get; set; }
    
    [JsonConstructor]    
    protected Vehicle()
    {
    }
    
    protected Vehicle(string model, int numOfSeats, int numOfDoors, TransmissionType transmissionType, 
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
        if (host == null)
        {
            throw new ValidationException("Host cannot be null");
        }

        host.AddVehicle(this);
    }

    public void DeleteVehicle()
    {
        if (Host.Vehicles.Contains(this))
        {
            Host.DeleteVehicle(this);
        }
        Offer?.DeleteOffer();
        PersistenceContext.DeleteFromExtent(this);
    }
}