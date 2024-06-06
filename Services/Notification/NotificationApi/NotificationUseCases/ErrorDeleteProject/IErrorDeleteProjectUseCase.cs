
namespace NotificationApi.NotificationUseCases.ErrorDeleteProject
{
    public interface IErrorDeleteProjectUseCase
    {
        Task<ErrorDeleteProjectResult> Execute(ErrorDeleteProjectCommand command);
    }
}