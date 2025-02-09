using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;

namespace Common.Keycloak;

public static class KeycloakExtension
{
    private const string KeycloakScheme = "Keycloak";
    private const string RoleClaimName = "role";
    private const string AppUserRole = "appuser";
    private const string AdminRole = "admin"; //bisher nirgends verwendet

    public static IHostApplicationBuilder AddKeycloakAuthentication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(KeycloakScheme)
            .AddJwtBearer(KeycloakScheme, options =>
            {
                options.RequireHttpsMetadata = true;

               
                options.MapInboundClaims = false; //Schaltet das default Mapping Verhalten aus. Damit auch sub Claim gelesen werden kann.
                options.TokenValidationParameters.RoleClaimType = RoleClaimName;
            });

        return builder;
    }

    public static IHostApplicationBuilder AddKeycloakAuthorization(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()                      //AddFallbackPolicy wird verwendet, um eine Richtlinie zu definieren, 
                .AddFallbackPolicy(Policies.UserAccess, authBuilder =>  //die verwendet wird, wenn keine andere Richtlinie definiert ist
                {
                    authBuilder.RequireRole(AppUserRole);
                })
                .AddPolicy(Policies.AdminAccess, authBuilder =>
                {
                    authBuilder.RequireRole(AdminRole);
                });

        return builder;
    }
}
