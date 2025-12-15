namespace SchoolManager.Models.Entities
{
    public class Class
    {
        public Guid ClassId { get; set; }

        public string Name { get; set; } = null!;
        public ICollection<StudentClass> StudentClasses { get; set; }
            = new List<StudentClass>();
    }
}
