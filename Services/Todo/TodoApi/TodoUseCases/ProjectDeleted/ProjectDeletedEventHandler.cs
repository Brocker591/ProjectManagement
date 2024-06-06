using MassTransit;
using MassTransit.Transports;

namespace TodoApi.TodoUseCases.ProjectDeleted;

public class ProjectDeletedEventHandler(IProjectDeletedUseCase useCase, IPublishEndpoint publishEndpoint, ILogger<ProjectDeletedEventHandler> logger) : IConsumer<DeleteProjectEvent>
{
    public async Task Consume(ConsumeContext<DeleteProjectEvent> context)
    {
		try
		{
			ProjectDeletedCommand command = new(context.Message);
            var result = await useCase.Execute(command);
            if (result.isSuccess)
            {
                logger.LogInformation("DeleteProjectEvent was executed successfully");
            }
            else
            {

                logger.LogError("An error has occurred with DeleteProjectEvent");

                // Notification
                ErrorDeleteProjectEvent errorEvent = new("An error has occurred with DeleteProjectEvent", context.Message);
                await publishEndpoint.Publish(errorEvent);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            ErrorDeleteProjectEvent errorEvent = new($"An error has occurred with DeleteProjectEvent: {ex.Message}", context.Message);
            await publishEndpoint.Publish(errorEvent);
        }
    }
}
