
namespace ProjectApplication.ProjectUseCases.Commands.AddTodoToProject;

public class AddTodoToProjectHandler(IProjectRepositories repository) : ICommandHandler<AddTodoToProjectCommand, AddTodoToProjectResult>
{
    public async Task<AddTodoToProjectResult> Handle(AddTodoToProjectCommand command, CancellationToken cancellationToken)
    {
        var existingProject = await repository.GetProject(command.projectTodo.ProjectId);

        if (existingProject != null)
        {
            existingProject.Tasks.Add(command.projectTodo.Id);
            return new AddTodoToProjectResult(true);
        }

        return new AddTodoToProjectResult(false);
    }
}
