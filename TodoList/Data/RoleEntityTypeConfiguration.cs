using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Provider;

namespace TodoList.Data
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder
                .ToTable("Roles");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasMany<IdentityUserRole<int>>()
                .WithOne()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
