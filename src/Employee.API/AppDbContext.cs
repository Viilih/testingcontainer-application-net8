using Microsoft.EntityFrameworkCore;

namespace Employee.API;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Entities.Employee> Employees { get; set; }
}