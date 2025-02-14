using Common.Authorization;
using System.Security.Claims;

namespace TodoApi.TodoUseCases.GetTodosByTenant;

internal record TodoDto(Guid Id, string Desciption, int StatusId, Guid ResponsibleUser, List<Guid> EditorUsers, Guid? ProjectId);
internal record ResponseTodosByTenant(List<TodoDto> data);
internal static class GetTodosByTenantEndpoint
{
    public static IEndpointRouteBuilder MapGetTodosByTenantEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/ByTenant", async (IGetTodosByTenantUseCase useCase, ClaimsPrincipal user) =>
        {

            var userAndTenant = user.GetUserIdAndTenant();

            if (userAndTenant is null)
                return Results.Unauthorized();

            GetTodosByTenantQuery query = new(userAndTenant.Value.Value);
            GetTodosByTenantResult result = await useCase.Execute(query);

            List<TodoDto> dtos = result.data.Select(todo => new TodoDto(
                todo.Id,
                todo.Desciption,
                todo.StatusId,
                todo.ResponsibleUser,
                todo.EditorUsers,
                todo.ProjectId))
            .ToList();

            ResponseTodosByTenant response = new(dtos);
            return Results.Ok(response);

        }).WithName("GetTasksByTenant")
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithSummary("Get Tasks by Tenant")
          .WithDescription("Get Tasks by Tenant");

        return routes;
    }
}
