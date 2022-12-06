namespace DronesAPI.Dtos
{
    public class MedicationDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Weight { get; set; }
        public string? Code { get; set; }
        public string? Image { get; set; }
    }
}
