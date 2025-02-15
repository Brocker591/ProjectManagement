namespace ProjectApplication.ProjectUseCases.Queries.GetProjectByTenant;

public record GetProjectsByTenantQuery(string tenant) : IQuery<GetProjectsByTenantResult>;
public record GetProjectsByTenantResult(List<Project> data);

