using FluentValidation.Results;

namespace NotificationApi.NotificationUseCase.UpdateEmailUser;

public static class UpdateEmailUserEndpoint
{
    public static IEndpointRouteBuilder MapUpdateEmailUserEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/tasks", async (EmailUser emailUser, IUpdateEmailUserUseCase useCase, IValidator<EmailUser> validator) =>
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
            catch (EmailUserNotFoundException todoEx)
            {
                return Results.NotFound(todoEx.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "Task could not be updated", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
.WithName("UpdateTasks")
.ProducesProblem(StatusCodes.Status500InternalServerError)
.WithSummary("Update Tasks")
.WithDescription("Update Tasks");

        return routes;
    }
}
