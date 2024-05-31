namespace TodoApi.TodoUseCases.DeleteTodo;

public interface IDeleteTodoUseCase
{
    Task<DeleteTodoResult> Execute(DeleteTodoCommand command);
}