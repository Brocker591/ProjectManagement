namespace ProjectApplication.ProjectUseCases.Commands.DeleteProject;

public record DeleteProjectCommand(Guid id) : ICommand<DeleteProjectResult>;

public record DeleteProjectResult(bool isSuccess);
