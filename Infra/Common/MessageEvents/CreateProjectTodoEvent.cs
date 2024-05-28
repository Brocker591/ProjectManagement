namespace Common.MessageEvents;

public class CreateProjectTodoEvent
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
}
