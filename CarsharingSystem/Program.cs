using CarsharingSystem.Model;
using Host = CarsharingSystem.Model.Host;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

new User{Email = "email2", FirstName = "firstname2", LastName = "lastname2", PhoneNumber = "1563626232",
    RenterInfo = new Renter{DrivingLicenseNumber = "DL22222222"}, HostInfo = new Host()};
new User{Email = "email3", FirstName = "firstname3", LastName = "lastname3", PhoneNumber = "1563626232"};

var users = User.GetObjects();
User.Persist();

// app.Run();