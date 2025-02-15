namespace ProjectInfrastructure.Repositories;

public class ProjectRepositories(ProjectManagementContext dbContext) : IProjectRepositories
{
    public async Task<Project> CreateProject(Project project)
    {
        if (project.Id == Guid.Empty)
            project.Id = Guid.NewGuid();

        dbContext.Add(project);
        await dbContext.SaveChangesAsync();

        return project;
    }

    public async Task DeleteProject(Guid projectId)
    {
        var existingProject = await GetProject(projectId);
        dbContext.Projects.Remove(existingProject);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Project> GetProject(Guid projectId)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == projectId);
        if (project == null)
            throw new ProjectNotFoundException(projectId);

        return project;
    }

    public async Task<List<Project>> GetProjects()
    {
        var projects = await dbContext.Projects.ToListAsync();
        return projects;
    }

    public async Task<List<Project>> GetProjectsByTenant(string tenant)
    {
        var projects = await dbContext.Projects.Where(project => project.Tenant == tenant).ToListAsync();
        return projects;
    }

    public async Task UpdateProject(Project project)
    {
        var existingProject = await GetProject(project.Id);

        existingProject.Name = project.Name;
        existingProject.ResponsibleUser = project.ResponsibleUser;
        existingProject.Tasks = project.Tasks;
        existingProject.Users = project.Users;
        existingProject.IsClosed = project.IsClosed;
        existingProject.LastModifiedBy = project.LastModifiedBy;
        existingProject.LastModified = project.LastModified;

        dbContext.Projects.Update(existingProject);

        await dbContext.SaveChangesAsync();
    }
}

