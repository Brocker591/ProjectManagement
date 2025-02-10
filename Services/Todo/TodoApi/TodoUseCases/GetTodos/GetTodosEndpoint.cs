namespace TodoApi.TodoUseCases.GetTodos;

public record ResponseTodos(List<Todo> data);
public static class GetTodosEndpoint
{
    public static IEndpointRouteBuilder MapGetTodosEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/", async (IGetTodosUseCase useCase) =>
        {
            GetTodosResult result = await useCase.Execute();

            ResponseTodos response = new(result.data);
            return Results.Ok(response);

        }).WithName("GetTasks")
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithSummary("Get Tasks")
          .WithDescription("Get Tasks");

        return routes;
    }
}
