using DronesAPI.Commons;
using DronesAPI.Commons.Enums;
using DronesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DronesAPI.Data
{
    public class DronesContext : DbContext
    {
        public DronesContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Drone> Drones { get; set; }
        public DbSet<ItemBase> Items { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<BatteryLevelsHistory> BatteryLevelsHistories { get; set; }
    }
}
