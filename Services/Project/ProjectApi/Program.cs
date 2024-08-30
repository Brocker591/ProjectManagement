using ProjectInfrastructure;
using ProjectApplication;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ProjectInfrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration).AddInfrastructureServices(builder.Configuration);

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

KeycloakSetting keycloakSetting = builder.Configuration.GetSection(nameof(KeycloakSetting)).Get<KeycloakSetting>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

var app = builder.Build();

await app.InitialiseDatabaseAsync();


app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
