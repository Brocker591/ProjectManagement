
namespace ProjectApplication.ProjectUseCases.Commands.AddTodoToProject;

public class AddTodoToProjectHandler(IProjectRepositories repository) : ICommandHandler<AddTodoToProjectCommand, AddTodoToProjectResult>
{
    public async Task<AddTodoToProjectResult> Handle(AddTodoToProjectCommand command, CancellationToken cancellationToken)
    {
        var existingProject = await repository.GetProject(command.createProjectTodoEvent.ProjectId);

        if (existingProject != null)
        {
            existingProject.Tasks.Add(command.createProjectTodoEvent.Id);
            await repository.UpdateProject(existingProject);
            return new AddTodoToProjectResult(true);
        }

        return new AddTodoToProjectResult(false);
    }
}
