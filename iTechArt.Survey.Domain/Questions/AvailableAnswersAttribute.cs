using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace iTechArt.Survey.Domain.Questions
{
    public class AvailableAnswersAttribute: ValidationAttribute
    {
        private static string GetErrorMessage() => "The available answer to the question must not be empty.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var availableAnswers = (List<string>) value;

            if (availableAnswers == null)
            {
                return ValidationResult.Success;
            }

            return availableAnswers.Any(availableAnswer => availableAnswer == null) ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
        }
    }
}
