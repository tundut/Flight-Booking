using System.ComponentModel.DataAnnotations.Schema;
using FlightBooking.Enums;

namespace FlightBooking.Models;

[Table("payments")]
public class Payment
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public PaymentStatus Status { get; set; }
}