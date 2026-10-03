using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaret.DataAccess.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private static readonly DateTime SeedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(2000);
        builder.Property(x => x.Specifications).HasMaxLength(2000);
        builder.Property(x => x.Price).HasPrecision(18, 2);

        builder.HasOne(x => x.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            P(1, "Kablosuz Kulaklık", "Aktif gürültü engelleme, 30 saat pil.", 1899.90m, 25, 1, "kulaklik"),
            P(2, "Akıllı Saat", "Nabız ve uyku takibi, suya dayanıklı.", 3499.00m, 12, 1, "akilli-saat"),
            P(3, "Mekanik Klavye", "Hot-swap switch, RGB aydınlatma.", 2250.00m, 8, 1, "mekanik-klavye"),
            P(8, "Kablosuz Mouse", "Sessiz tıklama, 2.4 GHz alıcı, 18 ay pil ömrü.", 449.90m, 60, 1, "kablosuz-mouse"),
            P(9, "Levitasyon Bluetooth Hoparlör", "Havada duran küre hoparlör, 360° ses, 8 saat pil.", 1349.00m, 15, 1, "bluetooth-hoparlor"),
            P(10, "Powerbank 20000 mAh", "Dijital göstergeli, 2 USB çıkış, hızlı şarj.", 899.00m, 45, 1, "powerbank"),
            P(11, "24\" IPS Monitör", "1920x1200 çözünürlük, yükseklik ayarlı stand.", 5299.00m, 10, 1, "monitor"),
            P(12, "Full HD Web Kamerası", "1080p görüntü, dahili mikrofon, tak-çalıştır.", 799.00m, 35, 1, "web-kamerasi"),

            P(4, "Pamuklu Tişört", "%100 organik pamuk, robot baskılı, basic kesim.", 349.90m, 100, 2, "tisort"),
            P(5, "Denim Ceket", "Klasik yıkamalı denim ceket.", 1299.00m, 30, 2, "denim-ceket"),
            P(13, "Kapüşonlu Sweatshirt", "Şardonlu iç yüzey, kanguru cep.", 749.90m, 55, 2, "sweatshirt"),
            P(14, "Slim Fit Kot Pantolon", "Esnek denim, orta bel, 5 cep.", 899.90m, 70, 2, "kot-pantolon"),
            P(15, "Beyaz Sneaker", "Hafif taban, nefes alan file üst yüzey.", 1599.00m, 40, 2, "sneaker"),
            P(16, "Örgü Yün Atkı", "El örgüsü, yumuşak yün karışım.", 399.00m, 50, 2, "atki"),
            P(17, "Hakiki Deri Kemer", "Tam tahıl deri, metal toka, 3.5 cm.", 549.00m, 65, 2, "deri-kemer"),

            P(6, "Nota Desenli Kupa", "Keman kulplu seramik kupa, 350 ml.", 499.00m, 40, 3, "kupa-seti"),
            P(7, "Masa Lambası", "Ayarlanabilir renk sıcaklığı, USB şarj.", 799.50m, 18, 3, "masa-lambasi"),
            P(18, "French Press 1 L", "Çift katmanlı paslanmaz çelik süzgeç.", 649.00m, 30, 3, "french-press"),
            P(19, "Döküm Tava 26 cm", "Önceden yağlanmış, fırında kullanılabilir.", 1199.00m, 20, 3, "dokum-tava"),
            P(20, "Pamuklu Nevresim Takımı", "Çift kişilik, %100 pamuk saten.", 1449.00m, 25, 3, "nevresim"),
            P(21, "Monstera Saksı Bitkisi", "60–70 cm, dekoratif saksıyla birlikte.", 699.00m, 12, 3, "monstera"),
            P(22, "Kokulu Mum", "Soya mumu, ahşap fitil, 40 saat yanma.", 329.00m, 80, 3, "kokulu-mum"),

            P(23, "Yoga Matı 6 mm", "Kaymaz yüzey, taşıma askılı.", 459.00m, 50, 4, "yoga-mati"),
            P(24, "Döküm Dambıl Seti", "2 x 10 kg, kauçuk tutuş.", 1899.00m, 15, 4, "dambil"),
            P(25, "Koşu Ayakkabısı", "Amortisörlü taban, yansıtıcı detaylar.", 2499.00m, 30, 4, "kosu-ayakkabisi"),
            P(26, "Retro Termos Seti", "1 L termos ve metal beslenme çantası.", 799.00m, 22, 4, "termos"),
            P(27, "4 Kişilik Kamp Çadırı", "Su geçirmez, kolay kurulum, taşıma çantalı.", 3299.00m, 10, 4, "kamp-cadiri"),
            P(28, "Sırt Çantası 30 L", "Dizüstü bölmeli, yağmurluklu.", 1149.00m, 35, 4, "sirt-cantasi"),
            P(29, "Bisiklet Kaskı", "Hafif EPS gövde, 21 havalandırma kanalı.", 1099.00m, 25, 4, "bisiklet-kaski"),

            P(30, "Deri Kapaklı Defter", "A5, 192 sayfa, lastikli kapak.", 279.00m, 90, 5, "defter"),
            P(31, "Dolma Kalem", "Karbon gövde, çelik uç, kartuşlu.", 649.00m, 30, 5, "dolma-kalem"),
            P(32, "Suluboya Seti 24 Renk", "Tablet suluboya, fırça hediyeli.", 389.00m, 45, 5, "suluboya"),
            P(33, "Ahşap Kalemlik", "Masif ahşap, 8 bölmeli masa düzenleyici.", 229.00m, 60, 5, "masa-organizeri"),
            P(34, "Klasikler Kitap Seti", "10 kitaplık dünya klasikleri seti.", 1249.00m, 20, 5, "kitap-seti"),
            P(35, "Ajanda 2027", "Haftalık planlayıcı, keten kapak.", 319.00m, 75, 5, "ajanda"),
            P(36, "Kuru Boya Kalem Seti", "36 renk, yumuşak ve canlı tonlar.", 199.00m, 100, 5, "kalem-seti"),

            P(37, "Nemlendirici Krem", "Hyalüronik asitli, 24 saat nem, 50 ml.", 349.00m, 60, 6, "nemlendirici"),
            P(38, "Parfüm 50 ml", "Çiçeksi-odunsu, eau de parfum.", 1799.00m, 25, 6, "parfum"),
            P(39, "Bambu Diş Fırçası 4'lü", "Biyobozunur sap, yumuşak kıl.", 149.00m, 120, 6, "bambu-dis-fircasi"),
            P(40, "Saç Kurutma Makinesi", "2200 W, iyonik, 3 ısı kademesi.", 1299.00m, 20, 6, "sac-kurutma"),
            P(41, "Doğal Sabun Seti", "6 farklı bitkisel sabun, el yapımı.", 259.00m, 70, 6, "sabun-seti"),
            P(42, "Mat Ruj", "Uzun süre kalıcı, nemlendirici formül.", 229.00m, 85, 6, "ruj"),
            P(43, "Güneş Kremi SPF 50", "Suya dayanıklı, geniş spektrum koruma.", 399.00m, 55, 6, "gunes-kremi"),

            P(44, "Yapboz 1000 Parça", "Gün batımı manzaralı, 68 x 48 cm.", 299.00m, 40, 7, "yapboz"),
            P(45, "Satranç Takımı", "Ağırlıklı taşlar, katlanır tahta.", 649.00m, 25, 7, "satranc"),
            P(46, "Zeka Küpü Seti", "3x3, 2x2 ve piramit küp içeren set.", 399.00m, 50, 7, "rubik-kup"),
            P(47, "Uzaktan Kumandalı Araba", "4x4, 2.4 GHz, şarjlı batarya.", 1199.00m, 18, 7, "rc-araba"),
            P(48, "Peluş Ayı 40 cm", "Yumuşak peluş, fiyonklu.", 349.00m, 60, 7, "pelus-ayi"),
            P(49, "Yapı Blokları Seti", "500 parça, 6 yaş ve üzeri.", 899.00m, 35, 7, "lego"),
            P(50, "Akustik Gitar", "Ladin kapak, çelik tel, çantalı.", 3799.00m, 8, 7, "gitar")
        );
    }

    private static Product P(int id, string name, string description, decimal price, int stock, int categoryId, string image) => new()
    {
        Id = id, Name = name, Description = description, Price = price, Stock = stock,
        CategoryId = categoryId, ImageUrl = $"/img/products/{image}.jpg", CreatedDate = SeedDate,
        Specifications = string.Join("\n", Specs[id])
    };

    private static readonly Dictionary<int, string[]> Specs = new()
    {
        [1] = ["Bağlantı: Bluetooth 5.3", "Pil ömrü: 30 saat (ANC açık)", "Şarj süresi: 2 saat, USB-C", "Ağırlık: 250 g", "Garanti: 2 yıl"],
        [2] = ["Ekran: 1.4\" AMOLED", "Pil ömrü: 7 gün", "Su geçirmezlik: 5 ATM", "Sensörler: Nabız, SpO2, uyku", "Garanti: 2 yıl"],
        [3] = ["Switch: Hot-swap, lineer kırmızı", "Düzen: %75, Türkçe Q", "Aydınlatma: RGB, 18 mod", "Bağlantı: USB-C kablolu", "Garanti: 2 yıl"],
        [8] = ["Bağlantı: 2.4 GHz USB alıcı", "DPI: 800 / 1200 / 1600", "Pil: 1 x AA (18 ay)", "Ağırlık: 85 g", "Garanti: 2 yıl"],
        [9] = ["Çıkış gücü: 5 W", "Bağlantı: Bluetooth 5.0", "Pil ömrü: 8 saat", "Özellik: Manyetik levitasyon, 360° ses", "Garanti: 2 yıl"],
        [10] = ["Kapasite: 20000 mAh", "Çıkış: 2 x USB-A, 1 x USB-C", "Hızlı şarj: 22.5 W", "Ekran: Dijital yüzde göstergesi", "Ağırlık: 380 g"],
        [11] = ["Panel: 24\" IPS", "Çözünürlük: 1920 x 1200", "Yenileme hızı: 60 Hz", "Bağlantı: HDMI, DisplayPort, 4 x USB", "Stand: Yükseklik ve pivot ayarlı"],
        [12] = ["Çözünürlük: 1080p, 30 fps", "Mikrofon: Dahili, gürültü azaltmalı", "Bağlantı: USB-A, tak-çalıştır", "Görüş açısı: 78°", "Uyumluluk: Windows, macOS"],
        [4] = ["Malzeme: %100 organik pamuk", "Kalıp: Regular fit", "Yaka: Bisiklet yaka", "Bedenler: S – XXL", "Yıkama: 30 °C"],
        [5] = ["Malzeme: %100 pamuk denim", "Kalıp: Regular fit", "Cep: 2 göğüs, 2 yan", "Bedenler: S – XL", "Yıkama: 30 °C, ters çevirerek"],
        [13] = ["Malzeme: %80 pamuk, %20 polyester", "İç yüzey: Şardonlu", "Kalıp: Oversize", "Bedenler: XS – XXL", "Yıkama: 30 °C"],
        [14] = ["Malzeme: %98 pamuk, %2 elastan", "Kalıp: Slim fit, orta bel", "Paça: Dar", "Bedenler: 28 – 38", "Yıkama: 30 °C"],
        [15] = ["Üst malzeme: File ve suni deri", "Taban: EVA, hafif", "Bağcık: Pamuklu", "Numaralar: 36 – 45", "Ağırlık: 290 g (tek)"],
        [16] = ["Malzeme: %70 yün, %30 akrilik", "Ölçü: 180 x 30 cm", "Üretim: El örgüsü", "Renk: Antrasit melanj", "Yıkama: Elde, soğuk su"],
        [17] = ["Malzeme: Tam tahıl hakiki deri", "Genişlik: 3.5 cm", "Toka: Nikel kaplama metal", "Bedenler: 85 – 115 cm", "Üretim: Türkiye"],
        [6] = ["Malzeme: Seramik", "Hacim: 350 ml", "Desen: Nota baskılı, keman kulp", "Bulaşık makinesi: Uygun", "Mikrodalga: Uygun değil"],
        [7] = ["Güç: 8 W LED", "Renk sıcaklığı: 3000 – 6500 K", "Parlaklık: 5 kademe, dokunmatik", "Ekstra: USB şarj çıkışı", "Garanti: 2 yıl"],
        [18] = ["Hacim: 1 L (8 fincan)", "Gövde: Isıya dayanıklı borosilikat cam", "Süzgeç: Çift katmanlı paslanmaz çelik", "Bulaşık makinesi: Uygun", "Ağırlık: 620 g"],
        [19] = ["Malzeme: Dökme demir", "Çap: 26 cm", "Kaplama: Önceden yağlanmış", "Uyumluluk: İndüksiyon, gaz, fırın", "Ağırlık: 2.4 kg"],
        [20] = ["Malzeme: %100 pamuk saten", "İplik sıklığı: 300 TC", "Set: Nevresim, çarşaf, 2 yastık kılıfı", "Ölçü: Çift kişilik (200 x 220)", "Yıkama: 40 °C"],
        [21] = ["Tür: Monstera deliciosa", "Boy: 60 – 70 cm (saksı dahil)", "Saksı: 17 cm seramik", "Işık: Parlak, dolaylı", "Sulama: Haftada 1"],
        [22] = ["Malzeme: %100 soya mumu", "Fitil: Ahşap, çıtırtılı", "Yanma süresi: 40 saat", "Koku: Vanilya & sandal ağacı", "Ağırlık: 200 g"],
        [23] = ["Kalınlık: 6 mm", "Ölçü: 183 x 61 cm", "Malzeme: TPE, kaymaz yüzey", "Ekstra: Taşıma askısı", "Ağırlık: 900 g"],
        [24] = ["Ağırlık: 2 x 10 kg", "Malzeme: Döküm demir", "Tutuş: Tırtıklı, kauçuk kaplı", "Ayarlanabilir: Hayır", "Kullanım: Ev ve salon"],
        [25] = ["Taban: Amortisörlü köpük, kauçuk dış taban", "Drop: 10 mm", "Üst yüzey: Nefes alan file", "Numaralar: 36 – 46", "Ağırlık: 260 g (tek)"],
        [26] = ["Termos hacmi: 1 L", "Sıcak tutma: 12 saat", "Soğuk tutma: 24 saat", "Set: Termos + metal beslenme çantası", "Malzeme: Paslanmaz çelik iç hazne"],
        [27] = ["Kapasite: 4 kişi", "Su geçirmezlik: 3000 mm", "Kurulum: Yaklaşık 10 dakika", "Ölçü: 240 x 210 x 150 cm", "Ağırlık: 5.8 kg"],
        [28] = ["Hacim: 30 L", "Dizüstü bölmesi: 15.6\"e kadar", "Malzeme: Su itici polyester", "Ekstra: Yağmurluk, göğüs kayışı", "Ağırlık: 950 g"],
        [29] = ["Gövde: In-mold EPS", "Havalandırma: 21 kanal", "Beden: M (54 – 58 cm), ayarlanabilir", "Sertifika: EN 1078", "Ağırlık: 260 g"],
        [30] = ["Boyut: A5 (14.8 x 21 cm)", "Sayfa: 192, noktalı", "Kağıt: 100 g/m², asitsiz", "Kapak: Suni deri, lastikli", "Ekstra: Ayraç kurdelesi, arka cep"],
        [31] = ["Gövde: Karbon fiber desenli metal", "Uç: Paslanmaz çelik, F (ince)", "Dolum: Kartuş ve konvertör", "Kutu: Hediye kutulu", "Mürekkep: 2 mavi kartuş dahil"],
        [32] = ["Renk sayısı: 24", "Tür: Tablet (katı) suluboya", "Ekstra: 2 fırça, karıştırma paleti", "Kutu: Metal, katlanır", "Yaş: 6+"],
        [33] = ["Malzeme: Masif meşe", "Bölme: 8 kalem yuvası", "Ölçü: 20 x 6 x 6 cm", "Yüzey: Doğal yağ ile işlenmiş", "Üretim: El yapımı"],
        [34] = ["Kitap sayısı: 10", "Dil: Türkçe", "Kapak: Karton kapak", "Toplam sayfa: Yaklaşık 3200", "Yayın: Tam metin, kısaltılmamış"],
        [35] = ["Dönem: Ocak – Aralık 2027", "Düzen: Haftalık, iki sayfaya bir hafta", "Boyut: B5", "Kapak: Keten kumaş", "Sayfa: 176"],
        [36] = ["Renk sayısı: 36", "Uç: 3.3 mm yumuşak grafit", "Gövde: Altıgen, ahşap", "Kutu: Metal", "Yaş: 3+"],
        [37] = ["Hacim: 50 ml", "Cilt tipi: Tüm cilt tipleri", "İçerik: Hyalüronik asit, seramid", "Parfüm: İçermez", "Dermatolojik test: Evet"],
        [38] = ["Hacim: 50 ml", "Tür: Eau de parfum", "Koku ailesi: Çiçeksi – odunsu", "Kalıcılık: 6 – 8 saat", "Şişe: Cam, sprey"],
        [39] = ["Adet: 4", "Sap: Biyobozunur bambu", "Kıl: Yumuşak, BPA içermez", "Ambalaj: Geri dönüştürülmüş karton", "Değişim önerisi: 3 ayda bir"],
        [40] = ["Güç: 2200 W", "Isı kademesi: 3", "Hız kademesi: 2", "Teknoloji: İyonik, soğuk hava tuşu", "Aparat: Yoğunlaştırıcı başlık"],
        [41] = ["Adet: 6 x 100 g", "Üretim: Soğuk proses, el yapımı", "İçerik: Zeytinyağı, hindistancevizi yağı", "Kokular: Lavanta, defne, portakal…", "Hayvan testi: Yok"],
        [42] = ["Bitiş: Mat", "Kalıcılık: 8 saate kadar", "İçerik: E vitamini, jojoba yağı", "Ağırlık: 3.5 g", "Hayvan testi: Yok"],
        [43] = ["Koruma: SPF 50, UVA/UVB", "Hacim: 200 ml", "Suya dayanıklılık: 80 dakika", "Doku: Hafif losyon, yağsız", "Cilt tipi: Tüm cilt tipleri"],
        [44] = ["Parça sayısı: 1000", "Bitmiş ölçü: 68 x 48 cm", "Malzeme: Geri dönüştürülmüş karton", "Tema: Gün batımı manzarası", "Yaş: 12+"],
        [45] = ["Tahta: Katlanır ahşap, 40 x 40 cm", "Kare: 4.5 cm", "Taşlar: Ağırlıklı, keçe tabanlı", "Şah yüksekliği: 7.5 cm", "Saklama: Tahta içinde"],
        [46] = ["Set: 3x3, 2x2, piramit ve 2 özel küp", "Mekanizma: Hızlı dönüşlü, manyetik", "Etiket: Etiketsiz, renkli plastik", "Ekstra: Stand ve taşıma çantası", "Yaş: 6+"],
        [47] = ["Ölçek: 1:16", "Çekiş: 4x4", "Frekans: 2.4 GHz, 80 m menzil", "Batarya: 7.4 V şarjlı, 25 dk", "Hız: 30 km/s'e kadar"],
        [48] = ["Boy: 40 cm", "Malzeme: Yumuşak peluş, polyester elyaf dolgu", "Güvenlik: CE, EN71 sertifikalı", "Yıkama: 30 °C, hassas program", "Yaş: 0+"],
        [49] = ["Parça sayısı: 500", "Uyumluluk: Standart yapı blokları ile uyumlu", "Malzeme: ABS plastik", "Ekstra: Saklama kutusu, fikir kitapçığı", "Yaş: 6+"],
        [50] = ["Kapak: Ladin", "Gövde: Maun, dreadnought", "Tel: Çelik", "Perde: 20", "Ekstra: Yumuşak taşıma çantası"]
    };
}
