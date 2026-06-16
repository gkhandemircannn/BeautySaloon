namespace LumiBeauty.Api.Models;

public class Specialist
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public decimal Rating { get; set; } = 5.0m;
    public string SupportedCategories { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public bool SupportsCategory(string category)
    {
        return SupportedCategories
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(item => item.Trim())
            .Any(item => string.Equals(item, category.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}
