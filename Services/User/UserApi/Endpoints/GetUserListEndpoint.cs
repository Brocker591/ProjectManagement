using Common.Keycloak;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace UserApi.Endpoints;

public record UserModel(Guid id, string username, string firstname, string lastname, string email);

public static class GetUserListEndpoint
{
    public static IEndpointRouteBuilder MapGetUserListEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/GetUsers", async (IConfiguration config) =>
        {
            try
            {
                string userUrl = config.GetSection("KeycloakUserSetting:UserUrl").Value;
                string urlToken = config.GetSection("KeycloakUserSetting:TokenUrl").Value;
                string clientId = config.GetSection("KeycloakSetting:Audience").Value;
                string clientSecret = config.GetSection("KeycloakUserSetting:ClientSecret").Value;
                string username = config.GetSection("KeycloakUserSetting:adminname").Value;
                string password = config.GetSection("KeycloakUserSetting:password").Value;

                var httpClient = new HttpClient();

                var data = new Dictionary<string, string>
                {
                    {"username", username},
                    {"password", password},
                    {"client_id", clientId},
                    {"client_secret" ,clientSecret},
                    {"grant_type", "password"},
                };

                var content = new FormUrlEncodedContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await httpClient.PostAsync(urlToken, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject jobject = JObject.Parse(responseContent);

                var request = new HttpRequestMessage(HttpMethod.Get, userUrl);
                request.Headers.Add("Authorization", "Bearer " + jobject["access_token"].ToString());
                var responseUsers = await httpClient.SendAsync(request);
                string contentUsers = await responseUsers.Content.ReadAsStringAsync();
                List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(contentUsers);

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
}
