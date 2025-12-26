using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models.Entities;

namespace SchoolManager.Data.Repositories
{
    public class SubjectTeacherRepository : ISubjectTeacherRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<SubjectTeacher> _subjectTeachers;

        public SubjectTeacherRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _subjectTeachers = _dbContext.SubjectTeacher;
        }
        public async Task AddAsync(SubjectTeacher subjectTeacher)
        {
            if(await Exists(subjectTeacher))
            {
                return;
            }
            await _subjectTeachers.AddAsync(subjectTeacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(SubjectTeacher subjectTeacher)
        {
            return await _subjectTeachers.AnyAsync(st=>
            st.TeacherId==subjectTeacher.TeacherId &&
            st.ClassId==subjectTeacher.ClassId &&
            st.SubjectId==subjectTeacher.SubjectId
            );
        }

        public async Task<List<SubjectTeacher>> GetAssignmentsForClass(Guid classId)
        {
            return await _subjectTeachers
                .Where(st => st.ClassId == classId)
                .Include(st => st.Teacher)
                .Include(st => st.Class)
                .Include(st => st.Subject)
                .ToListAsync();
        }

        public async Task<List<SubjectTeacher>> GetAssignmentsForSubject(Guid subjectId)
        {
            return await _subjectTeachers
                .Where(st => st.SubjectId == subjectId)
                .Include(st => st.Teacher)
                .Include(st => st.Class)
                .Include(st => st.Subject)
                .ToListAsync();
        }

        public async Task<List<SubjectTeacher>> GetAssignmentsForTeacher(Guid teacherId)
        {
            return await _subjectTeachers
              .Where(st => st.TeacherId == teacherId)
              .Include(st => st.Teacher)
              .Include(st => st.Class)
              .Include(st => st.Subject)
              .ToListAsync();
        }

        public async Task<bool> Remove(SubjectTeacher subjectTeacher)
        {
            var teacher= await _subjectTeachers.FindAsync(
                    subjectTeacher.TeacherId ,
                    subjectTeacher.ClassId,
                    subjectTeacher.SubjectId);
            if(teacher is null)
            {
                return false;
            }
            _subjectTeachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
