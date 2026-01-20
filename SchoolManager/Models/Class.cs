namespace SchoolManager.Models
{
    public class Class
    {
        public Guid ClassId { get; set; }

        public string Name { get; set; } = null!;

        public List<Student> Students { get; set; } = new();
        public List<SubjectTeacher> SubjectTeachers { get; set; }
            = new List<SubjectTeacher>();
        public List<SubjectClass> SubjectClasses { get; set; }
            = new List<SubjectClass>();
    }
}
