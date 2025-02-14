using Common.Keycloak;
using System.Linq;

namespace TodoApi.TodoUseCases.GetTodos;

internal record TodoDto(Guid Id, string Desciption, int StatusId, Guid ResponsibleUser, List<Guid> EditorUsers, Guid? ProjectId);
internal record ResponseTodos(List<TodoDto> data);
internal static class GetTodosEndpoint
{
    public static IEndpointRouteBuilder MapGetTodosEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/", async (IGetTodosUseCase useCase) =>
        {
            GetTodosResult result = await useCase.Execute();

            List<TodoDto> dtos = result.data.Select(todo => 
            new TodoDto(
                todo.Id, 
                todo.Desciption, 
                todo.StatusId, 
                todo.ResponsibleUser,
                todo.EditorUsers,
                todo.ProjectId))
            .ToList();

            ResponseTodos response = new(dtos);

            return Results.Ok(response);

        }).WithName("GetTasks")
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithSummary("Get Tasks")
          .WithDescription("Get Tasks")
          .RequireAuthorization(Policies.AdminAccess);

        return routes;
    }
}
