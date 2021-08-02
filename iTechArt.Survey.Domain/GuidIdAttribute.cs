using System;
using System.ComponentModel.DataAnnotations;

namespace iTechArt.Survey.Domain
{
    public class GuidIdAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return value switch
            {
                Guid guid => guid != Guid.Empty,
                _ => false
            };
        }
    }
}
