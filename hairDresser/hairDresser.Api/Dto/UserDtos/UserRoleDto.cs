using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.UserDtos
{
    public class UserRoleDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
    }
}
