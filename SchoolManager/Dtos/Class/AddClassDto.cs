using SchoolManager.Models;

namespace SchoolManager.Dtos.Class
{
    public class AddClassDto
    {
        public string Name { get; set; } = null!;
        public Branch Branch { get; set; }
    }
}
