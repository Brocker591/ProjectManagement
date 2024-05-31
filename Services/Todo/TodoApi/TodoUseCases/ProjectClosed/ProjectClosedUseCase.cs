namespace TodoApi.TodoUseCases.ProjectClosed;

public record ProjectClosedCommand(ClosingProjectEvent closingProjectEvent);
public record ProjectClosedResult(bool isSuccess);

public class ProjectClosedUseCase(ITodoRepository repository, IGetTodosByProjectIdUseCase getTodoByProject) : IProjectClosedUseCase
{
    public async Task<ProjectClosedResult> Execute(ProjectClosedCommand command)
    {
        GetTodosByProjectIdQuery query = new(command.closingProjectEvent.Id);

        GetTodosByProjectIdResult result = await getTodoByProject.Execute(query);

        foreach (Todo item in result.data)
        {
            item.IsProcessed = true;
            await repository.UpdateTodo(item);
        }

        return new ProjectClosedResult(true);
    }

}
