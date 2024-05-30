namespace ProjectApplication.ProjectUseCases.Commands.DeleteTodoFromProject;

public class DeleteTodoFromProjectHandler(IProjectRepositories repository) : ICommandHandler<DeleteTodoFromProjectCommand, DeleteTodoFromProjectResult>
{
    public async Task<DeleteTodoFromProjectResult> Handle(DeleteTodoFromProjectCommand command, CancellationToken cancellationToken)
    {
        var existingProcjet = await repository.GetProject(command.deleteProjectTodoEvent.ProjectId);

        if (existingProcjet is null)
            return new DeleteTodoFromProjectResult(true);

        existingProcjet.Tasks.Remove(command.deleteProjectTodoEvent.Id);

        await repository.UpdateProject(existingProcjet);

        return new DeleteTodoFromProjectResult(true);
    }
}
