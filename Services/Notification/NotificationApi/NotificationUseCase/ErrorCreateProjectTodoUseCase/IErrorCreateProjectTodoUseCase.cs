
namespace NotificationApi.NotificationUseCase.ErrorCreateProjectTodoUseCase
{
    public interface IErrorCreateProjectTodoUseCase
    {
        Task<ErrorUpdateProjectResult> Execute(ErrorCreateProjectTodoCommand command);
    }
}