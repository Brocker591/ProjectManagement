using MassTransit;
using MassTransit.Transports;

namespace TodoApi.TodoUseCases.ProjectClosed;

public class ProjectClosedEventHandler(
    IProjectClosedUseCase useCase, 
    IPublishEndpoint publishEndpoint, 
    ILogger<ProjectDeletedEventHandler> logger) : IConsumer<ClosingProjectEvent>
{
    public async Task Consume(ConsumeContext<ClosingProjectEvent> context)
    {
        try
        {
            ProjectClosedCommand command = new(context.Message);
            var result = await useCase.Execute(command);
            if (result.isSuccess)
            {
                logger.LogInformation("ProjectClosedEvent wurde erfolgreich durchgeführt");
            }
            else
            {
                //TODO Notifikation
                logger.LogError("Ein Fehler ist bei ProjectClosedEvent aufgetreten");
                // Notification
                ErrorClosingProjectEvent errorEvent = new("An error has occurred with ClosingProjectEvent", context.Message);
                await publishEndpoint.Publish(errorEvent);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            ErrorClosingProjectEvent errorEvent = new($"An error has occurred with ClosingProjectEvent: {ex.Message}", context.Message);
            await publishEndpoint.Publish(errorEvent);
        }
    }
}
