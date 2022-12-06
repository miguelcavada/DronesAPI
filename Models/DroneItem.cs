using System.ComponentModel.DataAnnotations;

namespace DronesAPI.Models
{
    public class DroneItem
    {
        [Key]
        public Guid DroneId { get; set; }    
        public Drone? Drone { get; set; }
        [Key]
        public Guid ItemBaseId { get; set; }    
        public ItemBase? ItemBase { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
