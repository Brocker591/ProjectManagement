namespace ProjectDomain.Models;

public class Project : Entity<Guid>
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Guid ResponsibleUser { get; set; } = default!;
    public List<Guid> Tasks { get; set; } = new List<Guid>();
    public List<Guid> Users { get; set; } = new List<Guid>();
    public bool IsClosed { get; set; } = false;

    public static Project Create(string name, Guid responsibleUser, List<Guid> tasks, List<Guid> users, string userName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new Project
        {
            Id = Guid.NewGuid(),
            Name = name,
            ResponsibleUser = responsibleUser,
            Tasks = tasks,
            Users = users,
            IsClosed = false,
            CreateAt = DateTime.UtcNow,
            CreatedBy = userName,
        };
    }
}
