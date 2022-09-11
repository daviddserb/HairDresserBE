using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.CustomDataValidations
{
    public sealed class DateStartAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Console.WriteLine("### DateStartAttribute:");
            // Get the StartDate input.
            DateTime dateStart = (DateTime)value;
            Console.WriteLine("dateStart= " + dateStart);

            // Appointment must start in the future time.
            return (dateStart > DateTime.Now);
        }
    }

    public sealed class DateGreaterThanAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' must be greater than '{1}'";
        private string _basePropertyName;

        public DateGreaterThanAttribute(string basePropertyName) : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        // Override default FormatErrorMessage Method.
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        // Override IsValid.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Console.WriteLine("@@@ DateGreaterThanAttribute:");

            //Get PropertyInfo Object
            var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

            //Get Value of the property
            var startDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            Console.WriteLine("startDate= " + startDate);

            var endDate = (DateTime)value;
            Console.WriteLine("endDate= " + endDate);

            if (endDate <= startDate)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }
            return null;
        }
    }
}
