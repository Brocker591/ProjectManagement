namespace NotificationApi.NotificationUseCases.SendNotification;

public record SendNotificationDto(string Subject, string Body);
public record SendNotificationResponse(bool isSending);
public static class SendNotificationEndpoint
{
    public static IEndpointRouteBuilder MapSendNotificationEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/SendNotification", async (SendNotificationDto dto, ISendNotificationUseCase useCase) =>
        {
            try
            {
                var command = new SendNotificationCommand(dto.Subject, dto.Body);

                SendNotificationResult result = await useCase.Execute(command);

                SendNotificationResponse response = new(result.isSending);

                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "Notification could not be send", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("SendNotification")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Send Notification")
        .WithDescription("Send Notification");

        return routes;
    }
}
