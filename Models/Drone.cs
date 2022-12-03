using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Drone
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string SerialNumber { get; set; }

        public string Model { get; set; }

        [MaxLength(500)]
        public int WeightLimit { get; set; }

        public decimal BatteryCapacity { get; set; }

        public string State { get; set; }
    }
}
