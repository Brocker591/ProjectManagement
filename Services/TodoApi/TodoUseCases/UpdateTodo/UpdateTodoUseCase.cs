namespace TodoApi.TodoUseCases.UpdateTodo;

public record UpdateTodoCommand(Todo todo);
public record UpdateTodoResult();

public class UpdateTodoUseCase(ITodoRepository repository) : IUpdateTodoUseCase
{
    public async Task<UpdateTodoResult> Execute(UpdateTodoCommand command)
    {
        await repository.UpdateTodo(command.todo);

        return new UpdateTodoResult();
    }
}
