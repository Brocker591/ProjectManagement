namespace TodoApi.TodoUseCases.DeleteTodo;

public static class DeleteTodoEndpoint
{
    public static IEndpointRouteBuilder MapDeleteTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapDelete("/{id}", async (Guid id, IDeleteTodoUseCase useCase) =>
        {
            DeleteTodoCommand command = new(id);
            await useCase.Execute(command);

            return Results.NoContent();
        })
        .WithName("DeleteTask")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete Task")
        .WithDescription("Delete Task");

        return routes;
    }
}
