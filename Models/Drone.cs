using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Drone
    {
        public Guid Id { get; set; }

        [StringLength(maximumLength: 100)]
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public int WeightLimit { get; set; }
        public decimal BatteryCapacity { get; set; }
        public string State { get; set; }
    }
}
