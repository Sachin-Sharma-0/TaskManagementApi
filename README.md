# Task Management API

A simple ASP.NET Core Web API for managing tasks, users, and task comments, built with Entity Framework Core, JWT authentication, and unit tests. This project demonstrates API development, database design, debugging, and testing skills.

## Project Structure
- **TaskManagementApi/**: Main API project containing controllers, services, models, and data context.
- **TaskManagementApi.Tests/**: xUnit test project for unit testing the `TaskService`.
- **docs/**: Documentation including database schema, ER diagram, migration script, SQL queries, and code fixes.

## Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 (or VS Code with C# extension)
- Git

## Setup Instructions
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/TaskManagementApi.git
   cd TaskManagementApi

2. **Restore Dependencies**:
   ```bash
   dotnet restore

3. **Run the API**:
   Run TaskManagementApi.sln in Visual Studio and press F5, or:
   ```bash
   cd TaskManagementApi
   dotnet run
The API runs at https://localhost:7265. Access Swagger UI at https://localhost:7265/swagger.

## API Usage
1. **Swagger UI**: Test endpoints at https://localhost:7265/swagger.

2. **Authentication**:
   Use POST /api/auth/login to obtain a JWT token:
   ```json
   {
    "username": "admin",
    "password": "admin123"
   }
  In Swagger, click Authorize and enter Bearer <token>.

3. **Key Endpoints**:
   - POST /api/tasks: Create a task (Admin role only).
   - GET /api/tasks/{id}: Retrieve a task by ID.
   - GET /api/tasks/user/{userId}: Retrieve all tasks for a user.

## Database Design
- **Schema**:
  - `Users`: `Id` (PK), `Username`, `Password`, `Role` (Enum: Admin=0, User=1).
  - `Tasks`: `Id` (PK), `Title`, `Description`, `UserId` (FK to Users).
  - `TaskComments`: `Id` (PK), `Comment`, `TaskId` (FK to Tasks), `UserId` (FK to Users).
- **Relationships**:
  - `Users` 1:N `Tasks` (via `UserId`).
  - `Tasks` 1:N `TaskComments` (via `TaskId`).
  - `Users` 1:N `TaskComments` (via `UserId`).
- **ER Diagram**: See `docs/er_diagram.png`.
- **Migration Script**: See `docs/migrations.sql`.
- **Sample Queries**: See `docs/queries.sql`.

## Running Unit Tests
- Tests are located in `TaskManagementApi.Tests/`.
- To run tests:
  ```bash
  cd TaskManagementApi.Tests
  dotnet test
- Tests cover:
  *TaskService.GetTask*: Valid ID, invalid ID, non-existent ID.
  *TaskService.GetAllTasks*: Returns all tasks.

## Code Fixes
- Fixed issues in `TaskService` (async/await, return types, error handling).
- See `docs/code_fixes.md` for details.

## Notes
- The API uses an in-memory database for simplicity.
- Passwords are stored as plain text for demo purposes; in production, use hashing (e.g., BCrypt).
- JWT key is stored in `appsettings.json`; secure it in a production environment.

## Deliverables
- **API Project**: Fully functional with JWT authentication and Swagger UI (`TaskManagementApi/`).
- **Database Design**: Schema, ER diagram, migration script, and SQL queries (`docs/`).
- **Code Fixes**: Corrected `TaskService` implementation (`Services/TaskService.cs`).
- **Unit Tests**: xUnit tests with Moq for `TaskService` (`TaskManagementApi.Tests/`).

