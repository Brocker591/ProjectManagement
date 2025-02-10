namespace TodoApi.Repositories
{
    public class TodoStatusRepository(TodoContext dbContext) : ITodoStatusRepository
    {
        public async Task<TodoStatus> CreateTodoStatus(TodoStatus todoStatus)
        {
            dbContext.TodoStatuses.Add(todoStatus);
            await dbContext.SaveChangesAsync();

            return todoStatus;
        }

        public async Task<List<TodoStatus>> GetTodoStatusList()
        {
            var todoStatus = await dbContext.TodoStatuses.ToListAsync();
            return todoStatus;
        }

        public async Task<TodoStatus> GetTodoStatus(int todoStatusId)
        {
            var todoStatus = await dbContext.TodoStatuses.FirstOrDefaultAsync(x => x.Id == todoStatusId);
            if (todoStatus == null)
                throw new TodoStatusNotFoundException(todoStatusId);

            return todoStatus;
        }

        public async Task UpdateTodoStatus(TodoStatus todoStatus)
        {
            var existingTodoStatus = await GetTodoStatus(todoStatus.Id);

            existingTodoStatus.Name = todoStatus.Name;

            dbContext.TodoStatuses.Update(existingTodoStatus);

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteTodo(int id)
        {
            var existingTodoStatus = await GetTodoStatus(id);
            dbContext.TodoStatuses.Remove(existingTodoStatus);
            await dbContext.SaveChangesAsync();
        }
    }
}
