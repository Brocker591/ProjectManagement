namespace ProjectApplication.ProjectUseCases.Commands.UpdateProject;

public record UpdateProjectCommand(ProjectDto ProjectDto, string userName) : ICommand<UpdateProjectResult>;
public record UpdateProjectResult(bool isSuccess);
