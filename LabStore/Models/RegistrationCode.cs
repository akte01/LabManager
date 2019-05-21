using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace LabStore.Models
{
    public class RegistrationCode : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var code = ConfigurationManager.AppSettings["RegistrationCode"];
            return (value.ToString() == code)
            ? ValidationResult.Success
            : new ValidationResult("Kod dostępu jest nieprawidłowy");
        }
    }
}