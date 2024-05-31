namespace NotificationApi.Exceptions;

public class EmailUserNotFoundException : Exception
{
    public EmailUserNotFoundException(Guid Guid) : base($"EmailUser with Id {Guid.ToString()} was not found.")
    {

    }
}
