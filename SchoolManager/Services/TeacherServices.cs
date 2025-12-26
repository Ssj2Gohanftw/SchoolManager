using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Dtos.Teacher;
using SchoolManager.Models.Entities;
using SchoolManager.Models.Mappings;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class TeacherServices : ITeacherService
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

        public async Task<Teacher?> AddTeacherAsync(AddTeacherDto addTeacherDto)
        {
            var teachers = new Teacher()
            {
                FirstName = addTeacherDto.FirstName,
                LastName = addTeacherDto.LastName,
                Email = addTeacherDto.Email
            };

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

            teacher.FirstName = updateTeacherDto.FirstName;
            teacher.LastName = updateTeacherDto.LastName;
            teacher.Email = updateTeacherDto.Email;
            return true;
        }
    }
}

