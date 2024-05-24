namespace TodoApi.TodoUseCases.CreateTodo;

public interface ICreateTodoUseCase
{
    Task<CreateTodoResult> Execute(CreateTodoCommand command);
}
