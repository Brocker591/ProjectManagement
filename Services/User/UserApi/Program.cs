using Common.Keycloak;
using UserApi.Endpoints;

const string corsSettings = "AllowedOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddAuthorization();
builder.AddKeycloakAuthentication();
builder.AddKeycloakAuthorization();
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


var app = builder.Build();


app.MapGetUserListEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthorization();

app.UseCors();
app.Run();


