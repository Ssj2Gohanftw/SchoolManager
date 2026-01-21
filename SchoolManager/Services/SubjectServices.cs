using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Subject;
using SchoolManager.Mappers.Common;
using SchoolManager.Mappers.Subjects;
using SchoolManager.Models;
using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class SubjectServices : ISubjectServices
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectServices(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<Subject> AddSubjectAsync(AddSubjectDto addSubjectDto)
        {
            Subject subject = addSubjectDto.ToSubject();
            try
            {
                await _subjectRepository.AddAsync(subject);
                return subject;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Unable to add subject.", ex);
            }
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            Subject? subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return false;
            }
            try
            {
                await _subjectRepository.Remove(subject);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }

        }

        public async Task<List<SubjectSummaryDto>> GetAllAsync()
        {
            List<Subject> subjects = await _subjectRepository.GetAllAsync();
            return subjects.Select(s => s.ToSubjectSummaryDto()).ToList();
        }

        public async Task<PagedResults<SubjectDetailsDto>> GetPagedSubjectsAsync(SubjectQueryDto subjectQueryDto)
        {
            PagedResults<Subject> results = await _subjectRepository.GetPagedResults(subjectQueryDto);
            List<SubjectDetailsDto> subjectDetails = results.Results.Select(sub => sub.ToSubjectDetailsDto()).ToList();
            return subjectDetails.ToPagedResults(results.PageNumber, results.PageSize, results.TotalCount);
        }

        public async Task<SubjectDetailsDto?> GetSubjectByIdAsync(Guid id)
        {
            Subject? subject = await _subjectRepository.GetByIdAsync(id);
            return subject?.ToSubjectDetailsDto();
        }

        public async Task<bool> UpdateSubjectAsync(Guid id, UpdateSubjectDto updateSubjectDto)
        {
            Subject? subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return false;
            }
            updateSubjectDto.ToUpdateSubject(subject);

            try
            {
                await _subjectRepository.Update(subject);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
