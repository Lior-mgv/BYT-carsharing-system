using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

[JsonDerivedType(typeof(BoxVan), 0)]
public class BoxVan : Vehicle
{
    [Range(1,double.MaxValue)]
    public double BoxVolume { get; set; }

    public BoxVan(string model, int numOfSeats, int numOfDoors, TransmissionType transmissionType, 
        ElectricVehicle? electricVehicleInfo, GasVehicle? gasVehicleInfo, Offer? offer, Host host, double boxVolume)
        : base(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host)
    {
        BoxVolume = boxVolume;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.Add(this);
    }

    [JsonConstructor]
    private BoxVan()
    {
    }
}