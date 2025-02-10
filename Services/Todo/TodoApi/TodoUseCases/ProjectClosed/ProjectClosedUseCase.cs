namespace TodoApi.TodoUseCases.ProjectClosed;

public record ProjectClosedCommand(ClosingProjectEvent closingProjectEvent);
public record ProjectClosedResult(bool isSuccess);

public class ProjectClosedUseCase(ITodoRepository repository, ITodoStatusRepository todoStatusRepository, IGetTodosByProjectIdUseCase getTodoByProject) : IProjectClosedUseCase
{
    public async Task<ProjectClosedResult> Execute(ProjectClosedCommand command)
    {
        GetTodosByProjectIdQuery query = new(command.closingProjectEvent.Id);

        GetTodosByProjectIdResult result = await getTodoByProject.Execute(query);

        foreach (Todo item in result.data)
        {
            var todoStatuses = await todoStatusRepository.GetTodoStatusList();
            item.StatusId = todoStatuses.FirstOrDefault(x => x.Name == TodoStatus.Done)!.Id;
            await repository.UpdateTodo(item);
        }

        return new ProjectClosedResult(true);
    }

}
