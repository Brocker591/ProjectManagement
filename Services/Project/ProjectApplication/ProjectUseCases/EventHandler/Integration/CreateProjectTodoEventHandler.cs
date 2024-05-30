using ProjectApplication.ProjectUseCases.Commands.AddTodoToProject;

namespace ProjectApplication.ProjectUseCases.EventHandler.Integration;

public class CreateProjectTodoEventHandler(ISender sender, ILogger<CreateProjectTodoEventHandler> logger) : IConsumer<CreateProjectTodoEvent>
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
                //TODO Notifikation
                logger.LogError("Ein Fehler ist bei CreateProjectTodoEvent aufgetreten");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
