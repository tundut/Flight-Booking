using System.ComponentModel.DataAnnotations.Schema;
using FlightBooking.Enums;

namespace FlightBooking.Models;

[Table("bookings")]
public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;
    public int Seats { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    public Payment Payment { get; set; } = null!;
}