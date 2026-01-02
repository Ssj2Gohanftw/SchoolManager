namespace SchoolManager.Dtos.SubjectTeacher
{
    public class SubjectTeacherDto
    {
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; } = null!;

        public Guid ClassId { get; set; }
        public string ClassName { get; set; } = null!;

        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
    }
}
