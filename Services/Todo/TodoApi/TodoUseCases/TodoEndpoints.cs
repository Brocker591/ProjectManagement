using TodoApi.TodoUseCases.GetTodosByTenant;
using TodoApi.TodoUseCases.GetTodoStatusList;

namespace TodoApi.TodoUseCases;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tasks");

        group.MapCreateTodoEndpoint();
        group.MapUpdateTodoEndpoint();
        group.MapDeleteTodoEndpoint();
        group.MapGetTodoEndpoint();
        group.MapGetTodosEndpoint();
        group.MapGetTodosByProjectIdEndpoint();
        group.MapGetTodoStatusListEndpoint();
        group.MapGetTodosByTenantEndpoint();
    }
}
