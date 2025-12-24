namespace SchoolManager.Models.Dtos.Class
{
    //DTO for returning all classes without any student details in it
    public class ClassesDto
    {
        public Guid ClassId { get; set; }
        public string Name { get; set; } = null!;

    }
}
