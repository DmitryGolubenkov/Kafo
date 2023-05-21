using Kafo.Application.Interfaces;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;

namespace Kafo.Server.Application.Users.Command;
public class DeleteUserCommand
{

    private readonly IRepository<User> _usersRepository;
    private readonly IAccessControl _accessControl;

    public DeleteUserCommand(IRepository<User> usersRepository, IAccessControl accessControl)
    {
        _usersRepository = usersRepository;
        _accessControl = accessControl;
    }

    public async Task Execute(Guid userId)
    {
        _accessControl.CheckIfAuthenticated();

        var user = _usersRepository.Where(x => x.Id == userId).FirstOrDefault();

        if (user is null)
            return;

        _usersRepository.Remove(user);

        await _usersRepository.SaveChangesAsync();

    }

}
