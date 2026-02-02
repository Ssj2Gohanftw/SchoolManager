namespace SchoolManager.Models
{
    public enum Branch
    {
        General,
        Science,
        Arts,
        Commerce,
        Technology
    }
    public class Class
    {
        public Guid ClassId { get; set; }

        public string Name { get; set; } = null!;

        public Branch Branch { get; set; } = Branch.General;
        public List<Student> Students { get; set; } = new List<Student>();
        public List<SubjectTeacher> SubjectTeachers { get; set; }
            = new List<SubjectTeacher>();
        public List<SubjectClass> SubjectClasses { get; set; }
            = new List<SubjectClass>();
        public List<Fee> Fees { get; set; }
            = new List<Fee>();
        public List<FeeClass> FeeClasses { get; set; }
            = new List<FeeClass>();
    }
}
