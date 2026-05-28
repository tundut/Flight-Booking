namespace FlightBooking.DTOs.Payment;
using FlightBooking.Enums;

public class PaymentResponseDto
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }
}