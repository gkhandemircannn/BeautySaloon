using LumiBeauty.Api.Data;
using LumiBeauty.Api.DTOs;
using LumiBeauty.Api.Models;
using LumiBeauty.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiBeauty.Api.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db; private readonly ISmsSender _sms;
    public BookingsController(AppDbContext db, ISmsSender sms) { _db = db; _sms = sms; }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingRequest r)
    {
        if (string.IsNullOrWhiteSpace(r.ServiceName) || string.IsNullOrWhiteSpace(r.ServiceCategory) || string.IsNullOrWhiteSpace(r.CustomerName) || string.IsNullOrWhiteSpace(r.CustomerPhone) || r.ServicePrice <= 0 || r.DurationMinutes <= 0) return BadRequest("Eksik veya geçersiz bilgi.");
        var specialist = await _db.Specialists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == r.SpecialistId && x.IsActive);
        if (specialist is null) return BadRequest("Seçilen uzman aktif değil.");
        if (!specialist.SupportsCategory(r.ServiceCategory)) return BadRequest("Seçilen uzman bu hizmeti vermiyor.");
        var items = await _db.Bookings.AsNoTracking().Where(x => x.Status != "Cancelled" && x.SpecialistId == specialist.Id && x.AppointmentDate == r.AppointmentDate).ToListAsync();
        var start = r.AppointmentTime.ToTimeSpan(); var end = start.Add(TimeSpan.FromMinutes(r.DurationMinutes));
        if (items.Any(x => start < x.AppointmentTime.ToTimeSpan().Add(TimeSpan.FromMinutes(x.DurationMinutes)) && x.AppointmentTime.ToTimeSpan() < end)) return Conflict("Seçilen saat dolu.");
        var b = new Booking { ServiceName = r.ServiceName.Trim(), ServiceCategory = r.ServiceCategory.Trim(), ServicePrice = r.ServicePrice, DurationMinutes = r.DurationMinutes, SpecialistId = specialist.Id, SpecialistName = specialist.Name, AppointmentDate = r.AppointmentDate, AppointmentTime = r.AppointmentTime, CustomerName = r.CustomerName.Trim(), CustomerPhone = r.CustomerPhone.Trim(), CustomerNote = r.CustomerNote?.Trim(), Status = "Pending" };
        _db.Bookings.Add(b); await _db.SaveChangesAsync(); return Ok(b);
    }

    [HttpGet("booked-times")]
    public async Task<IActionResult> Times([FromQuery] int specialistId, [FromQuery] DateOnly appointmentDate) => Ok(await _db.Bookings.AsNoTracking().Where(x => x.Status != "Cancelled" && x.SpecialistId == specialistId && x.AppointmentDate == appointmentDate).Select(x => new { appointmentTime = x.AppointmentTime.ToString("HH:mm"), x.DurationMinutes }).ToListAsync());

    [Authorize(Roles = "Admin")][HttpGet]
    public async Task<IActionResult> Get() => Ok(await _db.Bookings.AsNoTracking().OrderByDescending(x => x.AppointmentDate).ThenBy(x => x.AppointmentTime).ToListAsync());

    [Authorize(Roles = "Admin")][HttpPatch("{id:int}/confirm")]
    public async Task<IActionResult> Confirm(int id) { var b = await _db.Bookings.FindAsync(id); if (b is null) return NotFound(); if (b.Status == "Cancelled") return Conflict("İptal edilmiş randevu onaylanamaz."); if (b.Status != "Confirmed") { b.Status = "Confirmed"; b.ConfirmedAt = DateTime.UtcNow; await _db.SaveChangesAsync(); await _sms.SendConfirmationAsync(b); b.ConfirmationSmsSentAt = DateTime.UtcNow; await _db.SaveChangesAsync(); } return Ok(new { message = "Onaylandı." }); }

    [Authorize(Roles = "Admin")][HttpPatch("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id) { var b = await _db.Bookings.FindAsync(id); if (b is null) return NotFound(); b.Status = "Cancelled"; b.CancelledAt = DateTime.UtcNow; await _db.SaveChangesAsync(); return Ok(new { message = "İptal edildi." }); }
}
