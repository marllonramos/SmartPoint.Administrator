using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPoint.Administrator.Infra.Migrations.App
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "smartpoint");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "smartpoint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BlockDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                schema: "smartpoint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    RegisterDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RegisterHours = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Obs = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    IsOvertime = table.Column<bool>(type: "boolean", nullable: false),
                    ReasonOvertime = table.Column<string>(type: "text", nullable: true),
                    UrlPicture = table.Column<string>(type: "text", nullable: true),
                    IsManual = table.Column<bool>(type: "boolean", nullable: false),
                    ReasonManual = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                schema: "smartpoint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Obs = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies",
                schema: "smartpoint");

            migrationBuilder.DropTable(
                name: "Points",
                schema: "smartpoint");

            migrationBuilder.DropTable(
                name: "Vacations",
                schema: "smartpoint");
        }
    }
}
