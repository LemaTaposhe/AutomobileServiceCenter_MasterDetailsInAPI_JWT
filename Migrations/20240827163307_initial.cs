using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutomobileServiceCenter_MasterDetailsInAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointMasters",
                columns: table => new
                {
                    AppointId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointMasters", x => x.AppointId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "AppointDetails",
                columns: table => new
                {
                    AppointDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointDetails", x => x.AppointDetailId);
                    table.ForeignKey(
                        name: "FK_AppointDetails_AppointMasters_AppointId",
                        column: x => x.AppointId,
                        principalTable: "AppointMasters",
                        principalColumn: "AppointId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointDetails_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppointMasters",
                columns: new[] { "AppointId", "AppointDate", "CustomerName", "ImagePath", "IsComplete" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 27, 22, 33, 4, 600, DateTimeKind.Local).AddTicks(9591), "Sharmin Shumi", "image1.jpg", true },
                    { 2, new DateTime(2024, 8, 26, 22, 33, 4, 600, DateTimeKind.Local).AddTicks(9609), "Nazmul Alam", "image2.jpg", false }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "ServiceName" },
                values: new object[,]
                {
                    { 1, "Engine Oil Change" },
                    { 2, "Brake Inspection" },
                    { 3, "Tire Rotation" },
                    { 4, "Battery Replacement" },
                    { 5, "Transmission Repair" },
                    { 6, "Wheel Alignment" },
                    { 7, "Air Filter Replacement" },
                    { 8, "Coolant Flush" },
                    { 9, "Exhaust Repair" },
                    { 10, "Suspension Repair" }
                });

            migrationBuilder.InsertData(
                table: "AppointDetails",
                columns: new[] { "AppointDetailId", "AppointId", "Price", "Quantity", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1, 100m, 1, 1 },
                    { 2, 1, 200m, 2, 2 },
                    { 3, 2, 300m, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointDetails_AppointId",
                table: "AppointDetails",
                column: "AppointId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointDetails_ServiceId",
                table: "AppointDetails",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointDetails");

            migrationBuilder.DropTable(
                name: "AppointMasters");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
