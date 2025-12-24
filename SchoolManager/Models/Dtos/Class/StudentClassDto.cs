namespace SchoolManager.Models.Dtos.Class
{
    //Helper Dto that is used to return student details which is used in classdetails Dto
    public class StudentClassDto
    {
        public Guid StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
