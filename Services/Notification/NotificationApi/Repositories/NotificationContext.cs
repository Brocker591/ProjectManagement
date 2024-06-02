namespace NotificationApi.Repositories;

public class NotificationContext : DbContext
{
    public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
    {
    }

    public DbSet<EmailUser> EmailUsers => Set<EmailUser>();
    public DbSet<NotificationEmail> NotificationEmails => Set<NotificationEmail>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<EmailUser>(w =>
        {
            w.HasKey(x => x.Id);
        });

        builder.Entity<NotificationEmail>(x => 
        {
            x.HasKey(x => x.Id);
        });
    }
}
