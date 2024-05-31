namespace NotificationApi.Repositories;

public class EmailUserRepository(NotificationContext dbContext) : IEmailUserRepository
{
    public async Task<EmailUser> CreateEmailUser(EmailUser emailUser)
    {
        if (emailUser.Id == Guid.Empty)
            emailUser.Id = Guid.NewGuid();

        dbContext.EmailUsers.Add(emailUser);
        await dbContext.SaveChangesAsync();

        return emailUser;
    }

    public async Task<List<EmailUser>> GetEmailUsers()
    {
        var emailUsers = await dbContext.EmailUsers.ToListAsync();
        return emailUsers;
    }

    public async Task<EmailUser> GetEmailUser(Guid emailUserId)
    {
        var emailUser = await dbContext.EmailUsers.FirstOrDefaultAsync(x => x.Id == emailUserId);
        if (emailUser == null)
            throw new EmailUserNotFoundException(emailUserId);

        return emailUser;
    }

    public async Task UpdateEmailUser(EmailUser emailUser)
    {
        var existingEmailUser = await GetEmailUser(emailUser.Id);

        existingEmailUser.Smtp_Username = emailUser.Smtp_Username;
        existingEmailUser.Smtp_Password = emailUser.Smtp_Password;
        existingEmailUser.Host = emailUser.Host;
        existingEmailUser.Port = emailUser.Port;
        existingEmailUser.EnableSsl = emailUser.EnableSsl;

        dbContext.EmailUsers.Update(existingEmailUser);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteEmailUser(Guid emailUserId)
    {
        var existingEmailUser = await GetEmailUser(emailUserId);
        dbContext.EmailUsers.Remove(existingEmailUser);
        await dbContext.SaveChangesAsync();
    }
}
