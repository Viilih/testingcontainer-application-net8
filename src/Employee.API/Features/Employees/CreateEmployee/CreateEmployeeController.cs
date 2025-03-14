using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Features.Employees.CreateEmployee;

[ApiController]
[Route("api")]
public class CreateEmployeeController  : ControllerBase
{

    [HttpPost]
    [Route("employee")]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand createEmployeeCommand, ISender sender)
    {
        var result = await sender.Send(createEmployeeCommand);
        if (result.IsFailed)
        {
            return BadRequest();
        }
        return Ok(result.Value);
    }
}