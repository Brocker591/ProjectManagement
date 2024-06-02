
namespace NotificationApi.NotificationUseCases.ErrorCreateProjectTodo
{
    public interface IErrorCreateProjectTodoUseCase
    {
        Task<ErrorUpdateProjectResult> Execute(ErrorCreateProjectTodoCommand command);
    }
}