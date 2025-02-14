
namespace TodoApi.TodoUseCases.GetTodosByTenant
{
    internal interface IGetTodosByTenantUseCase
    {
        Task<GetTodosByTenantResult> Execute(GetTodosByTenantQuery query);
    }
}