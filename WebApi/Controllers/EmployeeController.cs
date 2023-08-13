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
