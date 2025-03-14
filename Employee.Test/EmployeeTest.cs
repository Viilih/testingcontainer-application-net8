using Employee.API.Features.Employees.CreateEmployee;
using Employee.API.Features.Employees.GetAllEmployees;
using Employee.Test.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Employee.Test;

public class EmployeeTest:  BaseIntegrationTest
{
     public EmployeeTest(CustomWebApplicationFactory factory): base(factory){}

    [Fact]
    public async  Task  Test1()
    {
        var command = new CreateEmployeeCommand
        {
            FirstName = "Teste",
            LastName = "Testedois",
            Email = "",
            PhoneNumber = "219999999"
        };
        
        var employeeId = await _sender.Send(command);
        var  employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == employeeId.Value);

        Assert.NotNull(employee);
    }

    [Fact]
    public async Task Test2()
    {
        var query = new GetAllEmployeesQuery();
        
        var employees = await  _sender.Send(query);
        
        var employeesCount = await _context.Employees.ToListAsync(); 
        
        Assert.NotNull(employees);
    }
}