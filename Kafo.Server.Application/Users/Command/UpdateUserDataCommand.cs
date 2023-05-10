using Kafo.Application.Interfaces;
using Kafo.Application.Models.Users;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization;

namespace Kafo.Server.Application.Users.Command;

/// <summary>
/// Обновляет данные о пользователе
/// </summary>
public class UpdateUserDataCommand
{
    private readonly IRepository<User> _usersRepository;
    private IAccessControl _accessControl;

    public UpdateUserDataCommand(IRepository<User> usersRepository, IAccessControl accessControl)
    {
        this._usersRepository = usersRepository;
        _accessControl = accessControl;
    }


    /// <summary>
    /// Обновляет данные о пользователе
    /// </summary>
    public async Task Execute(UserModel model)
    {
        _accessControl.CheckIfAuthenticated();
        var existingUser = _usersRepository.Where(user=>user.Id==model.Id).FirstOrDefault();

        if (existingUser is null)
            throw new Exception("User не существует");

        // Проверяем, что такого пользователя ещё нет
        if (_usersRepository.Where(x => x.Username == model.Username && x.Id != model.Id).FirstOrDefault() is not null)
            throw new ArgumentException("Такой пользователь уже существует");

        existingUser.Username = model.Username;
        existingUser.PhoneNumber = model.PhoneNumber;

        await _usersRepository.SaveChangesAsync();
    }
}
