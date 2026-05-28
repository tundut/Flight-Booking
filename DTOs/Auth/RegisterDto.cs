using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlightBooking.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [DefaultValue("Test")]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    [DefaultValue("test@gmail.com")]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6)]
    [DefaultValue("123456")]
    public string Password { get; set; } = null!;
}