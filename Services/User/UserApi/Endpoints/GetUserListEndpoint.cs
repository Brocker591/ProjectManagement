using Common.Authorization;
using Common.Authorization.Models;
using Common.Keycloak;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace UserApi.Endpoints;

public record UserModel(Guid id, string username, string firstname, string lastname, string email);
public record GroupModel(Guid id, string name, string path);

public static class GetUserListEndpoint
{
    public static IEndpointRouteBuilder MapGetUserListEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/GetUsers", async (IConfiguration config, ClaimsPrincipal user) =>
        {
            try
            {
                string userUrl = config.GetSection("KeycloakUserSetting:UserUrl").Value;
                string groupUrl = config.GetSection("KeycloakUserSetting:GroupUrl").Value;
                string urlToken = config.GetSection("KeycloakUserSetting:TokenUrl").Value;
                string clientId = config.GetSection("KeycloakUserSetting:ClientId").Value;
                string clientSecret = config.GetSection("KeycloakUserSetting:ClientSecret").Value;
                string username = config.GetSection("KeycloakUserSetting:Adminname").Value;
                string password = config.GetSection("KeycloakUserSetting:Password").Value;

                var httpClient = new HttpClient();

                var data = new Dictionary<string, string>
                {
                    {"username", username},
                    {"password", password},
                    {"client_id", clientId},
                    {"client_secret" ,clientSecret},
                    {"grant_type", "password"},
                };

                IdentifiedUser? identifiedUser = user.IdentifyUser();

                if (identifiedUser is null)
                {
                    return Results.Unauthorized();
                }

                var content = new FormUrlEncodedContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await httpClient.PostAsync(urlToken, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject jobject = JObject.Parse(responseContent);
                string bearerToken = jobject["access_token"].ToString();

                List<GroupModel>? groups = await GetGroupFromUserId(userUrl, httpClient, identifiedUser, bearerToken);

                if(groups is null)
                {
                    return Results.Problem(detail: "Fehler keine Group gefunden", statusCode: StatusCodes.Status500InternalServerError);
                }

                List<UserModel>? users = await GetUserListFromGroup(groupUrl, httpClient, groups[0], bearerToken);

                if(users is null)
                {
                    return Results.Ok(new List<UserModel>());
                }


                return Results.Ok(users);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: "Fehler bei GetUsers", statusCode: StatusCodes.Status500InternalServerError);
            }
        })
        .WithName("GetUsers")
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Get Users")
        .WithDescription("Get Users")
        .RequireAuthorization();

        return routes;
    }

    private static async Task<List<GroupModel>?> GetGroupFromUserId(string userUrl, HttpClient httpClient, IdentifiedUser identifiedUser, string bearerToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{userUrl}/{identifiedUser.UserId.ToString()}/groups");
        request.Headers.Add("Authorization", "Bearer " + bearerToken);
        var responseGroup = await httpClient.SendAsync(request);
        string contentGroup = await responseGroup.Content.ReadAsStringAsync();
        List<GroupModel>? groups = JsonConvert.DeserializeObject<List<GroupModel>>(contentGroup);

        return groups;
    }

    private static async Task<List<UserModel>?> GetUserListFromGroup(string groupUrl, HttpClient httpClient, GroupModel groupModel, string bearerToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{groupUrl}/{groupModel.id.ToString()}/members");
        request.Headers.Add("Authorization", "Bearer " + bearerToken);
        var responseUser = await httpClient.SendAsync(request);
        string contentUser = await responseUser.Content.ReadAsStringAsync();
        List<UserModel>? users = JsonConvert.DeserializeObject<List<UserModel>>(contentUser);

        return users;
    }
}
