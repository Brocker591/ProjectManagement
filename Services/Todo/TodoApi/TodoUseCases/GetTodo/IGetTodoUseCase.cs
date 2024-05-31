namespace TodoApi.TodoUseCases.GetTodo;

public interface IGetTodoUseCase
{
    Task<GetTodoResult> Execute(GetTodoQuery query);
}