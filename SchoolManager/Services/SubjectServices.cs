using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Dtos.Common;
using SchoolManager.Dtos.Subject;
using SchoolManager.Mappers.Subjects;
using SchoolManager.Models.Entities;
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
            var subject = addSubjectDto.ToSubject();
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
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject is null)
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
            var subjects = await _subjectRepository.GetAllAsync();
            return subjects.Select(s=>s.ToSubjectSummaryDto()).ToList();
        }

        public async Task<PagedResults<SubjectSummaryDto>> GetPagedSubjectsAsync(SubjectQueryDto subjectQueryDto)
        {
            subjectQueryDto = subjectQueryDto.Normalize();
            var results = await _subjectRepository.GetPagedResults(subjectQueryDto);
            return new PagedResults<SubjectSummaryDto>()
            {
                Results = results.Results.Select(sub => sub.ToSubjectSummaryDto()).ToList(),
                TotalCount = results.TotalCount,
                PageNumber = results.PageNumber,
                PageSize = results.PageSize
            };
        }

        public async Task<SubjectSummaryDto?> GetSubjectByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            return subject?.ToSubjectSummaryDto();
        }

        public async Task<bool> UpdateSubjectAsync(Guid id, UpdateSubjectDto updateSubjectDto)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject is null)
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
