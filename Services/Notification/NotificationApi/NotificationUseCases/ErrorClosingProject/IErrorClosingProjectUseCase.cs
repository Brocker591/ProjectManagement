
namespace NotificationApi.NotificationUseCases.ErrorClosingProject
{
    public interface IErrorClosingProjectUseCase
    {
        Task<ErrorClosingProjectResult> Execute(ErrorClosingProjectCommand command);
    }
}