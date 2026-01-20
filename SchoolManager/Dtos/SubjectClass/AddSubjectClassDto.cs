namespace SchoolManager.Dtos.SubjectClass
{
    public class AddSubjectClassDto
    {
        public required List<Guid> SubjectId { get; set; }
        public Guid ClassId { get; set; }
    }
}
