namespace TodoApi.Exceptions;

public class TodoNotFoundException : Exception
{
    public TodoNotFoundException(Guid Guid) : base($"Task with Id {Guid.ToString()} was not found.")
    {

    }
}
