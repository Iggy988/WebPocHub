using Dal;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApi.Controllers;

//Perform CRUD operations on Events table by writing 6 action methods same as Employees Controller
//Use DI via Constructor
[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly ICommonRepository<Event> _repository;

    public EventsController(ICommonRepository<Event> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<Event>> Get()
    {
        var events = _repository.GetAll();
        if (events.Count <= 0)
        {
            return NotFound();
        }
        return Ok(events);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Event> GetDetails(int id)
    {

        var events = _repository.GetDetails(id);
        return events == null ? NotFound() : Ok(events);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Create(Event @event)
    {
        _repository.Insert(@event);
        var result = _repository.SaveChanges();
        if (result > 0)
        {
            //actionName - The name of the action to use for generating the URL
            //routeValues - The route data to use for generating the URL
            //value - The content value to format in the entity body
            return CreatedAtAction("GetDetails", new { id = @event.EventId }, @event);
        }
        return BadRequest();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Update(Event @event)
    {
        _repository.Update(@event);
        var result = _repository.SaveChanges();
        if (result > 0)
        {
            return NoContent();
        }
        return BadRequest();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Event> Delete(int id)
    {
        var @event = _repository.GetDetails(id);
        if (@event != null)
        {
            _repository.Delete(@event);
            _repository.SaveChanges();
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}
