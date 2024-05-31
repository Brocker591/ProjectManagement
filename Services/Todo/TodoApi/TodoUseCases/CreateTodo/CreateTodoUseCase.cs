using MassTransit;

namespace TodoApi.TodoUseCases.CreateTodo;

public record CreateTodoCommand(string Desciption, Guid? ResponsibleUser, List<Guid>? EditorUsers, Guid? ProjectId);
public record CreateTodoResult(Todo data);

public class CreateTodoUseCase(ITodoRepository repository, IPublishEndpoint publishEndpoint) : ICreateTodoUseCase
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

        if(createdTodo.ProjectId != null &&  createdTodo.ProjectId != Guid.Empty)
        {
            CreateProjectTodoEvent projectTodo = new() { Id = createdTodo.Id, ProjectId = (Guid)createdTodo.ProjectId };
            await publishEndpoint.Publish(projectTodo);
        }
            
        return new CreateTodoResult(createdTodo);
    }
}
