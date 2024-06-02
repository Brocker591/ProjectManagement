namespace NotificationApi.NotificationEmailUseCases.GetNotificationEmails;

public record ResponseNotificationEmails(List<NotificationEmail> data);
public static class GetNotificationEmailsEndpoint
{
    public static IEndpointRouteBuilder MapGetNotificationEmailsEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/NotificationEmails", async (IGetNotificationEmailsUseCase useCase) =>
        {
			try
			{
				GetNotificationEmailsResult result = await useCase.Execute();

                ResponseNotificationEmails response = new(result.data);
                return Results.Ok(response);

            }
			catch (Exception ex)
			{
                return Results.Problem(detail: "could not read NotificationEmails table", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("GetNotificationEmails")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get NotificationEmails")
        .WithDescription("Get NotificationEmails");

        return routes;
    }
}
