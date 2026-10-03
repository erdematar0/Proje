using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETicaret.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreCategoriesAndProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fitness, koşu, kamp ve bisiklet", true, "Spor & Outdoor", null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kitap, defter, kalem ve sanat malzemeleri", true, "Kitap & Kırtasiye", null },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cilt, saç ve kişisel bakım ürünleri", true, "Kozmetik & Bakım", null },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Oyuncak, oyun, yapboz ve müzik", true, "Oyuncak & Hobi", null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/img/products/kulaklik.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/img/products/akilli-saat.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/img/products/mekanik-klavye.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "%100 organik pamuk, robot baskılı, basic kesim.", "/img/products/tisort.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/img/products/denim-ceket.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Keman kulplu seramik kupa, 350 ml.", "/img/products/kupa-seti.jpg", "Nota Desenli Kupa" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "/img/products/masa-lambasi.jpg");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "ImageUrl", "IsActive", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[,]
                {
                    { 8, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sessiz tıklama, 2.4 GHz alıcı, 18 ay pil ömrü.", "/img/products/kablosuz-mouse.jpg", true, "Kablosuz Mouse", 449.90m, 60, null },
                    { 9, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Havada duran küre hoparlör, 360° ses, 8 saat pil.", "/img/products/bluetooth-hoparlor.jpg", true, "Levitasyon Bluetooth Hoparlör", 1349.00m, 15, null },
                    { 10, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dijital göstergeli, 2 USB çıkış, hızlı şarj.", "/img/products/powerbank.jpg", true, "Powerbank 20000 mAh", 899.00m, 45, null },
                    { 11, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1920x1200 çözünürlük, yükseklik ayarlı stand.", "/img/products/monitor.jpg", true, "24\" IPS Monitör", 5299.00m, 10, null },
                    { 12, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1080p görüntü, dahili mikrofon, tak-çalıştır.", "/img/products/web-kamerasi.jpg", true, "Full HD Web Kamerası", 799.00m, 35, null },
                    { 13, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Şardonlu iç yüzey, kanguru cep.", "/img/products/sweatshirt.jpg", true, "Kapüşonlu Sweatshirt", 749.90m, 55, null },
                    { 14, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Esnek denim, orta bel, 5 cep.", "/img/products/kot-pantolon.jpg", true, "Slim Fit Kot Pantolon", 899.90m, 70, null },
                    { 15, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hafif taban, nefes alan file üst yüzey.", "/img/products/sneaker.jpg", true, "Beyaz Sneaker", 1599.00m, 40, null },
                    { 16, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "El örgüsü, yumuşak yün karışım.", "/img/products/atki.jpg", true, "Örgü Yün Atkı", 399.00m, 50, null },
                    { 17, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tam tahıl deri, metal toka, 3.5 cm.", "/img/products/deri-kemer.jpg", true, "Hakiki Deri Kemer", 549.00m, 65, null },
                    { 18, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Çift katmanlı paslanmaz çelik süzgeç.", "/img/products/french-press.jpg", true, "French Press 1 L", 649.00m, 30, null },
                    { 19, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Önceden yağlanmış, fırında kullanılabilir.", "/img/products/dokum-tava.jpg", true, "Döküm Tava 26 cm", 1199.00m, 20, null },
                    { 20, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Çift kişilik, %100 pamuk saten.", "/img/products/nevresim.jpg", true, "Pamuklu Nevresim Takımı", 1449.00m, 25, null },
                    { 21, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "60–70 cm, dekoratif saksıyla birlikte.", "/img/products/monstera.jpg", true, "Monstera Saksı Bitkisi", 699.00m, 12, null },
                    { 22, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Soya mumu, ahşap fitil, 40 saat yanma.", "/img/products/kokulu-mum.jpg", true, "Kokulu Mum", 329.00m, 80, null },
                    { 23, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kaymaz yüzey, taşıma askılı.", "/img/products/yoga-mati.jpg", true, "Yoga Matı 6 mm", 459.00m, 50, null },
                    { 24, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "2 x 10 kg, kauçuk tutuş.", "/img/products/dambil.jpg", true, "Döküm Dambıl Seti", 1899.00m, 15, null },
                    { 25, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Amortisörlü taban, yansıtıcı detaylar.", "/img/products/kosu-ayakkabisi.jpg", true, "Koşu Ayakkabısı", 2499.00m, 30, null },
                    { 26, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1 L termos ve metal beslenme çantası.", "/img/products/termos.jpg", true, "Retro Termos Seti", 799.00m, 22, null },
                    { 27, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Su geçirmez, kolay kurulum, taşıma çantalı.", "/img/products/kamp-cadiri.jpg", true, "4 Kişilik Kamp Çadırı", 3299.00m, 10, null },
                    { 28, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dizüstü bölmeli, yağmurluklu.", "/img/products/sirt-cantasi.jpg", true, "Sırt Çantası 30 L", 1149.00m, 35, null },
                    { 29, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hafif EPS gövde, 21 havalandırma kanalı.", "/img/products/bisiklet-kaski.jpg", true, "Bisiklet Kaskı", 1099.00m, 25, null },
                    { 30, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A5, 192 sayfa, lastikli kapak.", "/img/products/defter.jpg", true, "Deri Kapaklı Defter", 279.00m, 90, null },
                    { 31, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Karbon gövde, çelik uç, kartuşlu.", "/img/products/dolma-kalem.jpg", true, "Dolma Kalem", 649.00m, 30, null },
                    { 32, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tablet suluboya, fırça hediyeli.", "/img/products/suluboya.jpg", true, "Suluboya Seti 24 Renk", 389.00m, 45, null },
                    { 33, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Masif ahşap, 8 bölmeli masa düzenleyici.", "/img/products/masa-organizeri.jpg", true, "Ahşap Kalemlik", 229.00m, 60, null },
                    { 34, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "10 kitaplık dünya klasikleri seti.", "/img/products/kitap-seti.jpg", true, "Klasikler Kitap Seti", 1249.00m, 20, null },
                    { 35, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Haftalık planlayıcı, keten kapak.", "/img/products/ajanda.jpg", true, "Ajanda 2027", 319.00m, 75, null },
                    { 36, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "36 renk, yumuşak ve canlı tonlar.", "/img/products/kalem-seti.jpg", true, "Kuru Boya Kalem Seti", 199.00m, 100, null },
                    { 37, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hyalüronik asitli, 24 saat nem, 50 ml.", "/img/products/nemlendirici.jpg", true, "Nemlendirici Krem", 349.00m, 60, null },
                    { 38, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Çiçeksi-odunsu, eau de parfum.", "/img/products/parfum.jpg", true, "Parfüm 50 ml", 1799.00m, 25, null },
                    { 39, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Biyobozunur sap, yumuşak kıl.", "/img/products/bambu-dis-fircasi.jpg", true, "Bambu Diş Fırçası 4'lü", 149.00m, 120, null },
                    { 40, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "2200 W, iyonik, 3 ısı kademesi.", "/img/products/sac-kurutma.jpg", true, "Saç Kurutma Makinesi", 1299.00m, 20, null },
                    { 41, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6 farklı bitkisel sabun, el yapımı.", "/img/products/sabun-seti.jpg", true, "Doğal Sabun Seti", 259.00m, 70, null },
                    { 42, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Uzun süre kalıcı, nemlendirici formül.", "/img/products/ruj.jpg", true, "Mat Ruj", 229.00m, 85, null },
                    { 43, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Suya dayanıklı, geniş spektrum koruma.", "/img/products/gunes-kremi.jpg", true, "Güneş Kremi SPF 50", 399.00m, 55, null },
                    { 44, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gün batımı manzaralı, 68 x 48 cm.", "/img/products/yapboz.jpg", true, "Yapboz 1000 Parça", 299.00m, 40, null },
                    { 45, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ağırlıklı taşlar, katlanır tahta.", "/img/products/satranc.jpg", true, "Satranç Takımı", 649.00m, 25, null },
                    { 46, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3x3, 2x2 ve piramit küp içeren set.", "/img/products/rubik-kup.jpg", true, "Zeka Küpü Seti", 399.00m, 50, null },
                    { 47, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "4x4, 2.4 GHz, şarjlı batarya.", "/img/products/rc-araba.jpg", true, "Uzaktan Kumandalı Araba", 1199.00m, 18, null },
                    { 48, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yumuşak peluş, fiyonklu.", "/img/products/pelus-ayi.jpg", true, "Peluş Ayı 40 cm", 349.00m, 60, null },
                    { 49, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "500 parça, 6 yaş ve üzeri.", "/img/products/lego.jpg", true, "Yapı Blokları Seti", 899.00m, 35, null },
                    { 50, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ladin kapak, çelik tel, çantalı.", "/img/products/gitar.jpg", true, "Akustik Gitar", 3799.00m, 8, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://picsum.photos/seed/kulaklik/600/450");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://picsum.photos/seed/saat/600/450");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://picsum.photos/seed/klavye/600/450");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "%100 organik pamuk, basic kesim.", "https://picsum.photos/seed/tisort/600/450" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://picsum.photos/seed/ceket/600/450");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "4'lü el yapımı kupa seti.", "https://picsum.photos/seed/kupa/600/450", "Seramik Kupa Seti" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://picsum.photos/seed/lamba/600/450");
        }
    }
}
