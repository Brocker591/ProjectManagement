using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Common.Keycloak;

public static class Extension
{
    public static IServiceCollection AddKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        KeycloakSetting keycloakSetting = configuration.GetSection(nameof(KeycloakSetting)).Get<KeycloakSetting>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.MetadataAddress = keycloakSetting.MetadataAddress;
                o.Authority = keycloakSetting.Authority;
                o.Audience = keycloakSetting.Audience;

                o.RequireHttpsMetadata = false;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    AudienceValidator = (audiences, securityToken, validationParameters) =>
                    {
                        return audiences.Contains(keycloakSetting.Audience);
                    }
                };
            });

        return services;
    }
}
