using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;
using Store.Infra.SeedData;

namespace Store.Infra.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.Name).HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Description).HasColumnType("varchar(200)");

            builder.Property(p => p.Image).HasColumnType("varchar(200)");

            builder.Property(p => p.Description).HasColumnType("varchar(200)");

            builder.Property(p => p.Value).HasColumnType("decimal(18, 2)").IsRequired();

            builder.Property(p => p.QuantityOnHand).HasColumnType("int").IsRequired();

            builder.Property(p => p.LastUpdated).HasColumnType("datetime");

            builder.Property(p => p.CreatedDate).HasColumnType("datetime");

            DateTime yesterday = DateTime.UtcNow.AddDays(-1);

            builder.HasData(ProductSeedData.Seed());
        }
    }
}
