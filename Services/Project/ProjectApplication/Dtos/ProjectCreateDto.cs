namespace ProjectApplication.Dtos;

public class ProjectCreateDto
{
    public string Name { get; set; } = default!;
    public Guid ResponsibleUser { get; set; } = default!;
    public List<Guid> Tasks { get; set; } = new List<Guid>();
    public List<Guid> Users { get; set; } = new List<Guid>();
}
