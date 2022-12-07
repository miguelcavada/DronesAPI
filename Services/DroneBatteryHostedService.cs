using AutoMapper;
using DronesAPI.Data;
using DronesAPI.Dtos;
using DronesAPI.Models;

namespace DronesAPI.Services
{
    public class DroneBatteryHostedService : IHostedService, IDisposable
    {
        private Timer? _timer;
        public IServiceProvider Service { get; private set; }

        public DroneBatteryHostedService(IServiceProvider service)
        {
            Service = service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            using (var scope = Service.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DronesContext>();

                var dronesBatteryLevels = context.Drones.Select(x => new { x.Id, x.BatteryCapacity });
                foreach (var item in dronesBatteryLevels)
                {
                    context.DroneBatteryLogs.Add(new DroneBatteryLog
                    {
                        DroneId = item.Id,
                        BatteryLevel = item.BatteryCapacity,
                        CreatedDate = DateTime.Now
                    });
                }
                context.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
}
