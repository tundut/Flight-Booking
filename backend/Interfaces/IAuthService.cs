using FlightBooking.DTOs.Auth;

namespace FlightBooking.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
}