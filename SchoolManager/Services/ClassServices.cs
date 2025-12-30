using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Dtos.Class;
using SchoolManager.Models.Dtos.Common;
using SchoolManager.Models.Entities;
using SchoolManager.Models.Mappings.Class;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class ClassServices : IClassServices
    {
        private readonly IClassRepository _classRepository;
        public ClassServices(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<List<ClassesDto>> GetAllAsync()
        {
            var classes = await _classRepository.GetAllAsync();
            return classes.Select(c => c.ToClassDto()).ToList();
        }

        public async Task<ClassDetailsDto?> GetClassByIdAsync(Guid id)
        {
            var @class = await _classRepository.GetByIdAsync(id);
            return @class?.ToClassDetailsDto();

        }

        public async Task<Class?> AddClassAsync(AddClassDto addClassDto)
        {

            var className = addClassDto.Name.Trim();

            var existingClass = await _classRepository.GetByNameAsync(className);
            if (existingClass is not null)
            {
                return null;
            }
            var @class = new Class()
            {
                Name = className
            };
            try
            {

                await _classRepository.AddAsync(@class);
                return @class;
            }
            catch (DbUpdateException)
            {
                return null;
            }

        }

        public async Task<bool> UpdateClassAsync(Guid id, UpdateClassDto updateClassDto)
        {
            var @class = await _classRepository.GetByIdAsync(id);
            if (@class is null)
            {
                return false;
            }
            @class.Name = updateClassDto.Name.Trim();
            return true;

        }

        public async Task<bool> DeleteClassAsync(Guid id)
        {
            var @class = await _classRepository.GetByIdAsync(id);
            if (@class is null)
            {
                return false;
            }
            try
            {
                await _classRepository.Remove(@class);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<PagedResults<ClassesDto>> GetPagedClassesAsync(ClassQueryDto classQueryDto)
        {
            classQueryDto = classQueryDto.Normalize();
            var result = await _classRepository.GetPagedResultsAsync(classQueryDto);
            return new PagedResults<ClassesDto> {
                Items = result.Items.Select(c => c.ToClassDto()).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount

            };
        }
    }
}
