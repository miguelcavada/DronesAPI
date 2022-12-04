using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Drone
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for the SerialNumber is 100 characters.")]
        public string? SerialNumber { get; set; }

        [Required]
        public string? Model { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for the WeightLimit is 500 g (gram).")]
        public int WeightLimit { get; set; }

        public int BatteryCapacity { get; set; }

        public string? State { get; set; }
    }
}
