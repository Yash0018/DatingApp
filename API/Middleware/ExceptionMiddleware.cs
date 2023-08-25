using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    // The purpose of using this middleware is that we want to handle the exceptions in our program at highest level
    // This save use a lot time since we don't need to put try-catch in every controller or service
    // This save developer time and make a centralized system to handle errros
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        // This method decides what's gonna happen next once catching an exception
        public async Task InvokeAsync(HttpContext context)  // This has to be called "InvokeAsync" to let framework recongnize it as our middleware
        {
            // The only place we add try-catch
            try
            {
                await _next(context);   // Keep moving through middlewares as long as no exception is being caught
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);   // First step would be to log error into our terminal (doesn't matter what env we on)
                context.Response.ContentType = "application/json";  // We need to do this here since its not inside BaseAPIController
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                var response = _env.IsDevelopment() 
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };  // We need to do this here since its not inside BaseAPIController

                var json = JsonSerializer.Serialize(response, options); // Create our json response to print to client
                await context.Response.WriteAsync(json);
            }
        }
    }
}
