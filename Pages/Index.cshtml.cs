using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTodoList.Models;

namespace MyTodoList.Pages;

public class IndexModel : PageModel
{
  // Page model instances are created per request, so the list is static
  // to survive across requests (in-memory only; resets on app restart).
  private static int _nextId = 3;

  public static List<TodoItem> TodoList { get; set; } = new()
  {
    new TodoItem { Id = 1, Title = "Practice for the interview" },
    new TodoItem { Id = 2, Title = "Do exercises" }
  };

  [BindProperty]
  [Required(ErrorMessage = "Please enter a task.")]
  [StringLength(100, ErrorMessage = "Tasks can't be longer than 100 characters.")]
  public string TaskName { get; set; } = string.Empty;

  public void OnGet()
  {
  }

  public IActionResult OnPost()
  {
    // Validation runs during model binding, BEFORE this method executes.
    if (!ModelState.IsValid)
    {
      return Page(); // re-render the form WITH validation errors
    }

    TodoList.Add(new TodoItem { Id = _nextId++, Title = TaskName.Trim() });
    TempData["Message"] = $"Added: {TaskName.Trim()}";
    return RedirectToPage();
  }

  public IActionResult OnPostDelete(int id)
  {
    var item = TodoList.FirstOrDefault(t => t.Id == id);
    if (item is not null)
    {
      TodoList.Remove(item);
      TempData["Message"] = $"Deleted: {item.Title}";
    }
    return RedirectToPage();
  }

  public IActionResult OnPostToggle(int id)
  {
    var item = TodoList.FirstOrDefault(t => t.Id == id);
    if (item is not null)
    {
      item.IsDone = !item.IsDone;
    }
    return RedirectToPage();
  }
}
