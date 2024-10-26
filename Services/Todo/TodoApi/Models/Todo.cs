namespace TodoApi.Models;

public class Todo
{
    public Guid Id { get; set; }
    public string Desciption { get; set; } = default!;
    public int StatusId { get; set; } = default!;
    public Guid? ResponsibleUser { get; set; }
    public List<Guid>? EditorUsers { get; set; }
    public Guid? ProjectId { get; set; }
}
