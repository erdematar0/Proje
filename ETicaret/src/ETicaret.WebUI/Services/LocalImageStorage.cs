using ETicaret.Business.Common;
using ETicaret.Business.Validation;

namespace ETicaret.WebUI.Services;

public class LocalImageStorage : IImageStorage
{
    public const long MaxBytes = 2 * 1024 * 1024;
    public const string UrlPrefix = ProductSaveDtoValidator.UploadsPrefix + "products/";

    private static readonly Dictionary<string, Func<byte[], bool>> Signatures = new(StringComparer.OrdinalIgnoreCase)
    {
        [".jpg"] = h => h is [0xFF, 0xD8, 0xFF, ..],
        [".jpeg"] = h => h is [0xFF, 0xD8, 0xFF, ..],
        [".png"] = h => h is [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, ..],
        [".webp"] = h => h is [0x52, 0x49, 0x46, 0x46, _, _, _, _, 0x57, 0x45, 0x42, 0x50, ..]
    };

    private readonly string _folder;

    public LocalImageStorage(IWebHostEnvironment env)
        => _folder = Path.Combine(env.WebRootPath, "uploads", "products");

    public async Task<ServiceResult<string>> SaveAsync(IFormFile file, CancellationToken ct = default)
    {
        if (file.Length == 0) return ServiceResult<string>.Fail("Seçilen dosya boş.");
        if (file.Length > MaxBytes) return ServiceResult<string>.Fail("Görsel en fazla 2 MB olabilir.");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!Signatures.TryGetValue(ext, out var matchesSignature))
            return ServiceResult<string>.Fail("Sadece JPG, PNG veya WEBP yükleyebilirsiniz.");

        var header = new byte[12];
        int read;
        await using (var s = file.OpenReadStream())
            read = await s.ReadAtLeastAsync(header, header.Length, throwOnEndOfStream: false, ct);
        if (!matchesSignature(header[..read]))
            return ServiceResult<string>.Fail("Dosya içeriği geçerli bir görsel değil.");

        var fileName = $"{Guid.NewGuid():N}{ext}";
        Directory.CreateDirectory(_folder);
        await using (var target = File.Create(Path.Combine(_folder, fileName)))
            await file.CopyToAsync(target, ct);

        return ServiceResult<string>.Ok(UrlPrefix + fileName);
    }

    public void Delete(string? url)
    {
        if (url is null || !url.StartsWith(UrlPrefix, StringComparison.Ordinal)) return;

        var fileName = url[UrlPrefix.Length..];
        if (fileName != Path.GetFileName(fileName)) return;

        var path = Path.Combine(_folder, fileName);
        if (File.Exists(path)) File.Delete(path);
    }
}
