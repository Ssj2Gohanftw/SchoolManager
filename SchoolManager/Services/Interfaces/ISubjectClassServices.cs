using SchoolManager.Dtos.SubjectClass;

namespace SchoolManager.Services.Interfaces
{
    public interface ISubjectClassServices
    {
        Task<List<SubjectClassDto>> AssignSubjects(AddSubjectClassDto addSubjectClassDto);
    }
}
