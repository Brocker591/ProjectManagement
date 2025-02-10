namespace TodoApi.TodoUseCases.GetTodosByProjectId;

public record ResponseTodosByProjectId(List<Todo> data);
public static class GetTodosByProjectIdEndpoint
{
    public static IEndpointRouteBuilder MapGetGetTodosByProjectIdEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/ByProjectId/{projectId}", async (Guid projectId, IGetTodosByProjectIdUseCase useCase) =>
        {
                GetTodosByProjectIdQuery query = new(projectId);
                GetTodosByProjectIdResult result = await useCase.Execute(query);

                ResponseTodos response = new(result.data);
                return Results.Ok(response);

        }).WithName("GetTasksByProjectId")
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithSummary("Get Tasks by ProjectId")
          .WithDescription("Get Tasks by ProjectId");

        return routes;
    }
}
