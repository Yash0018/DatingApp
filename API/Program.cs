using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

// The purpose of using this middleware is that we want to handle the exceptions in our program at highest level
// This save use a lot time since we don't need to put try-catch in every controller or service
// This save developer time and make a centralized system to handle errros
// Anything below the useMiddleware line when catches exception use the following middleware to send it to the client
app.UseMiddleware<ExceptionMiddleware>();
 
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200"));

app.UseAuthentication();    // means you hold valid JWT token 
app.UseAuthorization();     // means what are you allowed to do 

app.MapControllers();

app.Run();
