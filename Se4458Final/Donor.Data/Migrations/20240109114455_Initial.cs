using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Donor.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "branch",
                columns: table => new
                {
                    branch_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<int>(type: "int", nullable: false),
                    town = table.Column<int>(type: "int", nullable: false),
                    a_plus_blood = table.Column<int>(type: "int", nullable: false),
                    a_minus_blood = table.Column<int>(type: "int", nullable: false),
                    b_plus_blood = table.Column<int>(type: "int", nullable: false),
                    b_minus_blood = table.Column<int>(type: "int", nullable: false),
                    ab_plus_blood = table.Column<int>(type: "int", nullable: false),
                    ab_minus_blood = table.Column<int>(type: "int", nullable: false),
                    zero_plus_blood = table.Column<int>(type: "int", nullable: false),
                    zero_minus_blood = table.Column<int>(type: "int", nullable: false),
                    geopoint_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branch", x => x.branch_id);
                });

            migrationBuilder.CreateTable(
                name: "donor",
                columns: table => new
                {
                    donor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branch_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    blood_type = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    phonenumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    city = table.Column<int>(type: "int", nullable: false),
                    town = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donor", x => x.donor_id);
                    table.ForeignKey(
                        name: "FK_donor_branch_branch_id",
                        column: x => x.branch_id,
                        principalTable: "branch",
                        principalColumn: "branch_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "donation_history",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    donor_id = table.Column<int>(type: "int", nullable: false),
                    tuple_count = table.Column<int>(type: "int", nullable: false),
                    donation_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donation_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_donation_history_donor_donor_id",
                        column: x => x.donor_id,
                        principalTable: "donor",
                        principalColumn: "donor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_donation_history_donor_id",
                table: "donation_history",
                column: "donor_id");

            migrationBuilder.CreateIndex(
                name: "IX_donor_branch_id",
                table: "donor",
                column: "branch_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "donation_history");

            migrationBuilder.DropTable(
                name: "donor");

            migrationBuilder.DropTable(
                name: "branch");
        }
    }
}
