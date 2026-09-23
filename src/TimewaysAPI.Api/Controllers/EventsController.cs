using Microsoft.AspNetCore.Mvc;
using TimewaysAPI.Application.Events;
using Microsoft.AspNetCore.Authorization;

namespace TimewaysAPI.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EventResponse>>> GetAll()
    {
        var events = await _eventService.GetAllAsync();

        return Ok(events);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventResponse>> GetById(int id)
    {
        var @event = await _eventService.GetByIdAsync(id);

        if (@event is null)
        {
            return NotFound();
        }

        return Ok(@event);
    }

    [HttpPost]
    public async Task<ActionResult<EventResponse>> Create(
        CreateEventRequest request)
    {
        var @event = await _eventService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = @event.Id },
            @event);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<EventResponse>> Update(
    int id,
    UpdateEventRequest request)
    {
        var @event = await _eventService.UpdateAsync(id, request);

        if (@event is null)
        {
            return NotFound();
        }

        return Ok(@event);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _eventService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}