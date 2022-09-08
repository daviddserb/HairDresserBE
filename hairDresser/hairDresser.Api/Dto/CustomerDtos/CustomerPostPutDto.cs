using System.ComponentModel.DataAnnotations;

namespace hairDresser.Presentation.Dto.CustomerDtos
{
    public class CustomerPostPutDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
