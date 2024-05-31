namespace NotificationApi.Models;

public class MailModel
{
    public MailAddress ToAddress { get; set; } = default!;
    public MailAddress FromAddress { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
    public List<EmailUser> EmailUsers { get; set; }
}

