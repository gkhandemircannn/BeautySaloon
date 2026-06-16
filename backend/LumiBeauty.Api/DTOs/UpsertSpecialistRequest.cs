namespace LumiBeauty.Api.DTOs;

public class UpsertSpecialistRequest
{
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public decimal Rating { get; set; } = 5.0m;
    public List<string> SupportedCategories { get; set; } = [];
    public bool IsActive { get; set; } = true;
}
