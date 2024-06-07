
namespace NotificationApi.NotificationUseCases.SendNotification
{
    public interface ISendNotificationUseCase
    {
        Task<SendNotificationResult> Execute(SendNotificationCommand command);
    }
}