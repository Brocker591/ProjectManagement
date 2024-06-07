using MassTransit;
using MassTransit.Transports;
using ProjectApplication.ProjectUseCases.Commands.DeleteTodoFromProject;

namespace ProjectApplication.ProjectUseCases.EventHandler.Integration;

public class DeleteProjectTodoEventHandler(ISender sender, IPublishEndpoint publishEndpoint, ILogger<DeleteProjectTodoEventHandler> logger) : IConsumer<DeleteProjectTodoEvent>
{
    public async Task Consume(ConsumeContext<DeleteProjectTodoEvent> context)
    {
        try
        {
            DeleteTodoFromProjectCommand command = new(context.Message);
            var result = await sender.Send(command);

            if (result.isSuccess)
            {
                logger.LogInformation("An assigned project task could not be deleted");
            }
            else
            {
                logger.LogInformation("An assigned project task could not be deleted");

                // Notification
                ErrorDeleteProjectTodoEvent errorEvent = new("An assigned project task could not be deleted", context.Message);
                await publishEndpoint.Publish(errorEvent);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, context.Message);
        }
    }
}
