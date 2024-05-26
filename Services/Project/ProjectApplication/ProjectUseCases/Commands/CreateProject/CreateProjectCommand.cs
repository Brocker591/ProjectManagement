namespace ProjectApplication.ProjectUseCases.Commands.CreateProject;

public record CreateProjectCommand(ProjectCreateDto projectDto) : ICommand<CreateProjectResult>;
public record CreateProjectResult(Project data);