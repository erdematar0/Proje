using ETicaret.Business.Common;

namespace ETicaret.WebUI.Services;

public interface IImageStorage
{
    Task<ServiceResult<string>> SaveAsync(IFormFile file, CancellationToken ct = default);

    void Delete(string? url);
}
