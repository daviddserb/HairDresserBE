using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.UserDtos
{
    public class UserRoleDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        public string Role { get; set; }
    }
}
