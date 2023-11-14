using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Models.Entities;

namespace TodoList.Data
{
    public class UserRolesEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUserRoles>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoles> builder)
        {
            builder
                .ToTable("UserRoles");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder
                .Property(x => x.RoleId)
                .IsRequired();
        }
    }
}
