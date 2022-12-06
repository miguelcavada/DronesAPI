using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class ItemBase
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]+$")]
        public string? Name { get; set; }

        [Required]
        public int Weight { get; set; }

        public List<Drone>? Drones { get; set; }
    }
}
