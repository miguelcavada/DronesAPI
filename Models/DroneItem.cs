using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class DroneItem
    {
        [Key]
        public int DroneId { get; set; }    
        public Drone? Drone { get; set; }
        [Key]
        public int ItemBaseId { get; set; }    
        public ItemBase? ItemBase { get; set; }
        public int WeightMedication { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
