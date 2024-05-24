namespace TodoApi.TodoUseCases.GetTodo;

public record ResponseTodo(Todo data);
public static class GetTodoEndpoint
{
    public static IEndpointRouteBuilder MapGetTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/tasks/{id}", async (Guid id, IGetTodoUseCase useCase) =>
        {
            try
            {
                GetTodoQuery query = new(id);
                GetTodoResult result = await useCase.Execute(query);

                ResponseTodo response = new(result.data);
                return Results.Ok(response);
            }
            catch(TodoNotFoundException todoEx)
            {
                return Results.NotFound(todoEx.Message);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("GetTask")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get Task")
        .WithDescription("Get Task");

        return routes;
    }
}
