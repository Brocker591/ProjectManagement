namespace ProjectApplication.Repositories;

public interface IProjectRepositories
{
    Task<Project> CreateProject(Project project);
    Task<List<Project>> GetProjects();
    Task<Project> GetProject(Guid projectId);
    Task UpdateProject(Project project);
    Task DeleteProject(Guid projectId);

}
