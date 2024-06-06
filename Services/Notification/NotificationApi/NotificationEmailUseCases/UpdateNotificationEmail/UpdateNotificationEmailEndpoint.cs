using FluentValidation.Results;

namespace NotificationApi.NotificationEmailUseCases.UpdateNotificationEmail;

public record NotificationEmailDto(Guid Id, string Email);


public static class UpdateNotificationEmailEndpoint
{
    public static IEndpointRouteBuilder MapUpdateNotificationEmailEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/NotificationEmails", async (NotificationEmailDto notificationEmailDto, IUpdateNotificationEmailUseCase useCase, IValidator<NotificationEmailDto> validator) =>
        {
            try
            {
                //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
                ValidationResult validationResult = await validator.ValidateAsync(notificationEmailDto);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var command = new UpdateNotificationEmailCommand(
                    new NotificationEmail
                    {
                        Id = notificationEmailDto.Id,
                        Email = notificationEmailDto.Email
                    });

                UpdateNotificationEmailResult result = await useCase.Execute(command);

                return Results.NoContent();

            }
            catch (NotificationEmailNotFoundException exc)
            {
                return Results.NotFound(exc.Message);
            }
            catch (Exception ex)
            {

                return Results.Problem(detail: "EmailUser could not be created", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("UpdateNotificationEmails")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Update NotificationEmails")
        .WithDescription("Update NotificationEmails");

        return routes;

    
    }
}
