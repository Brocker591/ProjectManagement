using ProjectApplication.ProjectUseCases.Queries.GetProject;
using ProjectApplication.ProjectUseCases.Queries.GetProjects;

namespace ProjectApplication.ProjectUseCases.Queries.GetProjectByTenant;

internal class GetProjectsByTenantHandler(IProjectRepositories repository) : IQueryHandler<GetProjectsByTenantQuery, GetProjectsByTenantResult>
{
    public async Task<GetProjectsByTenantResult> Handle(GetProjectsByTenantQuery request, CancellationToken cancellationToken)
    {
        var projects = await repository.GetProjectsByTenant(request.tenant);

        return new GetProjectsByTenantResult(projects);
    }
}
