using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Teacher;
using SchoolManager.Mappers.Teachers;
using SchoolManager.Models.Entities;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class TeacherServices : ITeacherServices
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherServices(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<List<TeacherSummaryDto>> GetAllAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return teachers.Select(t => t.ToTeacherSummaryDto()).ToList();
        }

        //public async Task<TeacherSummaryDto?> GetTeacherByIdAsync(Guid id)
        //{
        //    var teacher = await _teacherRepository.GetByIdAsync(id);
        //    return teacher?.ToTeacherSummaryDto();
        //}

        public async Task<TeacherDetailsDto?> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            return teacher?.ToTeacherDetailsDto();
        }

        public async Task<Teacher?> AddTeacherAsync(AddTeacherDto addTeacherDto)
        {
            var teachers = addTeacherDto.ToTeacher();
            try
            {
                await _teacherRepository.AddAsync(teachers);
                return teachers;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public async Task<bool> DeleteTeacherAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return false;
            }

            try
            {
                await _teacherRepository.Remove(teacher);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherDto updateTeacherDto)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return false;
            }
            updateTeacherDto.ToUpdateTeacher(teacher);

            try
            {
                await _teacherRepository.Update(teacher);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
        public async Task<PagedResults<TeacherDetailsDto>> GetPagedTeachersAsync(TeacherQueryDto teacherQueryDto)
        {
            var result = await _teacherRepository.GetPagedAsync(teacherQueryDto);
            return new PagedResults<TeacherDetailsDto>
            {
                Results = result.Results.Select(t => t.ToTeacherDetailsDto()).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };

        }
    }
}

