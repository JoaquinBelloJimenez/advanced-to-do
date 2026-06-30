# AGENTS.md

## Project Overview

Full-stack To-Do application with an ASP.NET Core Web API backend and Angular frontend.

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | .NET 10, ASP.NET Core Web API, C# |
| ORM | Entity Framework Core 10.0 (registered, but actual data access uses raw ADO.NET via `Microsoft.Data.SqlClient`) |
| Database | SQL Server LocalDB with stored procedures |
| Frontend | Angular 21.1.0, Angular Material 21.2.14, TypeScript 5.9.2 |
| SSR | Angular SSR (Express 5.1) |
| Tests | Vitest 4.0.8 (frontend only) |
| Package Manager | npm 11.12.1 |

## Project Structure

```
advanced-to-do/
├── TodoListApi/                    # Backend: ASP.NET Core Web API
│   ├── Controllers/
│   │   └── TasksController.cs      # REST API CRUD endpoints
│   ├── Data/
│   │   └── AppDbContext.cs         # EF Core DbContext (not actively used)
│   ├── Models/
│   │   └── UserTask.cs             # Domain model
│   ├── Repositories/
│   │   └── TaskRepository.cs       # ADO.NET + stored procedures
│   ├── Program.cs                  # Entry point, DI, middleware
│   └── TodoListApi.csproj          # net10.0
│
└── todo-list-frontend/             # Frontend: Angular app
    ├── src/
    │   ├── app/
    │   │   ├── models/user-task.ts         # UserTask interface
    │   │   ├── services/user-task.ts       # TaskService (HTTP calls)
    │   │   ├── task-list/                  # List tasks component
    │   │   ├── task-form/                  # Create task component
    │   │   └── task-detail/                # Task detail component (stub)
    │   └── server.ts                       # Express SSR server
    ├── angular.json
    └── package.json
```

## Commands

### Backend (TodoListApi/)

- `dotnet build` — Build
- `dotnet run` — Run (http://localhost:5214, https://localhost:7255)
- No test project configured

### Frontend (todo-list-frontend/)

- `npm start` / `ng serve` — Dev server
- `npm build` / `ng build` — Production build
- `npm test` / `ng test` — Run Vitest unit tests
- `npm run watch` — Build in watch mode (development)

## Code Conventions

- **EditorConfig**: 2-space indentation, UTF-8, final newline
- **Prettier**: `printWidth: 100`, `singleQuote: true`, Angular parser for HTML
- **TypeScript**: strict mode, `noImplicitOverride`, `noPropertyAccessFromIndexSignature`
- **Angular compiler**: strict templates, strict injection, strict input access modifiers
- **Quotes**: single quotes for TypeScript

## Architecture & Known Issues

- **API URL mismatch**: `TaskService` points to `https://localhost:5001/api/tasks` but backend runs on `http://localhost:5214` / `https://localhost:7255`
- **Database**: Stored procedures (`GetAllTasks`, `GetTaskById`, `CreateTask`, `UpdateTask`, `DeleteTask`) must exist in the LocalDB instance — they are not defined in the codebase
- **Routing**: `app.routes.ts` is empty (`[]`); existing components (task-list, task-form, task-detail) are not wired via router
- **EF Core**: `AppDbContext` is registered but unused — actual data access is via raw ADO.NET
- **Backend tests**: None exist

## Project Status

Early development stage. The API endpoints and basic UI components are scaffolded but the frontend-backend connection, routing, and styling are incomplete.
