using Employee.API.Features.Employees.CreateEmployee;
using Employee.API.Features.Employees.GetAllEmployees;
using Employee.Test.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Employee.Test;

public class EmployeeTests : BaseIntegrationTest
{
    public EmployeeTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task CreateEmployee_ShouldCreateEmployee_WhenDataIsValid()
    {
        // Arrange
        var command = new CreateEmployeeCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            PhoneNumber = "21999999999"
        };

        // Act
        var employeeId = await _sender.Send(command);
        var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == employeeId.Value);

        // Assert
        Assert.NotNull(employee);
        Assert.Equal(command.FirstName, employee.FirstName);
        Assert.Equal(command.LastName, employee.LastName);
        Assert.Equal(command.Email, employee.Email);
        Assert.Equal(command.PhoneNumber, employee.PhoneNumber);
    }

    [Theory]
    [InlineData("", "Doe", "johndoe@example.com", "21999999999")] // FirstName vazio
    [InlineData("John", "", "johndoe@example.com", "21999999999")] // LastName vazio
    [InlineData("John", "Doe", "", "21999999999")] // Email vazio
    public async Task CreateEmployee_ShouldFail_WhenInvalidDataIsProvided(
        string firstName, string lastName, string email, string phoneNumber)
    {
        // Arrange
        var command = new CreateEmployeeCommand
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        // Act
        var result = await _sender.Send(command);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsFailed);
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task GetAllEmployees_ShouldReturnEmployees_WhenEmployeesExist()
    {
        // Arrange
        var command = new CreateEmployeeCommand
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "janesmith@example.com",
            PhoneNumber = "21888888888"
        };
        
        await _sender.Send(command);

        var query = new GetAllEmployeesQuery();

        // Act
        var employees = await _sender.Send(query);
        var employeesFromDb = await _context.Employees.ToListAsync();

        // Assert
        Assert.NotNull(employees);
        Assert.NotEmpty(employees.Value);
        Assert.Equal(employeesFromDb.Count, employees.Value.Count());
    }

    [Fact]
    public async Task GetAllEmployees_ShouldReturnEmptyList_WhenNoEmployeesExist()
    {
        // Arrange
        await _context.Employees.ExecuteDeleteAsync(); // Garante que o banco esteja limpo

        var query = new GetAllEmployeesQuery();

        // Act
        var employees = await _sender.Send(query);

        // Assert
        Assert.NotNull(employees.Value);
        Assert.Empty(employees.Value);
    }
}
