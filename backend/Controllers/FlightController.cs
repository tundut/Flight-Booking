using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightBooking.Models;
using FlightBooking.Services;
using FlightBooking.DTOs.Flight;

namespace FlightBooking.Controllers;

[ApiController]
[Route("api/flight")]
public class FlightController : ControllerBase
{
    private readonly FlightService _service;
    public FlightController(FlightService service)
    {
        _service = service;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var flights = await _service.GetAll();
        return Ok(flights);
    }

    // GET by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var flight = await _service.GetById(id);
        if (flight == null)
        {
            return NotFound();
        }
        return Ok(flight);
    }

    // POST
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateFlightDto dto)
    {
        try
        {
            var flight = await _service.Create(dto);
            return Ok(flight);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok("Deleted");
    }

    // SEARCH
    [HttpGet("search/{from}/{to}")]
    public async Task<IActionResult> Search(string from, string to)
    {
        var flights = await _service.Search(from, to);
        if (flights.Count == 0)
        {
            return NotFound();
        }
        return Ok(flights);
    }
}
