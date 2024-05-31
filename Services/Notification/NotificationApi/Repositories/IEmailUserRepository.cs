
namespace NotificationApi.Repositories
{
    public interface IEmailUserRepository
    {
        Task<EmailUser> CreateEmailUser(EmailUser emailUser);
        Task DeleteEmailUser(Guid emailUserId);
        Task<List<EmailUser>> GetEmailUsers();
        Task<EmailUser> GetEmailUser(Guid emailUserId);
        Task UpdateEmailUser(EmailUser emailUser);
    }
}