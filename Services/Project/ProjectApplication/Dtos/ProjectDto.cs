namespace ProjectApplication.Dtos;

public class ProjectDto
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Guid ResponsibleUser { get; set; } = default!;
    public List<Guid> Tasks { get; set; } = new List<Guid>();
    public List<Guid> Users { get; set; } = new List<Guid>();

}
