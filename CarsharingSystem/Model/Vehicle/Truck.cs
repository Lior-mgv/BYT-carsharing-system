using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Model;

public class Truck : Vehicle
{
    [Range(1, double.MaxValue)]
    public double BedLength { get; set; }

    public Truck(string model, int numOfSeats, int numOfDoors, TransmissionType transmissionType, 
        ElectricVehicle? electricVehicleInfo, GasVehicle? gasVehicleInfo, Offer? offer, Host host, double bedLength) 
        : base(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host)
    {
        BedLength = bedLength;
    }
}