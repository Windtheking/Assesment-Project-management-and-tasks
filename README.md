# Project and Task Management

Short description
-----------------
A lightweight Project and Task Management application implemented with a layered .NET backend and a JavaScript-based frontend. The backend follows a clean separation of concerns (Domain, Application, Infrastructure) and uses Entity Framework Core for data access. The project exposes typical CRUD operations for projects and tasks, pagination and basic filtering.

Tech stack
----------
- Backend: .NET (C#), Entity Framework Core
- Frontend: JavaScript (framework unspecified)
- Database: SQL (configured via EF Core / `ApplicationDbContext`)
- Development environment: JetBrains Rider on Linux

Repository structure
--------------------
- `src/ProjectAndTaskManagement.Domain` - Domain entities and value objects.
- `src/ProjectAndTaskManagement.Application` - Application interfaces, DTOs, and use case contracts.
- `src/ProjectAndTaskManagement.Infrastructure` - Persistence, repository implementations, EF Core context.
  - Example: `src/ProjectAndTaskManagement.Infrastructure/Repositories/ProjectRepository.cs` implements project CRUD, pagination, filtering and existence checks.
- `src/ProjectAndTaskManagement.WebApi` (if present) - API controllers and web host.
- `frontend/` (if present) - JavaScript frontend application.

Key concepts & patterns
-----------------------
- Layered architecture (Domain, Application, Infrastructure).
- Repository pattern for data access (repositories encapsulate EF Core operations).
- Use of `DbContext` (e.g., `ApplicationDbContext`) with EF Core for persistence.
- Common repository features: asynchronous CRUD, pagination, filtering, existence checks, and use of `AsNoTracking()` for read-only queries.

Notable repository behaviors
----------------------------
The project repository implementation provides:
- Basic list and paged queries with optional filtering (e.g., by priority).
- Read operations using no-tracking where appropriate.
- Standard `Add`, `Update`, `Delete` operations that call `SaveChangesAsync`.
- `ExistsAsync` helper to check entity presence by id.

Build & run (backend)
---------------------
1. Restore dependencies:
   - `dotnet restore`
2. Build:
   - `dotnet build`
3. Run:
   - `dotnet run --project src/ProjectAndTaskManagement.WebApi`
4. Database migrations (if using EF Core migrations):
   - `dotnet ef migrations add <Name> --project src/ProjectAndTaskManagement.Infrastructure`
   - `dotnet ef database update --project src/ProjectAndTaskManagement.Infrastructure`

Testing
-------
- Run unit/integration tests (if present):
  - `dotnet test`

Frontend
--------
- Navigate to the frontend folder (if present) and follow its README.
- Typical commands:
  - `npm install`
  - `npm run start`
  - `npm run build`

Configuration
-------------
- Connection strings and environment-specific settings should be stored in configuration files or environment variables consumed by `ApplicationDbContext`.
- Ensure secrets (DB credentials, API keys) are not committed to source control.

Contributing
------------
- Follow the existing project structure and layered architecture.
- Add unit tests for new features and repository methods.
- Keep database schema changes in EF Core migrations.

License
-------
- Add a `LICENSE` file to the repository to specify project licensing.

Contact / Maintainers
---------------------
- Project owner and contributors are available in the repository metadata (GitHub).
