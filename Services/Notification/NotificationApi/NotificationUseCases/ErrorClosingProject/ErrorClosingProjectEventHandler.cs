using MassTransit;
using NotificationApi.NotificationUseCases.ErrorDeleteProject;

namespace NotificationApi.NotificationUseCases.ErrorClosingProject;

public class ErrorClosingProjectEventHandler(IErrorClosingProjectUseCase useCase) : IConsumer<ErrorClosingProjectEvent>
{
    public async Task Consume(ConsumeContext<ErrorClosingProjectEvent> context)
    {
        var command = new ErrorClosingProjectCommand(context.Message.message, context.Message.eventObject);
        await useCase.Execute(command);
    }
}
