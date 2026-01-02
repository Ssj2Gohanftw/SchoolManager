using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task AddAsync(Class @class);
        Task<List<Class>> GetAllAsync();
        Task<Class?> GetByIdAsync(Guid id);
        Task<Class?> GetByNameAsync(string name);
        Task<bool> Remove(Class @class);

        Task<bool> Update(Class @class);

        Task<PagedResults<Class>> GetPagedResultsAsync(ClassQueryDto classQueryDto);
    }
}
