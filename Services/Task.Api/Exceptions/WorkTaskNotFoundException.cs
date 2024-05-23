using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Task.Api.Exceptions
{
    public class WorkTaskNotFoundException : Exception
    {
        public WorkTaskNotFoundException(Guid Guid) : base($"Task with Id {Guid.ToString()} was not found.")
        {

        }
    }
}
