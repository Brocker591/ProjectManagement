namespace TodoApi.TodoStatusUseCases.GetTodoStatuses;

public record GetTodoStatusesResult(List<TodoStatus> data);
public class GetTodoStatusesUseCase(ITodoStatusRepository repository) : IGetTodoStatusesUseCase
{
    public async Task<GetTodoStatusesResult> Execute()
    {
        var todoStatuses = await repository.GetTodoStatuses();

        return new GetTodoStatusesResult(todoStatuses);
    }
}
