# MyTodoList

A simple to-do list web app built with **ASP.NET Core 10 Razor Pages**.

## Features

- Add tasks through a validated form (POST-Redirect-GET pattern)
- Mark tasks complete / incomplete with a single click
- Delete tasks with per-item handler methods
- Server- and client-side validation (required, max 100 characters)
- Flash messages via `TempData`
- Responsive UI with Bootstrap 5

## Tech stack

- .NET 10 SDK
- ASP.NET Core Razor Pages
- Bootstrap 5 + jQuery Validation (bundled in `wwwroot/lib`)

## Getting started

```bash
dotnet run
```

Then open http://localhost:5252 (or https://localhost:7023).

Use `dotnet watch run` to auto-reload on file changes.

## Project structure

```
Program.cs                    App entry point: DI + middleware pipeline
Pages/Index.cshtml(.cs)       Home page: form + task list (add/toggle/delete handlers)
Pages/Error.cshtml(.cs)       Error page
Models/TodoItem.cs            Task model (Id, Title, IsDone)
Pages/Shared/                 _Layout.cshtml, _ValidationScriptsPartial.cshtml
wwwroot/                      Static assets (Bootstrap, jQuery, site css/js)
```

## Learning notes

- Tasks currently live in a `static` in-memory list, so they reset when the app restarts.
- Page model instances are created per request; handlers follow the `OnPost<Handler>` naming
  convention and are selected via `asp-page-handler`.
- All POST forms automatically include an anti-forgery token, validated on every handler.

## Next steps

- Persist tasks with EF Core + SQLite
- Repository pattern with dependency injection
- Authentication/authorization
- Integration tests with `WebApplicationFactory`
