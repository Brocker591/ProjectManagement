
namespace TodoApi.TodoUseCases.GetTodosByProjectId
{
    public interface IGetTodosByProjectIdUseCase
    {
        Task<GetTodosByProjectIdResult> Execute(GetTodosByProjectIdQuery query);
    }
}