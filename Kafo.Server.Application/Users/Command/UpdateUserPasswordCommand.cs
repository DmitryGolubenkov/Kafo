using Kafo.Application.Interfaces;
using Kafo.Application.Models.Users;
using Kafo.Application.Utilities;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;

namespace Kafo.Server.Application.Users.Command;

public class UpdateUserPasswordCommand
{
    private readonly IRepository<User> _usersRepository;
    private readonly IAccessControl _accessControl;

    public UpdateUserPasswordCommand(IRepository<User> usersRepository, IAccessControl accessControl)
    {
        this._usersRepository = usersRepository;
        _accessControl = accessControl;
    }

    public async Task Execute(UpdateUserPasswordInputModel model)
    {
        _accessControl.CheckIfAuthenticated();
        
        var existingUser = _usersRepository.Where(user => user.Id == model.Id).FirstOrDefault();

        if (existingUser is null)
            throw new Exception("User не существует");

        var hashSalt = PasswordHashUtility.GenerateSaltedHash(model.NewPassword);

        existingUser.PasswordHash = hashSalt.Hash;
        existingUser.PasswordSalt = hashSalt.Salt;
        existingUser.Token = Guid.NewGuid();
        
        await _usersRepository.SaveChangesAsync();
    }
}