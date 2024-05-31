namespace TodoApi.TodoUseCases.ProjectDeleted;

public interface IProjectDeletedUseCase
{
    Task<ProjectDeletedResult> Execute(ProjectDeletedCommand command);
}