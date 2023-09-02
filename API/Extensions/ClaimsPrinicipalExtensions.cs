using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrinicipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            // ClaimTypes.NameIdentifier is same as JwtRegisteredClaimNames.NameId in TokenService
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
