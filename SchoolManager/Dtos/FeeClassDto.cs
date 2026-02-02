using SchoolManager.Models;

namespace SchoolManager.Dtos
{
    public class FeeClassDto
    {
        public Guid FeeId { get; set; }
        public FeeType FeeName { get; set; }
        public string ClassName { get; set; }
        public Guid ClassId { get; set; }
    }
}
