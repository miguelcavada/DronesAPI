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

                    new Drone { Id = Guid.NewGuid(), SerialNumber = "DRON-01", Model = ModelEnum.Lightweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { Id = Guid.NewGuid(), SerialNumber = "DRON-02", Model = ModelEnum.Middleweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { Id = Guid.NewGuid(), SerialNumber = "DRON-03", Model = ModelEnum.Cruiserweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { Id = Guid.NewGuid(), SerialNumber = "DRON-04", Model = ModelEnum.Heavyweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() }

                    );
                await context.SaveChangesAsync();
            }

            if (!context.Medications.Any())
            {
                await context.Medications.AddRangeAsync(

                    new Medication { Id = Guid.NewGuid(), Name = "ACETAMINOPHEN", Weight = 200, Code = "MEDIC_01", Image = null },
                    new Medication { Id = Guid.NewGuid(), Name = "ASPIRIN", Weight = 100, Code = "MEDIC_02", Image = null },
                    new Medication { Id = Guid.NewGuid(), Name = "ANTACID TABLETS", Weight = 300, Code = "MEDIC_03", Image = null }

                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
