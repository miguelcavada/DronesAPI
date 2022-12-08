using DronesAPI.Commons;
using DronesAPI.Commons.Enums;
using DronesAPI.Data;
using System.Text;

namespace DronesAPI.Models
{
    public class DatabaseInitializer
    {
        public static async void SeedData(DronesContext context)
        {
            if (!context.Drones.Any())
            {
                await context.Drones.AddRangeAsync(

                    new Drone { SerialNumber = "DRON-01", Model = ModelEnum.Lightweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { SerialNumber = "DRON-02", Model = ModelEnum.Middleweight.ToString(), WeightLimit = 500, BatteryCapacity = 50, State = StateEnum.IDLE.ToString() },
                    new Drone { SerialNumber = "DRON-03", Model = ModelEnum.Cruiserweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { SerialNumber = "DRON-04", Model = ModelEnum.Heavyweight.ToString(), WeightLimit = 500, BatteryCapacity = 100, State = StateEnum.IDLE.ToString() },
                    new Drone { SerialNumber = "DRON-05", Model = ModelEnum.Lightweight.ToString(), WeightLimit = 500, BatteryCapacity = 20, State = StateEnum.IDLE.ToString() }

                    );
                await context.SaveChangesAsync();
            }

            if (!context.Items.OfType<Medication>().Any())
            {
                await context.Items.AddRangeAsync(

                    new Medication { Name = "ACETAMINOPHEN", Weight = 200, Code = "MEDIC-01", Image = ConvertImageToBase64("Acetaminophen.jpeg") },
                    new Medication { Name = "ASPIRIN", Weight = 100, Code = "MEDIC-02", Image = ConvertImageToBase64("Antacid.jpeg") },
                    new Medication { Name = "ANTACID TABLETS", Weight = 300, Code = "MEDIC-03", Image = ConvertImageToBase64("Aspirin.jpg") },
                    new Medication { Name = "B-COMPLEX", Weight = 200, Code = "MEDIC-04", Image = ConvertImageToBase64("B_Complex.jpeg") },
                    new Medication { Name = "VITAMIN C", Weight = 400, Code = "MEDIC-05", Image = ConvertImageToBase64("Vitamin_C.jpeg") }

                    );
                await context.SaveChangesAsync();
            }
        }

        private static string ConvertImageToBase64(string imageName)
        {
            try
            {
                var fullPath = Directory.GetCurrentDirectory() + $"\\StaticFiles\\{imageName}";
                return Convert.ToBase64String(File.ReadAllBytes(fullPath));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
