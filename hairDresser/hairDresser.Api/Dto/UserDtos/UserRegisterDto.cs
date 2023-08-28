using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.UserDtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        // string? means nullable strings => makes the field not required.
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }
    }
}
