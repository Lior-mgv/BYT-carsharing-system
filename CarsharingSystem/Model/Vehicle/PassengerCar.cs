namespace CarsharingSystem.Model;

public class PassengerCar : Vehicle
{
    public PassengerCar(string model, int numOfSeats, int numOfDoors, TransmissionType transmissionType, 
        ElectricVehicle? electricVehicleInfo, GasVehicle? gasVehicleInfo, Offer? offer, Host host)
        : base(model, numOfSeats, numOfDoors, transmissionType, electricVehicleInfo, gasVehicleInfo, offer, host)
    {
    }
}