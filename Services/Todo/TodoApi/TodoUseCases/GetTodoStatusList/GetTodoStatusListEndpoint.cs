namespace TodoApi.TodoUseCases.GetTodoStatusList;


public record ResponseTodoStatus(List<TodoStatus> data);
public static class GetTodoStatusListEndpoint
{
    public static IEndpointRouteBuilder MapGetTodoStatusListEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/statuslist", async (IGetTodoStatusListUseCase useCase) =>
        {

                GetTodoStatusesResult result = await useCase.Execute();

                ResponseTodoStatus response = new(result.data);
                return Results.Ok(response);

        }).WithName("GetTaskStatuslist")
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithSummary("Get Task Statuslist")
          .WithDescription("Get Statuslist");

        return routes;
    }
}

