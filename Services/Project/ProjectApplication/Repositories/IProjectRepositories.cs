namespace ProjectApplication.Repositories;

public interface IProjectRepositories
{
    Task<Project> CreateProject(Project project);
    Task<List<Project>> GetProjects();
    Task<List<Project>> GetProjectsByTenant(string tenant);
    Task<Project> GetProject(Guid projectId);
    Task UpdateProject(Project project);
    Task DeleteProject(Guid projectId);

}
