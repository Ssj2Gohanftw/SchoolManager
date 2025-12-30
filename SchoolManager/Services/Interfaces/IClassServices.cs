using SchoolManager.Models.Entities;
using SchoolManager.Models.Dtos.Class;
using SchoolManager.Models.Dtos.Common;

namespace SchoolManager.Services.Interfaces
{
    public interface IClassServices
    {
        Task<List<ClassesDto>> GetAllAsync();
        Task<ClassDetailsDto?> GetClassByIdAsync(Guid id);
        Task<bool> UpdateClassAsync(Guid id, UpdateClassDto updateClassDto);
        Task<bool> DeleteClassAsync(Guid id);
        Task<Class?> AddClassAsync(AddClassDto addClassDto);
        Task<PagedResults<ClassesDto>> GetPagedClassesAsync(ClassQueryDto classQueryDto);

    }
}
