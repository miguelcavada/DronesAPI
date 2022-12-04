using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Medication
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$")]
        public string? Name { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9_]+$")]
        public string? Code { get; set; }

        public byte[]? Image { get; set; }
    }
}
