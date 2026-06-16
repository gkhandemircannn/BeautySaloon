namespace LumiBeauty.Api.DTOs;

public class CreateBookingRequest
{
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceCategory { get; set; } = string.Empty;
    public decimal ServicePrice { get; set; }
    public int DurationMinutes { get; set; }
    public int SpecialistId { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentTime { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string? CustomerNote { get; set; }
}
