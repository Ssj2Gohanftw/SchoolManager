using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManager.Migrations
{
    /// <inheritdoc />
    public partial class StudentMetaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeacher_TeacherId",
                table: "SubjectTeacher");

            migrationBuilder.AddColumn<StudentMetaData>(
                name: "AdditionalInfo",
                table: "Students",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher",
                columns: new[] { "TeacherId", "ClassId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_SubjectId",
                table: "SubjectTeacher",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeacher_SubjectId",
                table: "SubjectTeacher");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Students");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher",
                columns: new[] { "SubjectId", "TeacherId", "ClassId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId");
        }
    }
}
