namespace SchoolManager.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string Name { get; set; } = null;
        public List<SubjectTeacher> SubjectTeachers { get; set; }
            = new List<SubjectTeacher>();
        public List<SubjectClass> SubjectClasses { get; set; }
               = new List<SubjectClass>();
            
    }
    public class ElectiveSubject
    {
        public Guid ElectiveId {get;set;}
        public string? Name { get; set; } = null;
        public List<StudentSubject> StudentSubjects { get; set; } = new();
    }
}
