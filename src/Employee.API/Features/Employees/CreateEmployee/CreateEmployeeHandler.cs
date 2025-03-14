using FluentResults;
using FluentValidation;
using MediatR;

namespace Employee.API.Features.Employees.CreateEmployee;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result<int>>
{
    private readonly AppDbContext _context;
    private readonly IValidator<CreateEmployeeCommand> _validator;

    public CreateEmployeeHandler(AppDbContext context, IValidator<CreateEmployeeCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.ToString());
        }
        
        // Refactor to  the private method  of the  entity
        var employee = new Entities.Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);
        
        return employee.Id;
    }
}