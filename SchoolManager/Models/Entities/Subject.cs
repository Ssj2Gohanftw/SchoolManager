namespace SchoolManager.Models.Entities
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string Name { get; set; } = null!;  
        //public Guid TeacherId { get; set; }
        public List<SubjectTeacher> SubjectTeachers { get; set; }
            = new List<SubjectTeacher>();
    }
}
