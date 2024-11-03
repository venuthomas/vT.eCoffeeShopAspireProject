using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace vT.eCoffeeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSQLOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cfe");

            migrationBuilder.CreateTable(
                name: "CoffeeItems",
                schema: "cfe",
                columns: table => new
                {
                    CoffeeItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeItems", x => x.CoffeeItemId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "cfe",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.InsertData(
                schema: "cfe",
                table: "CoffeeItems",
                columns: new[] { "CoffeeItemId", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "Weight" },
                values: new object[,]
                {
                    { new Guid("187cd072-72f2-433e-9e69-1735a33ce036"), "Espresso with steamed milk foam.", "assets/img/Cappuccino.jpg", true, "Cappuccino", 75.75m, 190m },
                    { new Guid("4277459d-5dfb-48c6-8cfc-13f8c978b111"), "Smooth coffee with milk.", "assets/img/Latte.jpg", true, "Latte", 123.50m, 240m },
                    { new Guid("67ea6e2d-4c7e-43fa-8341-c0fc00235e71"), "Espresso diluted with hot water.", "assets/img/Americano.jpg", false, "Americano", 112.00m, 200m },
                    { new Guid("ac3ba645-ea57-4ea5-beb8-35b2bac1045a"), "Espresso with steamed milk and chocolate syrup.", "assets/img/Mocha.jpg", true, "Mocha", 144.00m, 220m },
                    { new Guid("dfeba5cf-393f-4587-9a26-291577e11198"), "Strong and bold coffee.", "assets/img/Espresso.jpg", true, "Espresso", 102.50m, 60m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeItems",
                schema: "cfe");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "cfe");
        }
    }
}
