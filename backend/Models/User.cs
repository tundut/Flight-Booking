using System.ComponentModel.DataAnnotations.Schema;
using FlightBooking.Enums;

namespace FlightBooking.Models;

[Table("users")]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; }

    public List<Booking> Bookings { get; set; } = null!;
}