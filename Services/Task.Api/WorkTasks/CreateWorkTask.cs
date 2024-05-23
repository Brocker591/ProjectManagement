namespace Task.Api.WorkTasks;

public record CreateWorkTaskCommand(string Desciption, Guid? ResponsibleUser, List<Guid>? EditorUsers, Guid? ProjectId);

public class CreateWorkTask
{
}
