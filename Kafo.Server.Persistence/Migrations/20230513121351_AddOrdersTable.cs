using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafo.Server.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNumber = table.Column<int>(type: "integer", nullable: true),
                    ClientName = table.Column<string>(type: "text", nullable: false),
                    EmployeePhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ClientPhonePrimary = table.Column<string>(type: "text", nullable: false),
                    ClientPhoneSecondary = table.Column<string>(type: "text", nullable: true),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    CoffeeMachineId = table.Column<Guid>(type: "uuid", nullable: false),
                    Appearance = table.Column<int>(type: "integer", nullable: false),
                    Malfunction = table.Column<string>(type: "text", nullable: false),
                    AcceptanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderFinishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WarrantyBefore = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Pallet = table.Column<bool>(type: "boolean", nullable: false),
                    Filter = table.Column<bool>(type: "boolean", nullable: false),
                    WaterTank = table.Column<bool>(type: "boolean", nullable: false),
                    CoffeeLid = table.Column<bool>(type: "boolean", nullable: false),
                    WasteTray = table.Column<bool>(type: "boolean", nullable: false),
                    CappuccinatorHose = table.Column<bool>(type: "boolean", nullable: false),
                    PowerCord = table.Column<bool>(type: "boolean", nullable: false),
                    MilkKettle = table.Column<bool>(type: "boolean", nullable: false),
                    HotWaterNozzle = table.Column<bool>(type: "boolean", nullable: false),
                    CappuccinatorNozzle = table.Column<bool>(type: "boolean", nullable: false),
                    OtherText = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_CoffeeMachineModel_CoffeeMachineId",
                        column: x => x.CoffeeMachineId,
                        principalTable: "CoffeeMachineModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CoffeeMachineId",
                table: "Order",
                column: "CoffeeMachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
