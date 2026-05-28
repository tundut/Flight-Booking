using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightBooking.Models;
using FlightBooking.Services;
using FlightBooking.DTOs.Payment;

namespace FlightBooking.Controllers;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreatePaymentDto dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        try
        {
            var payment = await _paymentService.Create(userId, dto);
            return Ok(payment);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });

        }
    }

    [Authorize]
    [HttpGet("booking/{bookingId}")]
    public async Task<IActionResult> GetByBooking(int bookingId)
    {
        var payment = await _paymentService.GetByBookingId(bookingId);
        if (payment == null)
        {
            return NotFound();
        }
        return Ok(payment);
    }
}