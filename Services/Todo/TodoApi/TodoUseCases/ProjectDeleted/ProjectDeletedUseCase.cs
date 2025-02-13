namespace TodoApi.TodoUseCases.ProjectDeleted;

public record ProjectDeletedCommand(DeleteProjectEvent deleteProjectEvent);
public record ProjectDeletedResult(bool isSuccess);


internal sealed class ProjectDeletedUseCase(ITodoRepository repository, IGetTodosByProjectIdUseCase getTodoByProject) : IProjectDeletedUseCase
{
    public async Task<ProjectDeletedResult> Execute(ProjectDeletedCommand command)
    {
        GetTodosByProjectIdQuery query = new(command.deleteProjectEvent.Id);

        GetTodosByProjectIdResult result = await getTodoByProject.Execute(query);

        foreach (Todo item in result.data)
        {
            await repository.DeleteTodo(item.Id);
        }

        return new ProjectDeletedResult(true);
    }
}