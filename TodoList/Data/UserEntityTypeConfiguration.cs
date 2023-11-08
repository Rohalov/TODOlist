using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Models.Entities;

namespace TodoList.Data
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .ToTable("Users");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired();

            builder
                .Property(x => x.UserName)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder
                .HasMany<ApplicationRole>()
                .WithOne()
                .HasForeignKey(ur => ur.Id)
                .IsRequired();
        }
    }
}
