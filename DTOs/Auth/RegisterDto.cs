using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlightBooking.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [DefaultValue("Test")]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [DefaultValue("test@gmail.com")]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    [DefaultValue("123456")]
    public string Password { get; set; }
}