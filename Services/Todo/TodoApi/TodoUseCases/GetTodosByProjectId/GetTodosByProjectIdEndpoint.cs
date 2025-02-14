namespace TodoApi.TodoUseCases.GetTodosByProjectId;


internal record TodoDto(Guid Id, string Desciption, int StatusId, Guid ResponsibleUser, List<Guid> EditorUsers, Guid? ProjectId);
internal record ResponseTodosByProjectId(List<TodoDto> data);
internal static class GetTodosByProjectIdEndpoint
{
    public static IEndpointRouteBuilder MapGetTodosByProjectIdEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/ByProjectId/{projectId}", async (Guid projectId, IGetTodosByProjectIdUseCase useCase) =>
        {
            GetTodosByProjectIdQuery query = new(projectId);
            GetTodosByProjectIdResult result = await useCase.Execute(query);

            List<TodoDto> dtos = result.data.Select(todo => new TodoDto(
                todo.Id,
                todo.Desciption,
                todo.StatusId,
                todo.ResponsibleUser,
                todo.EditorUsers,
                todo.ProjectId))
            .ToList();

            ResponseTodosByProjectId response = new(dtos);
            return Results.Ok(response);

        }).WithName("GetTasksByProjectId")
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithSummary("Get Tasks by ProjectId")
          .WithDescription("Get Tasks by ProjectId");

        return routes;
    }
}
