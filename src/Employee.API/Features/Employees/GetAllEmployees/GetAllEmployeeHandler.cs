using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.API.Features.Employees.GetAllEmployees;

public class GetAllEmployeeHandler :  IRequestHandler<GetAllEmployeesQuery,Result<IEnumerable<GetAllEmployeesResponse>>>
{
    private readonly AppDbContext _context;

    public GetAllEmployeeHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<GetAllEmployeesResponse>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _context.Employees
            .Select(e => new GetAllEmployeesResponse
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
            })
            .ToListAsync(cancellationToken);

        return employees;
    }
}