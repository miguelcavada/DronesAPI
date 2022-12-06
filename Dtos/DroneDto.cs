namespace DronesAPI.Dtos
{
    public class DroneDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public int WeightLimit { get; set; }
        public int BatteryCapacity { get; set; }
        public string State { get; set; }
        public IEnumerable<MedicationDto> Medications { get; set; }
    }
}
