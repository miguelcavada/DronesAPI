using Microsoft.EntityFrameworkCore;

namespace DronesAPI.Models
{
    public class DronesContext : DbContext
    {
        public DronesContext(DbContextOptions options) : base(options)
        {
        }
    }
}
