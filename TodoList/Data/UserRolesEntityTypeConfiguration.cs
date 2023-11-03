using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TodoList.Data
{
    public class UserRolesEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder
                .ToTable("UserRoles");

            builder
                .HasKey(x => x.UserId);

            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder
                .Property(x => x.RoleId)
                .IsRequired();
        }
    }
}
