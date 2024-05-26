namespace TodoApi.TodoUseCases.GetTodosByProjectId;

public record GetTodosByProjectIdQuery(Guid projectId);
public record GetTodosByProjectIdResult(List<Todo> data);


public class GetTodosByProjectIdUseCase(ITodoRepository repository) : IGetTodosByProjectIdUseCase
{
    public async Task<GetTodosByProjectIdResult> Execute(GetTodosByProjectIdQuery query)
    {
        var todos = await repository.GetTodos();
        var todosByProjectId = todos.Where(x => x.ProjectId == query.projectId).ToList();

        return new GetTodosByProjectIdResult(todosByProjectId);
    }
}