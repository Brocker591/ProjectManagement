namespace ProjectApplication.ProjectUseCases.Commands.UpdateProject;

public record UpdateProjectCommand(ProjectDto ProjectDto) : ICommand<UpdateProjectResult>;
public record UpdateProjectResult(bool isSuccess);
