namespace ProjectApplication.ProjectUseCases.Commands.UpdateProject;

public class UpdateProjectHandler(IProjectRepositories repository) : ICommandHandler<UpdateProjectCommand, UpdateProjectResult>
{
    public async Task<UpdateProjectResult> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = command.ProjectDto.AsModel();

        await repository.UpdateProject(project);

        return new UpdateProjectResult(true);

    }
}

