using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManager.Models;

namespace SchoolManager.Data.Configs
{
    public class ClassConfiguration:IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.HasMany(c => c.Fees)
                .WithMany(f => f.Classes)
                .UsingEntity<FeeClass>();
        }
    }
}
