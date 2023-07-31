using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.ReviewDtos
{
    public class ReviewPostDto
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "The rating must be between {1} and {2}.")]
        public int Rating { get; set; }

        [Required]
        public string Comments { get; set; }
    }
}
