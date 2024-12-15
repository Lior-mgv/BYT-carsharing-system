using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class User
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    
    public Host? HostInfo { get; set; }
    public Renter? RenterInfo { get; set; }
    
    public bool IsRenter => RenterInfo != null;
    public bool IsHost => HostInfo != null;
    
    [JsonConstructor]
    private User()
    {
    }

    public User(string firstName, string lastName, string email, string phoneNumber, Host? hostInfo, Renter? renterInfo)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        HostInfo = hostInfo;
        RenterInfo = renterInfo;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.AddToExtent(this);
    }
}