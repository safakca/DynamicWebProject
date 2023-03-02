using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;
public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(nameof(AppRole));
        builder.HasKey(x => x.Id);
 

        AppRole[] roles =
        {
            new() { Id=1, Defination="Admin" },
            new() { Id=2, Defination="Member" }
        };
        builder.HasData(roles);
    }
}
