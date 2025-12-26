using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManager.Migrations
{
    /// <inheritdoc />
    public partial class SubjectTeacherFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeacher_SubjectId",
                table: "SubjectTeacher");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "SubjectTeacher",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher",
                columns: new[] { "SubjectId", "TeacherId", "ClassId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_ClassId",
                table: "SubjectTeacher",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_Class_ClassId",
                table: "SubjectTeacher",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_Class_ClassId",
                table: "SubjectTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeacher_ClassId",
                table: "SubjectTeacher");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeacher_TeacherId",
                table: "SubjectTeacher");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "SubjectTeacher");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher",
                columns: new[] { "TeacherId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_SubjectId",
                table: "SubjectTeacher",
                column: "SubjectId");
        }
    }
}
