using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTodoList.Data;
using MyTodoList.Models;

namespace MyTodoList.Pages;

public class IndexModel : PageModel
{
    private readonly ITodoRepository _todos;

    // Loaded per request from the repository (backed by SQLite, not a static field)
    public List<TodoItem> TodoList { get; private set; } = new();

    [BindProperty]
    [Required(ErrorMessage = "Please enter a task.")]
    [StringLength(100, ErrorMessage = "Tasks can't be longer than 100 characters.")]
    public string TaskName { get; set; } = string.Empty;

    // Constructor injection: DI provides the repository (and its DbContext)
    public IndexModel(ITodoRepository todos)
    {
        _todos = todos;
    }

    public void OnGet()
    {
        TodoList = _todos.GetAll();
    }

    public IActionResult OnPost()
    {
        // Validation runs during model binding, BEFORE this method executes.
        if (!ModelState.IsValid)
        {
            return Page(); // re-render the form WITH validation errors
        }

        _todos.Add(TaskName.Trim());
        TempData["Message"] = $"Added: {TaskName.Trim()}";
        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        var item = _todos.Delete(id);
        if (item is not null)
        {
            TempData["Message"] = $"Deleted: {item.Title}";
        }
        return RedirectToPage();
    }

    public IActionResult OnPostToggle(int id)
    {
        var item = _todos.Toggle(id);
        if (item is not null)
        {
            TempData["Message"] = $"Task \"{item.Title}\" marked {(item.IsDone ? "done" : "not done")}.";
        }
        return RedirectToPage();
    }
}
