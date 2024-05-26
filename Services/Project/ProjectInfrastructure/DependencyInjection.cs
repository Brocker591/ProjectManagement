using Microsoft.AspNetCore.Builder;

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

    public static async Task<WebApplication> InitialiseDatabaseAsync(this WebApplication app)
    {
        //Create Database
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ProjectManagementContext>();
            await context.Database.MigrateAsync();
        };

        return app;
    }
}
