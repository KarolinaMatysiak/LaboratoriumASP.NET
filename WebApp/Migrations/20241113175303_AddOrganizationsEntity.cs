using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "computers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 101);

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NIP = table.Column<string>(type: "TEXT", nullable: false),
                    REGON = table.Column<string>(type: "TEXT", nullable: false),
                    Address_City = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Street = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "OrganizationId" },
                values: new object[] { new DateTime(2024, 11, 13, 18, 53, 3, 281, DateTimeKind.Local).AddTicks(1384), 101 });

            migrationBuilder.UpdateData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "OrganizationId" },
                values: new object[] { new DateTime(2024, 11, 13, 18, 53, 3, 281, DateTimeKind.Local).AddTicks(1439), 101 });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "Id", "Address_City", "Address_Street", "NIP", "Name", "REGON" },
                values: new object[,]
                {
                    { 101, "Krakow", "sw. Filipa 17", "234567", "Firma", "756383292932" },
                    { 102, "Wroclaw", "Dworcowa 22", "234547", "WSEI", "756333296932" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_computers_OrganizationId",
                table: "computers",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_computers_organizations_OrganizationId",
                table: "computers",
                column: "OrganizationId",
                principalTable: "organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_computers_organizations_OrganizationId",
                table: "computers");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropIndex(
                name: "IX_computers_OrganizationId",
                table: "computers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "computers");

            migrationBuilder.UpdateData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 6, 19, 38, 24, 309, DateTimeKind.Local).AddTicks(6078));

            migrationBuilder.UpdateData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 6, 19, 38, 24, 309, DateTimeKind.Local).AddTicks(6136));
        }
    }
}
