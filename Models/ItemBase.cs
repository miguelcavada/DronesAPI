using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class ItemBase
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]+$")]
        public string Name { get; set; }

        [Required]
        public int Weight { get; set; }

        public List<DroneItem>? DroneItems { get; set; }
    }
}
