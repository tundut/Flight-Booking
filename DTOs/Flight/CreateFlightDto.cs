using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlightBooking.DTOs.Flight;

public class CreateFlightDto
{
    [Required]
    [DefaultValue("AB123")]
    public string FlightNumber { get; set; } = null!;

    [Required]
    [DefaultValue("New York")]
    public string From { get; set; } = null!;

    [Required]
    [DefaultValue("Los Angeles")]
    public string To { get; set; } = null!;

    [Required]
    public DateTime DepartureTime { get; set; }

    [Required]
    public DateTime ArrivalTime { get; set; }

    [Required]
    [DefaultValue(100)]
    public decimal Price { get; set; }

    [Required]
    [DefaultValue(100)]
    public int TotalSeats { get; set; }

    [Required]
    [DefaultValue(100)]
    public int AvailableSeats { get; set; }
}