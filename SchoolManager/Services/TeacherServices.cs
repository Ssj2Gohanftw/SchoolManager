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

        public async Task<TeacherSummaryDto?> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            return teacher?.ToTeacherSummaryDto();
        }

        public async Task<TeacherDetailsDto?> GetTeacherDetailsByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdWithAssignmentsAsync(id);
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
            if (teacher is null)
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
            if (teacher is null)
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
        public async Task<PagedResults<TeacherSummaryDto>> GetPagedTeachersAsync(TeacherQueryDto teacherQueryDto)
        {
            teacherQueryDto = teacherQueryDto.Normalize();
            var result = await _teacherRepository.GetPagedAsync(teacherQueryDto);
            return new PagedResults<TeacherSummaryDto>
            {
                Results = result.Results.Select(t => t.ToTeacherSummaryDto()).ToList(),
                PageNumber=result.PageNumber,
                PageSize=result.PageSize,
                TotalCount=result.TotalCount
            };
            
        }
    }
}

