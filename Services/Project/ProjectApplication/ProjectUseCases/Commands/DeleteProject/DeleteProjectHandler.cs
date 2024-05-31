using ProjectDomain.Events;

namespace ProjectApplication.ProjectUseCases.Commands.DeleteProject;

public class DeleteProjectHandler(IProjectRepositories repositories, IMediator mediator) : ICommandHandler<DeleteProjectCommand, DeleteProjectResult>
{
    public async Task<DeleteProjectResult> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        await repositories.DeleteProject(command.id);


        ProjectDeletedEvent domainEvent = new ProjectDeletedEvent(command.id);
        await mediator.Publish(domainEvent, cancellationToken);

        return new DeleteProjectResult(true);
    }
}
