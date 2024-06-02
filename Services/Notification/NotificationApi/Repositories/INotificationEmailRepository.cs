namespace NotificationApi.Repositories;

public interface INotificationEmailRepository
{
    Task<NotificationEmail> CreateNotificationEmail(NotificationEmail notificationEmail);
    Task DeleteNotificationEmail(Guid id);
    Task<NotificationEmail> GetNotificationEmail(Guid id);
    Task<List<NotificationEmail>> GetNotificationEmails();
    Task UpdateNotificationEmail(NotificationEmail notificationEmail);
}