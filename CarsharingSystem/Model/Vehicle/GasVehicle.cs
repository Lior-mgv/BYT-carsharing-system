using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Model;

public class GasVehicle
{
    [Required]
    public string FuelType { get; set; }
    [Range(1, int.MaxValue)]
    public int FuelConsumption { get; set; }
}