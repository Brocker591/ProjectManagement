namespace TodoApi.TodoUseCases.GetTodoStatusList;

public record GetTodoStatusesResult(List<TodoStatus> data);
public class GetTodoStatusListUseCase(ITodoStatusRepository repository) : IGetTodoStatusListUseCase
{
    public async Task<GetTodoStatusesResult> Execute()
    {
        var todoStatuses = await repository.GetTodoStatusList();

        return new GetTodoStatusesResult(todoStatuses);
    }
}
