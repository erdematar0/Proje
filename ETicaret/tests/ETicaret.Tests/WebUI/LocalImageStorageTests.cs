using ETicaret.WebUI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace ETicaret.Tests.WebUI;

public class LocalImageStorageTests : IDisposable
{
    private static readonly byte[] PngHeader = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0, 0, 0, 0];
    private static readonly byte[] JpgHeader = [0xFF, 0xD8, 0xFF, 0xE0, 0, 0, 0, 0, 0, 0, 0, 0];
    private static readonly byte[] WebpHeader = "RIFF\0\0\0\0WEBP"u8.ToArray();

    private readonly string _root = Path.Combine(Path.GetTempPath(), "eticaret-tests-" + Guid.NewGuid());
    private readonly LocalImageStorage _sut;

    public LocalImageStorageTests() => _sut = new LocalImageStorage(new TestEnv(_root));

    public void Dispose()
    {
        if (Directory.Exists(_root)) Directory.Delete(_root, recursive: true);
    }

    private static IFormFile File(string name, byte[] header, int totalSize = 0)
    {
        var bytes = new byte[Math.Max(header.Length, totalSize)];
        header.CopyTo(bytes, 0);
        return new FormFile(new MemoryStream(bytes), 0, bytes.Length, "imageFile", name);
    }

    private string DiskPath(string url) => Path.Combine(_root, "uploads", "products", Path.GetFileName(url));

    [Theory]
    [InlineData("urun.png")]
    [InlineData("URUN.PNG")]
    public async Task SaveAsync_ValidPng_SavesWithRandomName(string name)
    {
        var result = await _sut.SaveAsync(File(name, PngHeader));

        Assert.True(result.Success, result.Message);
        Assert.StartsWith(LocalImageStorage.UrlPrefix, result.Data);
        Assert.EndsWith(".png", result.Data);
        Assert.DoesNotContain("urun", result.Data, StringComparison.OrdinalIgnoreCase);
        Assert.True(System.IO.File.Exists(DiskPath(result.Data!)));
    }

    [Theory]
    [InlineData("a.jpg")]
    [InlineData("a.jpeg")]
    public async Task SaveAsync_ValidJpg_Succeeds(string name)
        => Assert.True((await _sut.SaveAsync(File(name, JpgHeader))).Success);

    [Fact]
    public async Task SaveAsync_ValidWebp_Succeeds()
        => Assert.True((await _sut.SaveAsync(File("a.webp", WebpHeader))).Success);

    [Fact]
    public async Task SaveAsync_TooLarge_Fails()
    {
        var result = await _sut.SaveAsync(File("a.png", PngHeader, (int)LocalImageStorage.MaxBytes + 1));
        Assert.False(result.Success);
        Assert.Equal("Görsel en fazla 2 MB olabilir.", result.Message);
    }

    [Theory]
    [InlineData("script.exe")]
    [InlineData("sayfa.html")]
    [InlineData("vektor.svg")]
    [InlineData("dosya")]
    public async Task SaveAsync_DisallowedExtension_Fails(string name)
        => Assert.False((await _sut.SaveAsync(File(name, PngHeader))).Success);

    [Fact]
    public async Task SaveAsync_ExtensionDoesNotMatchContent_Fails()
    {
        var result = await _sut.SaveAsync(File("sahte.png", "<html>not an image"u8.ToArray()));

        Assert.False(result.Success);
        Assert.Equal("Dosya içeriği geçerli bir görsel değil.", result.Message);
    }

    [Fact]
    public async Task SaveAsync_EmptyFile_Fails()
        => Assert.False((await _sut.SaveAsync(File("a.png", []))).Success);

    [Fact]
    public async Task Delete_RemovesUploadedFile()
    {
        var url = (await _sut.SaveAsync(File("a.png", PngHeader))).Data!;

        _sut.Delete(url);

        Assert.False(System.IO.File.Exists(DiskPath(url)));
    }

    [Theory]
    [InlineData("https://picsum.photos/seed/x/600/450")]
    [InlineData("/uploads/products/../../appsettings.json")]
    [InlineData(null)]
    public void Delete_IgnoresExternalOrSuspiciousUrls(string? url)
    {
        var outside = Path.Combine(_root, "appsettings.json");
        Directory.CreateDirectory(_root);
        System.IO.File.WriteAllText(outside, "{}");

        _sut.Delete(url);

        Assert.True(System.IO.File.Exists(outside));
    }

    private sealed class TestEnv(string root) : IWebHostEnvironment
    {
        public string WebRootPath { get; set; } = root;
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public string ApplicationName { get; set; } = "ETicaret.Tests";
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
        public string ContentRootPath { get; set; } = root;
        public string EnvironmentName { get; set; } = "Test";
    }
}
