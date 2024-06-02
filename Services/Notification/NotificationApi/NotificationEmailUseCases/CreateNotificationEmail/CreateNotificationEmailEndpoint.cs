using FluentValidation.Results;

namespace NotificationApi.NotificationEmailUseCases.CreateNotificationEmail;

public record CreateNotificationEmailDto(string Email);
public record CreateNotificationEmailResponse(NotificationEmail data);

public static class CreateNotificationEmailEndpoint
{
    public static IEndpointRouteBuilder MapCreateNotificationEmailEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/NotificationEmails", async (CreateNotificationEmailDto createNotificationEmailDto, ICreateNotificationEmailUseCase useCase, IValidator<CreateNotificationEmailDto> validator) =>
        {
            try
            {
                //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
                ValidationResult validationResult = await validator.ValidateAsync(createNotificationEmailDto);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var command = new CreateNotificationEmailCommand(new MailAddress(createNotificationEmailDto.Email));

                CreateNotificationEmailResult result = await useCase.Execute(command);

                CreateNotificationEmailResponse response = new(result.data);

                return Results.Created($"/NotificationEmails/{response.data.Id}", response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "Notification Email could not be created", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("CreateNotificationEmail")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Create NotificationEmail")
        .WithDescription("Create NotificationEmail");

        return routes;
    }
}
