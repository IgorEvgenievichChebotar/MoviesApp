using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Filters;

public class ShortNameValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var Name = (string)value;

        return Name.Length < 2
            ? new ValidationResult($"Не может быть такого короткого имени или фамилии")
            : ValidationResult.Success;
    }
}