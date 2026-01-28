using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManager.Models;
using System.Reflection.Emit;

namespace SchoolManager.Data.Configs
{
    public class SubjectClassConfiguration : IEntityTypeConfiguration<SubjectClass>
    {
        public void Configure(EntityTypeBuilder<SubjectClass> builder)
        {
            builder
                .HasKey(sc => new { sc.SubjectId, sc.ClassId });

            builder
                .HasOne(sc => sc.Subject)
                .WithMany(s => s.SubjectClasses)
                .HasForeignKey(sc => sc.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(sc => sc.Class)
                .WithMany(s => s.SubjectClasses)
                .HasForeignKey(sc => sc.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
