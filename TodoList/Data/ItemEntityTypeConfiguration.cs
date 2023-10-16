using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Models.Entities;

namespace TodoList.Data
{
    public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder
                .ToTable("Items");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(x => x.IsComplete)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
