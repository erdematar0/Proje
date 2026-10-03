using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.DataAccess.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    private static readonly DateTime SeedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(x => x.AuthorName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Comment).IsRequired().HasMaxLength(1000);

        builder.HasOne(x => x.Product)
               .WithMany(p => p.Reviews)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => new { x.ProductId, x.UserId })
               .IsUnique()
               .HasFilter("[UserId] IS NOT NULL");

        builder.HasData(SeedReviews());
    }

    private static readonly string[] Names =
    [
        "Ayşe K.", "Mehmet T.", "Zeynep A.", "Emre Ç.", "Elif D.", "Burak S.", "Selin Y.", "Can Ö.", "Merve B.", "Onur G.",
        "Deniz P.", "Gizem E.", "Kerem U.", "İrem H.", "Tolga N.", "Ece F.", "Serkan İ.", "Buse R.", "Hakan V.", "Nazlı Z."
    ];

    private static readonly Dictionary<int, string[]> Comments = new()
    {
        [5] =
        [
            "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.",
            "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.",
            "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.",
            "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.",
            "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!",
            "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı."
        ],
        [4] =
        [
            "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.",
            "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.",
            "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.",
            "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.",
            "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım."
        ],
        [3] =
        [
            "Fena değil ama fiyatına göre daha iyisini beklerdim.",
            "Ürün idare eder, iki hafta kullandım şimdilik sorun yok.",
            "Ortalama bir ürün. Kargo hızlıydı, bunun için teşekkürler."
        ],
        [2] =
        [
            "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.",
            "Ürün geç geldi ve kutusu hasarlıydı. Satıcı ilgilendi ama keyfim kaçtı."
        ]
    };

    private static readonly int[] RatingPattern = [5, 4, 5, 3, 5, 4, 2, 5, 4, 5];

    private static IEnumerable<Review> SeedReviews()
    {
        var id = 1;
        for (var productId = 1; productId <= 50; productId++)
        {
            var count = 2 + productId * 7 % 5;
            for (var i = 0; i < count; i++)
            {
                var rating = RatingPattern[(productId * 3 + i) % RatingPattern.Length];
                var pool = Comments[rating];
                yield return new Review
                {
                    Id = id++,
                    ProductId = productId,
                    AuthorName = Names[(productId * 7 + i * 3) % Names.Length],
                    Rating = rating,
                    Comment = pool[(productId + i) % pool.Length],
                    IsVerifiedPurchase = (productId + i) % 3 != 0,
                    CreatedDate = SeedDate.AddDays((productId * 13 + i * 29) % 240).AddHours(9 + i * 2)
                };
            }
        }
    }
}
