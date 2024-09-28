
namespace TodoApi.TodoStatusUseCases.GetTodoStatuses
{
    public interface IGetTodoStatusesUseCase
    {
        Task<GetTodoStatusesResult> Execute();
    }
}