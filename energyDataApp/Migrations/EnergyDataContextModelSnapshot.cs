﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using energyDataApp.Models;

namespace energyDataApp.Migrations
{
    [DbContext(typeof(EnergyDataContext))]
    partial class EnergyDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("energyDataApp.Models.EnergyData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvgCongestion");

                    b.Property<string>("AvgPrice");

                    b.Property<string>("County");

                    b.Property<string>("ISO");

                    b.Property<string>("MaxCongestion");

                    b.Property<string>("MaxPrice");

                    b.Property<string>("MinCongestion");

                    b.Property<string>("MinPrice");

                    b.Property<string>("NODE");

                    b.Property<string>("NodeType");

                    b.Property<string>("PricingType");

                    b.Property<string>("State");

                    b.HasKey("ID");

                    b.ToTable("EnergyRecord");
                });
#pragma warning restore 612, 618
        }
    }
}