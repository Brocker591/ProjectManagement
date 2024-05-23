using System.Text.Json;

namespace Task.Api.Repositories;

public class WorkTaskContext : DbContext
{
    public WorkTaskContext(DbContextOptions<WorkTaskContext> options) : base(options)
    {
    }

    public DbSet<WorkTask> WorkTasks => Set<WorkTask>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<WorkTask>(w => 
        {
            w.HasKey(x => x.Id);
            w.Property(x => x.EditorUsers).HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions)null));
        });

    }
}
