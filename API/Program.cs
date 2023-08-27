using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);    // Create an instance of a WebApplication    

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);    // This will configure our database add our services to the scope of our app which we can inject in our controllers             
builder.Services.AddIdentityServices(builder.Configuration);    // This will help use authenticate user based on JWT token we generate for them using our "TokenKey" from appSettings 

var app = builder.Build();

// Configure the HTTP request pipeline.

// The purpose of using this middleware is that we want to handle the exceptions in our program at highest level
// This save use a lot time since we don't need to put try-catch in every controller or service
// This save developer time and make a centralized system to handle errros
// Anything below the useMiddleware line when catches exception use the following middleware to send it to the client
app.UseMiddleware<ExceptionMiddleware>();
 
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200"));

app.UseAuthentication();    // Means you hold valid JWT token 
app.UseAuthorization();     // Means what are you allowed to do 

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext> ();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
	var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
