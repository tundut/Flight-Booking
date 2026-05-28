using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightBooking.Models;
using FlightBooking.Services;
using FlightBooking.DTOs.Booking;

namespace FlightBooking.Controllers;

[ApiController]
[Route("api/booking")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // GET my bookings
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyBookings()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var bookings = await _bookingService.GetByUserId(userId);
        return Ok(bookings);
    }

    // GET by id
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var booking = await _bookingService.GetById(id, userId);

        if (booking == null)
        {
            return NotFound();
        }

        return Ok(booking);
    }

    // POST
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingDto dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        try
        {
            var booking = await _bookingService.Create(userId, dto);
            return Ok(booking);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var deleted = await _bookingService.Delete(id, userId);
        if (!deleted)
        {
            return NotFound();
        }
        return Ok("Deleted");
    }
}
