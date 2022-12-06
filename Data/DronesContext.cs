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
            modelBuilder.Entity<DroneItem>().HasKey(x => new { x.DroneId, x.ItemBaseId });

            modelBuilder.Entity<DroneItem>()
                .HasOne(x => x.Drone)
                .WithMany(x => x.DroneItems)
                .HasForeignKey(x => x.DroneId);

            modelBuilder.Entity<DroneItem>()
                .HasOne(x => x.ItemBase)
                .WithMany(x => x.DroneItems)
                .HasForeignKey(x => x.ItemBaseId);
        }

        public DbSet<Drone> Drones { get; set; }
        public DbSet<ItemBase> Items { get; set; }
        public DbSet<DroneItem> DroneItems { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<BatteryLevelsHistory> BatteryLevelsHistories { get; set; }
    }
}
