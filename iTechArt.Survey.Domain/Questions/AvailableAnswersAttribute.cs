using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace iTechArt.Survey.Domain.Questions
{
    public class AvailableAnswersAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var availableAnswers = (List<string>) value;

            if (availableAnswers == null)
            {
                return ValidationResult.Success;
            }

            if (availableAnswers.Count == 0)
            {
                return new ValidationResult("The question does not have available answers.");
            }

            return availableAnswers.Any(availableAnswer => availableAnswer == null) 
                ? new ValidationResult("The available answer to the question must not be empty.") : ValidationResult.Success;
        }
    }
}
