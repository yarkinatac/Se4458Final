using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBank.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blood_request",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    blood_number = table.Column<int>(type: "int", nullable: false),
                    blood_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duration_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hospital_longitude = table.Column<double>(type: "float", nullable: false),
                    hospital_latitude = table.Column<double>(type: "float", nullable: false),
                    hospital_email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blood_request", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hospital",
                columns: table => new
                {
                    hospital_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    a_plus_blood = table.Column<int>(type: "int", nullable: false),
                    a_minus_blood = table.Column<int>(type: "int", nullable: false),
                    b_plus_blood = table.Column<int>(type: "int", nullable: false),
                    b_minus_blood = table.Column<int>(type: "int", nullable: false),
                    ab_plus_blood = table.Column<int>(type: "int", nullable: false),
                    ab_minus_blood = table.Column<int>(type: "int", nullable: false),
                    zero_plus_blood_unit = table.Column<int>(type: "int", nullable: false),
                    zero_minus_blood_unit = table.Column<int>(type: "int", nullable: false),
                    geopoint_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospital", x => x.hospital_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blood_request");

            migrationBuilder.DropTable(
                name: "hospital");
        }
    }
}
