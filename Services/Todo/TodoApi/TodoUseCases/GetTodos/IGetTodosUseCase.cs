namespace TodoApi.TodoUseCases.GetTodos;

public interface IGetTodosUseCase
{
    Task<GetTodosResult> Execute();
}