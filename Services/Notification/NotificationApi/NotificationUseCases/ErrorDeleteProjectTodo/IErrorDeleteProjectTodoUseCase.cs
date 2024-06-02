
namespace NotificationApi.NotificationUseCases.ErrorDeleteProjectTodo
{
    public interface IErrorDeleteProjectTodoUseCase
    {
        Task<ErrorUpdateProjectResult> Execute(ErrorDeleteProjectTodoCommand command);
    }
}