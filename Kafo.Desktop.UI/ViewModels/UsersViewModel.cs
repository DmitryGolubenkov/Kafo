using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kafo.Desktop.AppLayer.Users.Commands;
using Kafo.Desktop.AppLayer.Users.Queries;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using Kafo.Application.Models.Users;
using Kafo.Desktop.Infrastructure.InvoiceGenerator.Models;
using Kafo.Desktop.UI.Extensions;
using Kafo.Desktop.UI.Models;

namespace Kafo.Desktop.UI.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    #region ObservableProperties

    // Users
    [ObservableProperty] private List<UserModel> _usersList;

    [ObservableProperty] private UserModel _selectedUser;

    [ObservableProperty] private bool _isUserSelected;

    [ObservableProperty] private SecureString? _newPassword;
    [ObservableProperty] private SecureString? _newPasswordRepeat;



    partial void OnNewPasswordRepeatChanged(SecureString value)
    {
        PasswordsInvalid = NewPassword.ToUnsecureString() != NewPasswordRepeat.ToUnsecureString();
    }

    [ObservableProperty] private bool passwordsInvalid;

    // New User
    [ObservableProperty] private NewUserModel _newUserModel = new NewUserModel();

    partial void OnNewUserModelChanged(NewUserModel? oldValue, NewUserModel newValue)
    {
        // Игнорируем изменения, если пароли не менялись
        if (oldValue.Password == newValue.Password && oldValue.PasswordRepeat == newValue.PasswordRepeat)
            return;

        NewUserPasswordsInvalid = newValue.Password.ToUnsecureString() != newValue.PasswordRepeat.ToUnsecureString();
    }

    [ObservableProperty]
    private bool newUserPasswordsInvalid;

    #endregion


    partial void OnSelectedUserChanged(UserModel value)
    {
        IsUserSelected = SelectedUser is not null;
    }


    #region Fields

    private readonly IMessenger _messenger;
    private readonly GetUsersQuery _getUsersQuery;
    private readonly UpdateUserCommand _updateUserCommand;
    private readonly UpdateUserPasswordCommand _updateUserPasswordCommand;
    private readonly CreateNewUserCommand _createNewUserCommand;

    #endregion

    public UsersViewModel(GetUsersQuery getUsersQuery,
        UpdateUserCommand updateUserCommand,
        UpdateUserPasswordCommand updateUserPasswordCommand,
        CreateNewUserCommand createNewUserCommand,
        IMessenger messenger)
    {
        _getUsersQuery = getUsersQuery;
        _updateUserCommand = updateUserCommand;
        _updateUserPasswordCommand = updateUserPasswordCommand;
        _createNewUserCommand = createNewUserCommand;
        _messenger = messenger;
    }

    private async Task RefreshUsersList()
    {
        UsersList = (await _getUsersQuery.Execute()).ToList();
    }

    /// <summary>
    /// Вызывается при загрузке UI элемента
    /// </summary>
    [RelayCommand]
    public async Task ControlLoaded()
    {
        if (UsersList is null)
            await RefreshUsersList();
    }

    [RelayCommand]
    public async Task UpdateUserData()
    {
        if (string.IsNullOrWhiteSpace(SelectedUser.Username) || string.IsNullOrWhiteSpace(SelectedUser.PhoneNumber))
            return;

        await _updateUserCommand.Execute(SelectedUser);
    }

    /// <summary>
    /// Сохраняет новый пароль
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public async Task SaveNewPassword()
    {
        if (NewPassword.ToUnsecureString() != NewPasswordRepeat.ToUnsecureString())
            return;

        await _updateUserPasswordCommand.Execute(new UpdateUserPasswordInputModel()
        {
            Id = SelectedUser.Id,
            NewPassword = NewPassword.ToUnsecureString()
        });
    }

    [RelayCommand]
    public async Task SaveNewUser()
    {
        if (string.IsNullOrWhiteSpace(NewUserModel.Username) || string.IsNullOrWhiteSpace(NewUserModel.PhoneNumber))
            return;

        if (NewPassword.ToUnsecureString() != NewPasswordRepeat.ToUnsecureString())
            return;

        var newUser = new CreateNewUserInputModel()
        {
            Password = NewUserModel.Password.ToUnsecureString(),
            PhoneNumber = NewUserModel.PhoneNumber,
            Username = NewUserModel.Username
        };

        await _createNewUserCommand.Execute(newUser);

        // После успешного создания нового пользователя обнуляем данные
        NewUserModel.Clear();
        OnPropertyChanged(nameof(NewUserModel));
        await RefreshUsersList();
    }
}