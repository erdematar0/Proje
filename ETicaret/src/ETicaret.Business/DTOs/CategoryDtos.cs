namespace ETicaret.Business.DTOs;

public record CategoryDto(int Id, string Name, string? Description, int ProductCount);

public class CategorySaveDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
