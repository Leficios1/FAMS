using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FAMS.Core.Databases.Configs
{
    public class SyllabusObjectiveConfiguration : IEntityTypeConfiguration<SyllabusObjective>
    {
        public void Configure(EntityTypeBuilder<SyllabusObjective> builder)
        {
            builder.HasOne(so => so.Syllabus)
                .WithMany(s => s.SyllabusObjectives)
                .HasForeignKey(so => so.SyllabusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(so => so.LearningObjective)
                .WithMany(lo => lo.SyllabusObjectives)
                .HasForeignKey(so => so.ObjectiveCode)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasKey(x => x.Id);
        }
    }
}
