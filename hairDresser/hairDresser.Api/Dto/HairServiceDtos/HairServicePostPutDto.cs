using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.HairServiceDtos
{
    public class HairServicePostPutDto
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can contain only letters and spaces")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 1439, ErrorMessage = "Duration must be between 1 and 1439 minutes")]
        // Changend the Duration property data type from TimeSpan to int because Angular doesn't support TimeSpan or something like this.
        public int DurationInMinutes { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1.0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }
}
