using MassTransit;
using MediatR;
using ProjectApplication.ProjectUseCases.Commands.AddTodoToProject;

namespace ProjectApplication.ProjectUseCases.EventHandler.Integration;

public class CreateProjectTodoEventHandler(ISender sender) : IConsumer<CreateProjectTodoEvent>
{
    public async Task Consume(ConsumeContext<CreateProjectTodoEvent> context)
    {
        AddTodoToProjectCommand command = new(context.Message);
        var result = await sender.Send(command);

        if (!result.isSuccess)
            Console.WriteLine("FEHLER");         //TODO Notifikation
    }
}
