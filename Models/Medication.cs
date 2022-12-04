using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class Medication
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public string Code { get; set; }

        public string Image { get; set; }

    }
}
