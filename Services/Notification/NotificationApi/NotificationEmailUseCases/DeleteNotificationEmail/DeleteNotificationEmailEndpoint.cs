using NotificationApi.EmailUserUseCases.DeleteEmailUser;

namespace NotificationApi.NotificationEmailUseCases.DeleteNotificationEmail;

public static class DeleteNotificationEmailEndpoint
{
    public static IEndpointRouteBuilder MapDeleteNotificationEmailEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapDelete("/NotificationEmails/{id}", async (Guid id, IDeleteNotificationEmailUseCase useCase) =>
        {
            try
            {
                DeleteNotificationEmailCommand command = new(id);
                await useCase.Execute(command);

                return Results.NoContent();

            }
            catch (EmailUserNotFoundException exc)
            {
                return Results.NotFound(exc.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "Notification Email could not be deleted", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("DeleteNotificationEmail")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete NotificationEmail")
        .WithDescription("Delete NotificationEmail");

        return routes;
    }
}
