namespace hairDresser.Presentation.Dto.HairServiceDtos
{
    public class HairServiceGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
    }
}
