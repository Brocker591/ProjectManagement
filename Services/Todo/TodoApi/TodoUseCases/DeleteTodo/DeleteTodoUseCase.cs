namespace TodoApi.TodoUseCases.DeleteTodo;

public record DeleteTodoCommand(Guid id);
public record DeleteTodoResult();

public class DeleteTodoUseCase(ITodoRepository repository) : IDeleteTodoUseCase
{
    public async Task<DeleteTodoResult> Execute(DeleteTodoCommand command)
    {
        await repository.DeleteTodo(command.id);

        return new DeleteTodoResult();
    }
}
