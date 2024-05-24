namespace TodoApi.Repositories;

public interface ITodoRepository
{
    Task<Todo> CreateTodo(Todo todo);
    Task<List<Todo>> GetTodos();
    Task<Todo> GetTodo(Guid todoId);
    Task UpdateTodo(Todo todo);
    Task DeleteTodo(Guid todoId);

}
