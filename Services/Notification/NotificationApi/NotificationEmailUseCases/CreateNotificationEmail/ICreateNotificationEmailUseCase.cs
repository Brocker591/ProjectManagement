
namespace NotificationApi.NotificationEmailUseCases.CreateNotificationEmail
{
    public interface ICreateNotificationEmailUseCase
    {
        Task<CreateNotificationEmailResult> Execute(CreateNotificationEmailCommand command);
    }
}