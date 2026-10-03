using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.DataAccess.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.OrderNumber).IsRequired().HasMaxLength(30);
        builder.HasIndex(x => x.OrderNumber).IsUnique();
        builder.Property(x => x.TotalPrice).HasPrecision(18, 2);
        builder.Property(x => x.FullName).HasMaxLength(150);
        builder.Property(x => x.Address).HasMaxLength(500);
        builder.Property(x => x.City).HasMaxLength(50);
        builder.Property(x => x.Phone).HasMaxLength(20);
        builder.Property(x => x.CardLast4).HasMaxLength(4);

        builder.HasOne(x => x.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);

        builder.HasOne(x => x.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(x => x.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Product)
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
