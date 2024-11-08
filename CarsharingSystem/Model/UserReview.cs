using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarsharingSystem.Services;

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

    public UserReview(DateTime date, double score, string comment, User reviewer, User reviewee)
    {
        Date = date;
        Score = score;
        Comment = comment;
        Reviewer = reviewer;
        Reviewee = reviewee;
        ValidationHelpers.ValidateObject(this);
        PersistenceContext.Add(this);
        
        if (Date == default)
        {
            throw new ValidationException("Date is not set.");
        }
    }

    [JsonConstructor]
    private UserReview()
    {
    }
}