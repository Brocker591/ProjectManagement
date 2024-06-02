
namespace NotificationApi.NotificationEmailUseCases.GetNotificationEmails
{
    public interface IGetNotificationEmailsUseCase
    {
        Task<GetNotificationEmailsResult> Execute();
    }
}