using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: true),
                    Procesor = table.Column<string>(type: "TEXT", nullable: true),
                    Pamiec = table.Column<int>(type: "INTEGER", nullable: true),
                    kartagraficzna = table.Column<string>(name: "karta graficzna", type: "TEXT", nullable: true),
                    Producent = table.Column<string>(type: "TEXT", nullable: true),
                    dataprodukcji = table.Column<DateTime>(name: "data produkcji", type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "computers",
                columns: new[] { "Id", "Category", "Created", "data produkcji", "karta graficzna", "Nazwa", "Pamiec", "Procesor", "Producent" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2024, 11, 6, 19, 38, 24, 309, DateTimeKind.Local).AddTicks(6078), new DateTime(2000, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "NVIDIA", "Gaming Machine Pro", 16, "Intel i5", "Mateusz Matysiak" },
                    { 2, 1, new DateTime(2024, 11, 6, 19, 38, 24, 309, DateTimeKind.Local).AddTicks(6136), new DateTime(1997, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "NVIDIA", "Quiet-book", 16, "Intel i7", "Karolina Bruzda" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "computers");
        }
    }
}
