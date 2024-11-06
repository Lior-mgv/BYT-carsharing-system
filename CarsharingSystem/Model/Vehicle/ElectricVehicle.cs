using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Model;

public class ElectricVehicle
{
    [Range(1, int.MaxValue)]
    public int BatteryCapacity { get; set; }
    [Range(1, int.MaxValue)]
    public int ChargingTime { get; set; }
}