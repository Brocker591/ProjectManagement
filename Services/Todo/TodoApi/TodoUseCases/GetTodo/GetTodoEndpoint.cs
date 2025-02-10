namespace TodoApi.TodoUseCases.GetTodo;

public record ResponseTodo(Todo data);
public static class GetTodoEndpoint
{
    public static IEndpointRouteBuilder MapGetTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/{id}", async (Guid id, IGetTodoUseCase useCase) =>
        {
            GetTodoQuery query = new(id);
            GetTodoResult result = await useCase.Execute(query);

            ResponseTodo response = new(result.data);
            return Results.Ok(response);
        })
        .WithName("GetTask")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get Task")
        .WithDescription("Get Task");

        return routes;
    }
}
