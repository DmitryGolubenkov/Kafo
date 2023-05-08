using Kafo.Application.Interfaces;
using Kafo.Application.Utilities;
using Kafo.Domain.Models;

namespace Kafo.Server.Application.Authorization.Commands;

/// <summary>
/// Создаёт первого пользователя в системе
/// </summary>
public class CreateFirstUserCommand
{
    private readonly IRepository<User> userRepository;

    public CreateFirstUserCommand(IRepository<User> userRepository)
    {
        this.userRepository = userRepository;
    }

    /// <summary>
    /// Создаёт первого пользователя в системе
    /// </summary>
    public async Task Execute()
    {
        if (!userRepository.Any())
        {
            var hashSalt = PasswordHashUtility.GenerateSaltedHash("admin");
            userRepository.Add(new User()
            {
                Username = "admin",
                PasswordHash = hashSalt.Hash,
                PasswordSalt = hashSalt.Salt,
                Token = Guid.NewGuid(),

            });
            await userRepository.SaveChangesAsync();
        }

    }
}
