using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LivestockFarmManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livestocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HealthStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PenNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livestocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaccineRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivestockId = table.Column<int>(type: "int", nullable: false),
                    VaccineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdministeredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdministeredBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineRecords_Livestocks_LivestockId",
                        column: x => x.LivestockId,
                        principalTable: "Livestocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Livestocks",
                columns: new[] { "Id", "BirthDate", "Breed", "Category", "Code", "EstimatedValue", "Gender", "HealthStatus", "ImageUrl", "Name", "Notes", "PenNumber", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "โฮลสไตน์ ฟรีเชียน (Holstein Friesian)", "โคนม", "COW-001", 55000.00m, "เมีย", "สมบูรณ์ดี", "https://images.unsplash.com/photo-1546445317-29f4545f9d52?w=600&auto=format&fit=crop", "แม่มะลิ", "แม่พันธุ์ให้สถิติผลผลิตน้ำนมสูง สุขภาพแข็งแรงดี", "โรงเรือนโคนม A1", 480.50m },
                    { 2, new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "อเมริกันบราห์มัน (American Brahman)", "โคเนื้อ", "BEEF-002", 85000.00m, "ผู้", "สมบูรณ์ดี", "https://images.unsplash.com/photo-1570042225831-d98fa7577f1e?w=600&auto=format&fit=crop", "เจ้าขุน", "พ่อพันธุ์โคเนื้อกล้ามเนื้อแน่น กินอาหารเก่ง", "คอกโคเนื้อ B2", 620.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRecords_LivestockId",
                table: "VaccineRecords",
                column: "LivestockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccineRecords");

            migrationBuilder.DropTable(
                name: "Livestocks");
        }
    }
}
