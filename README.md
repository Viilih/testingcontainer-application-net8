<h1 align="center" style="font-weight: bold;">Testing Container Employee API</h1>

<p align="center">
  <a href="#tech">Technologies</a> •
  <a href="#architecture">Architecture</a> •
  <a href="#started">Getting Started</a> •
  <a href="#routes">API Endpoints</a> •
  <a href="#testing">Testing</a>
</p>

<p align="center">
  <b>A .NET 8 Web API with testing containers for integration testing, showcasing CQRS pattern with handlers, commands, and queries</b>
</p>

<h2 id="tech">💻 Technologies</h2>

- C# 10
- .NET 8
- PostgreSQL
- xUnit
- Testing Containers
- MediatR (for CQRS implementation)
- Entity Framework Core

<h2 id="architecture">🏗️ Architecture</h2>

This project follows Clean Architecture principles with CQRS (Command Query Responsibility Segregation) pattern:

### Project Structure

- **Employee.API** - Main web API project
  - **Entities** - Domain models like `Employee.cs`
  - **Features** - Organized by domain features
    - **Employees**
      - **CreateEmployee** - Command pattern for creating employees
        - `CreateEmployeeCommand.cs` - DTO for create operation
        - `CreateEmployeeController.cs` - API endpoint controller
        - `CreateEmployeeHandler.cs` - Business logic handler
      - **GetAllEmployees** - Query pattern for retrieving employees
        - `GetAllEmployeeHandler.cs` - Query handler
        - `GetAllEmployeesQuery.cs` - Query definition
        - `GetAllEmployeesResponse.cs` - Response DTO
        - `GetEmployeeController.cs` - API endpoint controller
  - **Migrations** - Database migration files
  - `AppDbContext.cs` - Entity Framework database context
  - `Program.cs` - Application entry point and service configuration

- **Employee.Test** - Test project
  - **Infrastructure** - Testing infrastructure
    - `BaseIntegrationTest.cs` - Base test class for integration tests
    - `CustomWebApplicationFactory.cs` - Test server factory
  - **TestData**
    - `TestDataSeeder.cs` - Seed data for tests
  - `EmployeeTest.cs` - Employee-related tests

### CQRS Pattern Explained

This project implements the CQRS (Command Query Responsibility Segregation) pattern using MediatR:

- **Commands** - Represent actions that change state (e.g., `CreateEmployeeCommand`)
- **Queries** - Represent requests for data (e.g., `GetAllEmployeesQuery`)
- **Handlers** - Process commands and queries (e.g., `CreateEmployeeHandler`, `GetAllEmployeeHandler`)
- **Controllers** - Expose API endpoints and delegate to handlers via MediatR

<h2 id="started">🚀 Getting Started</h2>

You need to update the connection string for your PostgreSQL database in the `appsettings.json` file.

The database can be run locally or through a container using this command:

```bash
docker run --name postgres-employee -e POSTGRES_PASSWORD=mysecretpassword -e POSTGRES_USER=postgres -e POSTGRES_DB=employeedb -p 5432:5432 -d postgres
```

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (for testing containers)
- [Git](https://git-scm.com/)

### Cloning

```bash
git clone https://github.com/Viilih/testingcontainer-application-net8.git
```

### Starting

```bash
cd testingcontainer-application-net8
dotnet restore
cd src/Employee.API
dotnet run
```

The API will be available at `https://localhost:5001` or `http://localhost:5000`.

<h2 id="routes">📍 API Endpoints</h2>

| Route | Description |
|-------|------------|
| <kbd>GET /api/employee</kbd> | List all employees [response details](#get-employees-detail) |
| <kbd>POST /api/employee</kbd> | Create a new employee [request details](#post-employee-detail) |

<h3 id="get-employees-detail">GET /api/employee</h3>

**RESPONSE**
```json
[
  {
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "phoneNumber": "22123434578"
  },
  {
    "id": 2,
    "firstName": "Jane",
    "lastName": "Smith",
    "email": "jane.smith@example.com",
    "phoneNumber": "22987654321"
  }
]
```

<h3 id="post-employee-detail">POST /api/employee</h3>

**REQUEST**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "22123434578"
}
```

**RESPONSE**
```json
{
  1
}
```

<h2 id="testing">🧪 Testing</h2>

This project uses Testing Containers to run integration tests against a real PostgreSQL database in an isolated Docker container.

### Running Tests

```bash
cd test/Employee.Test
dotnet test
```

### Test Architecture

- **BaseIntegrationTest.cs** - Set up and teardown for the test database container
- **CustomWebApplicationFactory.cs** - Configures the test server with the container database
- **TestDataSeeder.cs** - Seeds the test database with sample data

### Benefits of Testing Containers

- Tests run against a real database, not in-memory mocks
- Each test runs in an isolated environment
- No test pollution between test runs
- Closely mimics production environment
