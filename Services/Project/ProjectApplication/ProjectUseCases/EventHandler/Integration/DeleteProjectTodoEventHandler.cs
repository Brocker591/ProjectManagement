using ProjectApplication.ProjectUseCases.Commands.DeleteTodoFromProject;

namespace ProjectApplication.ProjectUseCases.EventHandler.Integration;

public class DeleteProjectTodoEventHandler(ISender sender, ILogger<DeleteProjectTodoEventHandler> logger) : IConsumer<DeleteProjectTodoEvent>
{
    public async Task Consume(ConsumeContext<DeleteProjectTodoEvent> context)
    {
        try
        {
            DeleteTodoFromProjectCommand command = new(context.Message);
            var result = await sender.Send(command);

            if (result.isSuccess)
            {
                logger.LogInformation("DeleteProjectTodoEvent wurde erfolgreich durchgeführt");

            }
            else
            {
                //TODO Notifikation
                logger.LogError("Ein Fehler ist bei DeleteProjectTodoEvent aufgetreten");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
