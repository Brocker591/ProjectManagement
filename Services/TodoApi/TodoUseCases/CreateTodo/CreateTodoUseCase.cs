namespace TodoApi.TodoUseCases.CreateTodo;

public record CreateTodoCommand(string Desciption, Guid? ResponsibleUser, List<Guid>? EditorUsers, Guid? ProjectId);
public record CreateTodoResult(Todo data);

public class CreateTodoUseCase(ITodoRepository repository) : ICreateTodoUseCase
{
    public async Task<CreateTodoResult> Execute(CreateTodoCommand command)
    {
        Todo todo = new()
        {
            Id = Guid.NewGuid(),
            Desciption = command.Desciption,
            ResponsibleUser = command.ResponsibleUser,
            EditorUsers = command.EditorUsers,
            IsProcessed = false,
            ProjectId = command.ProjectId
        };

        var createdTodo = await repository.CreateTodo(todo);

        return new CreateTodoResult(createdTodo);
    }

}
