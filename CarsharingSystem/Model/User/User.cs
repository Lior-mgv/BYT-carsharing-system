using System.Text.Json.Serialization;
using CarsharingSystem.Abstractions;

namespace CarsharingSystem.Model;

public class User : ClassExtent<User>
{
    [JsonConstructor]
    private User()
    {
        _objects.Add(this);
    }

    public User(string firstName, string lastName, string email, string phoneNumber, Host? hostInfo, Renter? renterInfo)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        HostInfo = hostInfo;
        RenterInfo = renterInfo;
        DeepCopy();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Host? HostInfo { get; set; }
    public Renter? RenterInfo { get; set; }
    
    public bool IsRenter => RenterInfo != null;
    public bool IsHost => HostInfo != null;
}