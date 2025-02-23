using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;
using Common.MassTransit;
using Common.Keycloak;
using TodoApi.TodoUseCases;
using TodoApi.TodoUseCases.GetTodoStatusList;
using TodoApi.ErrorHandling;
using TodoApi.TodoUseCases.GetTodosByTenant;

const string corsSettings = "AllowedOrigin";

var builder = WebApplication.CreateBuilder(args);

//Repositories and DbContext
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddProblemDetails().AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoStatusRepository, TodoStatusRepository>();

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
builder.Services.AddTransient<IGetTodoStatusListUseCase, GetTodoStatusListUseCase>();
builder.Services.AddTransient<IGetTodosByTenantUseCase, GetTodosByTenantUseCase>();


builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());
builder.AddKeycloakAuthentication();
builder.AddKeycloakAuthorization();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

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

var app = builder.Build();

app.UseCors();

//Endpoints
app.MapTodoEndpoints();

app.UseExceptionHandler();
builder.Services.AddProblemDetails();

//Create Database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
    await context.Database.MigrateAsync();
}
;

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
