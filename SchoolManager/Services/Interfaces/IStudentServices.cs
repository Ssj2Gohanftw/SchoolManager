using SchoolManager.Models.Dtos.Student;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface IStudentServices
    {
         Task <IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken=default);
         Task<Student?> GetStudentByIdAsync(Guid id,CancellationToken cancellationToken=default);
         Task <Student> AddStudentAsync(AddStudentDto addStudentDto,CancellationToken cancellationToken = default);
         Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDto updateStudentDto,CancellationToken cancellationToken = default);
         Task<bool> DeleteStudentAsync(Guid id,CancellationToken cancellationToken = default);
    }
}
