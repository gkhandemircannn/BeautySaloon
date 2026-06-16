using LumiBeauty.Api.Data;
using LumiBeauty.Api.DTOs;
using LumiBeauty.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiBeauty.Api.Controllers;

[ApiController]
[Route("api/specialists")]
public class SpecialistsController : ControllerBase
{
    private static readonly HashSet<string> Allowed = new(StringComparer.OrdinalIgnoreCase) { "skin", "nails", "brows", "body", "hair", "makeup" };
    private readonly AppDbContext _db;
    public SpecialistsController(AppDbContext db) { _db = db; }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? category)
    {
        var list = await _db.Specialists.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Name).ToListAsync();
        if (!string.IsNullOrWhiteSpace(category)) list = list.Where(x => x.SupportsCategory(category)).ToList();
        return Ok(list.Select(Map));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public async Task<IActionResult> GetAdmin() => Ok((await _db.Specialists.AsNoTracking().OrderBy(x => x.Name).ToListAsync()).Select(Map));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(UpsertSpecialistRequest r)
    {
        var error = Validate(r); if (error is not null) return BadRequest(error);
        var x = new Specialist { Name = r.Name.Trim(), Specialty = r.Specialty.Trim(), Rating = r.Rating, SupportedCategories = Normalize(r.SupportedCategories), IsActive = r.IsActive };
        _db.Specialists.Add(x); await _db.SaveChangesAsync(); return Ok(Map(x));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpsertSpecialistRequest r)
    {
        var error = Validate(r); if (error is not null) return BadRequest(error);
        var x = await _db.Specialists.FindAsync(id); if (x is null) return NotFound("Uzman bulunamadı.");
        x.Name = r.Name.Trim(); x.Specialty = r.Specialty.Trim(); x.Rating = r.Rating; x.SupportedCategories = Normalize(r.SupportedCategories); x.IsActive = r.IsActive;
        await _db.SaveChangesAsync(); return Ok(Map(x));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        var x = await _db.Specialists.FindAsync(id); if (x is null) return NotFound("Uzman bulunamadı.");
        x.IsActive = false; await _db.SaveChangesAsync(); return Ok(new { message = "Uzman kaldırıldı." });
    }

    private static string? Validate(UpsertSpecialistRequest r)
    {
        if (string.IsNullOrWhiteSpace(r.Name)) return "Uzman adı zorunludur.";
        if (string.IsNullOrWhiteSpace(r.Specialty)) return "Uzmanlık açıklaması zorunludur.";
        if (r.Rating < 0 || r.Rating > 5) return "Puan 0 ile 5 arasında olmalıdır.";
        var cats = r.SupportedCategories.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        if (cats.Count == 0) return "En az bir hizmet kategorisi seçin.";
        if (cats.Any(x => !Allowed.Contains(x))) return "Geçersiz hizmet kategorisi.";
        return null;
    }
    private static string Normalize(IEnumerable<string> cats) => string.Join(",", cats.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim().ToLowerInvariant()).Distinct());
    private static object Map(Specialist x) => new { x.Id, x.Name, x.Specialty, x.Rating, supportedCategories = x.SupportedCategories.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()), x.IsActive };
}
