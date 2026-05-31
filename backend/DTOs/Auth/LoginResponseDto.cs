namespace FlightBooking.DTOs.Auth;

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? User { get; set; }
    public string? Message { get; set; }
}