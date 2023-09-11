using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();   // This means the request is complete and then do the following tasks

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;   // This is mostly unnecessary, but in our case we will be using this in BaseApiController so we need this

            var userId = resultContext.HttpContext.User.GetUserId();
                
            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();    // Use HttpContext to get hold of our services and use it to update user's lastActive property
            var user = await repo.GetUserByIdAsync(userId);
            user.LastActive = DateTime.UtcNow;
            await repo.SaveAllAsync();
        }
    }
}
