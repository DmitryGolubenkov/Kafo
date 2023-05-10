using Kafo.Application.Models.Users;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Users.Commands;

public class UpdateUserPasswordCommand
{
    private readonly AuthenticatedHttpClient _httpClient;

    public UpdateUserPasswordCommand(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task Execute(UpdateUserPasswordInputModel model)
    {
        await _httpClient.PostAuthenticatedAsync("api/users/updateUserPassword", model);
    }
}