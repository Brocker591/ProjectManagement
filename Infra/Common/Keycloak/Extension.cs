using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Common.Keycloak;

public static class Extension
{
    public static IServiceCollection AddKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        KeycloakSetting keycloakSetting = configuration.GetSection(nameof(KeycloakSetting)).Get<KeycloakSetting>()!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.MetadataAddress = keycloakSetting.MetadataAddress;
                options.Authority = keycloakSetting.Authority;
                options.Audience = keycloakSetting.Audience;

                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
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

                options.MapInboundClaims = false; //Schaltet das default Mapping Verhalten aus. Damit auch sub Claim gelesen werden kann.
            });

        return services;
    }
}
