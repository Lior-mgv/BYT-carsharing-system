namespace CarsharingSystem.Model;

public class UserReview
{
    public double Score { get; set; }
    public string Comment { get; set; }
    public DateTime Date => DateTime.Now;
    
    public User Reviewer { get; set; }
    public User Reviewee { get; set; }
}