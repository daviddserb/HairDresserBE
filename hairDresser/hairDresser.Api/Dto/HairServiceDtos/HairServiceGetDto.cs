namespace hairDresser.Presentation.Dto.HairServiceDtos
{
    public class HairServiceGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //era int inainte
        public TimeSpan Duration { get; set; }

        //era float inainte
        public float Price { get; set; }
    }
}
