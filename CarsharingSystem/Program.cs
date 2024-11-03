using CarsharingSystem.Model;
using CarsharingSystem.Services;
using Host = CarsharingSystem.Model.Host;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
PersistenceContext.LoadContext();

var users = User.GetObjects();
var vehicles = Vehicle.GetObjects();
var offers = Offer.GetObjects();

var hostInfo1 = new Host();
var renterInfo1 = new Renter { DrivingLicenseNumber = "DL22222222" };

var user1 = new User(
    firstName: null,
    lastName: "",
    email: "email1",
    phoneNumber: "1233",
    hostInfo: hostInfo1,
    renterInfo: renterInfo1
);

var user2 = new User(
    firstName: "firstname3",
    lastName: "lastname3",
    email: "email2@gmail.com",
    phoneNumber: "1563626232",
    hostInfo: null,
    renterInfo: null
);

user2.Email = "changed email";

var electricVehicleInfo1 = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5 };
var electricVehicleInfo2 = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5 };

// Create vehicle1 using the public constructor
Vehicle vehicle1 = new Vehicle(
    model: "Tesla Model S",
    numOfSeats: 5,
    numOfDoors: 4,
    transmissionType: TransmissionType.Automatic,
    electricVehicleInfo: electricVehicleInfo1,
    gasVehicleInfo: null, // Assuming no GasVehicle info is needed
    offer: null, // Offer will be set separately
    host: user1.HostInfo
);

// Create vehicle2 using the public constructor
Vehicle vehicle2 = new BoxVan(
    model: "Toyota Corolla",
    numOfSeats: 5,
    numOfDoors: 4,
    transmissionType: TransmissionType.Automatic,
    electricVehicleInfo: electricVehicleInfo2,
    gasVehicleInfo: null, // Assuming no GasVehicle info is needed
    offer: null, // Offer will be set separately
    host: user1.HostInfo,
    boxVolume: 15
);

// Create offer1 using the public constructor
Offer offer1 = new Offer(
    pricePerDay: 99.99m,
    description: "Tesla Model S - Luxury Electric Sedan",
    minimalAge: 25,
    vehicle: vehicle1
);

// Create offer2 using the public constructor
Offer offer2 = new Offer(
    pricePerDay: 49.99m,
    description: "Toyota Corolla - Reliable Sedan",
    minimalAge: 21,
    vehicle: vehicle2
);

PersistenceContext.SaveContext();


// app.Run();