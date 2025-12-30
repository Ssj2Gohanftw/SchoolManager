namespace SchoolManager.Models.Dtos.Teacher
{
    public class TeacherAssignmentDto
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; } = null!;

        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
    }
}