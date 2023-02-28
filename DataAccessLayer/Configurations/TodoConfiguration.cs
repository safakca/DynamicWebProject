using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;
public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable(nameof(Todo));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasColumnName("title");
        builder.Property(x => x.Description).IsRequired().HasColumnName("description");
        builder.Property(x => x.Status).HasColumnName("status");
         
        Todo[] todos =
        {
            new() { Id= 1, Title="title1", Description="description1", Status=0 },
            new() { Id= 2, Title="title2", Description="description2", Status=0 },
            new() { Id= 3, Title="title3", Description="description3", Status=0 },
            new() { Id= 4, Title="title4", Description="description4", Status=0 },
        };
        builder.HasData(todos);
    }
}
