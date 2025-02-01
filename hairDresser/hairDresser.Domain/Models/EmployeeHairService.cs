namespace hairDresser.Domain.Models
{
    public class EmployeeHairService
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }
        public User Employee { get; set; }

        public int HairServiceId { get; set; }
        public HairService HairService { get; set; }
    }
}
