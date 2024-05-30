using ProjectDomain.Events;

namespace ProjectApplication.ProjectUseCases.EventHandler.Domain;

public class ProjectDeleteEventHandler(IPublishEndpoint publishEndpoint, ILogger<ProjectDeleteEventHandler> logger) : INotificationHandler<ProjectDeletedEvent>
{
    public async Task Handle(ProjectDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event ProjectDeleteEvent wurde ausgelöst");


        DeleteProjectEvent deleteProjectEvent = new DeleteProjectEvent() { Id = domainEvent.projectId };
        await publishEndpoint.Publish(deleteProjectEvent, cancellationToken);
    }
}
