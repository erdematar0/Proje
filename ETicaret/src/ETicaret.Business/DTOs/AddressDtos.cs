namespace ETicaret.Business.DTOs;

public record AddressDto(int Id, string Title, string FullName, string Phone, string City,
                         string? District, string AddressLine, bool IsDefault);

public class AddressSaveDto
{
    public string Title { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? District { get; set; }
    public string AddressLine { get; set; } = null!;
    public bool IsDefault { get; set; }
}
