namespace SchoolManager.Dtos.SubjectClass
{
    public class DeleteSubjectClass
    {
        public required List<Guid> SubjectId { get; set; }
        public Guid ClassId { get; set; }
    }
}
