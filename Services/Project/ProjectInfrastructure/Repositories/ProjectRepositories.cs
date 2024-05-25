namespace ProjectInfrastructure.Repositories;

public class ProjectRepositories : IProjectRepositories
{
    public Task<Project> CreateProject(Project project)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProject(Guid projectId)
    {
        throw new NotImplementedException();
    }

    public Task<Project> GetProject(Guid projectId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Project>> GetProjects()
    {
        throw new NotImplementedException();
    }

    public Task UpdateProject(Project project)
    {
        throw new NotImplementedException();
    }
}
