namespace TodoApi.TodoUseCases.ProjectClosed;

public interface IProjectClosedUseCase
{
    Task<ProjectClosedResult> Execute(ProjectClosedCommand command);
}
