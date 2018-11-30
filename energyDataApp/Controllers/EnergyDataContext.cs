using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace energyDataApp.Models
{
    public partial class EnergyDataContext : DbContext
    {
        public EnergyDataContext()
        {
        }

        public EnergyDataContext(DbContextOptions<EnergyDataContext> options)
            : base(options)
        {

        }

        public virtual DbSet<EnergyRecord> EnergyRecord { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=energydatadb.cbdhcmfwzqc6.us-west-2.rds.amazonaws.com;Port=5432;Database=energydatadb;Username=londel;Password=halotwo4;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnergyRecord>(entity =>
            {
                //entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Iso).HasColumnName("ISO");

                entity.Property(e => e.Node).HasColumnName("NODE");
            });
        }
    }
}
