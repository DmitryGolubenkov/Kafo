using Kafo.Application.Interfaces;
using Kafo.Application.Models.Authorization;
using Kafo.Application.Utilities;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AuthOptions = Kafo.Server.Application.Authorization.Services.AuthOptions;

namespace Kafo.Server.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly AuthorizeUserCommand _authorizeUserCommand;
    private readonly IRepository<User> _userRepository;
    private readonly AuthOptions _authOptions;

    public AuthorizationController(AuthorizeUserCommand authorizeUserCommand)
    {
        _authorizeUserCommand = authorizeUserCommand;
    }

    
    /// <summary>
    /// Маршрут API для авторизации пользователя
    /// </summary>
    /// <param name="inputModel">Данные для авторизации</param>
    /// <returns>JWT токен и данные о пользователе, если вход успешен</returns>
    [Route("login")]
    [HttpPost]
    public async Task<ActionResult<UserLoginResultModel>> Login(UserLoginInputModel inputModel)
    {
        var result = await _authorizeUserCommand.Execute(inputModel);

        if (!result.IsSuccessful)
            return BadRequest(); 
        
            return Ok(result);
    }
}