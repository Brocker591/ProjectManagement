using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;
using Common.MassTransit;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Database");

//Repositories and DbContext
builder.Services.AddDbContext<NotificationContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IEmailUserRepository, EmailUserRepository>();


//Valitators
builder.Services.AddScoped<IValidator<CreateEmailUserDto>, CreateEmailUserValidator>();
builder.Services.AddScoped<IValidator<EmailUser>, UpdateEmailUserValidator>();


//UseCases
builder.Services.AddTransient<ICreateEmailUserUseCase, CreateEmailUserUseCase>();
builder.Services.AddTransient<IGetEmailUsersUseCase, GetEmailUsersUseCase>();
builder.Services.AddTransient<IGetEmailUserUseCase, GetEmailUserUseCase>();
builder.Services.AddTransient<IUpdateEmailUserUseCase, UpdateEmailUserUseCase>();
builder.Services.AddTransient<IDeleteEmailUserUseCase, DeleteEmailUserUseCase>();
builder.Services.AddTransient<ISmtpService, SmtpService>();
builder.Services.AddTransient<IErrorCreateProjectTodoUseCase, ErrorCreateProjectTodoUseCase>();


builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());



builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapCreateEmailUserEndpoint()
    .MapDeleteEmailUserEndpoint()
    .MapGetTodoEndpoint()
    .MapGetTodosEndpoint()
    .MapUpdateEmailUserEndpoint();


//Create Database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<NotificationContext>();
    await context.Database.MigrateAsync();
};

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



app.Run();
