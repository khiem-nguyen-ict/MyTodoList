using MyTodoList.Models;

namespace MyTodoList.Data;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _db;

    // Injected by DI (registered in Program.cs). Scoped lifetime:
    // one DbContext instance per HTTP request.
    public TodoRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<TodoItem> GetAll()
    {
        return _db.TodoItems.OrderBy(t => t.Id).ToList();
    }

    public TodoItem? GetById(int id)
    {
        return _db.TodoItems.Find(id);
    }

    public void Add(string title)
    {
        _db.TodoItems.Add(new TodoItem { Title = title, IsDone = false });
        _db.SaveChanges();
    }

    public TodoItem? Delete(int id)
    {
        var item = _db.TodoItems.Find(id);
        if (item is null)
        {
            return null;
        }

        _db.TodoItems.Remove(item);
        _db.SaveChanges();
        return item;
    }

    public TodoItem? Toggle(int id)
    {
        var item = _db.TodoItems.Find(id);
        if (item is null)
        {
            return null;
        }

        item.IsDone = !item.IsDone;
        _db.SaveChanges();
        return item;
    }
}
