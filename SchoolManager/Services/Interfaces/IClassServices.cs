using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Models;

namespace SchoolManager.Services.Interfaces
{
    public interface IClassServices
    {
        Task<List<ClassesDto>> GetAllAsync();
        Task<ClassDetailsDto?> GetClassByIdAsync(Guid id);
        Task<bool> UpdateClassAsync(Guid id, UpdateClassDto updateClassDto);
        Task<bool> DeleteClassAsync(Guid id);
        Task<Class?> AddClassAsync(AddClassDto addClassDto);
        Task<PagedResults<ClassDetailsDto>> GetPagedClassesAsync(ClassQueryDto classQueryDto);

    }
}
