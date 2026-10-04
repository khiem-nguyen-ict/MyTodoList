using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTodoList.Data;

namespace MyTodoList.Pages;

// Route template: /Edit/5  (non-integer ids are rejected with 404 automatically)
public class EditModel : PageModel
{
    private readonly ITodoRepository _todos;

    public EditModel(ITodoRepository todos)
    {
        _todos = todos;
    }

    // [BindProperty] binds the form field to this property on POST.
    // The validation attributes work exactly like on the Index page.
    [BindProperty]
    [Required(ErrorMessage = "Please enter a task.")]
    [StringLength(100, ErrorMessage = "Tasks can't be longer than 100 characters.")]
    public string Title { get; set; } = string.Empty;

    // GET /Edit/{id}: load the task and pre-fill the form.
    public IActionResult OnGet(int id)
    {
        var item = _todos.GetById(id);
        if (item is null)
        {
            return NotFound(); // 404: no task with this id
        }

        Title = item.Title;
        return Page();
    }

    // POST /Edit/{id}: validate, update, redirect (PRG pattern).
    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page(); // re-render WITH validation errors
        }

        var item = _todos.Update(id, Title.Trim());
        if (item is null)
        {
            return NotFound(); // task was deleted in the meantime
        }

        TempData["Message"] = $"Saved: {item.Title}";
        return RedirectToPage("/Index");
    }
}
