using TodoApi.TodoUseCases.DeleteTodo;
using TodoApi.TodoUseCases.UpdateTodo;

var builder = WebApplication.CreateBuilder(args);

//Repositories and DbContext
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ITodoRepository, TodoRepository>();


//UseCases
builder.Services.AddTransient<ICreateTodoUseCase, CreateTodoUseCase>();
builder.Services.AddTransient<IGetTodosUseCase, GetTodosUseCase>();
builder.Services.AddTransient<IGetTodoUseCase, GetTodoUseCase>();
builder.Services.AddTransient<IUpdateTodoUseCase, UpdateTodoUseCase>();
builder.Services.AddTransient<IDeleteTodoUseCase, DeleteTodoUseCase>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Endpoints 
app.MapCreateTodoEndpoint()
    .MapGetTodoEndpoint()
    .MapGetTodosEndpoint()
    .MapUpdateTodoEndpoint()
    .MapDeleteTodoEndpoint();


//Create Database
using (var scope = app.Services.CreateScope()) 
{
    var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
    await context.Database.MigrateAsync();
};




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
