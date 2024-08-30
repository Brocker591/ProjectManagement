namespace NotificationApi.NotificationEmailUseCases.GetNotificationEmail;

public record ResponseNotificationEmail(NotificationEmail data);
public static class GetNotificationEmailEndpoint
{
    public static IEndpointRouteBuilder MapGetNotificationEmailEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("NotificationEmails/{id}", async (Guid id, IGetNotificationEmailUseCase useCase) =>
        {
            try
            {
                GetNotificationEmailQuery query = new(id);
                GetNotificationEmailResult result = await useCase.Execute(query);

                ResponseNotificationEmail response = new(result.data);
                return Results.Ok(response);
            }
            catch (NotificationEmailNotFoundException exc)
            {
                return Results.NotFound(exc.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "Notification Email could not be read", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("GetNotificationEmail")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get NotificationEmail")
        .WithDescription("Get NotificationEmail")
        .RequireAuthorization();

        return routes;
    }
}
