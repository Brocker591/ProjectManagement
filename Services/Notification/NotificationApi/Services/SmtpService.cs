using System.Net;

namespace NotificationApi.Services;

public class SmtpService(ILogger<SmtpService> logger) : ISmtpService
{
    public async Task<bool> SendEmail(MailModel mailModel)
    {
        MailMessage email = new MailMessage(mailModel.FromAddress, mailModel.ToAddress);
        email.Subject = mailModel.Subject;
        email.Body = mailModel.Body;

        foreach (var user in mailModel.EmailUsers)
        {
            using (SmtpClient smtpClient = new SmtpClient(user.Host, user.Port))
            {
                smtpClient.Host = user.Host;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = user.EnableSsl;
                smtpClient.Credentials = new NetworkCredential(user.Smtp_Username, user.Smtp_Password);

                try
                {
                    smtpClient.Send(email);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    return false;
                }
            }

        }

        return true;
    }
}
