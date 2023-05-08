using System.Security;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kafo.Desktop.AppLayer.Authorization.Commands;
using Kafo.Desktop.UI.Extensions;
using Kafo.Desktop.UI.Services;

namespace Kafo.Desktop.UI.ViewModels;

public partial class LoginScreenViewModel : ObservableObject
{
    public LoginScreenViewModel(LoginCommand loginCommand, AppNavigator appNavigator)
    {
        _loginCommand = loginCommand;
        _appNavigator = appNavigator;
    }

    [ObservableProperty] private string? _userName;


    [ObservableProperty] private SecureString? _password;

    [ObservableProperty] private string? _errorText;

    public bool IsValid => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password.ToString());
    private readonly LoginCommand _loginCommand;
    private readonly AppNavigator _appNavigator;

    [RelayCommand]
    public async Task Login()
    {
        if (!IsValid)
            return;

        var result = await _loginCommand.Execute(UserName, Password.ToUnsecureString());

        if (result)
        {
            ErrorText = "";
            _appNavigator.NavigateTo<KafoMainViewModel>();
        }
        else
        {
            ErrorText =
                "При авторизации произошла ошибка. Пожалуйста, проверьте корректность введенных данных и повторите попытку.";
        }
    }
}