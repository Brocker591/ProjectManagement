namespace NotificationApi.Services;

public interface ISmtpService
{
    Task<bool> SendEmail(MailModel mailModel);
}