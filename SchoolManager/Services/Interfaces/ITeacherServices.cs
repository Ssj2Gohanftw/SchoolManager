using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Models.Entities;

namespace SchoolManager.Services.Interfaces
{
    public interface ITeacherServices
    {
        Task<List<TeacherSummaryDto>> GetAllAsync();
        Task<TeacherSummaryDto?> GetTeacherByIdAsync(Guid id);
        Task<TeacherDetailsDto?> GetTeacherDetailsByIdAsync(Guid id);

        Task<Teacher?> AddTeacherAsync(AddTeacherDto addTeacherDto);
        Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherDto updateTeacherDto);
        Task<bool> DeleteTeacherAsync(Guid id);
        Task<PagedResults<TeacherSummaryDto>> GetPagedTeachersAsync(TeacherQueryDto teacherQueryDto);

    }
}
