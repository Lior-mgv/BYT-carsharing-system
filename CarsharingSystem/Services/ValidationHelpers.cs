using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Services;

public static class ValidationHelpers
{
    public static void ValidateObject(object obj)
    {
        var context = new ValidationContext(obj);
        List<ValidationResult> results = [];
        if (Validator.TryValidateObject(obj, context, results, validateAllProperties: true))
        {
            return;
        }

        var message = results.Aggregate("\n", (current, res) => current + res.ErrorMessage + "\n");
        throw new ValidationException(message);
    }
}