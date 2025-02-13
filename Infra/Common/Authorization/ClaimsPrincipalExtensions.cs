using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Common.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static KeyValuePair<Guid,string>? GetUserIdAndTenant(this ClaimsPrincipal user)
    {
        var userIdString = user.FindFirstValue(JwtRegisteredClaimNames.Sub);

        Guid userId;

        if (!Guid.TryParse(userIdString, out userId))
        {
            return null;

        }

        string? tenant = user.FindFirstValue(TenantConstants.Tenant);

        if(tenant is not null)
            return new KeyValuePair<Guid, string>(userId, tenant);
        else
            return new KeyValuePair<Guid, string>(userId, TenantConstants.TenantUnknown);
    }
}
