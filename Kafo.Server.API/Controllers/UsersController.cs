﻿using Kafo.Application.Interfaces;
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

    #region Fields

    private readonly IAccessControl _accessControl;
    private readonly GetUsersQuery _getUsersQuery;
    private readonly CreateNewUserCommand _createNewUserCommand;
    private readonly UpdateUserDataCommand _updateUserDataCommand;
    private readonly UpdateUserPasswordCommand _updateUserPasswordCommand;
    private readonly GetPhoneNumbersQuery _getPhoneNumbersQuery;
    private readonly DeleteUserCommand _deleteUserCommand;

    #endregion

    #region Constructor

    public UsersController(IAccessControl accessControl,
        GetUsersQuery getUsersQuery,
        CreateNewUserCommand createNewUserCommand,
        UpdateUserDataCommand updateUserDataCommand,
        UpdateUserPasswordCommand updateUserPasswordCommand,
        GetPhoneNumbersQuery getPhoneNumbersQuery,
        DeleteUserCommand deleteUserCommand)
    {
        _accessControl = accessControl;
        _getUsersQuery = getUsersQuery;
        _createNewUserCommand = createNewUserCommand;
        _updateUserDataCommand = updateUserDataCommand;
        _updateUserPasswordCommand = updateUserPasswordCommand;
        _getPhoneNumbersQuery = getPhoneNumbersQuery;
        _deleteUserCommand = deleteUserCommand;
    }

    #endregion


    #region Methods

    [HttpGet]
    [Route("getUsers")]
    public async Task<ActionResult<List<UserModel>>> GetUsers()
    {
        if (!_accessControl.IsAuthenticated())
            return Unauthorized();

        return await _getUsersQuery.Execute();
    }

    [HttpGet]
    [Route("getPhoneNumbers")]
    public async Task<List<string>> GetPhoneNumbers()
    {
        return await _getPhoneNumbersQuery.Execute();
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

    [HttpPost]
    [Route("deleteUser")]
    public async Task<ActionResult> Execute(DeleteUserModel deleteUserModel)
    {
        await _deleteUserCommand.Execute(deleteUserModel.Id);

        return Ok();
    }


    #endregion
}