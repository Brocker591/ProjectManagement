namespace ProjectApplication.ProjectUseCases.Queries.GetProjects;

public record GetProjectsQuery() : IQuery<GetProjectsResult>;
public record GetProjectsResult(List<Project> data);

