namespace ProjectApplication.ProjectUseCases.Commands.AddTodoToProject;

public record AddTodoToProjectCommand(CreateProjectTodoEvent projectTodo) : ICommand<AddTodoToProjectResult>;
public record AddTodoToProjectResult(bool isSuccess);
