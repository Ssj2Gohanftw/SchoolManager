namespace SchoolManager.Dtos.SubjectTeacher
{
    public class AddSubjectTeacherDto
    {
            public Guid TeacherId { get; set; }
            public Guid ClassId { get; set; }
            public Guid SubjectId { get; set; }
    }
}
