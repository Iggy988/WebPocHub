using Dal;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly ICommonRepository<Employee> _employeeRepository;

    public EmployeeController(ICommonRepository<Employee> repository)
    {
        _employeeRepository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<Employee>> Get()
    {
        var employees = _employeeRepository.GetAll();
        if (employees.Count <= 0)
        {
            return NotFound();
        }
        return Ok(employees);
    }
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Employee> GetDetails(int id)
    {

        var employee = _employeeRepository.GetDetails(id);
        return employee == null ? NotFound() : Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Create(Employee employee)
    {
        _employeeRepository.Insert(employee);
        var result = _employeeRepository.SaveChanges();
        if (result > 0)
        {
            //actionName - The name of the action to use for generating the URL
            //routeValues - The route data to use for generating the URL
            //value - The content value to format in the entity body
            return CreatedAtAction("GetDetails", new { id = employee.EmplyeeId }, employee);
        }
        return BadRequest();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Update(Employee employee)
    {
        _employeeRepository.Update(employee);
        var result = _employeeRepository.SaveChanges();
        if (result > 0)
        {
            return NoContent();
        }
        return BadRequest();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Employee> Delete(int id)
    {
        var employee = _employeeRepository.GetDetails(id);
        if (employee == null)
        {
            return NotFound();
        }
        else
        {
            _employeeRepository.Delete(employee);
            _employeeRepository.SaveChanges();
            return NoContent();
        }
        
    }


    /*
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public IEnumerable<Employee> Get()
    public IActionResult Get()
    {
        var employees = _employeeRepository.GetAll();
        if (employees.Count <= 0)
        {
            return NotFound();
        }
        return Ok(employees);
    }
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetDetails(int id) 
    {

        var employee = _employeeRepository.GetDetails(id);
        return employee == null ? NotFound() : Ok(employee);
    }*/
}
