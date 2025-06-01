using FAMS.Domain.Models.Entities;
using FAMS.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FAMS.Core.Databases.Configs
{
    public class TrainingContentConfiguration : IEntityTypeConfiguration<TrainingContent>
    {
        public void Configure(EntityTypeBuilder<TrainingContent> builder)
        {
            builder.HasOne(x => x.TrainingUnit)
                .WithMany(tu => tu.TrainingContents)
                .HasForeignKey(x => x.UnitCode)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.LearningObjective)
                .WithMany(lo => lo.TrainingContents)
                .HasForeignKey(x => x.LearningObjectiveCode)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Delivery)
                .WithMany(delivery => delivery.TrainingContents)
                .HasForeignKey(x => x.DeliveryType)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
