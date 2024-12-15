using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

namespace CarsharingSystem.Model;

public class UserReview
{
    [Range(1,5)]
    public int Score { get; set; }
    [Required(AllowEmptyStrings = true)] 
    public string Comment { get; set; } = null!;
    
    public DateTime Date;
    [Required]
    public User Reviewer { get; set; } = null!;

    [Required]
    public User Reviewee { get; set; } = null!;

    public UserReview(DateTime date, int score, string comment, User reviewer, User reviewee)
    {
        Date = date;
        Score = score;
        Comment = comment;
        Reviewer = reviewer;
        Reviewee = reviewee;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.AddToExtent(this);
    }

    [JsonConstructor]
    private UserReview()
    {
    }
}