using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Features.Employees.GetAllEmployees;

[ApiController]
[Route("api")]
public class GetEmployeeController : ControllerBase
{
    
    [HttpGet]
    [Route("employees")]
    public async Task<IActionResult> GetAllEmployees(ISender sender)
    {
        var query = new GetAllEmployeesQuery();
        var result = await sender.Send(query);
        
        if (result.IsFailed)
        {
           return BadRequest(result.Errors);
        }
        
        return Ok(result.Value);
    }
}