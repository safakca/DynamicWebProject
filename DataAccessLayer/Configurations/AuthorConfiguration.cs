using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;
public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable(nameof(Author));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasColumnName("name");
        builder.Property(x => x.Surname).IsRequired().HasColumnName("surname");
        builder.Property(x => x.Age).HasColumnName("age");
        builder.Property(x => x.CreatedDate).HasColumnName("created_date");
        builder.Property(x => x.UpdatedDate).HasColumnName("updated_date");

        builder.HasMany(x => x.Articles);

        Author[] authors =
        {
            new() { Id= 1, Name="testName1", Surname="testSurname1", Age=35 },
            new() { Id= 2, Name="testName2", Surname="testSurname2", Age=36 },
            new() { Id= 3, Name="testName3", Surname="testSurname3", Age=37 }
        };
        builder.HasData(authors);
    }
}
