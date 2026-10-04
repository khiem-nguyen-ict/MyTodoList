using System.ComponentModel.DataAnnotations;

namespace MyTodoList.Models;

public class TodoItem
{
    public int Id { get; set; }

    // These attributes do double duty:
    // - validation in Razor Pages (ModelState)
    // - database schema (NOT NULL, VARCHAR(100))
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public bool IsDone { get; set; }
}
