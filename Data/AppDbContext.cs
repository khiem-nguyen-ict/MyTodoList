using Microsoft.EntityFrameworkCore;
using MyTodoList.Models;

namespace MyTodoList.Data;

public class AppDbContext : DbContext
{
    // EF Core injects these options (configured in Program.cs via AddDbContext)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Maps to a "TodoItems" table (EF convention: DbSet name pluralized)
    public DbSet<TodoItem> TodoItems { get; set; }
}
