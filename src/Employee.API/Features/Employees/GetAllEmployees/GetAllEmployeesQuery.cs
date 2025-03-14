using FluentResults;
using MediatR;

namespace Employee.API.Features.Employees.GetAllEmployees;

public class GetAllEmployeesQuery : IRequest<Result<IEnumerable<GetAllEmployeesResponse>>> { }