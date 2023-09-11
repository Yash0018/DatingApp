using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Updating "lastActivity" field everytime and api call is completed successfully and user was authenticated
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}
