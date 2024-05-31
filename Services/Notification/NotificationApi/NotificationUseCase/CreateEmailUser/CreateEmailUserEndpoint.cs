using FluentValidation.Results;

namespace NotificationApi.NotificationUseCase.CreateEmailUser;

public record CreateEmailUserDto(string Smtp_Username, string Smtp_Password, string Host, int Port, bool EnableSsl);
public record CreateEmailUserResponse(EmailUser data);

public static class CreateEmailUserEndpoint
{
    public static IEndpointRouteBuilder MapCreateEmailUserEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/EmailUsers", async (CreateEmailUserDto emailUserDto, ICreateEmailUserUseCase useCase, IValidator<CreateEmailUserDto> validator) =>
        {
            try
            {
                //Minimal Api hat keine Validierung aus diesem Grund wird FluentValidation verwendet
                ValidationResult validationResult = await validator.ValidateAsync(emailUserDto);
                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());


                var command = new CreateEmailUserCommand(emailUserDto.Smtp_Username, emailUserDto.Smtp_Password, emailUserDto.Host, emailUserDto.Port, emailUserDto.EnableSsl);

                CreateTodoResult result = await useCase.Execute(command);

                CreateEmailUserResponse response = new(result.data);

                return Results.Created($"/EmailUsers/{response.data.Id}", response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "EmailUser could not be created", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
    .WithName("CreateEmailUsers")
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithSummary("Create EmailUsers")
    .WithDescription("Create EmailUsers");

        return routes;

    }
}
