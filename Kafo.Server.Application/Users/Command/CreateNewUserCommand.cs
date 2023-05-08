using Kafo.Application.Interfaces;
using Kafo.Application.Models.Users;
using Kafo.Application.Utilities;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;

namespace Kafo.Server.Application.Users.Command;

public class CreateNewUserCommand
{
    private readonly IRepository<User> _usersRepository;
    private readonly IAccessControl _accessControl;

    public CreateNewUserCommand(IRepository<User> usersRepository, IAccessControl accessControl)
    {
        _usersRepository = usersRepository;
        _accessControl = accessControl;
    }

    public async Task Execute(CreateNewUserInputModel model)
    {
        _accessControl.CheckIfAuthenticated();

        if (string.IsNullOrWhiteSpace(model.Username)
            || string.IsNullOrWhiteSpace(model.PhoneNumber)
            || string.IsNullOrWhiteSpace(model.Password))
            throw new ArgumentException("Исходные данные некорректны");

        var hashSalt = PasswordHashUtility.GenerateSaltedHash(model.Password);
        var newUser = new User()
        {
            Username = model.Username,
            PhoneNumber = model.PhoneNumber,
            PasswordHash = hashSalt.Hash,
            PasswordSalt = hashSalt.Salt,
            Token = Guid.NewGuid()
        };


        _usersRepository.Add(newUser);
        await _usersRepository.SaveChangesAsync();
    }
}