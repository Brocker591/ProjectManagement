namespace Common.Authorization.Models;

public sealed class IdentifiedUser
{
    public Guid UserId { get; }
    public string UserName { get; }
    public string Tenant { get; }
    public IdentifiedUser(Guid userId, string userName, string tenant )
    {
        UserId = userId;
        UserName = userName;
        Tenant = tenant;
    }
}
