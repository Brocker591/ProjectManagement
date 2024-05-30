namespace ProjectApplication.ProjectUseCases.Commands.AddTodoToProject;

public record AddTodoToProjectCommand(CreateProjectTodoEvent createProjectTodoEvent) : ICommand<AddTodoToProjectResult>;
public record AddTodoToProjectResult(bool isSuccess);
