using Kafo.Application.Interfaces;
using Kafo.Application.Models.Users;
using Kafo.Domain.Models;

namespace Kafo.Server.Application.Users.Queries;

/// <summary>
/// Возвращает список пользователей системы
/// </summary>
public class GetUsersQuery
{
    private readonly IRepository<User> _usersRepository;

    public GetUsersQuery(IRepository<User> usersRepository)
    {
        _usersRepository = usersRepository;
    }

    /// <summary>
    /// Возвращает список пользователей системы
    /// </summary>
    public async Task<List<UserModel>> Execute()
    {
        return _usersRepository.Select(user => new UserModel()
        {
            Id = user.Id,
            Username = user.Username,
            PhoneNumber = user.PhoneNumber
        }).ToList();
    }
}