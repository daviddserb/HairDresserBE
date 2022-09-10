using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.CustomDataValidations
{
    public class DateNotInPast : ValidationAttribute
    {
        private readonly DateTime _date;

        public DateNotInPast(DateTime date) : base("{0} can't be in the past!")
        {
            _date = date;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_date < DateTime.Now)
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
