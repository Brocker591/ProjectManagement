using Common.Authorization.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Common.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static IdentifiedUser? IdentifyUser(this ClaimsPrincipal user)
    {

        if (user is null || user?.Identity?.IsAuthenticated == false)
        {
            return null;
        }

        string? userIdString = user?.FindFirstValue(JwtRegisteredClaimNames.Sub);


        Guid userId;

        if (!Guid.TryParse(userIdString, out userId))
        {
            return null;

        }

        var userName = user!.FindFirstValue(JwtRegisteredClaimNames.Email) ?? userId.ToString();

        if (string.IsNullOrEmpty(userName))
        {
            return null;
        }


        string? tenant = user!.FindFirstValue(TenantConstants.Tenant);

        if(tenant is not null)
            return new IdentifiedUser(userId, userName, tenant);
        else
            return new IdentifiedUser(userId, userName, TenantConstants.TenantUnknown);
    }
}
