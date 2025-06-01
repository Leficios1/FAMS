using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FAMS.Core.Databases.Configs
{
    public class TrainingProgramSyllabusConfiguration : IEntityTypeConfiguration<TrainingProgramSyllabus>
    {
        public void Configure(EntityTypeBuilder<TrainingProgramSyllabus> builder)
        {
            builder.HasOne(tps => tps.TrainingProgram)
                .WithMany(tp => tp.TrainingProgramSyllabuses)
                .HasForeignKey(tps => tps.TrainingProgramCode)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(tps => tps.Syllabus)
                .WithMany(s => s.TrainingProgramSyllabuses)
                .HasForeignKey(tps => tps.SyllabusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
