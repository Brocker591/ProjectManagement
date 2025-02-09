using ProjectInfrastructure;
using ProjectApplication;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Common.Keycloak;

const string corsSettings = "AllowedOrigin";

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration).AddInfrastructureServices(builder.Configuration);

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsBuilder =>
    {
        var allowedOrigin = builder.Configuration[corsSettings] ?? throw new InvalidOperationException("AllowedOrigin is not set");

        corsBuilder.WithOrigins(allowedOrigin).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});



builder.Services.AddAuthorization();

builder.AddKeycloakAuthentication();
builder.AddKeycloakAuthorization();

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.UseCors();
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
