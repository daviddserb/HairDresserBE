using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.UserDtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address!")] // must follow a valid email format
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        // dataype? means the field can be nullable => value is not necessarily required.
        public string? Phone { get; set; }
    }
}