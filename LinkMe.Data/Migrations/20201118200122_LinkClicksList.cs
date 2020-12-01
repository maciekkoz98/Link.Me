using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkMe.Migrations
{
    public partial class LinkClicksList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LinkClicks_LinkId",
                table: "LinkClicks",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkClicks_Links_LinkId",
                table: "LinkClicks",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkClicks_Links_LinkId",
                table: "LinkClicks");

            migrationBuilder.DropIndex(
                name: "IX_LinkClicks_LinkId",
                table: "LinkClicks");
        }
    }
}
