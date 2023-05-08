using Kafo.Application.Interfaces;

namespace Kafo.Server.Application.Authorization;

public class AccessControl : IAccessControl
{
    private readonly IUserInfo _userInfo;

    public AccessControl(IUserInfo userInfo)
    {
        _userInfo = userInfo;
    }

    public bool IsAuthenticated()
    {
        return _userInfo.IsAuthenticated;
    }

    public void CheckIfAuthenticated()
    {
        if (!_userInfo.IsAuthenticated)
            throw new Exception("Unauthorized");
    }
}