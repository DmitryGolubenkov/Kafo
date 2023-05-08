using System.Net.Http.Json;
using Kafo.Application.Models.Authorization;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Authorization.Commands;

/// <summary>
/// Выполняет авторизацию пользователя
/// </summary>
public class LoginCommand
{
    private readonly AuthenticatedHttpClient _httpClient;
    private readonly UserInfo _userInfo;

    public LoginCommand(AuthenticatedHttpClient httpClient, UserInfo userInfo)
    {
        _httpClient = httpClient;
        _userInfo = userInfo;
    }
    
    /// <summary>
    /// Выполняет авторизацию пользователя
    /// </summary>
    public async Task<bool> Execute(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", new UserLoginInputModel()
        {
            UserName = username,
            Password = password
        });

        if(!response.IsSuccessStatusCode)
            return false;

        var resultModel = await response.Content.ReadFromJsonAsync<UserLoginResultModel>();
        _userInfo.StartSession((Guid)resultModel.UserId, resultModel.Username, resultModel.JwtToken);

        return true;
    }
}