using API.Middleware;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

//Configure Logging providers
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: register ILogger for FactoryMiddleware
builder.Services.AddSingleton<ILogger<Program>>();
builder.Services.AddTransient<FactoryMiddleware>();

var app = builder.Build();

//Option 1: Adding Middleware With Request Delegates
//app.Use(async (httpContext, next) =>
//{
//    try
//    {
//        await next(httpContext);
//    }
//    catch (Exception ex)
//    {
//        app.Logger.LogError(ex, ex.Message);
//    }
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Option 2: Adding Middleware By Convention
//app.UseConventionMiddleware();

//Option 3: Adding Factory-Based Middleware
app.UseMiddleware<FactoryMiddleware>();

app.Run();