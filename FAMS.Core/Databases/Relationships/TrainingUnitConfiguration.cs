using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FAMS.Core.Databases.Configs
{
    public class TrainingUnitConfiguration : IEntityTypeConfiguration<TrainingUnit>
    {
        public void Configure(EntityTypeBuilder<TrainingUnit> builder)
        {
            builder.HasOne(tu => tu.Syllabus)
                .WithMany(s => s.TrainingUnits)
                .HasForeignKey(tu => tu.SyllabusId);
        }
    }
}
