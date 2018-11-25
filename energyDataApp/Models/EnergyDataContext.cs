using Microsoft.EntityFrameworkCore;
namespace energyDataApp.Models
{
    public class EnergyDataContext : DbContext
    {
     
        public EnergyDataContext(DbContextOptions<EnergyDataContext> options) : base(options) {}

        public DbSet<EnergyData> EnergyRecord { get; set; }

    }
}

