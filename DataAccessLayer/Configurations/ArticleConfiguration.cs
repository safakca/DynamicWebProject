using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;
public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable(nameof(Article));
        builder.HasKey(x => x.Id);

        Article[] articles =
        {
            new() { Id=1, AuthorId=1, Title="testTitle1", Description="testDescription1" },
            new() { Id=2, AuthorId=2, Title="testTitle2", Description="testDescription2" },
            new() { Id=3, AuthorId=3, Title="testTitle3", Description="testDescription3" }
        };
        builder.HasData(articles);
    }
}
