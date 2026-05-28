using Microsoft.EntityFrameworkCore;
using FlightBooking.Models;
using FlightBooking.Data;
using FlightBooking.DTOs.Booking;
using FlightBooking.DTOs.Flight;
using FlightBooking.DTOs.Payment;
using FlightBooking.Enums;

namespace FlightBooking.Services;

public class BookingService
{
    private readonly AppDbContext _context;
    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    private static BookingResponseDto ToBookingDto(Booking booking) => new()
    {
        Id = booking.Id,
        UserId = booking.UserId,
        FlightId = booking.FlightId,
        Seats = booking.Seats,
        TotalPrice = booking.TotalPrice,
        Status = booking.Status,
        BookingDate = booking.BookingDate,
        Flight = booking.Flight == null ? null : new FlightResponseDto
        {
            Id = booking.Flight.Id,
            FlightNumber = booking.Flight.FlightNumber,
            From = booking.Flight.From,
            To = booking.Flight.To,
            DepartureTime = booking.Flight.DepartureTime,
            ArrivalTime = booking.Flight.ArrivalTime,
            Price = booking.Flight.Price,
            TotalSeats = booking.Flight.TotalSeats,
            AvailableSeats = booking.Flight.AvailableSeats
        },
        Payment = booking.Payment == null ? null : new PaymentResponseDto
        {
            Id = booking.Payment.Id,
            BookingId = booking.Payment.BookingId,
            Amount = booking.Payment.Amount,
            PaymentMethod = booking.Payment.PaymentMethod,
            PaymentDate = booking.Payment.PaymentDate,
            Status = booking.Payment.Status
        }
    };

    public async Task<List<BookingResponseDto>> GetAll()
    {
        var bookings = await _context.Bookings
            .Include(b => b.Flight)
            .Include(b => b.Payment)
            .ToListAsync();

        return bookings.Select(ToBookingDto).ToList();
    }

    public async Task<List<BookingResponseDto>> GetByUserId(int userId)
    {
        var bookings = await _context.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.Flight)
            .Include(b => b.Payment)
            .ToListAsync();

        return bookings.Select(ToBookingDto).ToList();
    }

    public async Task<BookingResponseDto?> GetById(int id, int userId)
    {
        var booking = await _context.Bookings
            .Include(b => b.Flight)
            .Include(b => b.Payment)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

        return booking == null ? null : ToBookingDto(booking);
    }

    public async Task<BookingResponseDto> Create(int userId, CreateBookingDto dto)
    {
        var flight = await _context.Flights.FindAsync(dto.FlightId);
        if (flight == null)
        {
            throw new KeyNotFoundException("Flight not found");
        }

        if (dto.Seats <= 0)
        {
            throw new ArgumentException("Seats must be greater than 0");
        }

        var booking = new Booking
        {
            UserId = userId,
            FlightId = dto.FlightId,
            Seats = dto.Seats,
            TotalPrice = flight.Price * dto.Seats,
            Status = BookingStatus.Pending,
            BookingDate = DateTime.UtcNow,
        };
        _context.Bookings.Add(booking);
        flight.AvailableSeats -= dto.Seats;
        await _context.SaveChangesAsync();

        return ToBookingDto(booking);
    }

    public async Task<bool> Delete(int id, int userId)
    {
        var booking = await _context.Bookings
            .Include(b => b.Flight)
            .Include(b => b.Payment)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

        if (booking == null || booking.Status == BookingStatus.Cancelled)
        {
            return false;
        }
        booking.Status = BookingStatus.Cancelled;

        if (booking.Payment != null && booking.Payment.Status == PaymentStatus.Paid)
        {
            booking.Payment.Status = PaymentStatus.Refunded;
        }

        booking.Flight.AvailableSeats += booking.Seats;

        await _context.SaveChangesAsync();
        return true;
    }
}