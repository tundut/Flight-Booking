using Microsoft.EntityFrameworkCore;
using FlightBooking.Models;
using FlightBooking.Data;

namespace FlightBooking.Services;

public class FlightService
{
    private readonly AppDbContext _context;
    public FlightService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Flight>> GetAll()
    {
        return await _context.Flights.ToListAsync();
    }
    public async Task<Flight?> GetById(int id)
    {
        return await _context.Flights.FindAsync(id);
    }
    public async Task<Flight> Create(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        return flight;
    }
    public async Task<bool> Delete(int id)
    {
        var flight = await GetById(id);
        if (flight == null)
        {
            return false;
        }
        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<Flight>> Search(string from, string to)
    {
        return await _context.Flights.Where(f => f.From == from && f.To == to).ToListAsync();
    }
}
