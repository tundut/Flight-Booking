using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlightBooking.DTOs.Payment;

public class CreatePaymentDto
{
    [Required]
    [DefaultValue(1)]
    public int BookingId { get; set; }

    [Required]
    [DefaultValue("Credit Card")]
    public string PaymentMethod { get; set; } = null!;
}