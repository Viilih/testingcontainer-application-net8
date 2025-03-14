using Employee.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Employee.Test.Infrastructure;


public class CustomWebApplicationFactory : WebApplicationFactory<Program>,IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = 
        new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("localdb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(_postgreSqlContainer.GetConnectionString());
            });

        });
        

    }

    public Task InitializeAsync()
    {
        return _postgreSqlContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _postgreSqlContainer.StopAsync();
    }
}