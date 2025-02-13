namespace TodoApi.TodoUseCases.GetTodo;

internal record ResponseTodo(Guid Id, string Desciption, int StatusId, Guid ResponsibleUser, List<Guid> EditorUsers, Guid? ProjectId);
internal static class GetTodoEndpoint
{
    public static IEndpointRouteBuilder MapGetTodoEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/{id}", async (Guid id, IGetTodoUseCase useCase) =>
        {
            GetTodoQuery query = new(id);
            GetTodoResult result = await useCase.Execute(query);

            ResponseTodo response = new(
                result.data.Id, 
                result.data.Desciption, 
                result.data.StatusId, 
                result.data.ResponsibleUser, 
                result.data.EditorUsers, 
                result.data.ProjectId);

            return Results.Ok(response);
        })
        .WithName("GetTask")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get Task")
        .WithDescription("Get Task");

        return routes;
    }
}
