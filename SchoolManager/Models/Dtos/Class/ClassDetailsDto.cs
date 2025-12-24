namespace SchoolManager.Models.Dtos.Class
{
    //DTO for returning a class with specific student details in it such as their id,first and last name
  
    public class ClassDetailsDto
    {
        public Guid ClassId { get; set; }
        public string Name { get; set; } = null!;

        public List<StudentClassDto> Students { get; set; } = new();
    }
}
