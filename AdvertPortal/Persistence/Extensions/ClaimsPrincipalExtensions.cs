using System.Security.Claims;

namespace AdvertPortal.Persistence.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetLoggedUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) as string;
        }

        public static string GetLoggedUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Name) as string;
        }

        public static string GetLoggedUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Email) as string;
        }

    }
}
