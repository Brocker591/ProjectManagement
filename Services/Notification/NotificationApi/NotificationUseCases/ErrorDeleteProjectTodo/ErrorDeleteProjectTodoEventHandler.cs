using Common.MessageEvents;
using MassTransit;

namespace NotificationApi.NotificationUseCases.ErrorDeleteProjectTodo;

public class ErrorDeleteProjectTodoEventHandler(IErrorDeleteProjectTodoUseCase useCase) : IConsumer<ErrorDeleteProjectTodoEvent>
{
    public async Task Consume(ConsumeContext<ErrorDeleteProjectTodoEvent> context)
    {
        var command = new ErrorDeleteProjectTodoCommand(context.Message.message);

        await useCase.Execute(command);
    }
}
