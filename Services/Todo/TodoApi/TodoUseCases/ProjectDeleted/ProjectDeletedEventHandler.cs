using MassTransit;

namespace TodoApi.TodoUseCases.ProjectDeleted;

public class ProjectDeletedEventHandler(IProjectDeletedUseCase useCase, ILogger<ProjectDeletedEventHandler> logger) : IConsumer<DeleteProjectEvent>
{
    public async Task Consume(ConsumeContext<DeleteProjectEvent> context)
    {
		try
		{
			ProjectDeletedCommand command = new(context.Message);
            var result = await useCase.Execute(command);
            if (result.isSuccess)
            {
                logger.LogInformation("DeleteProjectEvent wurde erfolgreich durchgeführt");
            }
            else
            {
                //TODO Notifikation
                logger.LogError("Ein Fehler ist bei DeleteProjectEvent aufgetreten");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
