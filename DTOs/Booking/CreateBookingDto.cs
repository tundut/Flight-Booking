using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlightBooking.DTOs.Booking;

public class CreateBookingDto
{
    [Required]
    [DefaultValue(1)]
    public int FlightId { get; set; }

    [Required]
    [DefaultValue(1)]
    public int Seats { get; set; }
}