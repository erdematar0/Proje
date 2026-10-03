using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private static readonly DateTime SeedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasData(
            new Category { Id = 1, Name = "Elektronik", Description = "Telefon, bilgisayar ve aksesuarlar", CreatedDate = SeedDate },
            new Category { Id = 2, Name = "Giyim", Description = "Kadın, erkek ve çocuk giyim", CreatedDate = SeedDate },
            new Category { Id = 3, Name = "Ev & Yaşam", Description = "Mutfak, dekorasyon ve ev gereçleri", CreatedDate = SeedDate },
            new Category { Id = 4, Name = "Spor & Outdoor", Description = "Fitness, koşu, kamp ve bisiklet", CreatedDate = SeedDate },
            new Category { Id = 5, Name = "Kitap & Kırtasiye", Description = "Kitap, defter, kalem ve sanat malzemeleri", CreatedDate = SeedDate },
            new Category { Id = 6, Name = "Kozmetik & Bakım", Description = "Cilt, saç ve kişisel bakım ürünleri", CreatedDate = SeedDate },
            new Category { Id = 7, Name = "Oyuncak & Hobi", Description = "Oyuncak, oyun, yapboz ve müzik", CreatedDate = SeedDate }
        );
    }
}
