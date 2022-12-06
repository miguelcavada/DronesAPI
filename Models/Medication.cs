using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Medication : ItemBase
    {
        [Required]
        [RegularExpression("^[A-Z0-9_]+$")]
        public string Code { get; set; }

        public string Image { get; set; }
    }
}
