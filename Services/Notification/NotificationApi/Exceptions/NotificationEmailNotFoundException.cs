namespace NotificationApi.Exceptions;

public class NotificationEmailNotFoundException : Exception
{
    public NotificationEmailNotFoundException(Guid Guid) : base($"NotificationEmail with Id {Guid.ToString()} was not found.")
    {

    }
}
