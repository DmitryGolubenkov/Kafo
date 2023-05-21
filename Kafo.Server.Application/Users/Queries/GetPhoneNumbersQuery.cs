using Kafo.Application.Interfaces;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;

namespace Kafo.Server.Application.Users.Queries;
public class GetPhoneNumbersQuery
{
    private readonly IRepository<User> _usersRepository;
    private readonly IAccessControl _accessControl;

    public GetPhoneNumbersQuery(IRepository<User> usersRepository, IAccessControl accessControl)
    {
        _usersRepository = usersRepository;
        _accessControl = accessControl;
    }

    public async Task<List<string>> Execute()
    {
        _accessControl.CheckIfAuthenticated();

        return _usersRepository.Select(x => x.PhoneNumber).ToList();
    }
}
