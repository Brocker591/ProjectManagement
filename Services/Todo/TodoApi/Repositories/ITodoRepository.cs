namespace TodoApi.Repositories;

public interface ITodoRepository
{
    Task<Todo> CreateTodo(Todo todo);
    Task<List<Todo>> GetTodos();
    Task<List<Todo>> GetTodosByTenant(string tenant);
    Task<List<Todo>> GetTodosByProjectId(Guid projectId);
    Task<Todo> GetTodo(Guid todoId);
    Task UpdateTodo(Todo todo);
    Task DeleteTodo(Guid todoId);

}
