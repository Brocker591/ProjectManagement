namespace TodoApi.TodoStatusUseCases.GetTodoStatuses;


public record ResponseTodoStatus(List<TodoStatus> data);
public static class GetTodoStatusesEndpoint
{
    public static IEndpointRouteBuilder MapGetTodoStatuesEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/tasks/statuslist", async (IGetTodoStatusesUseCase useCase) =>
        {
            try
            {
                GetTodoStatusesResult result = await useCase.Execute();

                ResponseTodoStatus response = new(result.data);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "could not read TaskStatus table", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
    .WithName("GetTaskStatuslist")
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Get Task Statuslist")
    .WithDescription("Get Statuslist")
    .RequireAuthorization();

        return routes;
    }
}

