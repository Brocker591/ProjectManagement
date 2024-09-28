namespace TodoApi.TodoUseCases.UpdateTodo;

public record UpdateTodoCommand(Todo todo);
public record UpdateTodoResult();

public class UpdateTodoUseCase(ITodoRepository repository, ITodoStatusRepository statusRepository) : IUpdateTodoUseCase
{
    public async Task<UpdateTodoResult> Execute(UpdateTodoCommand command)
    {
        await statusRepository.GetTodoStatus(command.todo.StatusId);

        await repository.UpdateTodo(command.todo);

        return new UpdateTodoResult();
    }
}
