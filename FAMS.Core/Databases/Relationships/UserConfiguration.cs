using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FAMS.Core.Databases.Configs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(user => user.UserPermission)
                .WithMany(usp => usp.Users)
                .HasForeignKey(user => user.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
