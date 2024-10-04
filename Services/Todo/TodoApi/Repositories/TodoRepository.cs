namespace TodoApi.Repositories;

public class TodoRepository(TodoContext dbContext) : ITodoRepository
{
    public async Task<Todo> CreateTodo(Todo todo)
    {
        if (todo.Id == Guid.Empty)
            todo.Id = Guid.NewGuid();
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync();

        return todo;
    }

    public async Task<List<Todo>> GetTodos()
    {
        var todos = await dbContext.Todos.ToListAsync();
        return todos;
    }

    public async Task<Todo> GetTodo(Guid todoId)
    {
        var todo = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == todoId);
        if (todo == null)
            throw new TodoNotFoundException(todoId);

        return todo;
    }

    public async Task UpdateTodo(Todo todo)
    {
        var existingTodo = await GetTodo(todo.Id);

        existingTodo.Desciption = todo.Desciption;
        existingTodo.ResponsibleUser = todo.ResponsibleUser;
        existingTodo.EditorUsers = todo.EditorUsers;
        existingTodo.IsProcessed = todo.IsProcessed;
        existingTodo.StatusId = todo.StatusId;
        dbContext.Todos.Update(existingTodo);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteTodo(Guid todoId)
    {
        var existingTodo = await GetTodo(todoId);
        dbContext.Todos.Remove(existingTodo);
        await dbContext.SaveChangesAsync();
    }
}

