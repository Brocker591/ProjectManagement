namespace NotificationApi.Repositories;

public class NotificationEmailRepository(NotificationContext dbContect) : INotificationEmailRepository
{
    public async Task<NotificationEmail> CreateNotificationEmail(NotificationEmail notificationEmail)
    {
        if (notificationEmail.Id == Guid.Empty)
            notificationEmail.Id = Guid.NewGuid();

        dbContect.NotificationEmails.Add(notificationEmail);
        await dbContect.SaveChangesAsync();
        return notificationEmail;
    }

    public async Task<List<NotificationEmail>> GetNotificationEmails()
    {
        var notificationEmails = await dbContect.NotificationEmails.ToListAsync();
        return notificationEmails;
    }

    public async Task<NotificationEmail> GetNotificationEmail(Guid id)
    {
        var notificationEmail = await dbContect.NotificationEmails.FirstOrDefaultAsync(x => x.Id == id);
        if (notificationEmail == null)
            throw new NotificationEmailNotFoundException(id);
        return notificationEmail;
    }

    public async Task UpdateNotificationEmail(NotificationEmail notificationEmail)
    {
        var existingNotificationEmail = await GetNotificationEmail(notificationEmail.Id);

        existingNotificationEmail.Email = notificationEmail.Email;

        dbContect.NotificationEmails.Update(existingNotificationEmail);
        await dbContect.SaveChangesAsync();
    }

    public async Task DeleteNotificationEmail(Guid id)
    {
        var existingNotificationEmail = await GetNotificationEmail(id);
        dbContect.NotificationEmails.Remove(existingNotificationEmail);
        await dbContect.SaveChangesAsync();
    }
}
