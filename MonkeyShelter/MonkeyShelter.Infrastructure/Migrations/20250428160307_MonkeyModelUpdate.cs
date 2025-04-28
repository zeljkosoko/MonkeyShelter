using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonkeyShelter.Infrastructure.Migrations
{
    public partial class MonkeyModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDate",
                table: "Monkeys",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureDate",
                table: "Monkeys");
        }
    }
}
