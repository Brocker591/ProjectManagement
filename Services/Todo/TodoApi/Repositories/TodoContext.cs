using System.Reflection.Metadata;
using System.Text.Json;

namespace TodoApi.Repositories;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<TodoStatus> TodoStatuses => Set<TodoStatus>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Todo>(todo => 
        {
            todo.HasKey(x => x.Id);
            todo.Property(x => x.StatusId).IsRequired();
            todo.Property(x => x.EditorUsers).HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions)null));
        });

        builder.Entity<TodoStatus>(status => 
        {
            status.HasKey(x => x.Id);
            status.Property(x => x.Id).ValueGeneratedOnAdd();
            status.HasData( 
                new TodoStatus { Id = 1 , Name = "open" },
                new TodoStatus { Id = 2, Name = "doing" },
                new TodoStatus { Id = 3, Name = "done" });
        });

    }
}
