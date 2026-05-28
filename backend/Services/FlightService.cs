using Microsoft.EntityFrameworkCore;
using FlightBooking.Models;
using FlightBooking.Data;
using FlightBooking.DTOs.Flight;

namespace FlightBooking.Services;

public class FlightService
{
    private readonly AppDbContext _context;
    public FlightService(AppDbContext context)
    {
        _context = context;
    }
    private static FlightResponseDto ToFlightDto(Flight flight) => new()
    {
        Id = flight.Id,
        FlightNumber = flight.FlightNumber,
        From = flight.From,
        To = flight.To,
        DepartureTime = flight.DepartureTime,
        ArrivalTime = flight.ArrivalTime,
        Price = flight.Price,
        TotalSeats = flight.TotalSeats,
        AvailableSeats = flight.AvailableSeats
    };
    public async Task<List<FlightResponseDto>> GetAll()
    {
        var flights = await _context.Flights.ToListAsync();
        return flights.Select(ToFlightDto).ToList();
    }
    public async Task<FlightResponseDto?> GetById(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        return flight == null ? null : ToFlightDto(flight);
    }
    public async Task<FlightResponseDto> Create(CreateFlightDto dto)
    {
        var exists = await _context.Flights.AnyAsync(f => f.FlightNumber == dto.FlightNumber);

        if (exists)
        {
            throw new InvalidOperationException("Flight number already exists");
        }

        var flight = new Flight
        {
            FlightNumber = dto.FlightNumber,
            From = dto.From,
            To = dto.To,
            DepartureTime = dto.DepartureTime,
            ArrivalTime = dto.ArrivalTime,
            Price = dto.Price,
            TotalSeats = dto.TotalSeats,
            AvailableSeats = dto.AvailableSeats
        };

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        return ToFlightDto(flight);
    }
    public async Task<bool> Delete(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight == null)
        {
            return false;
        }
        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<FlightResponseDto>> Search(string from, string to)
    {
        var flights = await _context.Flights.Where(f => f.From == from && f.To == to).ToListAsync();
        return flights.Select(ToFlightDto).ToList();
    }
}
