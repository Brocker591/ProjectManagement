using Common.MessageEvents;
using MassTransit;

namespace NotificationApi.NotificationUseCases.ErrorCreateProjectTodo;

public class ErrorCreateProjectTodoEventHandler(IErrorCreateProjectTodoUseCase useCase) : IConsumer<ErrorCreateProjectTodoEvent>
{
    public async Task Consume(ConsumeContext<ErrorCreateProjectTodoEvent> context)
    {
        var command = new ErrorCreateProjectTodoCommand(context.Message.message, context.Message.eventObject);

        await useCase.Execute(command);
    }
}

