using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FAMS.Core.Databases.Configs
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasOne(x => x.TrainingProgram)
                .WithMany(x=>x.Classes)
                .HasForeignKey(x=>x.TrainingProgramCode);

            builder.HasIndex(x => x.ClassCode).IsUnique();
        }
    }
}
