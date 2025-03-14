using FluentResults;
using FluentValidation;
using MediatR;

namespace Employee.API.Features.Employees.CreateEmployee;

public class CreateEmployeeCommand : IRequest<Result<int>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
}

public class Validator : AbstractValidator<CreateEmployeeCommand>
{
    public Validator()
    {
         RuleFor(x => x.FirstName).NotEmpty();
         RuleFor(x => x.LastName).NotEmpty();
         RuleFor(x => x.Email).NotEmpty();
         
    }
}