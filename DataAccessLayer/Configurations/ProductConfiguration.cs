using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        builder.HasKey(x => x.Id);

        Product[] products =
        {
            new() {Id = 1, Name="Computer", Price=32460, Stock=250},
            new() {Id = 2, Name="Phone", Price=12500, Stock=550},
            new() {Id = 3, Name="Keyboard", Price=2300, Stock=1000},
            new() {Id = 4, Name="Mouse", Price=1200, Stock=1500},
        };
        builder.HasData(products);
    }
}
