namespace TodoApi.TodoUseCases.GetTodos;

public record ResponseTodos(List<Todo> data);
public static class GetTodosEndpoint
{
    public static IEndpointRouteBuilder MapGetTodosEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/tasks", async (IGetTodosUseCase useCase) =>
        {
            try
            {
                GetTodosResult result = await useCase.Execute();

                ResponseTodos response = new(result.data);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "could not read Task table", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
    .WithName("GetTasks")
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Get Tasks")
    .WithDescription("Get Tasks")
    .RequireAuthorization();

        return routes;
    }
}
