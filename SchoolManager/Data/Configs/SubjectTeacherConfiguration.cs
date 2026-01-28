using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManager.Models;

namespace SchoolManager.Data.Configs
{
    public class SubjectTeacherConfiguration : IEntityTypeConfiguration<SubjectTeacher>
    {
        public void Configure(EntityTypeBuilder<SubjectTeacher> builder)
        {
            builder.HasKey(st => new { st.TeacherId, st.ClassId, st.SubjectId });

            builder
                .HasOne(st => st.Class)
                .WithMany(st => st.SubjectTeachers)
                .HasForeignKey(st => st.ClassId);

            builder
                .HasOne(st => st.Teacher)
                .WithMany(st => st.SubjectTeachers)
                .HasForeignKey(st => st.TeacherId);

            builder.
                HasOne(st => st.Subject)
                .WithMany(st => st.SubjectTeachers)
                .HasForeignKey(st => st.SubjectId);
        }
    }
}
