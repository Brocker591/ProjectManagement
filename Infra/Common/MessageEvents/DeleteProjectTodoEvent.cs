namespace Common.MessageEvents;

public class DeleteProjectTodoEvent
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
}
