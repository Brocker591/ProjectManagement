namespace ProjectApplication.ProjectUseCases.Commands.CreateProject;

public record CreateProjectCommand(ProjectCreateDto projectDto, string userName, string tenant) : ICommand<CreateProjectResult>;
public record CreateProjectResult(Project data);