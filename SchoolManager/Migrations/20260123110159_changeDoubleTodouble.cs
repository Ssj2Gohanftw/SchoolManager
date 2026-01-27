using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManager.Migrations
{
    /// <inheritdoc />
    public partial class changeDoubleTodouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fees_Students_StudentId",
                table: "Fees");

            migrationBuilder.DropIndex(
                name: "IX_Fees_StudentId",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Fees");

            migrationBuilder.CreateTable(
                name: "StudentFee",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountPaid = table.Column<double>(type: "double precision", nullable: false),
                    Balance = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFee", x => new { x.StudentId, x.FeeId });
                    table.ForeignKey(
                        name: "FK_StudentFee_Fees_FeeId",
                        column: x => x.FeeId,
                        principalTable: "Fees",
                        principalColumn: "FeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentFee_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentFee_FeeId",
                table: "StudentFee",
                column: "FeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFee_StudentId_FeeId",
                table: "StudentFee",
                columns: new[] { "StudentId", "FeeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentFee");

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Fees",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Fees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Fees_StudentId",
                table: "Fees",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_Students_StudentId",
                table: "Fees",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
