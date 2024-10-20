namespace CarsharingSystem.Model;

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Host? HostInfo { get; set; }
    public Renter? RenterInfo { get; set; }
    
    public bool IsRenter => RenterInfo != null;
    public bool IsHost => HostInfo != null;
    
}