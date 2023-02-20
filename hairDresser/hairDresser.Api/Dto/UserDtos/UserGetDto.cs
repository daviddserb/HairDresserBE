namespace hairDresser.Presentation.Dto.UserDtos
{
    public class UserGetDto
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }

        public string? Token { get; set; }

        public DateTime? Expiration { get; set; }
    }
}
