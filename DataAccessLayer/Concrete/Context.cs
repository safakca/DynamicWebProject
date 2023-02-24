using DataAccessLayer.Configurations;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete;

public class Context : IdentityDbContext<AppUser, AppRole, int>
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Product> Products => this.Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

