namespace ProjectApplication.Exceptions;

public class ProjectNotFoundException : Exception
{
    public ProjectNotFoundException(Guid Guid) : base($"Project with Id {Guid.ToString()} was not found.")
    {

    }
}
