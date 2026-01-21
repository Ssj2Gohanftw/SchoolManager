using Microsoft.EntityFrameworkCore;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Models;

namespace SchoolManager.Data.Repositories
{
    public class SubjectTeacherRepository :Repository<SubjectTeacher>,ISubjectTeacherRepository
    {
        public SubjectTeacherRepository(ApplicationDbContext dbContext):base(dbContext)
        {
          
        }
        public override async Task AddAsync(SubjectTeacher subjectTeacher)
        {
            if (await Exists(subjectTeacher))
            {
                return;
            }
            await _entity.AddAsync(subjectTeacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(SubjectTeacher subjectTeacher)
        {
            return await _entity.AnyAsync(st =>
            st.TeacherId == subjectTeacher.TeacherId &&
            st.ClassId == subjectTeacher.ClassId &&
            st.SubjectId == subjectTeacher.SubjectId
            );
        }

        public async Task<List<SubjectTeacher>> GetAssignmentsForClass(Guid classId)
        {
            return await _entity
                .Where(st => st.ClassId == classId)
                .Include(st => st.Teacher)
                .Include(st => st.Class)
                .Include(st => st.Subject)
                .ToListAsync();
        }

        public async Task<List<SubjectTeacher>> GetAssignmentsForSubject(Guid subjectId)
        {
            return await _entity
                .Where(st => st.SubjectId == subjectId)
                .Include(st => st.Teacher)
                .Include(st => st.Class)
                .Include(st => st.Subject)
                .ToListAsync();
        }

        public async Task<List<SubjectTeacher>> GetAssignmentsForTeacher(Guid teacherId)
        {
            return await _entity
              .Where(st => st.TeacherId == teacherId)
              .Include(st => st.Teacher)
              .Include(st => st.Class)
              .Include(st => st.Subject)
              .ToListAsync();
        }

        public override async Task<bool> Remove(SubjectTeacher subjectTeacher)
        {
            SubjectTeacher? teacher = await _entity.FindAsync(
                    subjectTeacher.TeacherId,
                    subjectTeacher.ClassId,
                    subjectTeacher.SubjectId);
            if (teacher == null)
            {
                return false;
            }
            _entity.Remove(teacher);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
