namespace FlightBooking.DTOs.Flight;

public class FlightResponseDto
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = null!;
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal Price { get; set; }
    public int TotalSeats { get; set; }
    public int AvailableSeats { get; set; }
}