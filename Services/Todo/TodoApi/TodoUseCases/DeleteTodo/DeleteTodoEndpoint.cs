namespace TodoApi.TodoUseCases.DeleteTodo;

public static class DeleteTodoEndpoint
{
    public static IEndpointRouteBuilder MapDeleteTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapDelete("/tasks/{id}", async (Guid id, IDeleteTodoUseCase useCase) =>
        {
            try
            {
                DeleteTodoCommand command = new(id);
                await useCase.Execute(command);

                return Results.NoContent();
            }
            catch (TodoNotFoundException todoEx)
            {
                return Results.NotFound(todoEx.Message);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("DeleteTask")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete Task")
        .WithDescription("Delete Task");

        return routes;
    }
}
