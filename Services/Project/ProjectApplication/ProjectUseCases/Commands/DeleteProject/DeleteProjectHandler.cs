namespace ProjectApplication.ProjectUseCases.Commands.DeleteProject;

public class DeleteProjectHandler(IProjectRepositories repositories) : ICommandHandler<DeleteProjectCommand, DeleteProjectResult>
{
    public async Task<DeleteProjectResult> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        await repositories.DeleteProject(command.id);

        return new DeleteProjectResult(true);
    }
}
