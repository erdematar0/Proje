using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.DataAccess.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
        builder.Property(x => x.City).IsRequired().HasMaxLength(50);
        builder.Property(x => x.District).HasMaxLength(50);
        builder.Property(x => x.AddressLine).IsRequired().HasMaxLength(500);

        builder.HasOne(x => x.User)
               .WithMany(u => u.Addresses)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
