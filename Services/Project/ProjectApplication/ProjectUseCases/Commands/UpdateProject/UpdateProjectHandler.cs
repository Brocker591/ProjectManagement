namespace ProjectApplication.ProjectUseCases.Commands.UpdateProject;

public class UpdateProjectHandler(IProjectRepositories repository, IMediator mediator) : ICommandHandler<UpdateProjectCommand, UpdateProjectResult>
{
    public async Task<UpdateProjectResult> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = command.ProjectDto.AsModel();

        await repository.UpdateProject(project);

        if (project.IsClosed)
        {
            ProjectClosingEvent domainEvent = new ProjectClosingEvent(project.Id);
            await mediator.Publish(domainEvent, cancellationToken);
        }

        return new UpdateProjectResult(true);
    }
}

