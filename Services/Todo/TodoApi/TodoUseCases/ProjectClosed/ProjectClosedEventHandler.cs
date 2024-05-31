using MassTransit;

namespace TodoApi.TodoUseCases.ProjectClosed;

public class ProjectClosedEventHandler(IProjectClosedUseCase useCase, ILogger<ProjectDeletedEventHandler> logger) : IConsumer<ClosingProjectEvent>
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
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
