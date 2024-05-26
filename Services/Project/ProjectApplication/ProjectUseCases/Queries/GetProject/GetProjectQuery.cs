namespace ProjectApplication.ProjectUseCases.Queries.GetProject;

public record GetProjectQuery(Guid id) : IQuery<GetProjectResult>;
public record GetProjectResult(Project data);
