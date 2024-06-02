namespace NotificationApi.EmailUserUseCases.DeleteEmailUser;

public static class DeleteEmailUserEndpoint
{
    public static IEndpointRouteBuilder MapDeleteEmailUserEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapDelete("/EmailUsers/{id}", async (Guid id, IDeleteEmailUserUseCase useCase) =>
        {
            try
            {
                DeleteEmailUserCommand command = new(id);
                await useCase.Execute(command);

                return Results.NoContent();
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
        .WithName("DeleteEmailUsers")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete EmailUsers")
        .WithDescription("Delete EmailUsers");

        return routes;
    }
}
