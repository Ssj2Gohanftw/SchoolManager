namespace SchoolManager.Dtos.SubjectTeacher
{
    public class DeleteSubjectTeacherDto
    {
        public Guid TeacherId { get; set; }
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
