namespace TodoApi.TodoUseCases.GetTodosByTenant;

public record GetTodosByTenantQuery(string tenant);
public record GetTodosByTenantResult(List<Todo> data);

internal sealed class GetTodosByTenantUseCase(ITodoRepository repository) : IGetTodosByTenantUseCase
{
    public async Task<GetTodosByTenantResult> Execute(GetTodosByTenantQuery query)
    {
        var todos = await repository.GetTodosByTenant(query.tenant);

        return new GetTodosByTenantResult(todos);
    }
}
