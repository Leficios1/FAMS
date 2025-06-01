using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FAMS.Core.Databases.Configs
{
    public class AssessmentSchemeConfiguration : IEntityTypeConfiguration<AssessmentScheme>
    {
        public void Configure(EntityTypeBuilder<AssessmentScheme> builder)
        {
            builder.HasOne(x => x.Syllabus)
                .WithOne(x => x.AssessmentScheme)
                .HasForeignKey<AssessmentScheme>(x => x.SyllabusId);

            builder.HasIndex(x => x.SyllabusId).IsUnique();
        }
    }
}
