using MassTransit;

namespace NotificationApi.NotificationUseCases.ErrorDeleteProject;

public class ErrorDeleteProjectEventHandler(IErrorDeleteProjectUseCase useCase) : IConsumer<ErrorDeleteProjectEvent>
{
    public async Task Consume(ConsumeContext<ErrorDeleteProjectEvent> context)
    {
        var command = new ErrorDeleteProjectCommand(context.Message.message, context.Message.eventObject);
        await useCase.Execute(command);
    }

}
