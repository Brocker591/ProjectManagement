namespace TodoApi.TodoUseCases.GetTodoStatusList
{
    public interface IGetTodoStatusListUseCase
    {
        Task<GetTodoStatusesResult> Execute();
    }
}