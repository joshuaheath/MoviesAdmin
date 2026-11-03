using System.Security.Claims;

namespace MoviesAdmin
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user) =>
            user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("User is not authenticated");

        //public static string GetUsername(this ClaimsPrincipal user) =>
        //    user.GetUsername(ClaimTypes.Email)
        //    ?? throw new InvalidOperationException("User does not have a name");
    }
}
