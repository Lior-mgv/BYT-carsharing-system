using CarsharingSystem.Model;
using CarsharingSystem.Services;
using Host = CarsharingSystem.Model.Host;

PersistenceContext.LoadContext();

var users = PersistenceContext.GetExtent<User>();
var boxVans = PersistenceContext.GetExtent<BoxVan>();
var trucks = PersistenceContext.GetExtent<Truck>();
var offers = PersistenceContext.GetExtent<Offer>();

// var hostInfo1 = new Host();
// var renterInfo1 = new Renter { DrivingLicenseNumber = "DL22222222" };
//
// var user1 = new User(
//     firstName: "firstname1",
//     lastName: "lastname1",
//     email: "email1@gmail.com",
//     phoneNumber: "1233",
//     hostInfo: hostInfo1,
//     renterInfo: renterInfo1
// );

// var user2 = new User(
//     firstName: "firstname3",
//     lastName: "lastname3",
//     email: "email2@gmail.com",
//     phoneNumber: "1563626232",
//     hostInfo: null,
//     renterInfo: null
// );

// var user4 = new User(
//     firstName: "firstname4",
//     lastName: "lastname4",
//     email: "email4@gmail.com",
//     phoneNumber: "1233",
//     hostInfo: hostInfo1,
//     renterInfo: renterInfo1
// );
//
// var usersToSave = new List<User>() { user1, user2 };
// PersistenceManager.Save(usersToSave, "User.json");
// var deserialized = PersistenceManager.Load<User>()
//
// user4.Email = "changed email";

var electricVehicleInfo1 = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5 };
var electricVehicleInfo2 = new ElectricVehicle { BatteryCapacity = 100, ChargingTime = 5 };

// Create vehicle1 using the public constructor
// BoxVan vehicle1 = new BoxVan(
//     model: "Tesla Model S",
//     numOfSeats: 5,
//     numOfDoors: 4,
//     transmissionType: TransmissionType.Automatic,
//     electricVehicleInfo: electricVehicleInfo1,
//     gasVehicleInfo: null,
//     offer: null,
//     host: user1.HostInfo,
//     boxVolume: 10
// );

// Create vehicle2 using the public constructor
// Truck vehicle2 = new Truck(
//     model: "Toyota Corolla",
//     numOfSeats: 5,
//     numOfDoors: 4,
//     transmissionType: TransmissionType.Automatic,
//     electricVehicleInfo: electricVehicleInfo2,
//     gasVehicleInfo: null, // Assuming no GasVehicle info is needed
//     offer: null, // offer will be set separately
//     host: user1.HostInfo,
//     bedLength: 15
// );
//
// hostInfo1.Vehicles.Add(vehicle1);
// hostInfo1.Vehicles.Add(vehicle2);

// Create offer1 using the public constructor
// offer offer1 = new offer(
//     pricePerDay: 99.99m,
//     description: "Tesla Model S - Luxury Electric Sedan",
//     minimalAge: 25,
//     vehicle: vehicle1
//     addresses: new List<Address>(){new Address("city", "Street", 1, "PostalCode")}
// );

var address = new Address("city", "street", 5, "postcode");

// offer1.AddAddress(address);
//
// offer1.DeleteAddress(address);
//
// // Create offer2 using the public constructor
// offer offer2 = new offer(
//     pricePerDay: 49.99m,
//     description: "Toyota Corolla - Reliable Sedan",
//     minimalAge: 21,
//     vehicle: vehicle2
//     Address: new List<Address>(){new Address("city", "Street", 1, "PostalCode")}
// );

PersistenceContext.SaveContext();


// app.Run();