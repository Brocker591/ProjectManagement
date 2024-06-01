
namespace NotificationApi.NotificationUseCase.ErrorCreateProjectTodo
{
    public interface IErrorCreateProjectTodoUseCase
    {
        Task<ErrorUpdateProjectResult> Execute(ErrorCreateProjectTodoCommand command);
    }
}