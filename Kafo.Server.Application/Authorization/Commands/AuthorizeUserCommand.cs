using Kafo.Application.Interfaces;
using Kafo.Application.Models.Authorization;
using Kafo.Application.Utilities;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization.Services;
using Kafo.Server.Application.Authorization.Services.Models;
using Kafo.Server.Application.Utilities;
using Microsoft.Extensions.Options;

namespace Kafo.Server.Application.Authorization.Commands;

/// <summary>
/// Авторизует нового пользователя в системе, если логин и пароль верны
/// </summary>
public class AuthorizeUserCommand
{
    private readonly IRepository<User> _userRepository;
    private readonly AuthOptions _authOptions;

    public AuthorizeUserCommand(IRepository<User> userRepository, IOptions<AuthOptions> authOptions)
    {
        _userRepository = userRepository;
        _authOptions = authOptions.Value;
    }
    
    public async Task<UserLoginResultModel> Execute(UserLoginInputModel inputModel)
    {
        if (string.IsNullOrWhiteSpace(inputModel.UserName) || string.IsNullOrWhiteSpace(inputModel.Password))
            return new UserLoginResultModel() {IsSuccessful = false};

        var user = _userRepository.Where(user => user.Username == inputModel.UserName).FirstOrDefault();

        if (user is null)
            return new UserLoginResultModel() {IsSuccessful = false};

        if (!PasswordHashUtility.VerifyPassword(inputModel.Password, user.PasswordHash, user.PasswordSalt))
            return new UserLoginResultModel() {IsSuccessful = false};


        return new UserLoginResultModel
        {
            IsSuccessful = true,
            Username = user.Username,
            UserId = user.Id,
            JwtToken = JwtGenerator.CreateEncodedToken(new JwtData()
            {
                UserId = user.Id,
                Username = user.Username,
                UserToken = user.Token,
            }, _authOptions)
        };

    }
    
}
