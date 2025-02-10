
namespace TodoApi.Repositories
{
    public interface ITodoStatusRepository
    {
        Task<TodoStatus> CreateTodoStatus(TodoStatus todoStatus);
        Task DeleteTodo(int id);
        Task<TodoStatus> GetTodoStatus(int todoStatusId);
        Task<List<TodoStatus>> GetTodoStatusList();
        Task UpdateTodoStatus(TodoStatus todoStatus);
    }
}