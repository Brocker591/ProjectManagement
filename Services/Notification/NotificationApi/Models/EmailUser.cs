namespace NotificationApi.Models;

public class EmailUser
{
    public Guid Id { get; set; }
    public string Smtp_Username { get; set; } = default!;
    public string Smtp_Password { get; set; } = default!;
    public string Host { get; set; }  = default!;
    public int Port {  get; set; }
    public bool EnableSsl { get; set; } = true;
}
