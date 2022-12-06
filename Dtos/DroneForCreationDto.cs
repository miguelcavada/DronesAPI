namespace DronesAPI.Dtos
{
    public class DroneForCreationDto
    {
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public int WeightLimit { get; set; }
        public int BatteryCapacity { get; set; }
        public string? State { get; set; }        
    }
}
