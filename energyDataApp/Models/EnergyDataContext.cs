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
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=EnergyData;Username=londel;Password=;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnergyRecord>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Iso).HasColumnName("ISO");

                entity.Property(e => e.Node).HasColumnName("NODE");
            });
        }
    }
}
