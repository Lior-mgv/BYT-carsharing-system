using CarsharingSystem.Context;
using CarsharingSystem.Model;
using CarsharingSystem.Services;
using Host = CarsharingSystem.Model.Host;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPersistenceManager, JsonPersistenceManager>();
builder.Services.AddSingleton<ApplicationContext>();
var app = builder.Build();

var context = app.Services.GetService<ApplicationContext>();

context.Users.Add(new User
{
    Email = "email1", FirstName = "firstname1", LastName = "lastname1", PhoneNumber = "1563626232",
    RenterInfo = new Renter { DrivingLicenseNumber = "DL11111111" }
});
context.Users.Add(new User{Email = "email2", FirstName = "firstname2", LastName = "lastname2", PhoneNumber = "1563626232",
        RenterInfo = new Renter{DrivingLicenseNumber = "DL22222222"}, HostInfo = new Host()});
context.Users.Add(new User{Email = "email3", FirstName = "firstname3", LastName = "lastname3", PhoneNumber = "1563626232"});

context.Vehicles.Add(new Vehicle(){Model = "model1", NumOfDoors = 4, NumOfSeats = 4,
    ElectricVehicleInfo = new ElectricVehicle(){BatteryCapacity = 1, ChargingTime = 2},
    AdditionalFeatures = new List<string>(){"feature1", "feature2"}});

await context.SaveAsync();

await context.LoadContextAsync();

Console.WriteLine();

// app.Run();