using SchoolManager.Dtos.Class;
using SchoolManager.Dtos.Common;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories.Interfaces
{
    public interface IClassRepository : IRepository<Class>
    {
        Task<Class?> GetByNameAsync(string name);
        Task<PagedResults<Class>> GetPagedResultsAsync(ClassQueryDto classQueryDto);
    }
}
