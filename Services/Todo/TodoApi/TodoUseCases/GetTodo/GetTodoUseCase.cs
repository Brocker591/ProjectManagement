namespace TodoApi.TodoUseCases.GetTodo;

public record GetTodoQuery(Guid id);
public record GetTodoResult(Todo data);

public class GetTodoUseCase(ITodoRepository repository) : IGetTodoUseCase
{
    public async Task<GetTodoResult> Execute(GetTodoQuery query)
    {
        var todo = await repository.GetTodo(query.id);

        return new GetTodoResult(todo);
    }
}
