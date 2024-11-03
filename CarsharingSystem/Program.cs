using CarsharingSystem.Model;
using CarsharingSystem.Services;
using Host = CarsharingSystem.Model.Host;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
PersistenceContext.LoadContext();

// var vehicles = Vehicle.GetObjects();
// var offers = Offer.GetObjects();

var hostInfo1 = new Host();
var renterInfo1 = new Renter { DrivingLicenseNumber = "DL22222222" };

var user1 = new User(
    firstName: "firstname2",
    lastName: "lastname2",
    email: "email2",
    phoneNumber: "1563626232",
    hostInfo: hostInfo1,
    renterInfo: renterInfo1
);

var user2 = new User(
    firstName: "firstname3",
    lastName: "lastname3",
    email: "email3",
    phoneNumber: "1563626232",
    hostInfo: null,
    renterInfo: null
);

user2.Email = "changed email";

var users = User.GetObjects();

PersistenceContext.SaveContext();

// var user1 = new User{Email = "email2", FirstName = "firstname2", LastName = "lastname2", PhoneNumber = "1563626232",
//     RenterInfo = new Renter{DrivingLicenseNumber = "DL22222222"}, HostInfo = new Host()};
// var user2 = new User{Email = "email3", FirstName = "firstname3", LastName = "lastname3", PhoneNumber = "1563626232"};

// Vehicle vehicle1 = new Vehicle
// {
//     Model = "Tesla Model S",
//     NumOfSeats = 5,
//     NumOfDoors = 4,
//     TransmissionType = TransmissionType.Automatic,
//     AdditionalFeatures = new List<string> { "GPS", "Leather Seats", "Bluetooth" },
//     ElectricVehicleInfo = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5},
//     Host = user1.HostInfo
// };
//
// Vehicle vehicle2 = new Vehicle
// {
//     Model = "Toyota Corolla",
//     NumOfSeats = 5,
//     NumOfDoors = 4,
//     TransmissionType = TransmissionType.Automatic,
//     AdditionalFeatures = new List<string> { "Air Conditioning", "Cruise Control", "Backup Camera" },
//     ElectricVehicleInfo = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5},
//     Host = user1.HostInfo
//     // Host and Offer will be set separately
// };
//
// Offer offer1 = new Offer
// {
//     PricePerDay = 99.99m,
//     Description = "Tesla Model S - Luxury Electric Sedan",
//     MinimalAge = 25,
//     Vehicle = vehicle1
// };
// Offer offer2 = new Offer
// {
//     PricePerDay = 49.99m,
//     Description = "Toyota Corolla - Reliable Sedan",
//     MinimalAge = 21,
//     Vehicle = vehicle2
// };


// app.Run();