﻿// <auto-generated>
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkMe.Migrations
{
    public partial class LinkAndLinkClick : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkClicks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkId = table.Column<int>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CountryRegion = table.Column<string>(nullable: true),
                    WhenClicked = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkClicks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalLink = table.Column<string>(nullable: false),
                    ShortLink = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true),
                    ValidTo = table.Column<DateTime>(nullable: false),
                    ShownSummary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkClicks");

            migrationBuilder.DropTable(
                name: "Links");
        }
    }
}