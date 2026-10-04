# MyTodoList

A simple to-do list web app built with **ASP.NET Core 10 Razor Pages**.

## Features

- Add tasks through a validated form (POST-Redirect-GET pattern)
- Mark tasks complete / incomplete with a single click
- Delete tasks with per-item handler methods
- **Tasks persist across app restarts** (EF Core + SQLite)
- Server- and client-side validation (required, max 100 characters)
- Flash messages via `TempData`
- Responsive UI with Bootstrap 5

## Tech stack

- .NET 10 SDK
- ASP.NET Core Razor Pages
- EF Core 10 + SQLite (`todolist.db`, created/migrated at startup)
- Bootstrap 5 + jQuery Validation (bundled in `wwwroot/lib`)

## Getting started

```bash
dotnet run
```

Then open http://localhost:5252 (or https://localhost:7023).

Use `dotnet watch run` to auto-reload on file changes.

## Project structure

```
Program.cs                    App entry point: DI, middleware, migrations + seed
Pages/Index.cshtml(.cs)       Home page: form + task list (add/toggle/delete handlers)
Pages/Error.cshtml(.cs)       Error page
Models/TodoItem.cs            Task entity (Id, Title, IsDone)
Data/AppDbContext.cs          EF Core DbContext (TodoItems DbSet)
Data/ITodoRepository.cs       Repository interface
Data/TodoRepository.cs        Repository implementation (EF Core queries)
Pages/Shared/                 _Layout.cshtml, _ValidationScriptsPartial.cshtml
wwwroot/                      Static assets (Bootstrap, jQuery, site css/js)
```

## Data access

- `AppDbContext` is registered with `AddDbContext` + `UseSqlite` (scoped, one
  DbContext per request) and injected into `TodoRepository` (`AddScoped`).
- The page model takes `ITodoRepository` via constructor injection — it never
  touches EF Core directly.
- Startup applies pending migrations (`db.Database.Migrate()`) and seeds two
  starter tasks on first run. In production, apply migrations at deploy time.
- Manage migrations: `dotnet ef migrations add <Name>` then `dotnet ef database update`.

## Learning notes

- Page model instances are created per request; persistence comes from the
  repository + SQLite, not from static fields.
- Handlers follow the `OnPost<Handler>` naming convention and are selected
  via `asp-page-handler`.
- All POST forms automatically include an anti-forgery token, validated on
  every handler.
- Data annotations on `TodoItem` serve double duty: UI validation and
  database schema (NOT NULL / length constraints).

## Next steps

- Repository unit tests with an in-memory SQLite provider
- Authentication/authorization
- Integration tests with `WebApplicationFactory`
