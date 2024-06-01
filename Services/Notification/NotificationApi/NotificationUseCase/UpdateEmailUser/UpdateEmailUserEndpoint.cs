using FluentValidation.Results;

namespace NotificationApi.NotificationUseCase.UpdateEmailUser;

public static class UpdateEmailUserEndpoint
{
    public static IEndpointRouteBuilder MapUpdateEmailUserEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/EmailUsers", async (EmailUser emailUser, IUpdateEmailUserUseCase useCase, IValidator<EmailUser> validator) =>
        {
            try
            {
                //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
                ValidationResult validationResult = await validator.ValidateAsync(emailUser);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());


                UpdateEmailUserCommand command = new(emailUser);

                await useCase.Execute(command);

                return Results.NoContent();
            }
            catch (EmailUserNotFoundException exc)
            {
                return Results.NotFound(exc.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "EmailUsers could not be updated", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("UpdateEmailUsers")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Update EmailUsers")
        .WithDescription("Update EmailUsers");

        return routes;
    }
}
