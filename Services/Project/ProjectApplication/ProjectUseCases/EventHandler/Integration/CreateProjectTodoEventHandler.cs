using ProjectApplication.ProjectUseCases.Commands.AddTodoToProject;

namespace ProjectApplication.ProjectUseCases.EventHandler.Integration;

public class CreateProjectTodoEventHandler(ISender sender, IPublishEndpoint publishEndpoint, ILogger<CreateProjectTodoEventHandler> logger) : IConsumer<CreateProjectTodoEvent>
{
    public async Task Consume(ConsumeContext<CreateProjectTodoEvent> context)
    {
        try
        {
            AddTodoToProjectCommand command = new(context.Message);
            var result = await sender.Send(command);

            if (result.isSuccess)
            {
                logger.LogInformation("CreateProjectTodoEvent wurde erfolgreich durchgeführt");
            }
            else
            {
                // Notification
                ErrorCreateProjectTodoEvent errorEvent = new("task could not be assigned to a project");
                await publishEndpoint.Publish(errorEvent);
            }
        }
        catch (Exception ex)
        {
            // Notification
            ErrorCreateProjectTodoEvent errorEvent = new(ex.Message);
            await publishEndpoint.Publish(errorEvent);
        }
    }
}
