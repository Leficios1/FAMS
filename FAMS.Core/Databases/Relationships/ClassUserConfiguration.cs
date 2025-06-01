using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FAMS.Core.Databases.Configs
{
    public class ClassUserConfiguration : IEntityTypeConfiguration<ClassUser>
    {
        public void Configure(EntityTypeBuilder<ClassUser> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ClassId });

            builder.HasOne(x => x.User)
                .WithMany(u => u.ClassUsers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Class)
                .WithMany(c => c.Admins)
                .HasForeignKey(x => x.ClassId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
