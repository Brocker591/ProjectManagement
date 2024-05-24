namespace TodoApi.Repositories;

public class TodoRepository(TodoContext dbContect) : ITodoRepository
{
    public async Task<Todo> CreateTodo(Todo todo)
    {
        if (todo.Id == Guid.Empty)
            todo.Id = Guid.NewGuid();
        dbContect.Todos.Add(todo);
        await dbContect.SaveChangesAsync();

        return todo;
    }

    public async Task<List<Todo>> GetTodos()
    {
        var todos = await dbContect.Todos.ToListAsync();
        return todos;
    }

    public async Task<Todo> GetTodo(Guid todoId)
    {
        var todo = await dbContect.Todos.FirstOrDefaultAsync(x => x.Id == todoId);
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
        existingTodo.ProjectId = todo.ProjectId;
        dbContect.Update(existingTodo);

        await dbContect.SaveChangesAsync();
    }

    public async Task DeleteTodo(Guid todoId)
    {
        var existingTodo = await GetTodo(todoId);
        dbContect.Todos.Remove(existingTodo);
        await dbContect.SaveChangesAsync();
    }
}

