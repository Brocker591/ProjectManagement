namespace NotificationApi.NotificationUseCase.GetEmailUsers;

public record ResponseEmailUsers(List<EmailUser> data);
public static class GetEmailUsersEndpoint
{
    public static IEndpointRouteBuilder MapGetTodosEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/EmailUsers", async (IGetEmailUsersUseCase useCase) =>
        {
            try
            {
                GetEmailUsersResult result = await useCase.Execute();

                ResponseEmailUsers response = new(result.data);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "could not read EmailUsers table", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
    .WithName("GetEmailUsers")
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Get EmailUsers")
    .WithDescription("Get EmailUsers");

        return routes;
    }
}
