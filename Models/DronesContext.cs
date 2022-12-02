using Microsoft.EntityFrameworkCore;

namespace DronesAPI.Models
{
    public class DronesContext : DbContext
    {
        public DronesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Drone> Drones { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<BatteryLevelsHistory> BatteryLevelsHistories { get; set; }
    }
}
