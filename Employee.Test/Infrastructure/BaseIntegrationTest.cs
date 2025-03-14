using Employee.API;
using Employee.Test.TestData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.Test.Infrastructure;

public class BaseIntegrationTest: IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly ISender _sender;
    protected readonly AppDbContext _context;
    protected readonly TestDataSeeder DataSeeder;

    protected BaseIntegrationTest(CustomWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        _context = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        DataSeeder = new TestDataSeeder(_context);
    }
    
    public Task InitializeAsync() => DataSeeder.SeedAsync();
    
    public Task DisposeAsync() => Task.CompletedTask;
}