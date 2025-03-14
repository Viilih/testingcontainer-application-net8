using Bogus;
using Employee.API;

namespace Employee.Test.TestData;

public class TestDataSeeder
{
    private readonly AppDbContext _context;
    private readonly Faker _faker;
    
    
    private readonly List<API.Entities.Employee> _employees = new();

    public TestDataSeeder(AppDbContext context)
    {
        _context = context;
        _faker = new Faker();
    }

    public async Task SeedAsync()
    {
        await SeedEmployeesAsync();
        await _context.SaveChangesAsync();
    }

    private async Task SeedEmployeesAsync()
    {
        var employeeFaker = new Faker<API.Entities.Employee>().CustomInstantiator(f =>
            new API.Entities.Employee
            {
                Id = f.Random.Int(),
                FirstName = f.Name.FirstName(),
                LastName = f.Name.LastName(),
                Email = f.Person.Email,
                PhoneNumber = f.Phone.PhoneNumber(),
            });
        
        _employees.AddRange(employeeFaker.Generate(10));
        await _context.Employees.AddRangeAsync(_employees);
    }
}