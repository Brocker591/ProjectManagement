using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;
using Common.MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TodoApi.Settings;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

//Repositories and DbContext
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ITodoRepository, TodoRepository>();

//Valitators
builder.Services.AddScoped<IValidator<CreateTodoDto>, CreateTodoValidator>();
builder.Services.AddScoped<IValidator<UpdateTodoDto>, UpdateTodoValidator>();


//UseCases
builder.Services.AddTransient<ICreateTodoUseCase, CreateTodoUseCase>();
builder.Services.AddTransient<IGetTodosUseCase, GetTodosUseCase>();
builder.Services.AddTransient<IGetTodoUseCase, GetTodoUseCase>();
builder.Services.AddTransient<IGetTodosByProjectIdUseCase, GetTodosByProjectIdUseCase>();
builder.Services.AddTransient<IUpdateTodoUseCase, UpdateTodoUseCase>();
builder.Services.AddTransient<IDeleteTodoUseCase, DeleteTodoUseCase>();
builder.Services.AddTransient<IProjectDeletedUseCase, ProjectDeletedUseCase>();
builder.Services.AddTransient<IProjectClosedUseCase, ProjectClosedUseCase>();


builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());


builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

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

//Endpoints 
app.MapCreateTodoEndpoint()
    .MapGetTodoEndpoint()
    .MapGetTodosEndpoint()
    .MapGetGetTodosByProjectIdEndpoint()
    .MapUpdateTodoEndpoint()
    .MapDeleteTodoEndpoint();


//Create Database
using (var scope = app.Services.CreateScope()) 
{
    var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
    await context.Database.MigrateAsync();
};

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.Run();
