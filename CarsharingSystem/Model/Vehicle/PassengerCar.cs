using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

[JsonDerivedType(typeof(PassengerCar), 2)]
public class PassengerCar : Vehicle
{
    public PassengerCar(string model, int numOfSeats, int numOfDoors, TransmissionType transmissionType, 
        ElectricVehicle? electricVehicleInfo, GasVehicle? gasVehicleInfo, Offer? offer, Host host)
        : base(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host)
    {
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.AddToExtent(this);
    }

    [JsonConstructor]
    public PassengerCar()
    {
    }
}