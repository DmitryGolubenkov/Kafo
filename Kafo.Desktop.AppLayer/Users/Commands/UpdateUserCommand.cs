using Kafo.Application.Models.Users;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Users.Commands;

public class UpdateUserCommand
{
    private readonly AuthenticatedHttpClient _httpClient;

    public UpdateUserCommand(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task Execute(UserModel model)
    {
        await _httpClient.PostAuthenticatedAsync("api/users/updateUser", model);
    }
}