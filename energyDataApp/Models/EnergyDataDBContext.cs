using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace energyDataApp
{
    public partial class EnergyDataDBContext : DbContext
    {
        public EnergyDataDBContext()
        {
        }

        public EnergyDataDBContext(DbContextOptions<EnergyDataDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Energyrecords> Energyrecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=energydatadb.cbdhcmfwzqc6.us-west-2.rds.amazonaws.com; Port=5432; Database=EnergyDataDB; Username=londel;Password=halotwo4");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Energyrecords>(entity =>
            {
                entity.ToTable("energyrecords");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avgcongestion)
                    .HasColumnName("avgcongestion")
                    .HasColumnType("character varying");

                entity.Property(e => e.Avgprice)
                    .HasColumnName("avgprice")
                    .HasColumnType("character varying");

                entity.Property(e => e.County)
                    .HasColumnName("county")
                    .HasColumnType("character varying");

                entity.Property(e => e.Iso)
                    .HasColumnName("iso")
                    .HasColumnType("character varying");

                entity.Property(e => e.Maxcongestion)
                    .HasColumnName("maxcongestion")
                    .HasColumnType("character varying");

                entity.Property(e => e.Maxprice)
                    .HasColumnName("maxprice")
                    .HasColumnType("character varying");

                entity.Property(e => e.Mincongestion)
                    .HasColumnName("mincongestion")
                    .HasColumnType("character varying");

                entity.Property(e => e.Minprice)
                    .HasColumnName("minprice")
                    .HasColumnType("character varying");

                entity.Property(e => e.Node)
                    .HasColumnName("node")
                    .HasColumnType("character varying");

                entity.Property(e => e.Nodetype)
                    .HasColumnName("nodetype")
                    .HasColumnType("character varying");

                entity.Property(e => e.Pricingtype)
                    .HasColumnName("pricingtype")
                    .HasColumnType("character varying");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("character varying");
            });
        }
    }
}
