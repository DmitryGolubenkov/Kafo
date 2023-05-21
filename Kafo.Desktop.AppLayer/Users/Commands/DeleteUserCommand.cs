using Kafo.Application.Models.Users;
using Kafo.Desktop.AppLayer.Utilities;

namespace Kafo.Desktop.AppLayer.Users.Commands;

public class DeleteUserCommand
{
    private readonly AuthenticatedHttpClient _httpClient;

    public DeleteUserCommand(AuthenticatedHttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task Execute(DeleteUserModel model)
    {
        await _httpClient.PostAuthenticatedAsync("api/users/deleteUser", model);
    }
}