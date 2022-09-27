using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Validators;

public class MinimumYearAllowedAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // get the user enter the value
        var userEnteredYear = ((DateTime)value).Year;
        if (userEnteredYear < 1900)
        {
            return new ValidationResult("Year should be not less than 1900");
        }
        return ValidationResult.Success;
    }
}