using Common.Authorization;
using Common.Authorization.Models;
using Common.Keycloak;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using ProjectApplication.ProjectUseCases.Queries.GetProjectByTenant;
using ProjectApplication.ProjectUseCases.Queries.GetProjectsFromCurrentUser;
using System.Diagnostics;
using System.Security.Claims;

namespace ProjectApi.Controllers;

[ApiController]
[Authorize(Policy = Policies.UserAccess)]
[Route("[controller]")]

public class ProjectsController(ISender sender) : ControllerBase
{

    [HttpGet]
    [Authorize(Policy = Policies.AdminAccess)]
    public async Task<ActionResult<ProjectListResponse>> GetAllProjects()
    {
        try 
        {
            GetProjectsQuery query = new GetProjectsQuery();
            var result = await sender.Send(query);
            return Ok(new ProjectListResponse(result.data));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("FromCurrentUser")]
    public async Task<ActionResult<ProjectListResponse>> GetAllProjectsFromCurrentUser()
    {
        try
        {
            IdentifiedUser? identifiedUser = User.IdentifyUser();

            if (identifiedUser is null)
                return Forbid();


            GetProjectsFromCurrentUserQuery query = new GetProjectsFromCurrentUserQuery(identifiedUser.UserId);
            var result = await sender.Send(query);
            return Ok(new ProjectListResponse(result.data));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponse>> GetProject(Guid id)
    {
        try
        {
            GetProjectQuery query = new GetProjectQuery(id);
            var result = await sender.Send(query);
            return Ok(new ProjectResponse(result.data));
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("ByTenant")]
    public async Task<ActionResult<ProjectListResponse>> GetProjectsByTenant()
    {
        try
        {
            IdentifiedUser? identifiedUser = User.IdentifyUser();

            if (identifiedUser is null)
                return Forbid();


            GetProjectsByTenantQuery query = new GetProjectsByTenantQuery(identifiedUser.Tenant);
            GetProjectsByTenantResult result = await sender.Send(query);
            return Ok(new ProjectListResponse(result.data));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }

    [HttpGet("ByResponsibleUser/{id}")]
    public async Task<ActionResult<ProjectResponse>> GetProjectByResponsibleUser(Guid id)
    {
        try
        {
            GetProjectsByResponsibleUserQuery query = new GetProjectsByResponsibleUserQuery(id);
            var result = await sender.Send(query);
            return Ok(new ProjectListResponse(result.data));
        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }


    [HttpPost]
    public async Task<ActionResult<ProjectResponse>> CreateProject(ProjectCreateDto dto)
    {
        IdentifiedUser? identifiedUser = User.IdentifyUser();

        if (identifiedUser is null)
            return Forbid();

        try
        {
            CreateProjectCommand command = new CreateProjectCommand(dto, identifiedUser.UserName, identifiedUser.Tenant);
            CreateProjectResult result = await sender.Send(command);
            return Ok(new ProjectResponse(result.data));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }


    [HttpPut]
    public async Task<ActionResult> UpdateProject(ProjectDto dto)
    {
        string userName = this.User.FindFirst(ClaimTypes.Name).Value;

        try
        {
            UpdateProjectCommand command = new UpdateProjectCommand(dto, userName);
            UpdateProjectResult result = await sender.Send(command);
            return NoContent();

        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(Guid id, IPublishEndpoint publishEndpoint)
    {
        try
        {
            DeleteProjectCommand command = new DeleteProjectCommand(id);
            DeleteProjectResult result = await sender.Send(command);

            return NoContent();

        }
        catch (ProjectNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: 500);
        }
    }
}
