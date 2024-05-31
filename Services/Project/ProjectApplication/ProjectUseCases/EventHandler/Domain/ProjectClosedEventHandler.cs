namespace ProjectApplication.ProjectUseCases.EventHandler.Domain;

public class ProjectClosedEventHandler(IPublishEndpoint publishEndpoint, ILogger<ProjectDeleteEventHandler> logger) : INotificationHandler<ProjectClosingEvent>
{
    public async Task Handle(ProjectClosingEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event ProjectDeleteEvent wurde ausgelöst");

        ClosingProjectEvent messageObject = new(domainEvent.projectId);

        await publishEndpoint.Publish(messageObject, cancellationToken);
    }
}
