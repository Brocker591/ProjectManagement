
namespace NotificationApi.NotificationEmailUseCases.UpdateNotificationEmail
{
    public interface IUpdateNotificationEmailUseCase
    {
        Task<UpdateNotificationEmailResult> Execute(UpdateNotificationEmailCommand command);
    }
}