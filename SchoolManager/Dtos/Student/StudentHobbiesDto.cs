namespace SchoolManager.Dtos.Student
{
    public class StudentHobbiesDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<string>? Hobbies { get; set; }
    }
}
