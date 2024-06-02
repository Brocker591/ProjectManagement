
namespace NotificationApi.NotificationEmailUseCases.DeleteNotificationEmail
{
    public interface IDeleteNotificationEmailUseCase
    {
        Task<DeleteNotificationEmailResult> Execute(DeleteNotificationEmailCommand command);
    }
}