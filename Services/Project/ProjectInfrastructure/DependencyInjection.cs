namespace ProjectInfrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ProjectManagementContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IProjectRepositories, ProjectRepositories>();

        return services;
    }
}
