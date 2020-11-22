using Microsoft.EntityFrameworkCore.Migrations;

namespace Dominos.Data.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source_Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source_Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination_Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination_Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinate", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coordinate");
        }
    }
}
