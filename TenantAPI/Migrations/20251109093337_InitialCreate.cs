using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TenantAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdApartament = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectricityConsumptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdApartamento = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadKw = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectricityConsumptions_Apartments_IdApartamento",
                        column: x => x.IdApartamento,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "IdApartament", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "101", "Rafael Tavares Medina", "8092457896" },
                    { 2, "102", "Carmen Peña Rodriguez", "8095681234" },
                    { 3, "201", "Miguel Santos Jimenez", "8298764523" },
                    { 4, "202", "Yolanda Herrera Castillo", "8494512367" },
                    { 5, "301", "Franklin Gutierrez Mora", "8097835642" },
                    { 6, "302", "Esperanza Vasquez Luna", "8292378459" },
                    { 7, "401", "Domingo Pacheco Vargas", "8496547832" },
                    { 8, "402", "Miguelina Rosario Diaz", "8095432876" },
                    { 9, "501", "Eugenio Mercado Silva", "8297654321" },
                    { 10, "502", "Amparo Contreras Mejia", "8493876542" }
                });

            migrationBuilder.InsertData(
                table: "ElectricityConsumptions",
                columns: new[] { "Id", "CantidadKw", "Fecha", "IdApartamento" },
                values: new object[,]
                {
                    { 1, 287.50m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 334.25m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 412.80m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 298.70m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, 567.35m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, 315.45m, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, 289.60m, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, 378.90m, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 9, 445.20m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 10, 523.75m, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityConsumptions_IdApartamento",
                table: "ElectricityConsumptions",
                column: "IdApartamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricityConsumptions");

            migrationBuilder.DropTable(
                name: "Apartments");
        }
    }
}
