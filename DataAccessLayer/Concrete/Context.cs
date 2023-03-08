using DataAccessLayer.Configurations;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete;

public class Context : IdentityDbContext<AppUser, AppRole, int>
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Article> Articles => this.Set<Article>();
    public DbSet<Author> Authors => this.Set<Author>();
    public DbSet<Todo> Todos => this.Set<Todo>();
    //public DbSet<AppUser> AppUsers => this.Set<AppUser>();
    //public DbSet<AppRole> AppRoles => this.Set<AppRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new TodoConfiguration());
        //modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        //modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

