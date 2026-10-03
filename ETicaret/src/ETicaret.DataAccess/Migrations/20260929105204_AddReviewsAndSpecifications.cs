using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETicaret.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewsAndSpecifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specifications",
                table: "Products",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsVerifiedPurchase = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Specifications",
                value: "Bağlantı: Bluetooth 5.3\nPil ömrü: 30 saat (ANC açık)\nŞarj süresi: 2 saat, USB-C\nAğırlık: 250 g\nGaranti: 2 yıl");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Specifications",
                value: "Ekran: 1.4\" AMOLED\nPil ömrü: 7 gün\nSu geçirmezlik: 5 ATM\nSensörler: Nabız, SpO2, uyku\nGaranti: 2 yıl");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Specifications",
                value: "Switch: Hot-swap, lineer kırmızı\nDüzen: %75, Türkçe Q\nAydınlatma: RGB, 18 mod\nBağlantı: USB-C kablolu\nGaranti: 2 yıl");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Specifications",
                value: "Malzeme: %100 organik pamuk\nKalıp: Regular fit\nYaka: Bisiklet yaka\nBedenler: S – XXL\nYıkama: 30 °C");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Specifications",
                value: "Malzeme: %100 pamuk denim\nKalıp: Regular fit\nCep: 2 göğüs, 2 yan\nBedenler: S – XL\nYıkama: 30 °C, ters çevirerek");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "Specifications",
                value: "Malzeme: Seramik\nHacim: 350 ml\nDesen: Nota baskılı, keman kulp\nBulaşık makinesi: Uygun\nMikrodalga: Uygun değil");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "Specifications",
                value: "Güç: 8 W LED\nRenk sıcaklığı: 3000 – 6500 K\nParlaklık: 5 kademe, dokunmatik\nEkstra: USB şarj çıkışı\nGaranti: 2 yıl");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "Specifications",
                value: "Bağlantı: 2.4 GHz USB alıcı\nDPI: 800 / 1200 / 1600\nPil: 1 x AA (18 ay)\nAğırlık: 85 g\nGaranti: 2 yıl");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "Specifications",
                value: "Çıkış gücü: 5 W\nBağlantı: Bluetooth 5.0\nPil ömrü: 8 saat\nÖzellik: Manyetik levitasyon, 360° ses\nGaranti: 2 yıl");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "Specifications",
                value: "Kapasite: 20000 mAh\nÇıkış: 2 x USB-A, 1 x USB-C\nHızlı şarj: 22.5 W\nEkran: Dijital yüzde göstergesi\nAğırlık: 380 g");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "Specifications",
                value: "Panel: 24\" IPS\nÇözünürlük: 1920 x 1200\nYenileme hızı: 60 Hz\nBağlantı: HDMI, DisplayPort, 4 x USB\nStand: Yükseklik ve pivot ayarlı");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "Specifications",
                value: "Çözünürlük: 1080p, 30 fps\nMikrofon: Dahili, gürültü azaltmalı\nBağlantı: USB-A, tak-çalıştır\nGörüş açısı: 78°\nUyumluluk: Windows, macOS");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "Specifications",
                value: "Malzeme: %80 pamuk, %20 polyester\nİç yüzey: Şardonlu\nKalıp: Oversize\nBedenler: XS – XXL\nYıkama: 30 °C");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "Specifications",
                value: "Malzeme: %98 pamuk, %2 elastan\nKalıp: Slim fit, orta bel\nPaça: Dar\nBedenler: 28 – 38\nYıkama: 30 °C");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "Specifications",
                value: "Üst malzeme: File ve suni deri\nTaban: EVA, hafif\nBağcık: Pamuklu\nNumaralar: 36 – 45\nAğırlık: 290 g (tek)");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "Specifications",
                value: "Malzeme: %70 yün, %30 akrilik\nÖlçü: 180 x 30 cm\nÜretim: El örgüsü\nRenk: Antrasit melanj\nYıkama: Elde, soğuk su");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "Specifications",
                value: "Malzeme: Tam tahıl hakiki deri\nGenişlik: 3.5 cm\nToka: Nikel kaplama metal\nBedenler: 85 – 115 cm\nÜretim: Türkiye");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "Specifications",
                value: "Hacim: 1 L (8 fincan)\nGövde: Isıya dayanıklı borosilikat cam\nSüzgeç: Çift katmanlı paslanmaz çelik\nBulaşık makinesi: Uygun\nAğırlık: 620 g");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "Specifications",
                value: "Malzeme: Dökme demir\nÇap: 26 cm\nKaplama: Önceden yağlanmış\nUyumluluk: İndüksiyon, gaz, fırın\nAğırlık: 2.4 kg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "Specifications",
                value: "Malzeme: %100 pamuk saten\nİplik sıklığı: 300 TC\nSet: Nevresim, çarşaf, 2 yastık kılıfı\nÖlçü: Çift kişilik (200 x 220)\nYıkama: 40 °C");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                column: "Specifications",
                value: "Tür: Monstera deliciosa\nBoy: 60 – 70 cm (saksı dahil)\nSaksı: 17 cm seramik\nIşık: Parlak, dolaylı\nSulama: Haftada 1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                column: "Specifications",
                value: "Malzeme: %100 soya mumu\nFitil: Ahşap, çıtırtılı\nYanma süresi: 40 saat\nKoku: Vanilya & sandal ağacı\nAğırlık: 200 g");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                column: "Specifications",
                value: "Kalınlık: 6 mm\nÖlçü: 183 x 61 cm\nMalzeme: TPE, kaymaz yüzey\nEkstra: Taşıma askısı\nAğırlık: 900 g");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                column: "Specifications",
                value: "Ağırlık: 2 x 10 kg\nMalzeme: Döküm demir\nTutuş: Tırtıklı, kauçuk kaplı\nAyarlanabilir: Hayır\nKullanım: Ev ve salon");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                column: "Specifications",
                value: "Taban: Amortisörlü köpük, kauçuk dış taban\nDrop: 10 mm\nÜst yüzey: Nefes alan file\nNumaralar: 36 – 46\nAğırlık: 260 g (tek)");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                column: "Specifications",
                value: "Termos hacmi: 1 L\nSıcak tutma: 12 saat\nSoğuk tutma: 24 saat\nSet: Termos + metal beslenme çantası\nMalzeme: Paslanmaz çelik iç hazne");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                column: "Specifications",
                value: "Kapasite: 4 kişi\nSu geçirmezlik: 3000 mm\nKurulum: Yaklaşık 10 dakika\nÖlçü: 240 x 210 x 150 cm\nAğırlık: 5.8 kg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                column: "Specifications",
                value: "Hacim: 30 L\nDizüstü bölmesi: 15.6\"e kadar\nMalzeme: Su itici polyester\nEkstra: Yağmurluk, göğüs kayışı\nAğırlık: 950 g");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                column: "Specifications",
                value: "Gövde: In-mold EPS\nHavalandırma: 21 kanal\nBeden: M (54 – 58 cm), ayarlanabilir\nSertifika: EN 1078\nAğırlık: 260 g");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "Specifications",
                value: "Boyut: A5 (14.8 x 21 cm)\nSayfa: 192, noktalı\nKağıt: 100 g/m², asitsiz\nKapak: Suni deri, lastikli\nEkstra: Ayraç kurdelesi, arka cep");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                column: "Specifications",
                value: "Gövde: Karbon fiber desenli metal\nUç: Paslanmaz çelik, F (ince)\nDolum: Kartuş ve konvertör\nKutu: Hediye kutulu\nMürekkep: 2 mavi kartuş dahil");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                column: "Specifications",
                value: "Renk sayısı: 24\nTür: Tablet (katı) suluboya\nEkstra: 2 fırça, karıştırma paleti\nKutu: Metal, katlanır\nYaş: 6+");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                column: "Specifications",
                value: "Malzeme: Masif meşe\nBölme: 8 kalem yuvası\nÖlçü: 20 x 6 x 6 cm\nYüzey: Doğal yağ ile işlenmiş\nÜretim: El yapımı");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                column: "Specifications",
                value: "Kitap sayısı: 10\nDil: Türkçe\nKapak: Karton kapak\nToplam sayfa: Yaklaşık 3200\nYayın: Tam metin, kısaltılmamış");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                column: "Specifications",
                value: "Dönem: Ocak – Aralık 2027\nDüzen: Haftalık, iki sayfaya bir hafta\nBoyut: B5\nKapak: Keten kumaş\nSayfa: 176");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                column: "Specifications",
                value: "Renk sayısı: 36\nUç: 3.3 mm yumuşak grafit\nGövde: Altıgen, ahşap\nKutu: Metal\nYaş: 3+");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                column: "Specifications",
                value: "Hacim: 50 ml\nCilt tipi: Tüm cilt tipleri\nİçerik: Hyalüronik asit, seramid\nParfüm: İçermez\nDermatolojik test: Evet");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38,
                column: "Specifications",
                value: "Hacim: 50 ml\nTür: Eau de parfum\nKoku ailesi: Çiçeksi – odunsu\nKalıcılık: 6 – 8 saat\nŞişe: Cam, sprey");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39,
                column: "Specifications",
                value: "Adet: 4\nSap: Biyobozunur bambu\nKıl: Yumuşak, BPA içermez\nAmbalaj: Geri dönüştürülmüş karton\nDeğişim önerisi: 3 ayda bir");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                column: "Specifications",
                value: "Güç: 2200 W\nIsı kademesi: 3\nHız kademesi: 2\nTeknoloji: İyonik, soğuk hava tuşu\nAparat: Yoğunlaştırıcı başlık");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41,
                column: "Specifications",
                value: "Adet: 6 x 100 g\nÜretim: Soğuk proses, el yapımı\nİçerik: Zeytinyağı, hindistancevizi yağı\nKokular: Lavanta, defne, portakal…\nHayvan testi: Yok");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42,
                column: "Specifications",
                value: "Bitiş: Mat\nKalıcılık: 8 saate kadar\nİçerik: E vitamini, jojoba yağı\nAğırlık: 3.5 g\nHayvan testi: Yok");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                column: "Specifications",
                value: "Koruma: SPF 50, UVA/UVB\nHacim: 200 ml\nSuya dayanıklılık: 80 dakika\nDoku: Hafif losyon, yağsız\nCilt tipi: Tüm cilt tipleri");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44,
                column: "Specifications",
                value: "Parça sayısı: 1000\nBitmiş ölçü: 68 x 48 cm\nMalzeme: Geri dönüştürülmüş karton\nTema: Gün batımı manzarası\nYaş: 12+");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45,
                column: "Specifications",
                value: "Tahta: Katlanır ahşap, 40 x 40 cm\nKare: 4.5 cm\nTaşlar: Ağırlıklı, keçe tabanlı\nŞah yüksekliği: 7.5 cm\nSaklama: Tahta içinde");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46,
                column: "Specifications",
                value: "Set: 3x3, 2x2, piramit ve 2 özel küp\nMekanizma: Hızlı dönüşlü, manyetik\nEtiket: Etiketsiz, renkli plastik\nEkstra: Stand ve taşıma çantası\nYaş: 6+");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47,
                column: "Specifications",
                value: "Ölçek: 1:16\nÇekiş: 4x4\nFrekans: 2.4 GHz, 80 m menzil\nBatarya: 7.4 V şarjlı, 25 dk\nHız: 30 km/s'e kadar");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48,
                column: "Specifications",
                value: "Boy: 40 cm\nMalzeme: Yumuşak peluş, polyester elyaf dolgu\nGüvenlik: CE, EN71 sertifikalı\nYıkama: 30 °C, hassas program\nYaş: 0+");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                column: "Specifications",
                value: "Parça sayısı: 500\nUyumluluk: Standart yapı blokları ile uyumlu\nMalzeme: ABS plastik\nEkstra: Saklama kutusu, fikir kitapçığı\nYaş: 6+");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50,
                column: "Specifications",
                value: "Kapak: Ladin\nGövde: Maun, dreadnought\nTel: Çelik\nPerde: 20\nEkstra: Yumuşak taşıma çantası");

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "AuthorName", "Comment", "CreatedDate", "IsActive", "IsVerifiedPurchase", "ProductId", "Rating", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Can Ö.", "Ürün idare eder, iki hafta kullandım şimdilik sorun yok.", new DateTime(2026, 1, 14, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 1, 3, null, null },
                    { 2, "Deniz P.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 2, 12, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 1, 5, null, null },
                    { 3, "İrem H.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 3, 13, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 1, 4, null, null },
                    { 4, "Serkan İ.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 4, 11, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 1, 2, null, null },
                    { 5, "Tolga N.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 1, 27, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 2, 2, null, null },
                    { 6, "Buse R.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 2, 25, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 2, 5, null, null },
                    { 7, "Ayşe K.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 3, 26, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 2, 4, null, null },
                    { 8, "Emre Ç.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 4, 24, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 2, 5, null, null },
                    { 9, "Selin Y.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 5, 23, 17, 0, 0, 0, DateTimeKind.Utc), true, false, 2, 5, null, null },
                    { 10, "Onur G.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 6, 21, 19, 0, 0, 0, DateTimeKind.Utc), true, true, 2, 4, null, null },
                    { 11, "Mehmet T.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 2, 9, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 3, 5, null, null },
                    { 12, "Elif D.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 3, 10, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 3, 5, null, null },
                    { 13, "Can Ö.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 4, 8, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 3, 4, null, null },
                    { 14, "Merve B.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 2, 22, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 4, 5, null, null },
                    { 15, "Gizem E.", "Ortalama bir ürün. Kargo hızlıydı, bunun için teşekkürler.", new DateTime(2026, 3, 23, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 4, 3, null, null },
                    { 16, "Tolga N.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 4, 21, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 4, 5, null, null },
                    { 17, "Buse R.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 5, 20, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 4, 4, null, null },
                    { 18, "Ayşe K.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 6, 18, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 4, 2, null, null },
                    { 19, "Ece F.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 3, 7, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 5, 4, null, null },
                    { 20, "Hakan V.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 4, 5, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 5, 2, null, null },
                    { 21, "Zeynep A.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 3, 20, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 6, 4, null, null },
                    { 22, "Burak S.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 4, 18, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 6, 5, null, null },
                    { 23, "Merve B.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 5, 17, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 6, 5, null, null },
                    { 24, "Gizem E.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 6, 15, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 6, 4, null, null },
                    { 25, "Onur G.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 4, 2, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 7, 4, null, null },
                    { 26, "Kerem U.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 5, 1, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 7, 5, null, null },
                    { 27, "Ece F.", "Fena değil ama fiyatına göre daha iyisini beklerdim.", new DateTime(2026, 5, 30, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 7, 3, null, null },
                    { 28, "Hakan V.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 6, 28, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 7, 5, null, null },
                    { 29, "Mehmet T.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 7, 27, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 7, 4, null, null },
                    { 30, "Elif D.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 8, 25, 19, 0, 0, 0, DateTimeKind.Utc), true, false, 7, 2, null, null },
                    { 31, "Serkan İ.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 4, 15, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 8, 5, null, null },
                    { 32, "Nazlı Z.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 5, 14, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 8, 4, null, null },
                    { 33, "Zeynep A.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 6, 12, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 8, 2, null, null },
                    { 34, "Emre Ç.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 4, 28, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 9, 5, null, null },
                    { 35, "Selin Y.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 5, 27, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 9, 4, null, null },
                    { 36, "Onur G.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 6, 25, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 9, 5, null, null },
                    { 37, "Kerem U.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 7, 24, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 9, 5, null, null },
                    { 38, "Ece F.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 8, 22, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 9, 4, null, null },
                    { 39, "Deniz P.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 5, 11, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 10, 5, null, null },
                    { 40, "İrem H.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 6, 9, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 10, 4, null, null },
                    { 41, "Buse R.", "Ortalama bir ürün. Kargo hızlıydı, bunun için teşekkürler.", new DateTime(2026, 5, 24, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 11, 3, null, null },
                    { 42, "Ayşe K.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 6, 22, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 11, 5, null, null },
                    { 43, "Emre Ç.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 7, 21, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 11, 4, null, null },
                    { 44, "Selin Y.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 8, 19, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 11, 2, null, null },
                    { 45, "Elif D.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 6, 6, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 12, 2, null, null },
                    { 46, "Can Ö.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 7, 5, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 12, 5, null, null },
                    { 47, "Deniz P.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 8, 3, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 12, 4, null, null },
                    { 48, "İrem H.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 1, 4, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 12, 5, null, null },
                    { 49, "Serkan İ.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 2, 2, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 12, 5, null, null },
                    { 50, "Nazlı Z.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 3, 3, 19, 0, 0, 0, DateTimeKind.Utc), true, true, 12, 4, null, null },
                    { 51, "Gizem E.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 6, 19, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 13, 5, null, null },
                    { 52, "Tolga N.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 7, 18, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 13, 5, null, null },
                    { 53, "Buse R.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 8, 16, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 13, 4, null, null },
                    { 54, "Hakan V.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 7, 2, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 14, 5, null, null },
                    { 55, "Mehmet T.", "Fena değil ama fiyatına göre daha iyisini beklerdim.", new DateTime(2026, 7, 31, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 14, 3, null, null },
                    { 56, "Elif D.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 1, 1, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 14, 5, null, null },
                    { 57, "Can Ö.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 1, 30, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 14, 4, null, null },
                    { 58, "Deniz P.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 2, 28, 17, 0, 0, 0, DateTimeKind.Utc), true, false, 14, 2, null, null },
                    { 59, "Burak S.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 7, 15, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 15, 4, null, null },
                    { 60, "Merve B.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 8, 13, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 15, 2, null, null },
                    { 61, "Kerem U.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 7, 28, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 16, 4, null, null },
                    { 62, "Ece F.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 8, 26, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 16, 5, null, null },
                    { 63, "Hakan V.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 1, 27, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 16, 5, null, null },
                    { 64, "Mehmet T.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 2, 25, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 16, 4, null, null },
                    { 65, "Nazlı Z.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 8, 10, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 17, 4, null, null },
                    { 66, "Zeynep A.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 1, 11, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 17, 5, null, null },
                    { 67, "Burak S.", "Ürün idare eder, iki hafta kullandım şimdilik sorun yok.", new DateTime(2026, 2, 9, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 17, 3, null, null },
                    { 68, "Merve B.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 3, 10, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 17, 5, null, null },
                    { 69, "Gizem E.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 4, 8, 17, 0, 0, 0, DateTimeKind.Utc), true, false, 17, 4, null, null },
                    { 70, "Tolga N.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 5, 7, 19, 0, 0, 0, DateTimeKind.Utc), true, true, 17, 2, null, null },
                    { 71, "Selin Y.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 8, 23, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 18, 5, null, null },
                    { 72, "Onur G.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 1, 24, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 18, 4, null, null },
                    { 73, "Kerem U.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 2, 22, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 18, 2, null, null },
                    { 74, "İrem H.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 1, 8, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 19, 5, null, null },
                    { 75, "Serkan İ.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 2, 6, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 19, 4, null, null },
                    { 76, "Nazlı Z.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 3, 7, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 19, 5, null, null },
                    { 77, "Zeynep A.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 4, 5, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 19, 5, null, null },
                    { 78, "Burak S.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 5, 4, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 19, 4, null, null },
                    { 79, "Ayşe K.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 1, 21, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 20, 5, null, null },
                    { 80, "Emre Ç.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 2, 19, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 20, 4, null, null },
                    { 81, "Can Ö.", "Fena değil ama fiyatına göre daha iyisini beklerdim.", new DateTime(2026, 2, 3, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 21, 3, null, null },
                    { 82, "Deniz P.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 3, 4, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 21, 5, null, null },
                    { 83, "İrem H.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 4, 2, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 21, 4, null, null },
                    { 84, "Serkan İ.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 5, 1, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 21, 2, null, null },
                    { 85, "Tolga N.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 2, 16, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 22, 2, null, null },
                    { 86, "Buse R.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 3, 17, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 22, 5, null, null },
                    { 87, "Ayşe K.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 4, 15, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 22, 4, null, null },
                    { 88, "Emre Ç.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 5, 14, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 22, 5, null, null },
                    { 89, "Selin Y.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 6, 12, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 22, 5, null, null },
                    { 90, "Onur G.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 7, 11, 19, 0, 0, 0, DateTimeKind.Utc), true, false, 22, 4, null, null },
                    { 91, "Mehmet T.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 3, 1, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 23, 5, null, null },
                    { 92, "Elif D.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 3, 30, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 23, 5, null, null },
                    { 93, "Can Ö.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 4, 28, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 23, 4, null, null },
                    { 94, "Merve B.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 3, 14, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 24, 5, null, null },
                    { 95, "Gizem E.", "Ürün idare eder, iki hafta kullandım şimdilik sorun yok.", new DateTime(2026, 4, 12, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 24, 3, null, null },
                    { 96, "Tolga N.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 5, 11, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 24, 5, null, null },
                    { 97, "Buse R.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 6, 9, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 24, 4, null, null },
                    { 98, "Ayşe K.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 7, 8, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 24, 2, null, null },
                    { 99, "Ece F.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 3, 27, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 25, 4, null, null },
                    { 100, "Hakan V.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 4, 25, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 25, 2, null, null },
                    { 101, "Zeynep A.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 4, 9, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 26, 4, null, null },
                    { 102, "Burak S.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 5, 8, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 26, 5, null, null },
                    { 103, "Merve B.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 6, 6, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 26, 5, null, null },
                    { 104, "Gizem E.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 7, 5, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 26, 4, null, null },
                    { 105, "Onur G.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 4, 22, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 27, 4, null, null },
                    { 106, "Kerem U.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 5, 21, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 27, 5, null, null },
                    { 107, "Ece F.", "Ortalama bir ürün. Kargo hızlıydı, bunun için teşekkürler.", new DateTime(2026, 6, 19, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 27, 3, null, null },
                    { 108, "Hakan V.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 7, 18, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 27, 5, null, null },
                    { 109, "Mehmet T.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 8, 16, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 27, 4, null, null },
                    { 110, "Elif D.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 1, 17, 19, 0, 0, 0, DateTimeKind.Utc), true, true, 27, 2, null, null },
                    { 111, "Serkan İ.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 5, 5, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 28, 5, null, null },
                    { 112, "Nazlı Z.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 6, 3, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 28, 4, null, null },
                    { 113, "Zeynep A.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 7, 2, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 28, 2, null, null },
                    { 114, "Emre Ç.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 5, 18, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 29, 5, null, null },
                    { 115, "Selin Y.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 6, 16, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 29, 4, null, null },
                    { 116, "Onur G.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 7, 15, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 29, 5, null, null },
                    { 117, "Kerem U.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 8, 13, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 29, 5, null, null },
                    { 118, "Ece F.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 1, 14, 17, 0, 0, 0, DateTimeKind.Utc), true, false, 29, 4, null, null },
                    { 119, "Deniz P.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 5, 31, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 30, 5, null, null },
                    { 120, "İrem H.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 6, 29, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 30, 4, null, null },
                    { 121, "Buse R.", "Ürün idare eder, iki hafta kullandım şimdilik sorun yok.", new DateTime(2026, 6, 13, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 31, 3, null, null },
                    { 122, "Ayşe K.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 7, 12, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 31, 5, null, null },
                    { 123, "Emre Ç.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 8, 10, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 31, 4, null, null },
                    { 124, "Selin Y.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 1, 11, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 31, 2, null, null },
                    { 125, "Elif D.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 32, 2, null, null },
                    { 126, "Can Ö.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 7, 25, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 32, 5, null, null },
                    { 127, "Deniz P.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 8, 23, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 32, 4, null, null },
                    { 128, "İrem H.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 1, 24, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 32, 5, null, null },
                    { 129, "Serkan İ.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 2, 22, 17, 0, 0, 0, DateTimeKind.Utc), true, false, 32, 5, null, null },
                    { 130, "Nazlı Z.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 3, 23, 19, 0, 0, 0, DateTimeKind.Utc), true, true, 32, 4, null, null },
                    { 131, "Gizem E.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 7, 9, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 33, 5, null, null },
                    { 132, "Tolga N.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 8, 7, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 33, 5, null, null },
                    { 133, "Buse R.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 1, 8, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 33, 4, null, null },
                    { 134, "Hakan V.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 7, 22, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 34, 5, null, null },
                    { 135, "Mehmet T.", "Ortalama bir ürün. Kargo hızlıydı, bunun için teşekkürler.", new DateTime(2026, 8, 20, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 34, 3, null, null },
                    { 136, "Elif D.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 1, 21, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 34, 5, null, null },
                    { 137, "Can Ö.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 2, 19, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 34, 4, null, null },
                    { 138, "Deniz P.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 3, 20, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 34, 2, null, null },
                    { 139, "Burak S.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 8, 4, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 35, 4, null, null },
                    { 140, "Merve B.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 1, 5, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 35, 2, null, null },
                    { 141, "Kerem U.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 8, 17, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 36, 4, null, null },
                    { 142, "Ece F.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 1, 18, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 36, 5, null, null },
                    { 143, "Hakan V.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 2, 16, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 36, 5, null, null },
                    { 144, "Mehmet T.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 3, 17, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 36, 4, null, null },
                    { 145, "Nazlı Z.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 1, 2, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 37, 4, null, null },
                    { 146, "Zeynep A.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 1, 31, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 37, 5, null, null },
                    { 147, "Burak S.", "Fena değil ama fiyatına göre daha iyisini beklerdim.", new DateTime(2026, 3, 1, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 37, 3, null, null },
                    { 148, "Merve B.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 3, 30, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 37, 5, null, null },
                    { 149, "Gizem E.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 4, 28, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 37, 4, null, null },
                    { 150, "Tolga N.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 5, 27, 19, 0, 0, 0, DateTimeKind.Utc), true, false, 37, 2, null, null },
                    { 151, "Selin Y.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 1, 15, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 38, 5, null, null },
                    { 152, "Onur G.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 2, 13, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 38, 4, null, null },
                    { 153, "Kerem U.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 3, 14, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 38, 2, null, null },
                    { 154, "İrem H.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 1, 28, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 39, 5, null, null },
                    { 155, "Serkan İ.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 2, 26, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 39, 4, null, null },
                    { 156, "Nazlı Z.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 3, 27, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 39, 5, null, null },
                    { 157, "Zeynep A.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 4, 25, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 39, 5, null, null },
                    { 158, "Burak S.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 5, 24, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 39, 4, null, null },
                    { 159, "Ayşe K.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 2, 10, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 40, 5, null, null },
                    { 160, "Emre Ç.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 3, 11, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 40, 4, null, null },
                    { 161, "Can Ö.", "Ortalama bir ürün. Kargo hızlıydı, bunun için teşekkürler.", new DateTime(2026, 2, 23, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 41, 3, null, null },
                    { 162, "Deniz P.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 3, 24, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 41, 5, null, null },
                    { 163, "İrem H.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 4, 22, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 41, 4, null, null },
                    { 164, "Serkan İ.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 5, 21, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 41, 2, null, null },
                    { 165, "Tolga N.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 3, 8, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 42, 2, null, null },
                    { 166, "Buse R.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 4, 6, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 42, 5, null, null },
                    { 167, "Ayşe K.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 5, 5, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 42, 4, null, null },
                    { 168, "Emre Ç.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 6, 3, 15, 0, 0, 0, DateTimeKind.Utc), true, false, 42, 5, null, null },
                    { 169, "Selin Y.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 7, 2, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 42, 5, null, null },
                    { 170, "Onur G.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 7, 31, 19, 0, 0, 0, DateTimeKind.Utc), true, true, 42, 4, null, null },
                    { 171, "Mehmet T.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 3, 21, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 43, 5, null, null },
                    { 172, "Elif D.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 4, 19, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 43, 5, null, null },
                    { 173, "Can Ö.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 5, 18, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 43, 4, null, null },
                    { 174, "Merve B.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 4, 3, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 44, 5, null, null },
                    { 175, "Gizem E.", "Fena değil ama fiyatına göre daha iyisini beklerdim.", new DateTime(2026, 5, 2, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 44, 3, null, null },
                    { 176, "Tolga N.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 5, 31, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 44, 5, null, null },
                    { 177, "Buse R.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 6, 29, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 44, 4, null, null },
                    { 178, "Ayşe K.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 7, 28, 17, 0, 0, 0, DateTimeKind.Utc), true, false, 44, 2, null, null },
                    { 179, "Ece F.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 4, 16, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 45, 4, null, null },
                    { 180, "Hakan V.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 5, 15, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 45, 2, null, null },
                    { 181, "Zeynep A.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 4, 29, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 46, 4, null, null },
                    { 182, "Burak S.", "Uzun süredir böyle bir ürün arıyordum, tam aradığım gibi çıktı.", new DateTime(2026, 5, 28, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 46, 5, null, null },
                    { 183, "Merve B.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 6, 26, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 46, 5, null, null },
                    { 184, "Gizem E.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 7, 25, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 46, 4, null, null },
                    { 185, "Onur G.", "Güzel ürün, kullanımı kolay. Kutusu biraz ezik geldi ama ürüne bir şey olmamış.", new DateTime(2026, 5, 12, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 47, 4, null, null },
                    { 186, "Kerem U.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 6, 10, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 47, 5, null, null },
                    { 187, "Ece F.", "Ürün idare eder, iki hafta kullandım şimdilik sorun yok.", new DateTime(2026, 7, 9, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 47, 3, null, null },
                    { 188, "Hakan V.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 8, 7, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 47, 5, null, null },
                    { 189, "Mehmet T.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 1, 8, 17, 0, 0, 0, DateTimeKind.Utc), true, false, 47, 4, null, null },
                    { 190, "Elif D.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 2, 6, 19, 0, 0, 0, DateTimeKind.Utc), true, true, 47, 2, null, null },
                    { 191, "Serkan İ.", "Beklentimin çok üzerinde, kalitesi fotoğraflardakinden bile iyi. Kesinlikle tavsiye ederim.", new DateTime(2026, 5, 25, 9, 0, 0, 0, DateTimeKind.Utc), true, false, 48, 5, null, null },
                    { 192, "Nazlı Z.", "Kullanışlı ve sağlam görünüyor, birkaç ay kullandıktan sonra tekrar yorum yapacağım.", new DateTime(2026, 6, 23, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 48, 4, null, null },
                    { 193, "Zeynep A.", "Beklediğim kalitede çıkmadı, açıklamayı biraz abartılı buldum.", new DateTime(2026, 7, 22, 13, 0, 0, 0, DateTimeKind.Utc), true, true, 48, 2, null, null },
                    { 194, "Emre Ç.", "Siparişin ertesi günü elimdeydi, paketleme özenliydi. Ürün kusursuz.", new DateTime(2026, 6, 7, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 49, 5, null, null },
                    { 195, "Selin Y.", "Genel olarak memnunum, kargo bir gün gecikti ama ürün sağlam geldi.", new DateTime(2026, 7, 6, 11, 0, 0, 0, DateTimeKind.Utc), true, true, 49, 4, null, null },
                    { 196, "Onur G.", "İkinci kez alıyorum, bu sefer hediye olarak. Herkes çok beğendi.", new DateTime(2026, 8, 4, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 49, 5, null, null },
                    { 197, "Kerem U.", "Açıklamada yazan her şey doğru, malzeme kalitesi gayet iyi. Teşekkürler Jona!", new DateTime(2026, 1, 5, 15, 0, 0, 0, DateTimeKind.Utc), true, true, 49, 5, null, null },
                    { 198, "Ece F.", "İşimi görüyor, beklediğim gibi. Renk fotoğraftakinden bir ton koyu.", new DateTime(2026, 2, 3, 17, 0, 0, 0, DateTimeKind.Utc), true, true, 49, 4, null, null },
                    { 199, "Deniz P.", "Fiyat/performans olarak alabileceğiniz en iyi seçeneklerden biri. Çok memnunum.", new DateTime(2026, 6, 20, 9, 0, 0, 0, DateTimeKind.Utc), true, true, 50, 5, null, null },
                    { 200, "İrem H.", "Kaliteli bir ürün, fiyatı biraz daha uygun olsa beş yıldız verirdim.", new DateTime(2026, 7, 19, 11, 0, 0, 0, DateTimeKind.Utc), true, false, 50, 4, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId_UserId",
                table: "Reviews",
                columns: new[] { "ProductId", "UserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "Specifications",
                table: "Products");
        }
    }
}
