using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.ReviewDtos
{
    public class ReviewPostDto
    {
        [Range(1, 5, ErrorMessage = "The rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public string Comments { get; set; }
    }
}
