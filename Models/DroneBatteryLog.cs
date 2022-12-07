namespace DronesAPI.Models
{
    public class DroneBatteryLog
    {
        public Guid Id { get; set; }
        public Guid DroneId { get; set; }
        public int BatteryLevel {get;set;}
        public DateTime CreatedDate { get; set; }
    }
}
