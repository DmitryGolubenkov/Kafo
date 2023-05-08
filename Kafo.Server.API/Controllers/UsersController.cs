using Kafo.Application.Interfaces;
using Kafo.Application.Models.Users;
using Kafo.Server.Application.Authorization;
using Kafo.Server.Application.Users.Command;
using Kafo.Server.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kafo.Server.API.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IAccessControl _accessControl;
    private readonly GetUsersQuery _getUsersQuery;
    private readonly CreateNewUserCommand _createNewUserCommand;
    private readonly UpdateUserDataCommand _updateUserDataCommand;
    private readonly UpdateUserPasswordCommand _updateUserPasswordCommand;

    public UsersController(IAccessControl accessControl,
        GetUsersQuery getUsersQuery,
        CreateNewUserCommand createNewUserCommand,
        UpdateUserDataCommand updateUserDataCommand,
        UpdateUserPasswordCommand updateUserPasswordCommand
    )
    {
        _accessControl = accessControl;
        _getUsersQuery = getUsersQuery;
        _createNewUserCommand = createNewUserCommand;
        _updateUserDataCommand = updateUserDataCommand;
        _updateUserPasswordCommand = updateUserPasswordCommand;
    }

    [HttpGet]
    [Route("getUsers")]
    public async Task<ActionResult<List<UserModel>>> GetUsers()
    {
        if (!_accessControl.IsAuthenticated())
            return Unauthorized();

        return await _getUsersQuery.Execute();
    }

    [HttpPost]
    [Route("createNewUser")]
    public async Task<ActionResult> Execute(CreateNewUserInputModel model)
    {
        await _createNewUserCommand.Execute(model);

        return Ok();
    }

    [HttpPost]
    [Route("updateUser")]
    public async Task<ActionResult> Execute(UserModel model)
    {
        await _updateUserDataCommand.Execute(model);

        return Ok();
    }

    [HttpPost]
    [Route("updateUserPassword")]
    public async Task<ActionResult> Execute(UpdateUserPasswordInputModel model)
    {
        await _updateUserPasswordCommand.Execute(model);

        return Ok();
    }
}