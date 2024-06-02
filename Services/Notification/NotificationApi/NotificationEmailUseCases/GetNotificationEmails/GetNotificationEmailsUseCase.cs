namespace NotificationApi.NotificationEmailUseCases.GetNotificationEmails;

public record GetNotificationEmailsResult(List<NotificationEmail> data);
public class GetNotificationEmailsUseCase(INotificationEmailRepository repository) : IGetNotificationEmailsUseCase
{
    public async Task<GetNotificationEmailsResult> Execute()
    {
        var notificationEmails = await repository.GetNotificationEmails();
        return new GetNotificationEmailsResult(notificationEmails);
    }
}
