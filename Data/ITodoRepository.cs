using MyTodoList.Models;

namespace MyTodoList.Data;

public interface ITodoRepository
{
    List<TodoItem> GetAll();
    TodoItem? GetById(int id);
    void Add(string title);
    TodoItem? Delete(int id);
    TodoItem? Toggle(int id);
}
