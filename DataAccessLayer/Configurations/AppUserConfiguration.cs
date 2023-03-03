using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(nameof(AppUser));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.AppRole)
               .WithMany(x => x.AppUsers)
               .HasForeignKey(x => x.AppRoleId);

        AppUser[] users =
        {
            new() { Id=1, UserName="safakca", Email="safakcatest@gmail.com", MailCode=new Random().Next(10000, 999999).ToString(), Password="testSifre123**", AppRoleId=1 }
        };
        builder.HasData(users);
    }
}
