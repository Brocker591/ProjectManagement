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
            SmtpClient smtp = new SmtpClient(user.Host, user.Port);
            smtp.Host = user.Host;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = user.EnableSsl;

            try
            {
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        return true;
    }
}
