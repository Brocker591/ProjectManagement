namespace TodoApi.TodoUseCases.UpdateTodo;

public interface IUpdateTodoUseCase
{
    Task<UpdateTodoResult> Execute(UpdateTodoCommand command);
}