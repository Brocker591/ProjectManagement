
namespace NotificationApi.NotificationEmailUseCases.GetNotificationEmail
{
    public interface IGetNotificationEmailUseCase
    {
        Task<GetNotificationEmailResult> Execute(GetNotificationEmailQuery query);
    }
}