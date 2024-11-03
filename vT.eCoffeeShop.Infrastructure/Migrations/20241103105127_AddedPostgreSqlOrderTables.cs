using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vT.eCoffeeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPostgreSqlOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cfe");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "cfe",
                columns: table => new
                {
                    OrdersId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    TotalQty = table.Column<int>(type: "integer", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrdersId);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "cfe",
                columns: table => new
                {
                    OrderItemsId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdersId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoffeeItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrdersDtoOrdersId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemsId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrdersDtoOrdersId",
                        column: x => x.OrdersDtoOrdersId,
                        principalSchema: "cfe",
                        principalTable: "Orders",
                        principalColumn: "OrdersId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrdersDtoOrdersId",
                schema: "cfe",
                table: "OrderItems",
                column: "OrdersDtoOrdersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "cfe");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "cfe");
        }
    }
}
