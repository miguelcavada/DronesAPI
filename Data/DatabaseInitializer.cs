using DronesAPI.Commons;
using DronesAPI.Commons.Enums;
using DronesAPI.Data;

namespace DronesAPI.Models
{
    public class DatabaseInitializer
    {
        public static async void SeedData(DronesContext context)
        {
            if (!context.Drones.Any())
            {
                await context.Drones.AddRangeAsync(

                    new Drone { Id = 1, SerialNumber = "DRON-01", Model = ModelEnum.Lightweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { Id = 2, SerialNumber = "DRON-02", Model = ModelEnum.Middleweight.ToString(), WeightLimit = 500, BatteryCapacity = 50, State = StateEnum.IDLE.ToString() },
                    new Drone { Id = 3, SerialNumber = "DRON-03", Model = ModelEnum.Cruiserweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { Id = 4, SerialNumber = "DRON-04", Model = ModelEnum.Heavyweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { Id = 5, SerialNumber = "DRON-05", Model = ModelEnum.Lightweight.ToString(), WeightLimit = 500, BatteryCapacity = 20, State = StateEnum.IDLE.ToString() }

                    );
                await context.SaveChangesAsync();
            }

            if (!context.Items.OfType<Medication>().Any())
            {
                await context.Items.AddRangeAsync(

                    new Medication { Id = 1, Name = "ACETAMINOPHEN", Weight = 200, Code = "MEDIC-01", Image = string.Empty },
                    new Medication { Id = 2, Name = "ASPIRIN", Weight = 100, Code = "MEDIC-02", Image = string.Empty },
                    new Medication { Id = 3, Name = "ANTACID TABLETS", Weight = 300, Code = "MEDIC-03", Image = string.Empty },
                    new Medication { Id = 4, Name = "B-COMPLEX", Weight = 200, Code = "MEDIC-04", Image = string.Empty },
                    new Medication { Id = 5, Name = "VITAMIN C", Weight = 400, Code = "MEDIC-05", Image = string.Empty }

                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
