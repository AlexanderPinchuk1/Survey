using System;
using System.ComponentModel.DataAnnotations;

namespace iTechArt.Survey.Domain
{
    public class GuidIdAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var id = (Guid)value;

            return id != Guid.Empty;
        }
    }
}
