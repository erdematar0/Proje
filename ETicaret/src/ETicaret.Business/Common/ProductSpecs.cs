namespace ETicaret.Business.Common;

public record ProductSpec(string Name, string Value);

public static class ProductSpecs
{
    public static List<ProductSpec> Parse(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return [];

        return text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                   .Select(line =>
                   {
                       var i = line.IndexOf(':');
                       return i > 0
                           ? new ProductSpec(line[..i].Trim(), line[(i + 1)..].Trim())
                           : new ProductSpec("", line);
                   })
                   .ToList();
    }
}
