namespace NotificationApi.NotificationUseCase.GetEmailUser;

public record ResponseEmailUser(EmailUser data);

public static class GetEmailUserEndpoint
{
    public static IEndpointRouteBuilder MapGetTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/EmailUsers/{id}", async (Guid id, IGetEmailUserUseCase useCase) =>
        {
            try
            {
                GetEmailUserQuery query = new(id);
                GetEmailUserResult result = await useCase.Execute(query);

                ResponseEmailUser response = new(result.data);
                return Results.Ok(response);
            }
            catch (EmailUserNotFoundException exc)
            {
                return Results.NotFound(exc.Message);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("GetEmailUsers")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get EmailUsers")
        .WithDescription("Get EmailUsers");

        return routes;
    }
}
