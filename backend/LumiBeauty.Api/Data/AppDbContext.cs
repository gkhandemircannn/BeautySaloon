using LumiBeauty.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LumiBeauty.Api.Data;

public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BusinessSettings> BusinessSettings => Set<BusinessSettings>();
    public DbSet<Specialist> Specialists => Set<Specialist>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Booking>().Property(x => x.ServicePrice).HasPrecision(10, 2);
        modelBuilder.Entity<Booking>().Property(x => x.Status).HasDefaultValue("Pending");
        modelBuilder.Entity<Booking>().HasIndex(x => new { x.SpecialistId, x.AppointmentDate });
        modelBuilder.Entity<Specialist>().Property(x => x.Rating).HasPrecision(2, 1);
        modelBuilder.Entity<BusinessSettings>().HasData(new BusinessSettings { Id = 1, OpeningTime = new TimeOnly(9, 0), LastAppointmentTime = new TimeOnly(20, 0), SlotIntervalMinutes = 60 });
        modelBuilder.Entity<Specialist>().HasData(
            new Specialist { Id = 1, Name = "Elif Yılmaz", Specialty = "Cilt bakımı uzmanı", Rating = 4.9m, SupportedCategories = "skin", IsActive = true },
            new Specialist { Id = 2, Name = "Derya Kaya", Specialty = "Nail artist", Rating = 4.8m, SupportedCategories = "nails", IsActive = true },
            new Specialist { Id = 3, Name = "Melis Aksoy", Specialty = "Kaş, kirpik ve kalıcı makyaj uzmanı", Rating = 4.9m, SupportedCategories = "brows,makeup", IsActive = true },
            new Specialist { Id = 4, Name = "Selin Demir", Specialty = "Vücut bakımı ve epilasyon uzmanı", Rating = 4.7m, SupportedCategories = "body", IsActive = true },
            new Specialist { Id = 5, Name = "Ceren Arslan", Specialty = "Saç bakım ve şekillendirme uzmanı", Rating = 4.8m, SupportedCategories = "hair", IsActive = true }
        );
    }
}
