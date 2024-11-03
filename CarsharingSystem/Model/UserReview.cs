using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Model;

public class UserReview
{
    [Range(1,5)]
    public double Score { get; set; }
    [Required(AllowEmptyStrings = true)] 
    public string Comment { get; set; } = null!;
    [Required]
    public DateTime Date;
    [Required]
    public User Reviewer { get; set; } = null!;

    [Required]
    public User Reviewee { get; set; } = null!;
}