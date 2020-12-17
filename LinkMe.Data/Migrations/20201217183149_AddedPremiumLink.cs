using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkMe.Migrations
{
    public partial class AddedPremiumLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPremiumLink",
                table: "Links",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPremiumLink",
                table: "Links");
        }
    }
}
