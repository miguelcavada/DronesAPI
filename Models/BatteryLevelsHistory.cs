namespace DronesAPI.Models
{
    public class BatteryLevelsHistory
    {
        public Guid Id { get; set; }

        public Guid DroneId { get; set; }

        public decimal BatteryLevel {get;set;}

        public DateTime CreatedDate { get; set; }
    }
}
