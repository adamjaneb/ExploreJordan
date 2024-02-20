using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExploreJordan.Migrations
{
    /// <inheritdoc />
    public partial class AdCityToAccommodation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Accommodation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Accommodation");
        }
    }
}
