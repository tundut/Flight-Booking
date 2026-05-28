using FlightBooking.DTOs.Flight;
using FlightBooking.DTOs.Payment;
using FlightBooking.Enums;

namespace FlightBooking.DTOs.Booking;

public class BookingResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FlightId { get; set; }
    public int Seats { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime BookingDate { get; set; }

    public FlightResponseDto? Flight { get; set; }
    public PaymentResponseDto? Payment { get; set; }
}