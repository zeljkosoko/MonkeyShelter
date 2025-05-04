using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonkeyShelter.Infrastructure.Migrations
{
    public partial class SheltersSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shelters",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Obrenovac" });

            migrationBuilder.InsertData(
                table: "Shelters",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Batajnica" });

            migrationBuilder.InsertData(
                table: "Shelters",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Kovilovo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shelters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shelters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shelters",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
