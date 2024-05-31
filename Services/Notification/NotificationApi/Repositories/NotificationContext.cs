namespace NotificationApi.Repositories;

public class NotificationContext : DbContext
{
    public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
    {
    }

    public DbSet<EmailUser> EmailUsers => Set<EmailUser>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<EmailUser>(w =>
        {
            w.HasKey(x => x.Id);
        });
    }
}
