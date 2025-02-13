namespace TodoApi.Models;

public class Todo
{
    public Guid Id { get; set; }
    public required string Desciption { get; set; }
    public int StatusId { get; set; } = default!;
    public required Guid ResponsibleUser { get; set; }
    public required List<Guid> EditorUsers { get; set; }
    public Guid? ProjectId { get; set; }
    public required string Tenant { get; set; }
}
