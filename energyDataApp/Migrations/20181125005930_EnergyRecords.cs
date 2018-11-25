using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace energyDataApp.Migrations
{
    public partial class EnergyRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnergyRecord",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NODE = table.Column<string>(nullable: true),
                    ISO = table.Column<string>(nullable: true),
                    NodeType = table.Column<string>(nullable: true),
                    PricingType = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    AvgPrice = table.Column<string>(nullable: true),
                    MaxPrice = table.Column<string>(nullable: true),
                    MinPrice = table.Column<string>(nullable: true),
                    AvgCongestion = table.Column<string>(nullable: true),
                    MaxCongestion = table.Column<string>(nullable: true),
                    MinCongestion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyRecord", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergyRecord");
        }
    }
}
