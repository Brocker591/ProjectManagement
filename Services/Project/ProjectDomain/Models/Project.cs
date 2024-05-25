namespace ProjectDomain.Models;

public class Project : Entity<Guid>
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Guid ResponsibleUser { get; set; } = default!;
    public List<Guid> Tasks { get; set; } = new List<Guid>();
    public List<Guid> Users { get; set; } = new List<Guid>();

    public static Project Create(Guid Id, string Name, Guid ResponsibleUser, List<Guid> Tasks, List<Guid> Users)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Name);

        return new Project
        {
            Id = Id,
            Name = Name,
            ResponsibleUser = ResponsibleUser,
            Tasks = Tasks,
            Users = Users
        };
    }
}
