using System.ComponentModel.DataAnnotations;

namespace BlossmAPI.Attributes
{
    public class DateGreaterThanAttribute: ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if ((DateTime)value > comparisonValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
