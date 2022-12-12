using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Filters;

public class ShortNameValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var Name = (string) value;

        if (Name.Length < 2)
        {
            return new ValidationResult($"Не может быть такого короткого имени или фамилии");
        }
        
        return ValidationResult.Success;
    }
}