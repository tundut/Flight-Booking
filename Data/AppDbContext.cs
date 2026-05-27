using Microsoft.EntityFrameworkCore;
using FlightBooking.Models;

namespace FlightBooking.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<User> Users { get; set; }
}