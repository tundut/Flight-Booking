using Microsoft.EntityFrameworkCore;
using FlightBooking.Models;
using FlightBooking.Data;
using FlightBooking.DTOs.Payment;
using FlightBooking.Enums;

namespace FlightBooking.Services;

public class PaymentService
{
    private readonly AppDbContext _context;
    public PaymentService(AppDbContext context)
    {
        _context = context;
    }
    private static PaymentResponseDto ToPaymentDto(Payment payment) => new()
    {
        Id = payment.Id,
        BookingId = payment.BookingId,
        Amount = payment.Amount,
        PaymentMethod = payment.PaymentMethod,
        PaymentDate = payment.PaymentDate,
        Status = payment.Status
    };
    public async Task<PaymentResponseDto?> GetById(int id)
    {
        var payment = await _context.Payments
            .Include(p => p.Booking)
            .ThenInclude(b => b.Flight)
            .FirstOrDefaultAsync(p => p.Id == id);
        return payment == null ? null : ToPaymentDto(payment);
    }

    public async Task<PaymentResponseDto?> GetByBookingId(int bookingId)
    {
        var payment = await _context.Payments
            .Include(p => p.Booking)
            .FirstOrDefaultAsync(p => p.BookingId == bookingId);
        return payment == null ? null : ToPaymentDto(payment);
    }

    public async Task<PaymentResponseDto> Create(int userId, CreatePaymentDto dto)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == dto.BookingId && b.UserId == userId);

        if (booking == null)
        {
            throw new KeyNotFoundException("Booking not found");
        }

        if (booking.Status == BookingStatus.Cancelled)
        {
            throw new InvalidOperationException("Booking has been cancelled");
        }

        if (booking.Status == BookingStatus.Confirmed)
        {
            throw new InvalidOperationException("Booking already confirmed");
        }

        var existPayment = await _context.Payments.AnyAsync(p => p.BookingId == dto.BookingId);
        if (existPayment)
        {
            throw new InvalidOperationException("Payment for this booking already exists");
        }

        var payment = new Payment
        {
            BookingId = dto.BookingId,
            Amount = booking.TotalPrice,
            PaymentMethod = dto.PaymentMethod,
            PaymentDate = DateTime.UtcNow,
            Status = PaymentStatus.Paid
        };

        _context.Payments.Add(payment);
        booking.Status = BookingStatus.Confirmed;
        await _context.SaveChangesAsync();

        return ToPaymentDto(payment);
    }
}