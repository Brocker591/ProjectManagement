namespace NotificationApi.NotificationEmailUseCases.GetNotificationEmail;

public record GetNotificationEmailQuery(Guid id);
public record GetNotificationEmailResult(NotificationEmail data);
public class GetNotificationEmailUseCase(INotificationEmailRepository repository) : IGetNotificationEmailUseCase
{
    public async Task<GetNotificationEmailResult> Execute(GetNotificationEmailQuery query)
    {
        var notificationEmail = await repository.GetNotificationEmail(query.id);
        return new GetNotificationEmailResult(notificationEmail);
    }
}
