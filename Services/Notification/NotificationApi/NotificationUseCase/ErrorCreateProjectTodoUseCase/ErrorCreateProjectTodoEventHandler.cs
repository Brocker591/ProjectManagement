using Common.MessageEvents;
using MassTransit;

namespace NotificationApi.NotificationUseCase.ErrorCreateProjectTodoUseCase;

public class ErrorCreateProjectTodoEventHandler(IErrorCreateProjectTodoUseCase useCase) : IConsumer<ErrorCreateProjectTodoEvent>
{
    public async Task Consume(ConsumeContext<ErrorCreateProjectTodoEvent> context)
    {
        var command = new ErrorCreateProjectTodoCommand(context.Message.message);

        await useCase.Execute(command);
    }
}

