namespace ProjectApplication.ProjectUseCases.Commands.CreateProject;

public record CreateProjectCommand(ProjectCreateDto projectDto, string userName) : ICommand<CreateProjectResult>;
public record CreateProjectResult(Project data);