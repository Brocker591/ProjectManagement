using MassTransit;

namespace TodoApi.TodoUseCases.DeleteTodo;

public record DeleteTodoCommand(Guid id);
public record DeleteTodoResult();

internal sealed class DeleteTodoUseCase(ITodoRepository repository, IPublishEndpoint publishEndpoint) : IDeleteTodoUseCase
{
    public async Task<DeleteTodoResult> Execute(DeleteTodoCommand command)
    {
        Guid? projectId = (await repository.GetTodo(command.id)).ProjectId;
        await repository.DeleteTodo(command.id);


        if (projectId is not null)
        {
            DeleteProjectTodoEvent deleteProjectTodoEvent = new() { Id = command.id, ProjectId = (Guid)projectId };
            await publishEndpoint.Publish(deleteProjectTodoEvent);
        }
        return new DeleteTodoResult();
    }
}
